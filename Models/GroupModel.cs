using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace SSM.Models
{
    public class GroupModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Discription { get; set; }
        public bool IsActive { get; set; }
        public IList<UserGroup> UserGroups { get; set; }
        public long[] ListUserAccesses { get; set; } 

    }
}