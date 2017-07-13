using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.Office.Interop.Excel;
using SSM.Common;

using SSM.Models;
using SSM.ViewModels;

namespace SSM.Services
{
    public interface IStockReceivingService : IServices<MT72>
    {
        IEnumerable<Curency> GetAllCurencies();
        IEnumerable<MT72> GetAll(TradingStockSearch filter, int page, int pageSize, out int totalRow, User currenUser);
        IEnumerable<OrderModel> GetAllModel(TradingStockSearch filter, int page, int pageSize, out int totalRow, User currenUser);
        MT72 GetBy(Expression<Func<MT72, bool>> filter);
        MT72 GetById(long id);
        OrderModel GetByModel(long id);
        MT72 GetByNo(string no);
        void Insert(OrderModel order, out long id);
        void Update(OrderModel order);
        void DeleteOrder(long id);
        void VoucherAction(VoucherStatus status, long userId, long id);
        string GetVoucherNo();
        long GetVoucherId();

        int GetVoucherCodeId(string code = "");
        int SubmitStockCard(long voucherId, bool isSubmit);

    }
    public class StockReceivingService : Services<MT72>, IStockReceivingService
    {


        public IEnumerable<Curency> GetAllCurencies()
        {
            return Context.Curencies.OrderBy(x => x.Code).ToList();
        }

        public IEnumerable<MT72> GetAll(TradingStockSearch filter, int page, int pageSize, out int totalRow, User currenUser)
        {
            try
            {
                var qr = GetQueryFilter(filter, currenUser);
                totalRow = qr.Count();
                return GetListPager(qr, page, pageSize);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                totalRow = 0;
                return new List<MT72>();
            }
        }

        public IEnumerable<OrderModel> GetAllModel(TradingStockSearch filter, int page, int pageSize, out int totalRow, User currenUser)
        {
            var dbList = GetAll(filter, page, pageSize, out totalRow, currenUser);
            return dbList.Select(mt72 => CopyToModel(mt72)).ToList();
        }


        public MT72 GetBy(Expression<Func<MT72, bool>> filter)
        {
            return Context.MT72s.FirstOrDefault(filter);
        }

        public MT72 GetById(long id)
        {
            return Context.MT72s.FirstOrDefault(x => x.VoucherID == id);
        }

        public OrderModel GetByModel(long id)
        {
            var order = GetById(id);
            if (order == null) return null;
            return CopyToModel(order); ;
        }

        public MT72 GetByNo(string no)
        {
            return Context.MT72s.FirstOrDefault(x => x.VoucherNo == no);
        }

        public void Insert(OrderModel order, out long id)
        {
            var dbOrder = new MT72
             {
                 DateCreate = DateTime.Now,
                 CreateBy = order.CreateBy,
                 CurencyID = order.Curency.Id,
                 DeclaraDate = order.DeclaraDate,
                 ReceiptDate = order.ReceiptDate,
                 VoucherDate = order.VoucherDate,
                 DeclaraNo = order.DeclaraNo,
                 HBL = order.HBL,
                 MBL = order.MBL,
                 SupplierID = order.Supplier.Id,
                 VoucherCode = GetVoucherCodeId(),
                 VoucherID = order.VoucherId,
                 status = (byte)VoucherStatus.Pending,
                 VoucherNo = order.VoucherNo,
                 CountryID = order.Country.Id,
                 ExchangeRate = order.ExchangeRate,
                 Description = order.Description,
                 NotePrints = order.NotePrints,

             };
            var esited = GetById(order.VoucherId);
            if (esited != null)
            {
                dbOrder.VoucherID = GetVoucherId();
                dbOrder.VoucherNo = GetVoucherNo();
            }
            id = dbOrder.VoucherID;
            Context.MT72s.InsertOnSubmit(dbOrder);
            Commited();
            InsertToDetails(order);


        }

