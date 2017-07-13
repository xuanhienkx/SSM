using System;
using System.Collections.Generic;
using System.Linq;
using SSM.Common;
using SSM.Models;
using SSM.ViewModels.Reports;

namespace SSM.Services
{
    public interface IRevenueServices : IServices<Revenue>
    {
        List<ProfitReportModel> ProfitReportModels(FilterProfit filter);
        List<Shipment> GetAllShipments(string id, FilterProfit filter);
    }
    public class RevenueServices : Services<Revenue>, IRevenueServices
    {
        public List<ProfitReportModel> ProfitReportModels(FilterProfit filter)
        {
            IQueryable<ProfitReportModel> list;
            var qr =
                GetQuery(
                    x => x.Shipment.DateShp.Value >= filter.BeginDate &&
                         x.Shipment.DateShp.Value <= filter.EndDate &&
                         x.IsControl == false &&
                         x.Earning.Value > 0);
            switch (filter.TypeFilterProfit)
            {
                case TypeFilterProfit.Agent:
                    list =
                        qr.GroupBy(x => new { Id = x.Shipment.Agent.Id, Name = x.Shipment.Agent.AbbName })
                            .Select(x => new ProfitReportModel
                            {
                                Id = x.Key.Id,
                                Name = x.Key.Name,
                                TotalShipment = x.Count(),
                                Profit = x.Sum(g => g.Earning.Value)
                            });
                    break;
                case TypeFilterProfit.Cnee:
                    list =
                        qr.GroupBy(x => new { Id = x.Shipment.Customer.Id, Name = x.Shipment.Customer.CompanyName })
                            .Select(x => new ProfitReportModel
                            {
                                Id = x.Key.Id,
                                Name = x.Key.Name,
                                TotalShipment = x.Count(),
                                Profit = x.Sum(g => g.Earning.Value)
                            });
                    break;
                case TypeFilterProfit.Shipper:
                    list = qr.GroupBy(x => new {Id = x.Shipment.Customer1.Id, Name = x.Shipment.Customer1.CompanyName})
                        .Select(x => new ProfitReportModel
                        {
                            Id = x.Key.Id,
                            Name = x.Key.Name,
                            TotalShipment = x.Count(),
                            Profit = x.Sum(g => g.Earning.Value)
                        });

                    break;
                case TypeFilterProfit.Department:
                    list =
                        qr.GroupBy(x => new { Id = x.Shipment.User.Department.Id, Name = x.Shipment.User.Department.DeptName })
                            .Select(x => new ProfitReportModel
                            {
                                Id = x.Key.Id,
                                Name = x.Key.Name,
                                TotalShipment = x.Count(),
                                Profit = x.Sum(g => g.Earning.Value)
                            });
                    break;

                case TypeFilterProfit.Servics:
                    list =
                        qr.GroupBy(x => new { Id = x.Shipment.ServicesType.Id, Name = x.Shipment.ServicesType.Name })
                            .Select(x => new ProfitReportModel
                            {
                                Id = x.Key.Id,
                                Name = x.Key.Name,
                                TotalShipment = x.Count(),
                                Profit = x.Sum(g => g.Earning.Value)
                            });
                    break;
                case TypeFilterProfit.SalesType:
                    list =
                        qr.GroupBy(x => x.SaleType)
                            .Select(x => new ProfitReportModel
                            {
                                Id = x.Key,
                                Name = x.Key,
                                TotalShipment = x.Count(),
                                Profit = x.Sum(g => g.Earning.Value)
                            });
                    break;
                case TypeFilterProfit.Sales:
                default:
                    list = qr.GroupBy(x => new { Id = x.Shipment.User.Id, Name = x.Shipment.User.FullName })
                            .Select(x => new ProfitReportModel
                            {
                                Id = x.Key.Id,
                                Name = x.Key.Name,
                                TotalShipment = x.Count(),
                                Profit = x.Sum(g => g.Earning.Value)
                            });
                    break;
            }
            list = list.OrderByDescending(x => x.Profit);
            if (filter.Top > 0)
            {
                list = list.Take(filter.Top);
            }
            return list.ToList();
        }

        public List<Shipment> GetAllShipments(string id, FilterProfit filter)
        {
            var qr = Context.Shipments.Where(
                    x => x.DateShp.Value >= filter.BeginDate &&
                         x.DateShp.Value <= filter.EndDate &&
                         x.IsMainShipment == false &&
                         x.Revenue.Earning.Value > 0);
            long refId = 0;
            long idRef = 0;
            if (long.TryParse(id, out idRef))
            {
                refId = idRef;
            }
            switch (filter.TypeFilterProfit)
            {
                case TypeFilterProfit.Agent:

                    qr = qr.Where(x => x.AgentId.Value == refId);
                    break;
                case TypeFilterProfit.Cnee:
                    qr = qr.Where(x => x.CneeId.Value == refId);
                    break;
                case TypeFilterProfit.Shipper:
                    qr = qr.Where(x => x.ShipperId == refId);
                    break;
                case TypeFilterProfit.Department:
                    qr = qr.Where(x => x.User.DeptId == refId);
                    break;

                case TypeFilterProfit.Servics:
                    qr = qr.Where(x => x.ServiceId == refId);
                    break;
                case TypeFilterProfit.SalesType:
                    qr = qr.Where(x => x.SaleType == (string)id);
                    break;
                case TypeFilterProfit.Sales:
                default:
                    qr = qr.Where(x => x.SaleId == refId);
                    break;
            }
            var list = qr.ToList();
            return list;
        }
    }
}