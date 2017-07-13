using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SSM.Models.CRM
{
    public class CrmCusDocumentModel
    {
        public long Id { get; set; }
        [Required]
        public string DocName { get; set; } 
        public string Description { get; set; }
        
        public string LinkDoc { get; set; }

        public DateTime ModifiedDate { get; set; }
        public long CrmCusId { get; set; }
        public long CreatedById { get; set; }
        public long? ModifiedById { get; set; }
        public User Sales { get; set; }

        public CRMCustomer CRMCustomer { get; set; }
        public List<HttpPostedFileBase> Uploads { get; set; }
        public IList<ServerFile> FilesList { get; set; }
    }
}