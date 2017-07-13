using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using SSM.Common;

using SSM.Models;
using SSM.ViewModels;
using WebGrease.Css.Extensions;

namespace SSM.Services
{
    public interface ISalesServices : IServices<MT81>
    {
        IEnumerable<MT81> GetAll(TradingStockSearch filter, int page, int pageSize, out int totalRow, User currenUser);
        IEnumerable<SalesModel> GetAllModel(TradingStockSearch filter, int page, int pageSize, out int totalRow, User currenUser);
        MT81 GetById(long id);
        SalesModel GetModelById(long id);
        void InsertSale(SalesModel model, out long id);
        void Update(SalesModel order);
        string GetVoucherNo();
        int GetQtyInventory(int voucherId, int prodId, int warehouseId);
        void StockCardAction(VoucherStatus status, long userId, long id);
        bool DeleteOrder(MT81 db);
        bool CheckValidQty(long id);
        void UpdateRevenue(long id);
    }
    public class SalesServices : Services<MT81>, ISalesServices
    {
        public IEnumerable<MT81> GetAll(TradingStockSearch filter, int page, int pageSize, out int totalRow, User currenUser)
        {
            var qr = GetQueryFilter(filter, currenUser);
            totalRow = qr.Count();
            return GetListPager(qr, page, pageSize);
        }

        public IEnumerable<SalesModel> GetAllModel(TradingStockSearch filter, int page, int pageSize, out int totalRow, User currenUser)
        {
            var dbList = GetAll(filter, page, pageSize, out totalRow, currenUser);
            return dbList.Select(mt => CopyToModel(mt)).ToList();
        }

        public MT81 GetById(long id)
        {
            Context = new DataClasses1DataContext();
            return Context.MT81s.FirstOrDefault(x => x.VoucherID == id);
        }
        public string GetVoucherNo()
        {
            long vcNum = 1;
            var mtlast = Context.MT81s.OrderByDescending(x => x.DateCreate).FirstOrDefault();
            if (mtlast != null && mtlast.VoucherNo != null)
                vcNum = long.Parse(mtlast.VoucherNo.GetNumberFromStr()) + 1;

            return "XK" + vcNum.ToString("000000"); ;
        }

        public int GetQtyInventory(int voucherId, int prodId, int warehouseId)
        {
            try
            {
                var inventorys = Context.StockCards.Where(stockCard =>
                    stockCard.VoucherID != voucherId && stockCard.ProductID == prodId &&
                    stockCard.WarehouseID == warehouseId);
                var rs = inventorys.Any() ?
                    inventorys.Sum(x => (x.sl_nhap.Value - x.sl_xuat.Value)) : 0;
                var pendings = Context.DT81s.Where(
                    x =>
                        x.ProductID.Value == prodId && x.WarehouseID.Value == warehouseId && x.VoucherID != voucherId &&
                        x.MT81.Status == (byte)VoucherStatus.Pending);
                var sellPeding = pendings.Any() ?
                    pendings.Sum(x => x.Quantity.Value) : 0;
                var qty = (int)(rs - sellPeding);
                return qty > 0 ? qty : 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return 0;
            }

        }
        private int SubmitStockCard(long voucherId)
        {
            return Context.sp_postsv((int?)voucherId);
        }
        public void StockCardAction(VoucherStatus status, long userId, long id)
        {
            bool isCancle = false;
            var mt = GetById(id);
            if (mt == null)
                throw new NullReferenceException("not found voucherId:" + id);
            if (mt.Status > (byte)status)
                isCancle = true;
            mt.Status = (byte)status;
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
                //    if (mt.Status == (byte)VoucherStatus.Approved)
                //        mt.Status = (byte)VoucherStatus.Checked;
                //    if (mt.Status == (byte)VoucherStatus.Checked)
                //        mt.Status = (byte)VoucherStatus.Submited;
                //    if (mt.Status == (byte)VoucherStatus.Submited)
                //    {
                //        mt.Status = (byte)VoucherStatus.Pending;
                //        SubmitStockCard(id, false);
                //    }
                //    break;
                case VoucherStatus.Pending:
                default:
                    mt.T_Amount0 = 0;
                    mt.SubmittedBy = null;
                    mt.DateSubmited = null;
                    mt.CheckedBy = null;
                    mt.DateChecked = null;
                    mt.ApprovedBy = null;
                    mt.DateApproved = null;
                    break;

            }
            Commited();
            if (status == VoucherStatus.Submited || status == VoucherStatus.Pending)
            {
                SubmitStockCard(id);
            }
        }

