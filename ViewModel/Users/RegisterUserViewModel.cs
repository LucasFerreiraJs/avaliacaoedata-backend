using api.Models;

namespace api.ViewModel.Users
{
    public class RegisterUserViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string UserLogin { get; set; }
        public string Password { get; set; }
        public string Role { get; set;}
        //public string RoleName { get; set;}
        public Guid RoleId { get; set;}
        public int RhCode { get; set; }
        public int Region { get; set; }
        //public Guid RegionId { get; set; }
        //public string RegionName { get; set; }
        public int Status { get; set; }
        public DateTime DateAction { get; set; }
        //public Guid UserAction { get; set; }
        public DateTime LastAccess { get; set; }

    }
}
