using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SSM.Models
{
    public enum NewType : byte
    {
        Infomation,//REGULATION
        News
    }
    public enum NewCategory : byte
    {
        Info,//REGULATION
        News
    }
    public class NewsModel
    {
        public int Id { get; set; }
        [Required]
        public string Header { get; set; }
        [AllowHtml]
        [Required]
        public string Contents { get; set; }

        public NewType Type { get; set; }

        public string Sourse { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateModify { get; set; }
        [Display(Name = " ALLOW ANOTHER PERSON UP-DATE")]
        public bool IsAllowAnotherUpdate { get; set; }
        public int[] ListUserAccesses { get; set; }
        public long[] ListUserUpdate { get; set; }
        public IList<GroupAccessPermission> NewAccessPermissions { get; set; }

        public IList<ServerFile> FilesList { get; set; }
        public bool IsApproved{ get; set; }
        public int CateloryId { get; set; }
        public Catelory Catelory { get; set; }

        public User CreaterBy { get; set; }

        public User ModifiedBy { get; set; }
        public string RefDoc { get; set; }
        public User ApprovedBy { get; set; }
        public DateTime? DateApproved { get; set; }
        public DateTime? DatePromulgate { get; set; }
        public string UsersCanUpdate { get; set; }
        public string UserView { get; set; }
        public bool IsViewed { get; set; }

    }

    public class NewSearchModel
    {
        public string Keyworks { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
          [Display(Name = "Group")]
        public int GroupId { get; set; }
        [Display(Name = "Pending")]
        public bool IsPending { get; set; }
        public SSM.Services.SortField SortField { get; set; }

    }
}