        private void InsertToDetails(OrderModel model)
        {
            var dts = model.OrderDetails;
            if (dts != null && dts.Any())
            {
                var mt = GetById(model.VoucherId);
                decimal tQty, tAmount, tTax, tTransportFee, tInlandFee, tFee1, tfee2, tFee, tToltal;
                tQty = tAmount = tTax = tTransportFee = tInlandFee = tFee1 = tfee2 = tFee = tToltal = 0M;

                int id = 1;
                foreach (var dt in dts)
                {
                    var amount = dt.Quantity * dt.Price;
                    var tax = (amount * dt.ImportTaxRate) / 100;
                    var tfee = dt.Fee1 + dt.Fee2 + dt.TransportFee + dt.InlandFee;
                    var db = new DT72()
                    {
                        ProductID = dt.ProductId,
                        WarehouseID = dt.WarehouseId,
                        UOM = dt.UOM,
                        Quantity = dt.Quantity,
                        Price = dt.Price,
                        ImportTaxRate = dt.ImportTaxRate,
                        TransportFee = dt.TransportFee,
                        InlandFee = dt.InlandFee,
                        Fee1 = dt.Fee1,
                        Fee2 = dt.Fee2,
                        VoucherID = model.VoucherId,
                        RowID = id,
                        T_Fee = tfee,
                        Amount = amount,
                        Description = dt.Note,
                        VATTax = dt.Tax,
                        VATTaxRate = dt.TaxRate,
                        ImportTax = tax

                    };
                    var total = tfee + amount + tax;
                    db.TT = total;
                    db.PriceReceive = total / dt.Quantity;
                    tQty += dt.Quantity;
                    tAmount += amount;
                    tTax += db.ImportTax ?? 0 + db.VATTax ?? 0;
                    tTransportFee += dt.TransportFee;
                    tInlandFee += dt.InlandFee;
                    tFee1 += dt.Fee1;
                    tfee2 += dt.Fee2;
                    tFee += dt.Fee1 + dt.Fee2 + dt.TransportFee + dt.InlandFee;
                    tToltal += total;
                    Context.DT72s.InsertOnSubmit(db);
                    id++;
                }
                mt.T_Quantity = tQty;
                mt.T_Amount = tAmount;
                mt.T_VATTax = tTax;
                mt.t_TransportFee = tTransportFee;
                mt.t_InlandFee = tInlandFee;
                mt.t_Fee = tFee;
                mt.t_Fee1 = tFee1;
                mt.t_Fee2 = tfee2;
                mt.T_TT = tToltal;
            }
            Commited();
        }

        public void Update(OrderModel order)
        {
            var mt = GetById(order.VoucherId);
            if (mt == null)
                throw new NullReferenceException("not found voucherId:" + order.VoucherId);
            mt.ModifyBy = order.ModifyBy;
            mt.DateModify = order.DateModify;
            mt.CurencyID = (int)order.Curency.Id;
            mt.DeclaraDate = order.DeclaraDate;
            mt.ReceiptDate = order.ReceiptDate;
            mt.VoucherDate = order.VoucherDate;
            mt.DeclaraNo = order.DeclaraNo;
            mt.HBL = order.HBL;
            mt.MBL = order.MBL;
            mt.Description = order.Description;
            mt.SupplierID = order.Supplier.Id;
            mt.VoucherID = order.VoucherId;
            mt.CountryID = (int)order.Country.Id;
            mt.ExchangeRate = order.ExchangeRate;
            mt.NotePrints = order.NotePrints;
            //pro detail
            var dts = mt.DT72s.ToList();
            Context.DT72s.DeleteAllOnSubmit(dts);
            Commited();
            InsertToDetails(order);
        }

