using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Management;
using AutoMapper;
using SSM.Common;

using SSM.Models;
using SSM.Models.CRM;
using SSM.ViewModels.Reports.CRM;

namespace SSM.Services.CRM
{
  public interface ICRMCustomerService : IServices<CRMCustomer>
  {
    long InsertToDb(CRMCustomerModel model);
    // bool UpdateToDb(CRMCustomerModel model);
    bool DeleteToDb(CRMCustomerModel model);
    CRMCustomerModel GetModelById(long id);
    CRMCustomer GetById(long id);
    IQueryable<CRMCustomer> GetByMonthYear(PlanFilter filter);
    IEnumerable<CRMCustomer> GetAllModel(CRMSearchModel fModel, SSM.Services.SortField sortField, out int totalRows, int page, int pageSize, User currenUser);
    IEnumerable<CRMFilterProfitResult> GetAllCRMResults(CRMSearchModel fModel, SSM.Services.SortField sortField, out int totalRows, int page, int pageSize, User currenUser, out int totalSeccessCount, out int totalClientCount, out int totalFolowCount);
    SummaryCustomer Summary(long id, DateTime fromDate, DateTime toDate);
    List<SummaryCustomer> SummarysList(User currenUser, PlanFilter filter);
    List<SalesTypeSummaryReport> SalesTypeSumaryReport(PlanFilter filter);
    List<Shipment> GetListShipments(long ssmCusId);
    List<CRMFilterProfitResult> GetCRMListProfit(CRMFilterProfit filter, User user);
    List<CrmFilterTopProfitByStore> GetCRMListFollowProfit(CRMFilterFollowProfit filter, User user);
    List<MT81> GetListSalesTrading(long ssmCusId);
    CRMStatusCode SetSatus(long id, out bool isCancel);

    IEnumerable<CRMFilterProfitResult> GetAllSearchNotPaging(CRMSearchModel fModel, SortField sortField,
      User currenUser, out int totalRows);
  }
  public class CRMCustomerService : Services<CRMCustomer>, ICRMCustomerService
  {
    public long InsertToDb(CRMCustomerModel model)
    {
      try
      {
        var db = ToDbModel(model);
        Insert(db);
        var id = db.Id;
        model.Id = id;
        return id;
      }
      catch (NullReferenceException ex)
      {
        Logger.LogError(ex.ToString());
        throw ex;
      }
      catch (Exception ex)
      {
        Logger.LogError(ex.ToString());
        throw ex;
      }
    }

    public bool UpdateToDb(CRMCustomerModel model)
    {
      try
      {
        var db = GetById(model.Id);
        if (db == null)
          throw new ArgumentNullException("model");
        ConvertModel(model, db);
        Commited();
        return true;
      }
      catch (Exception ex)
      {
        Logger.LogError(ex.ToString());
        throw ex;
      }
    }

    public bool DeleteToDb(CRMCustomerModel model)
    {
      try
      {
        var db = FindEntity(x => x.Id == model.Id);
        if (db == null)
          throw new ArgumentNullException("model");
        Context.CRMContacts.DeleteAllOnSubmit(db.CRMContacts);
        Delete(db);
        Commited();
        return true;
      }
      catch (Exception ex)
      {
        Logger.LogError(ex.ToString());
        return false;
      }
    }

    public CRMCustomerModel GetModelById(long id)
    {
      return ToModels(GetById(id));
    }

    public CRMCustomer GetById(long id)
    {
      return FindEntity(x => x.Id == id);
    }

    public IQueryable<CRMCustomer> GetByMonthYear(PlanFilter filter)
    {
      var qr = GetQuery(x => (filter.Month == 0 || x.CreatedDate.Month == filter.Month) &&
                             x.CreatedDate.Year == filter.Year);
      switch (filter.SummaryByType)
      {
        case SummaryByType.ByUser:
          qr = qr.Where(x => x.CreatedById == filter.Id);
          break;
        case SummaryByType.ByDeparment:
          qr = qr.Where(x => x.User.DeptId == filter.Id);
          break;
        case SummaryByType.ByOffice:
          qr = qr.Where(x => x.User.ComId == filter.Id);
          break;
      }
      return qr;
    }

