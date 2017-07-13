using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using SSM.Models;
using SSM.Models.CRM;

namespace SSM.ViewModels
{
    public class EmailModel
    {
        [Required(ErrorMessage = "{0} không được để trống!")]
        [Display(Name = "Tiêu đề mail")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "{0} không được để trống!")]
        [Display(Name = "Nội dung")]
        public string Message { get; set; }
        [Display(Name = "File đính kèm")]
        public List<HttpPostedFileBase> Uploads { get; set; }
        [Required(ErrorMessage = "{0} không được để trống!")]
        [Display(Name = "Gửi tới")]
        public string EmailTo { get; set; }
        public string EmailCc { get; set; }
        public User User { get; set; }
        public long IdRef { get; set; }
        public bool IsUserSend { get; set; }
    }

    public enum NotificationType
    {
        Success,
        Error,
        Warning
    }
}