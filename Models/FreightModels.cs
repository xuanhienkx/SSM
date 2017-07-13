using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SSM.Utils;
namespace SSM.Models
{
    public class FreightModel
    {
        public enum FreightTypes
        {
            [StringLabel("Air Freight")]
            AirFreight,
            [StringLabel("OceanFreight")]
            OceanFreight,
            [StringLabel("Inland Rates")]
            InlandRates,
            [StringLabel("Company Tariff")]
            CompanyTariff

        }
        public long Id { get; set; }
        public String Type { get; set; }
        [Required]
        public long AirlineId { get; set; }
        public String AirlineName { get; set; }
        [Required]
        public String ValidDate { get; set; }
        public String UpdateDate { get; set; }
        public String CreateDate { get; set; }
        public long UserId { get; set; }
        public String UserName { get; set; }
        [Required]
        public long AgentId { get; set; }
        public String AgentName { get; set; }
        public string Remark { get; set; }
        [Required]
        public long DepartureId { get; set; }
        public String DepartureName { get; set; }
        [Required]
        public long DestinationId { get; set; }
        public String DestinationName { get; set; }
        public string ServiceName { get; set; }
        public long CountryNameDep { get; set; }
        public long CountryNameDes { get; set; }
        public String FileName { get; set; }
        public String SortType { get; set; }
        public String SortOder { get; set; }
        public int ServiceId { get; set; }
        public ServicesType ServicesType { get; set; }
        public static FreightModel ConvertFreight(Freight Freight1)
        {
            FreightModel FreightModel1 = new FreightModel();
            FreightModel1.Id = Freight1.Id;
            FreightModel1.AirlineId = Freight1.AirlineId.Value;
            FreightModel1.ValidDate = Freight1.ValidDate.Value.ToString("dd/MM/yyyy");
            FreightModel1.UpdateDate = Freight1.UpdateDate.Value.ToString("dd/MM/yyyy");
            FreightModel1.CreateDate = Freight1.CreateDate.Value.ToString("dd/MM/yyyy");
            FreightModel1.UserId = Freight1.UserId.Value;
            FreightModel1.AgentId = Freight1.AgentId.Value;
            FreightModel1.Remark = Freight1.Remark;
            FreightModel1.DepartureId = Freight1.DepartureId.Value;
            FreightModel1.DestinationId = Freight1.DestinationId.Value;
            FreightModel1.ServiceName = Freight1.ServiceName;
            FreightModel1.UserName = Freight1.User.FullName;
            FreightModel1.AirlineName = Freight1.CarrierAirLine.CarrierAirLineName;
            FreightModel1.AgentName = Freight1.Agent.AgentName;
            FreightModel1.DepartureName = Freight1.Area.AreaAddress;
            FreightModel1.DestinationName = Freight1.Area1.AreaAddress;
            FreightModel1.Type = Freight1.Type;
            FreightModel1.ServiceId = Freight1.ServiceId;
            FreightModel1.ServicesType = Freight1.ServicesType;
            return FreightModel1;
        }
        public static Freight ConvertFreight(FreightModel FreightModel1)
        {
            Freight Freight1 = new Freight();
            Freight1.Id = Freight1.Id;
            Freight1.AirlineId = FreightModel1.AirlineId;
            Freight1.ValidDate = DateUtils.Convert2DateTime(FreightModel1.ValidDate);
            Freight1.UpdateDate = DateUtils.Convert2DateTime(FreightModel1.UpdateDate);
            Freight1.CreateDate = DateUtils.Convert2DateTime(FreightModel1.CreateDate);
            Freight1.ServiceName = FreightModel1.ServiceName;
            Freight1.UserId = FreightModel1.UserId;
            Freight1.AgentId = FreightModel1.AgentId;
            Freight1.Remark = FreightModel1.Remark;
            Freight1.DepartureId = FreightModel1.DepartureId;
            Freight1.DestinationId = FreightModel1.DestinationId;
            Freight1.Type = FreightModel1.Type;
            Freight1.ServiceId = FreightModel1.ServiceId;
            return Freight1;
        }
        public static void ConvertFreight(FreightModel FreightModel1, Freight Freight1)
        {
            Freight1.Id = Freight1.Id;
            Freight1.AirlineId = FreightModel1.AirlineId;
            Freight1.ValidDate = DateUtils.Convert2DateTime(FreightModel1.ValidDate);
            Freight1.UpdateDate = DateUtils.Convert2DateTime(FreightModel1.UpdateDate);
            Freight1.CreateDate = DateUtils.Convert2DateTime(FreightModel1.CreateDate);
            Freight1.ServiceName = FreightModel1.ServiceName;
            Freight1.UserId = FreightModel1.UserId;
            Freight1.AgentId = FreightModel1.AgentId;
            Freight1.Remark = FreightModel1.Remark;
            Freight1.DepartureId = FreightModel1.DepartureId;
            Freight1.DestinationId = FreightModel1.DestinationId;
            Freight1.Type = FreightModel1.Type;
            Freight1.ServiceId = FreightModel1.ServiceId;
        }
        
        
   }
}