    public IEnumerable<CRMCustomer> GetAllModel(CRMSearchModel filter, SortField sortField, out int totalRows, int page, int pageSize, User currenUser)
    {
      totalRows = 0;
      var qr = GetQuyryCustomers(filter, currenUser);
      totalRows = qr.Count();
      qr = qr.OrderBy(sortField);
      var list = GetListPager(qr, page, pageSize);
      return list;
    }

    public IQueryable<CRMCustomer> GetQuyryCustomers(CRMSearchModel filter, User currenUser)
    {
      if (filter.ToDate.HasValue)
      {
        DateTime todate = filter.ToDate.Value;
        filter.ToDate = new DateTime(todate.Year, todate.Month, todate.Day, 23, 59, 59);
      }
      var qr = GetQuery(x => (!filter.Id.HasValue || filter.Id == x.Id)
                             && (string.IsNullOrEmpty(filter.CompanyName) || x.CompanyName.Contains(filter.CompanyName)
                             || x.CompanyShortName.Contains(filter.CompanyName) || x.Description.Contains(filter.CompanyName))
                             //&& (filter.CRMStatus.Id == 0 || (x.CrmStatusId == filter.CRMStatus.Id))
                             && (filter.DataType == 0 || x.CrmDataType == (byte)filter.DataType)
                             && (filter.CRMSource.Id == 0 || x.CrmSourceId == filter.CRMSource.Id)
                             && (filter.JobCategory.Id == 0 || x.CrmCategoryId == filter.JobCategory.Id)
                             && (filter.CrmGroup.Id == 0 || x.CrmGroupId == filter.CrmGroup.Id)
                             && (filter.SaleType.Id == 0 || x.SaleTypeId == filter.SaleType.Id)
                             && (filter.SalesId == 0 || x.CreatedById == filter.SalesId || Context.CRMFollowCusUsers.Any(f => f.CrmId == x.Id && f.UserId == filter.SalesId)));
      if (filter.DeptId != 0)
      {
        qr = qr.Where(x => x.User != null && x.User.DeptId == filter.DeptId);
      }
      if (filter.SalesId != 0)
      {
        qr = qr.Where(x => x.CreatedById == filter.SalesId || Context.CRMFollowCusUsers.Any(f => f.CrmId == x.Id && f.UserId == filter.SalesId));
      }
      if (filter.PeriodDate == PeriodDate.CreateDate)
      {
        if (filter.FromDate.HasValue)
          qr = qr.Where(x => x.CreatedDate >= filter.FromDate);
        if (filter.ToDate.HasValue)
          qr = qr.Where(x => x.CreatedDate <= filter.ToDate);
      }
      else
      {
        if (filter.FromDate.HasValue)
          qr = qr.Where(x => x.CreatedDate >= filter.FromDate);
        if (filter.ToDate.HasValue)
          qr = qr.Where(x => x.CreatedDate <= filter.ToDate);
      }
      if (!currenUser.IsDirecter() && filter.SalesId == 0)
      {
        if (currenUser.IsDepManager())
          qr = qr.Where(x => (x.User.DeptId == currenUser.DeptId || Context.CRMFollowCusUsers.Any(f => f.CrmId == x.Id && f.UserId == currenUser.Id)) && x.IsUserDelete == false);
        else
          qr = qr.Where(x => (x.CreatedById == currenUser.Id || Context.CRMFollowCusUsers.Any(f => f.CrmId == x.Id && f.UserId == currenUser.Id)) && x.IsUserDelete == false);

      }
      if (!string.IsNullOrEmpty(filter.Email))
      {
        qr = qr.Where(x => x.CRMContacts.Any(c => c.Email.Contains(filter.Email) || c.Email2.Contains(filter.Email)));
      }
      if (!string.IsNullOrEmpty(filter.Phone))
      {
        qr = qr.Where(x => x.CrmPhone.Contains(filter.Phone) || x.CRMContacts.Any(c => c.Phone.Contains(filter.Phone) || c.Phone2.Contains(filter.Phone)));
      }
      return qr;
    }
    public SummaryCustomer Summary(long id, DateTime fromDate, DateTime toDate)
    {
      var cus = GetById(id);
      var result = new SummaryCustomer() { Id = id };
      if (cus != null)
      {
        result.StatusCode = (CRMStatusCode)cus.CRMStatus.Code;
        result.SuccessFully = 1;
        result.CreatedDate = cus.CreatedDate;
        result.ModifyDate = cus.ModifyDate;
        result.TotalDocument = cus.CRMCusDocuments.Count;
        result.TotalVisitedSuccess = cus.CRMVisits.Count(x => x.IsEventAction == false && x.Status == (byte)CRMEventStatus.Finished);
        result.TotalVisited = cus.CRMVisits.Count(x => x.IsEventAction == false);
        result.TotalEvent = cus.CRMVisits.Count(x => x.IsEventAction == true);
        result.TotalSendEmail = cus.CRMPriceQuotations.Any() ? cus.CRMPriceQuotations.Sum(x => x.CountSendMail) : 0;
        result.TotalFirstSendEmail = cus.CRMPriceQuotations.Count(x => x.CountSendMail > 0);
        if (cus.SsmCusId.HasValue)
        {
          result.TotalShippments = Context.Shipments.Count(x => x.ShipperId == cus.SsmCusId || x.CneeId == cus.SsmCusId && x.DateShp <= toDate);
          if (result.TotalShippments > 0)
          {
            var listShipment =
                Context.Shipments.Where(x => x.ShipperId == cus.SsmCusId || x.CneeId == cus.SsmCusId)
                    .ToList();
            var sum = listShipment.Where(x => x.Revenue != null && x.Revenue.Earning.HasValue).Sum(x => x.Revenue.Earning.Value);
            result.TotalProfit = (decimal)sum;
          }
        }
        if (result.TotalShippments == 0)
          result.SuccessFully = 0;
      }

      return result;
    }



