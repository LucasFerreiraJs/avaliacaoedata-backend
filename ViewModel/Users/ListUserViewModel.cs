
using api.Models;

namespace api.ViewModel.Users
{
    public class ListUserViewModel
    {

        public Guid ID { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string UserLogin { get; set; }
        public Role Role { get; set; }
        public Region Region { get; set; }
        public int Status { get; set; }
        public DateTime DateAction { get; set; }
        public DateTime LastAccess { get; set; }


    }
}
