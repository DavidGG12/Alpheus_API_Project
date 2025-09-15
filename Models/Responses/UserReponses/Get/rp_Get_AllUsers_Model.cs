using Alpheus_API.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace Alpheus_API.Models.Responses.UserReponses.Get
{
    public class rp_Get_AllUsers_Model
    {
        [Required]
        public rp_Alpheus_API_General_Model Info { get; set; }
        [System.Text.Json.Serialization.JsonIgnore(Condition =
        System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)]
        public List<Tbl_Alpheus_User_Model>? Users { get; set; }
    }
}