        public void DeleteOrder(long id)
        {
            var mt = GetById(id);
            if (mt == null)
                throw new NullReferenceException("not found voucherId:" + id);
            if (mt.status == (byte)VoucherStatus.Pending)
            {
                try
                {
                    var dts = Context.DT72s.Where(x => x.VoucherID == id).ToList();
                    Context.DT72s.DeleteAllOnSubmit(dts);
                    Context.MT72s.DeleteOnSubmit(mt);
                    Commited();
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
            }
        }

        public void VoucherAction(VoucherStatus status, long userId, long id)
        {
            bool isCancle = false;
            var mt = GetById(id);
            if (mt == null)
                throw new NullReferenceException("not found voucherId:" + id);
            if (mt.status > (byte)status)
                isCancle = true;
            mt.status = (byte)status;
            switch (status)
            {
                case VoucherStatus.Approved:
                    mt.ApprovedBy = userId;
                    mt.DateApproved = DateTime.Now;
                    break;
                case VoucherStatus.Checked:
                    if (isCancle == false)
                    {
                        mt.CheckedBy = userId;
                        mt.DateChecked = DateTime.Now;
                    }
                    mt.ApprovedBy = null;
                    mt.DateApproved = null;
                    break;
                case VoucherStatus.Submited:
                    if (isCancle == false)
                    {
                        mt.SubmittedBy = userId;
                        mt.DateSubmited = DateTime.Now;
                    }
                    mt.CheckedBy = null;
                    mt.DateChecked = null;
                    mt.ApprovedBy = null;
                    mt.DateApproved = null;
                    break;
                //case VoucherStatus.Locked:
                //    if (mt.status == (byte)VoucherStatus.Approved)
                //        mt.status = (byte)VoucherStatus.Checked;
                //    if (mt.status == (byte)VoucherStatus.Checked)
                //        mt.status = (byte)VoucherStatus.Submited;
                //    if (mt.status == (byte)VoucherStatus.Submited)
                //    {
                //        mt.status = (byte)VoucherStatus.Pending;
                //        SubmitStockCard(id, false);
                //    }
                //    break;
                case VoucherStatus.Pending:
                default:
                    mt.SubmittedBy = null;
                    mt.CheckedBy = null;
                    mt.ApprovedBy = null;
                    mt.DateSubmited = null;
                    mt.DateChecked = null;
                    mt.DateApproved = null;
                    break;

            }
            Commited();
            if (status == VoucherStatus.Submited || status == VoucherStatus.Pending)
            {
                SubmitStockCard(id, true);
            }
        }

        public string GetVoucherNo()
        {
            long vcNum = 1;
            var mtlast = Context.MT72s.OrderByDescending(x => x.DateCreate).FirstOrDefault();
            if (mtlast != null)
                vcNum = long.Parse(mtlast.VoucherNo.GetNumberFromStr()) + 1;

            return "NK" + vcNum.ToString("000000"); ;
        }

        public long GetVoucherId()
        {
            var order = Context.Orders.FirstOrDefault(x => x.Id == 1);
            if (order == null)
                throw new SqlNullValueException("Order number not value");
            long newId = order.Number + 1;
            order.Number = newId;
            order.ngay_dn = DateTime.Now;
            Commited();
            return newId;
        }

        public int GetVoucherCodeId(string code = "")
        {
            if (string.IsNullOrEmpty(code))
            {
                code = "PNA";
            }
            var voucher = Context.Vouchers.FirstOrDefault(x => x.code.Trim() == code.Trim());
            if (voucher != null)
                return voucher.id;
            return 1;
        }

        public int SubmitStockCard(long voucherId, bool isSubmit)
        {
            return Context.sp_postpv((int)voucherId);
        }

        private OrderModel CopyToModel(MT72 order)
        {
            if (order.status != null)
            {
                var model = new OrderModel
                {
                    DateCreate = DateTime.Now,
                    CreateBy = order.CreateBy.HasValue ? order.CreateBy.Value : 0,
                    Curency = order.Curency ?? new Curency(),
                    DeclaraDate = order.DeclaraDate,
                    ReceiptDate = order.ReceiptDate,
                    VoucherDate = order.VoucherDate,
                    DeclaraNo = order.DeclaraNo,
                    HBL = order.HBL,
                    MBL = order.MBL,
                    Supplier = order.Supplier ?? new Supplier(),
                    VoucherCode = GetVoucherCodeId(),
                    VoucherId = order.VoucherID,
                    Status = (VoucherStatus)order.status,
                    VoucherNo = order.VoucherNo,
                    Country = order.Country ?? new Country(),
                    ExchangeRate = order.ExchangeRate ?? 0,
                    Description = order.Description,
                    Fee = order.t_Fee ?? 0,
                    Fee1 = order.t_Fee1 ?? 0,
                    Fee2 = order.t_Fee2 ?? 0,
                    InlnadFee = order.t_InlandFee ?? 0,
                    TransportFee = order.t_TransportFee ?? 0,
                    TAmount = order.T_Amount ?? 0,
                    QuantityTotal = order.T_Quantity ?? 0,
                    TTT = order.T_TT ?? 0,
                    TVATTax = order.T_VATTax ?? 0,
                    UserCreated = order.User ?? new User(),
                    UserChecked = order.User1 ?? new User(),
                    UserApproved = order.User2 ?? new User(),
                    UserSubmited = order.User3 ?? new User(),
                    DateSubmited = order.DateSubmited,
                    DateChecked = order.DateChecked,
                    DateApproved = order.DateApproved,
                    DateModify = order.DateModify,
                    NotePrints = order.NotePrints,


                };
                model.VnTTT = (model.ExchangeRate > 0 ? model.ExchangeRate : 1) * model.TTT;
                var dbdetails = Context.DT72s.Where(x => x.VoucherID == order.VoucherID).ToList();
                if (dbdetails.Any())
                {
                    var details = dbdetails.Select(x => CopyModeDetail(x)).ToList();
                    model.OrderDetails = details;
                    try
                    {

                        var prs = details.Any()
                            ? details.Aggregate(string.Empty,
                                (current, dt) => current + dt.ProductCode + ",<br/> ")
                            : string.Empty;
                        model.ProductView = prs;
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex);
                        model.ProductView = string.Empty;
                    }
                }

                return model;
            }
            return null;
        }

