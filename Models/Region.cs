using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api.Models
{
    public class Region
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegionID { get; set; }
        public string Name { get; set; }


        [JsonIgnore]
        public List<User> Users { get; set; }
    
    }
}