    public List<SummaryCustomer> SummarysList(User currenUser, PlanFilter filter)
    {
      var listdb = GetByMonthYear(filter);
      return listdb.Select(model => Summary(model.Id, filter.FromDate, filter.ToDate)).ToList();
    }

    private CRMCustomer ToDbModel(CRMCustomerModel model)
    {
      var db = new CRMCustomer
      {
        CompanyName = model.CompanyName,
        CompanyShortName = model.CompanyShortName,
        CrmAddress = model.CrmAddress,
        CrmPhone = model.CrmPhone,
        CrmCategoryId = model.CRMJobCategory.Id,
        CrmSourceId = model.CRMSource.Id,
        CrmGroupId = model.CRMGroup.Id,
        CrmProvinceId = model.Province.Id,
        SaleTypeId = model.SaleType.Id,
        CrmStatusId = model.CRMStatus.Id,
        Description = model.Description,
        CreatedById = model.CreatedBy.Id,
        CreatedDate = DateTime.Now,
        CrmDataType = (byte)model.DataType,
        SsmCusId = model.SsmCusId,
        CustomerType = (byte)model.CustomerType,

      };
      return db;
    }
    private void ConvertModel(CRMCustomerModel model, CRMCustomer db)
    {
      db.CompanyName = model.CompanyName;
      db.CompanyShortName = model.CompanyShortName;
      db.CrmAddress = model.CrmAddress;
      db.CrmPhone = model.CrmPhone;
      db.CrmCategoryId = model.CRMJobCategory.Id;
      db.CrmSourceId = model.CRMSource.Id;
      db.CrmGroupId = model.CRMGroup.Id;
      db.CrmProvinceId = 200;
      db.SaleTypeId = model.SaleType.Id;
      db.ModifyById = model.ModifyBy.Id;
      db.ModifyDate = model.ModifyDate;
      db.SsmCusId = model.SsmCusId;
      db.CrmStatusId = model.CRMStatus.Id;
      db.Description = model.Description;
      if (model.MoveToId.HasValue && model.MoveToId.Value > 0) return;
      db.CreatedById = model.MoveToId;
    }

