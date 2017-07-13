using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SSM.Models
{
    public class AgentModel
    {
        public long Id { get; set; }
        [Required]
        [DisplayName("Agent Name")]
        public string AgentName { get; set; }
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }
        [Required]
        [DisplayName("AbbName")]
        public string AbbName { get; set; }
        [DisplayName("Country")]
        public string CountryName { get; set; }

        public string GroupType { get; set; }
        public User User { get; set; }
        public bool IsActive { get; set; }
        public static void ConverModel(Agent Agent1, AgentModel Model1)
        {
            Model1.Id = Agent1.Id;
            Model1.AbbName = Agent1.AbbName;
            Model1.Address = Agent1.Address;
            Model1.PhoneNumber = Agent1.PhoneNumber;
            Model1.Description = Agent1.Description;
            Model1.CountryName = Agent1.CountryName;
            Model1.Email = Agent1.Email;
            Model1.AgentName = Agent1.AgentName;
            Model1.GroupType = Agent1.GroupType;
            Model1.User = Agent1.User;
            Model1.IsActive = Agent1.IsActive;
        }
        public static void ConvertModel(AgentModel Model1, Agent Agent1)
        {
            Agent1.Id = Model1.Id;
            Agent1.AbbName = Model1.AbbName;
            Agent1.Address = Model1.Address;
            Agent1.PhoneNumber = Model1.PhoneNumber;
            Agent1.Description = Model1.Description;
            Agent1.CountryName = Model1.CountryName;
            Agent1.Email = Model1.Email;
            Agent1.AgentName = Model1.AgentName;
            Agent1.GroupType = Model1.GroupType;
            Agent1.IsActive = Model1.IsActive;
            if (Model1.User != null)
                Agent1.UserId = Model1.User.Id;
        }
    }
}