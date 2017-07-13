using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using AutoMapper;
using SSM.Models;
using SSM.Models.CRM;

namespace SSM.DomainProfiles
{
    public class DataEntryProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Agent, AgentModel>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User ?? null));
            Mapper.CreateMap<Customer, CustomerModel>();
            Mapper.CreateMap<Area, AreaModel>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country ?? null))
                .ForMember(dest => dest.IsTrading, opt => opt.MapFrom(src => src.trading_yn ?? false));
            Mapper.CreateMap<Country, CountryModel>();
            Mapper.CreateMap<Unit, UnitModel>();
            Mapper.CreateMap<User, UsersModel>();
            Mapper.CreateMap<CRMGroup, CRMBaseModel>()
                .ForMember(dest => dest.ModelType, opt => opt.MapFrom(src => ModelType.CRMGroup));
            Mapper.CreateMap<CRMEventType, CRMBaseModel>()
                .ForMember(dest => dest.ModelType, opt => opt.MapFrom(src => ModelType.CRMEventType));
            Mapper.CreateMap<CRMJobCategory, CRMBaseModel>()
                .ForMember(dest => dest.ModelType, opt => opt.MapFrom(src => ModelType.CRMJobCategory));
            Mapper.CreateMap<CRMStatus, CRMBaseModel>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => (CRMStatusCode)src.Code))
                .ForMember(dest => dest.ModelType, opt => opt.MapFrom(src => ModelType.CRMStatus));
            Mapper.CreateMap<CRMSource, CRMBaseModel>()
                .ForMember(dest => dest.ModelType, opt => opt.MapFrom(src => ModelType.CRMSource));
            Mapper.CreateMap<CRMSchedule, CRMScheduleModel>()
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.DayOfWeek) ? new List<int>() : src.DayOfWeek.Split(',').Select(int.Parse)));
            Mapper.CreateMap<CRMContact, CRMContactModel>()
                .ForMember(dest => dest.CRMCustomer, opt => opt.MapFrom(src => src.CRMCustomer ?? null));
            Mapper.CreateMap<CRMCustomer, CRMCustomerModel>()
                 .ForMember(dest => dest.DataType, opt => opt.MapFrom(src => (CRMDataType)src.CrmDataType))
                 .ForMember(dest => dest.CustomerType, opt => opt.MapFrom(src => (CustomerType)src.CustomerType));
            Mapper.CreateMap<CRMPriceQuotation, CRMPriceQuotationModel>() 
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.User ?? null))
                .ForMember(dest => dest.CRMCustomer, opt => opt.MapFrom(src => src.CRMCustomer ?? null))
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => src.User1 ?? null));
            Mapper.CreateMap<CRMEmailHistory, CRMEmailHistoryModel>()
                .ForMember(dest => dest.PriceQuotation, opt => opt.MapFrom(src => src.CRMPriceQuotation ?? null));
            Mapper.CreateMap<CRMVisit, CRMEventModel>()
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.DayWeekOfRemider) ? new List<int>() : src.DayWeekOfRemider.Split(',').Select(int.Parse)))
                .ForMember(dest => dest.CRMCustomer, opt => opt.MapFrom(src => src.CRMCustomer ?? null))
                .ForMember(dest => dest.CRMEventType, opt => opt.MapFrom(src => src.CRMEventType ?? null)) 
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (CRMEventStatus)src.Status))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.User ?? null))
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => src.User1 ?? null));
            Mapper.CreateMap<CRMCusDocument, CrmCusDocumentModel>()
                .ForMember(dest => dest.CRMCustomer, opt => opt.MapFrom(src => src.CRMCustomer ?? null));
            Mapper.CreateMap<CRMPlanProgram, CRMBaseModel>()
                .ForMember(dest => dest.ModelType, opt => opt.MapFrom(src => ModelType.CRMPlanProgram));
            Mapper.CreateMap<CRMPlanProgram, CRMPlanProgramModel>();
            Mapper.CreateMap<CRMPLanSale, CRMPLanSaleModel>()
                .ForMember(dest => dest.IsApproval, opt => opt.MapFrom(src => src.ApprovedDate != null && src.ApprovedById != null))
                .ForMember(dest => dest.IsSubmited, opt => opt.MapFrom(src => src.SubmitedDate != null && src.SubmitedById != null));
            Mapper.CreateMap<CRMPlanMonth, CRMPlanMonthModel>();
            Mapper.CreateMap<CRMPlanProgMonth, CRMPlanProgMonthModel>();
            Mapper.CreateMap<CRMPriceStatus, CRMBaseModel>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => (CRMStatusCode)src.Code))
                .ForMember(dest => dest.ModelType, opt => opt.MapFrom(src => ModelType.CRMPriceStatus));
            Mapper.CreateMap<CRMFollowCusUser, CRMFollowCusUserModel>();
            Mapper.CreateMap<CRMFollowEventUser, CRMFollowEventUserModel>();
            Mapper.CreateMap<CarrierAirLine, CarrierModel>();
        }
    }
}