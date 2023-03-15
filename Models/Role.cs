using System.Text.Json.Serialization;

namespace api.Models
{
    public class Role
    {
        public Guid RoleID { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public List<User> Users { get; set; }
    }
}
