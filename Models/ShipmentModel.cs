using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace SSM.Models
{
    public class ShipmentModel
    {
        public static String ViewStatus(String status)
        {
            switch (status)
            {
                case "Pending": return " ";
                case "Submited": return "S";
                case "Approved": return "A";
                case "Locked": return "L";
                default: return " ";
            }
        }
        public enum RevenueStatusCollec
        {
            [StringLabel(" ")]
            Pending,
            [StringLabel("S")]
            Submited,
            [StringLabel("A")]
            Approved,
            [StringLabel("L")]
            Locked

        }
        public enum Bill
        {
            [StringLabel("No Bull")]
            NoBill
        }

        public enum SaleTypes
        {
            Sales,
            Handle,
            JointSales
        }
        public static String ViewServices(String status)
        {
            switch (status)
            {
                case "SeaInbound": return "Sea Inbound";
                case "SeaOutbound": return "Sea Outbound";
                case "AirInbound": return "Air Inbound";
                case "AirOutbound": return "Air Outbound";
                case "InlandService": return "Inland Service";
                case "Other": return "Other";
                default: return " ";
            }
        }

        public enum ServicesType
        {
            Sea,
            Air,
            Other
        }

        public enum Services
        {
            [StringLabel("Sea Inbound")]
            SeaInbound,
            [StringLabel("Sea Outbound")]
            SeaOutbound,
            [StringLabel("Air Inbound")]
            AirInbound,
            [StringLabel("Air Outbound")]
            AirOutbound,
            [StringLabel("Inland Service")]
            InlandService,
            [StringLabel("Other")]
            Other
        }

        public long Id { get; set; }
        public String RevenueStatus { get; set; }
        [DisplayName("Date")]
        [Required]
        public String Dateshp { get; set; }
        [DisplayName("Consignee")]
        public long CneeId { get; set; }
        public String CneeName { get; set; }
        public String CneeFullName { get; set; }
        [DisplayName("Agent")]
        public long AgentId { get; set; }
        public String AgentName { get; set; }
        public long SaleId { get; set; }
        public String SaleName { get; set; }
        [DisplayName("Shipper")]
        public long ShipperId { get; set; }
        public String ShipperName { get; set; }
        [DisplayName("Service")]
        public String ServiceName { get; set; }
        public String QtyName { get; set; }
        [DisplayName("Quantity")]
        public double QtyNumber { get; set; }
        [DisplayName("Unit")]
        public String QtyUnit { get; set; }
        [DisplayName("Sale Type")]
        public String SaleType { get; set; }
        [DisplayName("Description")]
        public String Description { get; set; }
        public long DepartmentId { get; set; }
        [DisplayName("Company")]
        public long CompanyId { get; set; }
        public String CompanyName { get; set; }
        [DisplayName("Departure")]
        public long DepartureId { get; set; }
        public String DepartureName { get; set; }
        [DisplayName("Destination")]
        public long DestinationId { get; set; }
        public String DestinationName { get; set; }
        [DisplayName("Carrier/Air Line Co-Loader")]
        public long CarrierAirId { get; set; }
        public String CarrierAirName { get; set; }
        public String HouseNum { get; set; }
        public bool HouseNumCheck { get; set; }
        public String MasterNum { get; set; }
        public bool MasterNumCheck { get; set; }
        [DisplayName("Freight Master/House")]
        public String SFreights { get; set; }
        public String LockDate { get; set; }
        public Boolean LockShipment { get; set; }
        public String CreateDate { get; set; }
        public String UpdateDate { get; set; }
        public String ApproveDate { get; set; }
        public long CountryDeparture { get; set; }
        public long CountryDestination { get; set; }
        public bool isDelivered { get; set; }
        public String DeliveredDate { get; set; }
        public MT81 Order { get; set; }
        public long? VoucherId { get; set; }
        public bool IsTrading { get; set; }
        [DisplayName("Service")]
        public int ServiceId { get; set; }
        [DisplayName("Users Control")]
        public IList<long> UserListInControl { get; set; }
        public Models.ServicesType TypeServices { get; set; }
        public bool IsMainControl { get; set; }
        public long? ShipmentRef { get; set; }
        public string UserListInControlDb { get; set; }
        public bool IsControl { get; set; }
        public int? ControlStep { get; set; }

    }
}