        private OrderDetailModel CopyModeDetail(DT72 dt)
        {

            var prod = Context.Products.FirstOrDefault(x => x.Id == dt.ProductID);
            var proName = prod != null ? prod.Code.Trim() + "-" + prod.Name.Trim() : string.Empty;
            var exRate = dt.MT72.ExchangeRate.Value > 0 ? dt.MT72.ExchangeRate.Value : 1;
            var db = new OrderDetailModel()
            {
                ProductId = dt.ProductID ?? 0,
                ProductCode = proName,
                WarehouseId = dt.WarehouseID ?? 0,
                UOM = dt.UOM,
                Quantity = dt.Quantity ?? 0,
                Price = dt.Price ?? 0,
                ImportTax = dt.ImportTax ?? 0,
                ImportTaxRate = dt.ImportTaxRate ?? 0,
                TransportFee = dt.TransportFee ?? 0,
                InlandFee = dt.InlandFee ?? 0,
                Fee1 = dt.Fee1 ?? 0,
                Fee2 = dt.Fee2 ?? 0,
                VoucherId = dt.VoucherID,
                RowId = dt.RowID,
                Amount = dt.Amount ?? 0,
                Note = dt.Description,
                Tax = dt.VATTax ?? 0,
                TaxRate = dt.VATTaxRate ?? 0,
                Total = dt.TT ?? 0,
                PriceReceive = dt.PriceReceive ?? 0,
                TFee = dt.T_Fee ?? 0,
                Product = dt.Product,
                Warehouse = dt.Warehouse, 

            };
            
            db.VnTotal = db.Total * exRate;
            return db;
        }

        private IQueryable<MT72> GetQueryFilter(TradingStockSearch filter, User currenUser)
        {
            filter = filter ?? new TradingStockSearch();
            filter.Product = filter.Product ?? new Product();
            filter.Supplier = filter.Supplier ?? new Supplier();
            filter.Warehouse = filter.Warehouse ?? new Warehouse();
            filter.Customer = filter.Customer ?? new Customer();
            filter.CreatedBy = filter.CreatedBy ?? new User();
            filter.SortField = filter.SortField ?? new SSM.Services.SortField(string.Empty, true);

            var query = GetQuery(x =>
                (string.IsNullOrEmpty(filter.Supplier.FullName) || x.Supplier.FullName.Contains(filter.Supplier.FullName))
                && (string.IsNullOrEmpty(filter.VoucherNo) || x.VoucherNo.Contains(filter.VoucherNo))
                && (string.IsNullOrEmpty(filter.HBL) || x.HBL.Contains(filter.HBL))
                && (string.IsNullOrEmpty(filter.MBL) || x.MBL.Contains(filter.MBL))
                && (filter.CreatedId == 0 || x.CreateBy == filter.CreatedId)
                && (string.IsNullOrEmpty(filter.Product.Code) || x.DT72s.Any(d => d.Product.Code.Contains(filter.Product.Code)))
                && (string.IsNullOrEmpty(filter.Product.Name) || x.DT72s.Any(d => d.Product.Name.Contains(filter.Product.Name)))
                && (filter.Warehouse.Id == 0 || x.DT72s.Any(d => d.WarehouseID.Value == filter.Warehouse.Id))
                );


            if (filter.FromDate.HasValue)
            {
                query = query.Where(x => x.VoucherDate >= filter.FromDate.Value.Date);
            }
            if (filter.ToDate.HasValue)
            {
                query = query.Where(x => x.VoucherDate <= filter.ToDate.Value.Date);
            }

            if (!(currenUser.IsAdmin() || currenUser.IsAccountant()))
            {
                if (currenUser.IsDirecter())
                {
                    var comid = Context.ControlCompanies.Where(x => x.UserId == currenUser.Id).Select(x => x.ComId).ToList();
                    comid.Add(currenUser.Id);
                    query = query.Where(x => comid.Contains(x.User.ComId.Value));
                }
                else if (currenUser.IsDepManager())
                {
                    query = query.Where(x => x.User.DeptId == currenUser.DeptId && x.User.ComId == currenUser.ComId);
                }
                else
                {
                    query = query.Where(x => x.User.Id == currenUser.Id);
                }
            }
            query = string.IsNullOrEmpty(filter.SortField.FieldName) ? query.OrderByDescending(x => x.VoucherDate) : query.OrderBy(filter.SortField);
            return query;
        }
    }
}