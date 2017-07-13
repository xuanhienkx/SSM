using System;
using System.Collections.Generic;
using System.Linq;
using SSM.Common;

using SSM.Models;
using SSM.ViewModels;

namespace SSM.Services
{
    public interface IStockCardService : IServices<StockCard>
    {
        IEnumerable<IssueVoucherModel> GetList(IssueVoucherModel filter, int page, int pageSize, out int totalRow);
        IEnumerable<IssueVoucherModel> GetListInventory(IssueVoucherModel filter, int page, int pageSize, out int totalRow);
        List<MonthYear> GetReportList(IssueVoucherModel filter);
        string CaculateCost(CalculateCostViewModel model);
        string CaculateCostToNextYear(int year);

    }
    public class StockCardService : Services<StockCard>, IStockCardService
    {
        public IEnumerable<IssueVoucherModel> GetList(IssueVoucherModel filter, int page, int pageSize, out int totalRow)
        {
            var skip = (page - 1) * pageSize;
            var query = GetQueryFilter(filter);
            if (filter.StockIn && filter.StockOut == false)
            {
                query = query.Where(x => x.nxt.Value == true);
            }
            if (filter.StockIn == false && filter.StockOut == true)
            {
                query = query.Where(x => x.nxt.Value == false);
            }

            //  var stockCards = query.ToList();
            // var db = stockCards.Select(x => CopyToModel(x));

            var proQry = query.GroupBy(x => x.Product);
            totalRow = proQry.Count();
            var prodts = proQry.Skip(skip).Take(pageSize).ToList();
            List<IssueVoucherModel> voucherModels = new List<IssueVoucherModel>();
            var count = 0;
            var inventorylist = GetListInventory(filter, page, pageSize, out count).Where(x => string.IsNullOrEmpty(x.VoucherNo)).ToList();
            foreach (var prodt in prodts)
            {
                var product = prodt.Key;
                var vInventotry = 0M;
                var producInventory = inventorylist.FirstOrDefault(x => x.Product == product);
                if (producInventory != null)
                {
                    vInventotry = producInventory.Amount;
                }
                if (filter.TopRowDetail > 0 && voucherModels.Any())
                {
                    var produtsG = prodt.Select(x => CopyToModel(x));
                    voucherModels.AddRange(produtsG.Where(x => x.Product.Id == product.Id).Take(filter.TopRowDetail));

                }
                else
                {
                    var produtsG = prodt.Select(x => CopyToModel(x));
                    voucherModels.AddRange(produtsG);
                }


                var total = new IssueVoucherModel()
                {
                    Product = product,
                    Quantity = prodt.Sum(s => s.sl_nhap ?? 0),
                    QuantityOut = prodt.Sum(s => s.sl_xuat ?? 0),
                    Amount = prodt.Sum(s => s.TT ?? 0),
                    Amount0 = prodt.Sum(s => s.tien_xuat ?? 0),
                    AmountOut = prodt.Sum(s => s.tien2 ?? 0),
                    AmountInventory = vInventotry,
                    Supplier = new Supplier(),
                    Warehouse = new Warehouse(),
                    VoucherDate = DateTime.MaxValue,
                    Customer = new Customer(),

                };

                voucherModels.Add(total);
            }
            return voucherModels.OrderBy(x => x.Product.Name).ThenByDescending(x => x.VoucherDate).ThenBy(x => x.VoucherNo);
        }

