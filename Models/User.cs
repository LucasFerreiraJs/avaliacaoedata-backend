namespace api.Models
{
    public class User
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string UserLogin { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public Guid RoleID { get; set; }
       
        public int RhCode { get; set; }

        public Region Region { get; set; }
        public int RegionID { get; set; }
        
        public int Status { get; set; }
        public DateTime DateAction { get; set; }
        public Guid UserAction { get; set; }
        public DateTime LastAccess { get; set; }

    }
}