    private CRMCustomerModel ToModels(CRMCustomer customer)
    {
      if (customer == null) return null;
      var model = Mapper.Map<CRMCustomerModel>(customer);
      if (customer.Province != null)
      {
        model.CrmCountryId = customer.Province.CountryId;
        model.CountryName = customer.Province.Country.CountryName;
        model.StateName = customer.Province.Name;
      }
      model.CRMContacts = new List<CRMContactModel>(customer.CRMContacts.Select(Mapper.Map<CRMContactModel>));
      if (customer.CRMFollowCusUsers.Any())
      {
        model.FollowCusUsers = new List<CRMFollowCusUserModel>(customer.CRMFollowCusUsers.Select(Mapper.Map<CRMFollowCusUserModel>));
      }
      if (!string.IsNullOrEmpty(model.UserTogheTheFollow))
      {
        var listId = model.UserTogheTheFollow.Split(';').Where(x => !string.IsNullOrEmpty(x)).Select(long.Parse);
        model.UsersFollow = Context.Users.Where(u => listId.Contains(u.Id)).ToList();
      }

      if (model.CRMStatus != null)
      {
        model.StatusCode = (CRMStatusCode)model.CRMStatus.Code;
      }

      model.CreatedBy = customer.User;
      model.ModifyBy = customer.User1;
      //  model.Summary = Summary(model.Id);
      return model;
    }

    public List<SalesTypeSummaryReport> SalesTypeSumaryReport(PlanFilter filter)
    {
      var qr = GetQuery(x => x.CreatedDate.Year == filter.Year && x.SsmCusId.HasValue);
      switch (filter.SummaryByType)
      {
        case SummaryByType.ByUser:
          qr = qr.Where(x => x.CreatedById == filter.Id);
          break;
        case SummaryByType.ByDeparment:
          qr = qr.Where(x => x.User.DeptId == filter.Id);
          break;
        case SummaryByType.ByOffice:
          qr = qr.Where(x => x.User.ComId == filter.Id);
          break;
      }
      var listCust = new List<CustomerSammaryReport>();
      foreach (var it in qr.ToList())
      {
        for (var i = 1; i <= 12; i++)
        {
          var cus = new CustomerSammaryReport
          {
            Customer = it,
            Month = i,
            ShipmentCount =
                  Context.Shipments.Count(x => (x.ShipperId == it.SsmCusId || x.CneeId == it.SsmCusId) && x.DateShp.Value.Month == i && x.DateShp.Value.Year == filter.Year)
          };
          listCust.Add(cus);
        }
      }
      var list = listCust.GroupBy(x => x.Customer.SaleType).Select(it => new SalesTypeSummaryReport
      {
        SaleType = it.Key,
        SammaryReports = it.ToList()
      }).ToList();
      return list;
    }

    public List<Shipment> GetListShipments(long ssmCusId)
    {
      var list = Context.Shipments.Where(x => x.ShipperId == ssmCusId || x.CneeId == ssmCusId).OrderByDescending(x => x.DateShp).ToList();
      return list;
    }
    public List<MT81> GetListSalesTrading(long ssmCusId)
    {
      var list = Context.MT81s.Where(x => x.CustomerID.Value == ssmCusId).ToList();
      return list;
    }