        public IEnumerable<IssueVoucherModel> GetListInventory(IssueVoucherModel filter, int page, int pageSize, out int totalRow)
        {
            var query = GetQueryFilter(filter);
            var queryIn = query.Where(x => x.nxt.Value == true);
            var queryOut = query.Where(x => x.nxt.Value == false);
            var skip = (page - 1) * pageSize;
            var proQry = queryIn.GroupBy(x => new
            {
                Product = x.Product,
                Warehouse = x.Warehouse,

            });
            totalRow = proQry.Count();
            var stockByProducts = proQry.Skip(skip).Take(pageSize).ToList();
            List<IssueVoucherModel> voucherModels = new List<IssueVoucherModel>();
            foreach (var stocks in stockByProducts)
            {
                Product pr = stocks.Key.Product;
                Warehouse wh = stocks.Key.Warehouse;
                var proQty = 0M;
                var proValue = 0M;
                var inputs = stocks.Where(x => x.nxt.Value == true && x.ProductID == pr.Id && x.WarehouseID == wh.Id).OrderBy(x => x.VoucherNo).ToList();
                var inputQty = (decimal)inputs.Sum(x => x.sl_nhap);
                var outs = queryOut.Where(x => x.ProductID == pr.Id && x.WarehouseID == wh.Id);
                var pendingout = Context.DT81s.Where(x => x.ProductID.Value == pr.Id && x.WarehouseID== wh.Id && x.MT81.Status == (byte)VoucherStatus.Pending);
                var qtyPendingOut = 0M;
                if (pendingout.Any())
                    qtyPendingOut = pendingout.Sum(x => x.Quantity.Value);
                decimal outputs = 0M;
                if (outs.Any())
                    outputs = (decimal)outs.Sum(x => x.sl_xuat);
                var total = new IssueVoucherModel()
                {
                    Product = pr,
                    Supplier = new Supplier(),
                    Warehouse = new Warehouse(),
                    VoucherDate = DateTime.MaxValue,
                    Customer = new Customer(),
                    QuantityPendingOut = qtyPendingOut
                };
                if (inputQty < outputs)
                {
                    var ip = CopyToModel(inputs.Last());
                    var qty = inputQty - outputs;
                    total.Quantity = qty;
                    ip.Quantity = qty;
                    ip.Amount = qty * ip.Price;
                    total.Amount = qty * ip.Price;
                    voucherModels.Add(ip);
                    voucherModels.Add(total);
                }
                else
                {
                    foreach (var input in inputs)
                    {
                        var qty = input.sl_nhap ?? 0;
                        outputs = outputs - qty;
                        if (outputs < 0)
                        {
                            qty = Math.Abs(outputs);
                            outputs = 0;
                        }
                        else if (outputs >= 0)
                        {
                            qty = 0;
                        }

                        proQty += qty;
                        proValue += qty * input.PriceReceive ?? 0;
                        var inputModel = new IssueVoucherModel()
                        {
                            VoucherNo = input.VoucherNo,
                            VoucherDate = input.VoucherDate ?? new DateTime(),
                            Quantity = qty,
                            QuantityOut = 0M,
                            Product = input.Product,
                            Warehouse = input.Warehouse,
                            Supplier = input.Supplier,
                            Customer = input.Customer,
                            Price = input.PriceReceive ?? 0,
                            PriceOut = 0,
                            Amount = qty * input.PriceReceive ?? 0,
                            Amount0 = 0,
                            AmountOut = 0,
                            StockIn = true,
                            StockOut = false,

                        };
                        voucherModels.Add(inputModel);
                    }

                    total.Quantity = proQty;
                    total.Amount = proValue;
                    voucherModels.Add(total);
                }
            }
            var list = voucherModels.Where(x => x.Quantity != 0);
            var newTotal = list.GroupBy(x => x.Product).Count();

            if (pageSize > newTotal)
            {
                totalRow = newTotal;
            }
            var listSum =
                voucherModels.Where(x => string.IsNullOrEmpty(x.VoucherNo))
                    .GroupBy(x => x.Product)
                    .Select(x => new IssueVoucherModel()
                    {
                        Product = x.Key,
                        Supplier = new Supplier(),
                        Warehouse = new Warehouse(),
                        VoucherDate = DateTime.MaxValue,
                        Customer = new Customer(),
                        QuantityPendingOut = (decimal)x.Sum(p => p.QuantityPendingOut),
                        Quantity = (decimal)x.Sum(p => p.Quantity),
                        Amount = (decimal)x.Sum(p => p.Amount),
                    }).ToList();
            var listItem = voucherModels.Where(x => !string.IsNullOrEmpty(x.VoucherNo)).ToList();
            var viewlist = new List<IssueVoucherModel>();
            viewlist.AddRange(listItem);
            viewlist.AddRange(listSum);

            return viewlist.Where(x => x.Quantity != 0).OrderBy(x => x.Product.Name).ThenByDescending(x => x.VoucherDate).ThenBy(x => x.VoucherNo).ToList();
        }

