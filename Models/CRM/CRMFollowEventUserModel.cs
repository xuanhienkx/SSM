namespace SSM.Models.CRM
{
    public class CRMFollowEventUserModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long VisitId { get; set; }
        public long AddById { get; set; }
        public long LockById { get; set; }
        public bool IsLook { get; set; }
        public CRMVisit CRMVisit { get; set; }
        public User User { get; set; } 
        public User User1 { get; set; } 
    }
    public class CRMFollowCusUserModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long AddById { get; set; }
        public long LockById { get; set; }
        public long CrmId { get; set; }
        public bool IsLook { get; set; }
        public CRMCustomer CRMCustomer { get; set; }
        public User User { get; set; }
    }
}