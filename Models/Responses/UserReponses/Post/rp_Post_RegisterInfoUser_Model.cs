using System.ComponentModel.DataAnnotations;

namespace Alpheus_API.Models.Responses.UserReponses.Post
{
    public class rp_Post_RegisterInfoUser_Model
    {
        [Required]
        public rp_Alpheus_API_General_Model Info { get; set; }
        [System.Text.Json.Serialization.JsonIgnore(Condition =
        System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)]
        public string? IDUser { get; set; }
    }
}
