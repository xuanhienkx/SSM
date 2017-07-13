using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSM.Models.CRM
{
    public class CRMBaseModel
    {
        public virtual int Id { get; set; }
        [Required]
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public ModelType ModelType { get; set; }
        public int? ParentId { get; set; }
        public bool IsSystem { get; set; }
        public CRMStatusCode Code { get; set; }
        public BaseModelTypeName ModelTypeName { get; set; }
    }
    public enum BaseModelTypeName
    {
        SOURCE,
        STATUS,
        GROUP,
        JOBCATEGORY,
        EVENTTYPE,
        PLANPROGRAM,
        PRICESTATUS
    }

    public enum ModelType
    {
        CRMSource,
        CRMStatus,
        CRMGroup,
        CRMJobCategory,
        CRMEventType,
        UserModel,
        CRMPlanProgram,
        CRMPriceStatus,
    }
    public enum CRMDataType
    {
        All = 0,
        SsmCustomer = 1,
        CRMNew = 2
    }

    public enum CRMStatusCode
    {
        All = 0,
        Potential = 1, 
        Success = 3,
        Client = 4,
        Orther = 9,
    }
}