        public List<MonthYear> GetReportList(IssueVoucherModel filter)
        {
            var query = GetQueryFilter(filter);
            var queryForMount = query.Where(x => x.VoucherDate.Value.Year == filter.Year)
                .GroupBy(x => x.VoucherDate.Value.Month).Select(x => new MonthYear()
                {
                    Month = x.Key,
                    Qty = x.Sum(s => s.sl_nhap ?? 0),
                    QtyOut = x.Sum(s => s.sl_xuat ?? 0),
                    AmmountIn = x.Sum(s => s.TT ?? 0),
                    AmmountOut = x.Sum(s => s.tien2 ?? 0),
                    Ammount0 = x.Sum(s => s.tien_xuat ?? 0),

                }).OrderBy(x => x.Month);

            var preYear = query.Where(x => x.VoucherDate.Value.Year >= (filter.Year - 1) && x.VoucherDate.Value.Year <= filter.Year);
            var summaryPreYear = preYear.GroupBy(x => x.VoucherDate.Value.Year).Select(x => new MonthYear()
            {
                Month = x.Key,
                Qty = x.Sum(s => s.sl_nhap ?? 0),
                QtyOut = x.Sum(s => s.sl_xuat ?? 0),
                AmmountIn = x.Sum(s => s.TT ?? 0),
                AmmountOut = x.Sum(s => s.tien2 ?? 0),
                Ammount0 = x.Sum(s => s.tien_xuat ?? 0),
            });
            var result = queryForMount.Union(summaryPreYear).ToList();
            return result;
        }

        public string CaculateCost(CalculateCostViewModel model)
        {
            try
            {
                Context.ExecuteCommand(string.Format("exec sp15_CalcFIFOPrice @nMF={0}, @nMT={1}, @nYear={2}", model.FromMonth, model.ToMonth, model.Year));
                return string.Format("<span class='successfuly {0}'>Caculate code successfully</span>", 0);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return string.Format("<span class='error'> {0}</span>", ex.Message);
            }
        }

        public string CaculateCostToNextYear(int year)
        {
            try
            {
                Context.ExecuteCommand(string.Format("exec sp16_ConvertItems2NextYear @nYear={0}", year));
                return string.Format("<span class='successfuly {0}'>Caculate cost to year {1} successfully</span>", 0, year + 1);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return string.Format("<span class='error'> {0}</span>", ex.Message);
            }
        }


        private IQueryable<StockCard> GetQueryFilter(IssueVoucherModel filter)
        {
            filter = filter ?? new IssueVoucherModel();
            filter.Product = filter.Product ?? new Product();
            filter.Supplier = filter.Supplier ?? new Supplier();
            filter.Warehouse = filter.Warehouse ?? new Warehouse();
            filter.Customer = filter.Customer ?? new Customer();

            var query = GetQuery(x =>
                x.ProductID.HasValue && x.WarehouseID.HasValue
                && (string.IsNullOrEmpty(filter.Supplier.FullName) || (x.Supplier.FullName != null && x.Supplier.FullName.Contains(filter.Supplier.FullName)))
                && (string.IsNullOrEmpty(filter.Customer.FullName) || (x.Customer.FullName != null && x.Customer.FullName.Contains(filter.Customer.FullName)))
                && (string.IsNullOrEmpty(filter.Product.Name) || x.Product.Name.Contains(filter.Product.Name))
                && (string.IsNullOrEmpty(filter.Product.Code) || x.Product.Code.Contains(filter.Product.Code))
                && (filter.Warehouse.Id == 0 || x.WarehouseID == filter.Warehouse.Id)
                );


            if (filter.FromDate.HasValue)
            {
                query = query.Where(x => x.VoucherDate >= filter.FromDate.Value.Date);
            }
            if (filter.ToDate.HasValue)
            {
                query = query.Where(x => x.VoucherDate <= filter.ToDate.Value.Date);
            }

            return query;
        }
        private IssueVoucherModel CopyToModel(StockCard db)
        {
            var mode = new IssueVoucherModel()
            {
                VoucherNo = db.VoucherNo,
                VoucherDate = db.VoucherDate ?? new DateTime(),
                Quantity = db.sl_nhap ?? 0M,
                QuantityOut = db.sl_xuat ?? 0M,
                Product = db.Product,
                Warehouse = db.Warehouse,
                Supplier = db.Supplier,
                Customer = db.Customer,
                Price = db.PriceReceive ?? 0,
                PriceOut = db.gia2 ?? 0,
                Amount = db.TT ?? 0,
                Amount0 = db.tien_xuat ?? 0,
                AmountOut = db.tien2 ?? 0,
                StockIn = db.VoucherNo.StartsWith("NK") ? true : false,
                StockOut = db.VoucherNo.StartsWith("XK") ? true : false,

            };
            return mode;
        }
    }
}