    public List<CRMFilterProfitResult> GetCRMListProfit(CRMFilterProfit filter, User user)
    {
      var qr = GetQuery(x => x.SsmCusId.HasValue &&
                             (filter.UserId == 0 || x.CreatedById == filter.UserId) &&
                             (filter.DeptId == 0 || x.User.DeptId == filter.DeptId) &&
                             (filter.OfficeId == 0 || x.User.ComId == filter.OfficeId) &&
                             x.CreatedDate.Date >= filter.BeginDate && x.CreatedDate.Date <= filter.EndDate &&
                             Context.Shipments.Any(
                                 s => s.CneeId == x.SsmCusId.Value || s.ShipperId == x.SsmCusId.Value)
      );
      if (!user.IsAdmin() && filter.UserId == 0 && filter.DeptId == 0 && filter.OfficeId == 0)
      {
        if (user.IsDirecter())
        {
          var comid = Context.ControlCompanies.Where(x => x.UserId == user.Id).Select(x => x.ComId).ToList();
          comid.Add(user.Id);
          qr = qr.Where(x => comid.Contains(x.User.ComId.Value) && x.IsUserDelete == false);
        }
        else if (user.IsDepManager())
        {
          qr = qr.Where(x => x.User.DeptId == user.DeptId && x.IsUserDelete == false);
        }
        else
        {
          qr = qr.Where(x => x.CreatedById == user.Id && x.IsUserDelete == false);
        }
      }
      var rs = qr.Select(r => new CRMFilterProfitResult
      {
        Customer = r,
        TotalProfit =
               Context.Shipments.Where(s => (s.ShipperId == r.SsmCusId || s.CneeId == r.SsmCusId) && (s.Revenue != null && s.Revenue.Earning.HasValue))
                   .Sum(x => x.Revenue.Earning ?? 0),
        TotalShipment = Context.Shipments.Count(s => s.ShipperId == r.SsmCusId || s.CneeId == r.SsmCusId),
      });
      if (filter.Top != 0)
      {
        rs = rs.Take(filter.Top);
      }
      var result = rs.OrderByDescending(r => r.TotalProfit);
      return result.ToList();
    }
    public List<CrmFilterTopProfitByStore> GetCRMListFollowProfit(CRMFilterFollowProfit filter, User user)
    {
      var query = $@"exec [dbo].[GetCrmProfitAndQuanty] 
                      @Top ={filter.Top}, @IsLost ={(filter.IsLost ? 1 : 0)},@IsProfit ={(filter.IsProfit ? 1 : 0)}, @DaysOfLost ={filter.DaysOfLost},
                      @BeginDate ='{filter.BeginDate:yyyy-MM-dd}', @EndDate ='{filter.EndDate:yyyy-MM-dd hh:mm:ss}',  @UserId ={filter.UserId}, @DeptId ={filter.DeptId },
                      @OfficeId ={filter.OfficeId}, @CurrentUserId ={user.Id},
                      @AgentId ={filter.AgentId}, @Status ={(byte)filter.Status},  @DepartureId ={filter.DepartureId},
                      @DestinationId ={filter.DestinationId}, @ProCode ='{filter.ProCode}', @ProName ='{filter.ProName}' ";
      var qr = Context.ExecuteQuery<CrmFilterTopProfitByStore>(query);
      return qr.ToList();
    }

    public CRMStatusCode SetSatus(long id, out bool isCancel)
    {
      var cus = GetById(id);
      isCancel = false;
      CRMStatusCode code = CRMStatusCode.Potential;
      if (cus != null)
      {
        DateTime cancelDate = cus.DateCancel ?? cus.CreatedDate.Date;
        DateTime endDate = cancelDate.AddDays(365);
        var shipments = GetListShipments(cus.SsmCusId ?? 0).Count;
        var lastShipment = Context.Shipments.Where(s => s.CneeId.Value == cus.SsmCusId.Value || s.ShipperId.Value == cus.SsmCusId.Value)
                                            .OrderByDescending(x => x.CreateDate).FirstOrDefault();
        if (endDate < DateTime.Today)
        {
          if (lastShipment != null && lastShipment.CreateDate.Value < cancelDate)
          {
            isCancel = true;
          }

        }

        if (isCancel)
        {
          code = CRMStatusCode.Client;
        }
        else
        {
          code = shipments >= 1 ? CRMStatusCode.Success : CRMStatusCode.Potential;
        }
      }
      return code;
    }



    public IEnumerable<CRMFilterProfitResult> GetAllCRMResults(CRMSearchModel fModel, SortField sortField, out int totalRows, int page, int pageSize,
        User currenUser, out int totalSeccessCount, out int totalClientCount, out int totalFolowCount)
    {
      IEnumerable<CRMFilterProfitResult> rs = GetAllSearchNotPaging(fModel, sortField, currenUser, out totalRows);
      var data = rs;
      var allrs = rs;//.ToList();
      totalSeccessCount = allrs.Count(x => x.AnyShipmentOld == false && x.TotalShipmentSuccess >= 1);
      totalClientCount = allrs.Count(x => x.AnyShipmentOld);
      totalFolowCount = allrs.Count(x => x.AnyShipmentOld == false && x.TotalShipment == 0);
      var skip = (page - 1) * pageSize;
      return data.Skip(skip).Take(pageSize).ToList();

    }

