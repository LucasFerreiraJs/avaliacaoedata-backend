namespace api.ViewModel.Users
{
    public class SuccessRegisterUserViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string UserLogin { get; set; }
        public string RoleName { get; set; }
        public string RegionName { get; set; }
        public int Status { get; set; }
        public DateTime LastAccess { get; set; }
    }
}