        public bool DeleteOrder(MT81 db)
        {
            try
            {

                var dt81s = Context.DT81s.Where(x => x.VoucherID == db.VoucherID).ToList();
                Context.DT81s.DeleteAllOnSubmit(dt81s);
                Delete(db);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool CheckValidQty(long id)
        {
            var dts = Context.DT81s.Where(x => x.VoucherID == id).ToList();
            return dts.All(x => x.Quantity >= GetQtyInventory((int)id, (int)x.ProductID.Value, x.WarehouseID.Value));
        }

        public void UpdateRevenue(MT81 mt)
        {
            if (mt.Shipments != null)
            {
                //update shipmet
                var shipment = Context.Shipments.FirstOrDefault(x => x.Id == mt.Shipments.Id);
                shipment.DateShp = mt.VoucherDate;
                shipment.CneeId = mt.CustomerID ?? 0;
                // update revenue
                var amount = mt.T_Amount ?? 0;
                var funds = mt.T_Amount0 ?? 0;
                var revenue = Context.Revenues.FirstOrDefault(x => x.Id == shipment.Id);
                var dbProfit = revenue.INVI - revenue.EXVI;
                var profit = amount - funds;
                //D 
                revenue.INVI = (revenue.INVI - revenue.INAutoValue1) + amount;
                revenue.Income = (revenue.Income - revenue.INAutoValue1) + amount;
                revenue.INAutoValue1 = amount;
                //C
                revenue.EXVI = (revenue.EXVI - revenue.EXManualValue1) + funds;
                revenue.Expense = (revenue.Expense - revenue.EXManualValue1) + funds;
                revenue.EXManualValue1 = funds;

                revenue.EarningVI = (revenue.EarningVI - dbProfit) + profit;
                revenue.Earning = (revenue.Earning - dbProfit) + profit;
                Commited();
            }
        }
        public void UpdateRevenue(long id)
        {
            var mt = GetById(id);
            UpdateRevenue(mt);
        }

        public SalesModel GetModelById(long id)
        {
            return CopyToModel(GetById(id));
        }

        public void InsertSale(SalesModel model, out long id)
        {
            var mt = new MT81();
            var esited = GetById(model.VoucherId);
            if (esited != null)
            {
                model.VoucherId = GetVoucherId();
                model.VoucherNo = GetVoucherNo();
            }
            id = model.VoucherId;
            mt = CopyToDb(model, mt);
            this.Insert(mt);
            InsertToDetails(model);

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
        public void Update(SalesModel order)
        {
            var mt = GetById(order.VoucherId);
            order.CreateBy = mt.CreateBy.Value;
            order.DateCreate = mt.DateCreate.Value;
            mt = CopyToDb(order, mt);
            //pro detail 
            var dts = mt.DT81s.ToList();
            Context.DT81s.DeleteAllOnSubmit(dts);
            Commited();
            InsertToDetails(order);
            UpdateRevenue(mt);

        }
        private IQueryable<MT81> GetQueryFilter(TradingStockSearch filter, User currenUser)
        {
            filter = filter ?? new TradingStockSearch();
            filter.Product = filter.Product ?? new Product();
            filter.Supplier = filter.Supplier ?? new Supplier();
            filter.Warehouse = filter.Warehouse ?? new Warehouse();
            filter.Customer = filter.Customer ?? new Customer();
            filter.CreatedBy = filter.CreatedBy ?? new User();
            filter.SortField = filter.SortField ?? new SSM.Services.SortField(string.Empty, true);
            var query = GetQuery(x =>
                (string.IsNullOrEmpty(filter.Customer.FullName) || x.Customer.FullName.Contains(filter.Customer.FullName))
                 && (string.IsNullOrEmpty(filter.Product.Code) || x.DT81s.Any(d => d.Product.Code.Contains(filter.Product.Code)))
                && (string.IsNullOrEmpty(filter.Product.Name) || x.DT81s.Any(d => d.Product.Name.Contains(filter.Product.Name)))
                && (filter.Warehouse.Id == 0 || x.DT81s.Any(d => d.WarehouseID.Value == filter.Warehouse.Id))
                && (filter.CreatedId == 0 || x.CreateBy == filter.CreatedId)
                && (string.IsNullOrEmpty(filter.VoucherNo) || x.VoucherNo.Contains(filter.VoucherNo))
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

        private MT81 CopyToDb(SalesModel order, MT81 mt)
        {
            mt.ModifyBy = order.ModifyBy;
            mt.DateModify = order.DateModify;
            mt.CurencyID = (int)order.Curency.Id;
            mt.VoucherDate = order.VoucherDate;
            mt.Description = order.Description;
            mt.CustomerID = order.Customer.Id;
            mt.VoucherID = (int)order.VoucherId;
            mt.CurencyID = order.Curency.Id;
            mt.ExchangeRate = order.ExchangeRate;
            mt.VoucherCode = order.VoucherCode;
            mt.VoucherNo = order.VoucherNo;
            mt.VAT_Rate = order.TaxRate;
            mt.CheckedBy = order.CheckedBy;
            mt.ApprovedBy = order.ApprovedBy;
            mt.SubmittedBy = order.SubmittedBy;
            mt.CreateBy = order.CreateBy;
            mt.DateCreate = order.DateCreate;
            mt.VoucherDate = order.VoucherDate;
            mt.Status = (byte?)order.Status;
            mt.NotePrints = order.NotePrints;
            return mt;
        }
        private void InsertToDetails(SalesModel model)
        {
            var dts = model.DetailModels;
            if (dts != null && dts.Any())
            {
                var mt = GetById(model.VoucherId);
                decimal tQty, tAmount, tTax, tTransportFee, tInlandFee, tFee1, tfee2, tFee, tToltal, tAmount0;
                tQty = tAmount = tTax = tTransportFee = tInlandFee = tFee1 = tfee2 = tFee = tToltal = tAmount0 = 0M;
                var exRate = mt.ExchangeRate.Value;
                if (exRate == 0) exRate = 1;
                int id = 1;
                foreach (var dt in dts)
                {
                    var pr = dt.VnPrice / exRate;
                    var p = Math.Round(pr, 4, MidpointRounding.AwayFromZero);
                    var am = Math.Round(dt.Quantity * p, 2, MidpointRounding.AwayFromZero);
                    var db = new DT81()
                    {
                        ProductID = dt.ProductId,
                        WarehouseID = dt.WarehouseId,
                        UOM = dt.UOM,
                        Quantity = dt.Quantity,
                        Price = p,
                        TransportFee = dt.TransportFee,
                        Fee1 = dt.Fee1,
                        Fee2 = dt.Fee2,
                        VoucherID = (int)model.VoucherId,
                        RowID = id,
                        Amount = am,
                        Description = dt.Notes,
                        VATTax = dt.VATTax,
                        Price0 = dt.Price0,
                        Amount0 = dt.Amount0,
                        VATTaxRate = dt.VATTaxRate,
                        VnPrice = dt.VnPrice

                    };
                    db.T_Fee = dt.Fee1 + dt.Fee2 + dt.TransportFee;
                    db.VATTax = am * dt.VATTaxRate;
                    db.TT = db.Amount + db.T_Fee + db.VATTax;
                    tQty += dt.Quantity;
                    tAmount += am;
                    tTax += db.VATTax.Value / 100;
                    tTransportFee += db.TransportFee ?? 0;
                    tFee1 += db.Fee1 ?? 0;
                    tfee2 += db.Fee2 ?? 0;
                    tFee += db.T_Fee ?? 0;
                    tToltal += db.TT ?? 0;
                    tAmount0 += db.Amount0 ?? 0;
                    Context.DT81s.InsertOnSubmit(db);
                    id++;
                }
                mt.T_Quantity = tQty;
                mt.T_Amount = tAmount;
                mt.T_VATTax = mt.VAT_Rate * mt.T_Amount / 100;
                mt.T_TransportFee = tTransportFee;
                mt.T_Fee = tFee;
                mt.T_Fee1 = tFee1;
                mt.T_Fee2 = tfee2;
                mt.T_Amount0 = tAmount0;
                mt.T_TT = tAmount + tTax - tFee - tAmount0;
                Commited();
            }
        }

        private SalesModel CopyToModel(MT81 order)
        {
            var model = new SalesModel
            {
                DateCreate = DateTime.Now,
                CreateBy = order.CreateBy.Value,
                Curency = order.Curency ?? new Curency(),
                VoucherDate = order.VoucherDate,
                Customer = order.Customer,
                VoucherCode = order.VoucherCode,
                VoucherId = order.VoucherID,
                Status = (VoucherStatus)order.Status,
                VoucherNo = order.VoucherNo,
                ExchangeRate = order.ExchangeRate ?? 0,
                Description = order.Description,
                Fee = order.T_Fee ?? 0,
                Fee1 = order.T_Fee1 ?? 0,
                Fee2 = order.T_Fee2 ?? 0,
                TransportFee = order.T_TransportFee ?? 0,
                Amount = order.T_Amount ?? 0,
                Quantity = order.T_Quantity ?? 0,
                Amount0 = order.T_Amount0.Value,
                SumTotal = order.T_TT ?? 0,
                VAT = order.T_VATTax ?? 0,
                TaxRate = order.VAT_Rate ?? 0,
                UserCreated = order.User ?? new User(),
                UserSubmited = order.User1 ?? new User(),
                UserChecked = order.User3 ?? new User(),
                UserApproved = order.User2 ?? new User(),
                DateSubmited = order.DateSubmited,
                DateChecked = order.DateChecked,
                DateApproved = order.DateApproved,
                DateModify = order.DateModify,
                NotePrints = order.NotePrints,
                ModifyBy = order.ModifyBy,
                SubmittedBy = order.SubmittedBy,
                CheckedBy = order.CheckedBy,
                ApprovedBy = order.ApprovedBy,
                CountryId = order.CountryID ?? 0,
                Shipment = order.Shipments ?? null

            };
            model.Shipment = order.Shipments ?? null;
            model.Profit = model.Amount + model.VAT - (model.Fee + model.Amount0);
            var dbdetails = Context.DT81s.Where(x => x.VoucherID == order.VoucherID).ToList();
            if (dbdetails.Any())
            {
                var details = dbdetails.Select(x => CopyModeDetail(x)).ToList();
                model.DetailModels = details;
                try
                {

                    var prs = details.Any()
                        ? details.Aggregate(string.Empty,
                            (current, dt) => current + dt.ProductCode + ",<br/>  ")
                        : string.Empty;
                    model.ProductView = prs;
                    model.VnAmount = details.Sum(x => x.VnAmount);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                    model.ProductView = string.Empty;
                }
            }

            return model;
        }

        private SalesDetailModel CopyModeDetail(DT81 dt)
        {

            var prod = Context.Products.FirstOrDefault(x => x.Id == dt.ProductID);
            var proName = prod != null ? prod.Code.Trim() + "-" + prod.Name.Trim() : string.Empty;

            var db = new SalesDetailModel()
            {
                ProductId = dt.ProductID ?? 0,
                ProductCode = proName,
                WarehouseId = dt.WarehouseID ?? 0,
                UOM = dt.UOM,
                Quantity = dt.Quantity ?? 0,
                Price = dt.Price ?? 0,
                TransportFee = dt.TransportFee ?? 0,
                Fee1 = dt.Fee1 ?? 0,
                Fee2 = dt.Fee2 ?? 0,
                VoucherId = dt.VoucherID,
                RowId = dt.RowID,
                Amount = dt.Amount ?? 0,
                Notes = dt.Description,
                Price0 = dt.Price0 ?? 0,
                Amount0 = dt.Amount0.Value,
                VATTax = dt.VATTax ?? 0,
                VATTaxRate = dt.VATTaxRate ?? 0,
                TT = dt.TT ?? 0,
                Product = dt.Product,
                Warehouse = dt.Warehouse,
                CurrentQty = GetQtyInventory((int)dt.VoucherID, (int)dt.ProductID.Value, dt.WarehouseID.Value),
                VnPrice = dt.VnPrice ?? 0

            };
            db.TFee = db.TransportFee + db.Fee1 + db.Fee2;
            db.VnAmount = db.Quantity * db.VnPrice;
            return db;
        }
    }
}