    public IEnumerable<CRMFilterProfitResult> GetAllSearchNotPaging(CRMSearchModel fModel, SortField sortField, User currenUser, out int totalRows)
    {
      fModel = fModel ?? new CRMSearchModel();
      fModel.SaleType = fModel.SaleType ?? new SaleType();
      fModel.CrmGroup = fModel.CrmGroup ?? new CRMGroup();
      fModel.CRMSource = fModel.CRMSource ?? new CRMSource();
      fModel.JobCategory = fModel.JobCategory ?? new CRMJobCategory();
      totalRows = 0;
      var qr = GetQuyryCustomers(fModel, currenUser);
      totalRows = qr.Count();
      qr = qr.OrderBy(sortField);
      IQueryable<CRMFilterProfitResult> rs;
      if (fModel.PeriodDate == PeriodDate.CreateDate)
      {
        rs = qr.Select(r => new CRMFilterProfitResult
        {
          Customer = r,
          TotalShipmentSuccess = Context.Shipments.Count(s => (s.CneeId.Value == r.SsmCusId.Value || s.ShipperId.Value == r.SsmCusId.Value) && s.DateShp >= fModel.FromDate && s.DateShp <= fModel.ToDate),
          TotalShipment = Context.Shipments.Count(s => (s.CneeId.Value == r.SsmCusId.Value || s.ShipperId.Value == r.SsmCusId.Value) && s.DateShp <= fModel.ToDate),
          AnyShipmentOld = Context.Shipments.Any(s => (s.CneeId.Value == r.SsmCusId.Value || s.ShipperId.Value == r.SsmCusId.Value) && s.DateShp < fModel.FromDate),
          TotalQuotation = r.CRMPriceQuotations.Any() ? r.CRMPriceQuotations.Sum(xq => xq.CountSendMail) : 0,
          TotalVisit = r.CRMVisits.Count(x => x.IsEventAction == false),
          TotalEvent = r.CRMVisits.Count(x => x.IsEventAction == true),
          FollowName = string.Join("; ", r.CRMFollowCusUsers.Select(f => f.User.FullName)),
          Status = r.CRMStatus,
          CreaterdBy = r.User,
          Source = r.CRMSource.Name,
          SaleType = r.SaleType.Name
        });

      }
      else
      {
        rs = qr.Select(r => new CRMFilterProfitResult
        {
          Customer = r,
          TotalShipmentSuccess = Context.Shipments.Count(s => (s.CneeId.Value == r.SsmCusId.Value || s.ShipperId.Value == r.SsmCusId.Value) && s.DateShp >= r.CreatedDate && s.DateShp <= fModel.ToDate),
          TotalShipment = Context.Shipments.Count(s => (s.CneeId.Value == r.SsmCusId.Value || s.ShipperId.Value == r.SsmCusId.Value) && s.DateShp <= fModel.ToDate),
          AnyShipmentOld = Context.Shipments.Any(s => (s.CneeId.Value == r.SsmCusId.Value || s.ShipperId.Value == r.SsmCusId.Value) && s.DateShp < r.CreatedDate),
          TotalQuotation = r.CRMPriceQuotations.Any() ? r.CRMPriceQuotations.Sum(xq => xq.CountSendMail) : 0,
          TotalVisit = r.CRMVisits.Count(x => x.IsEventAction == false),
          TotalEvent = r.CRMVisits.Count(x => x.IsEventAction == true),
          FollowName = string.Join(";", r.CRMFollowCusUsers.Select(f => f.User.FullName)),
          Status = r.CRMStatus,
          CreaterdBy = r.User,
          Source = r.CRMSource.Name,
          SaleType = r.SaleType.Name
        });
      }

      switch (fModel.CRMStatus)
      {
        case CRMStatusCode.Success:
          rs = rs.Where(x => x.AnyShipmentOld == false && x.TotalShipmentSuccess >= 1);
          break;
        case CRMStatusCode.Potential:
          rs = rs.Where(x => x.AnyShipmentOld == false && x.TotalShipment == 0);
          break;
        case CRMStatusCode.Client:
          rs = rs.Where(x => x.AnyShipmentOld);
          break;
      }
      return rs;
    }

  }
}