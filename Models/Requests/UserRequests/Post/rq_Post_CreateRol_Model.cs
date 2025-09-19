using System.ComponentModel.DataAnnotations;

namespace Alpheus_API.Models.Requests.UserRequests.Post
{
    public class rq_Post_CreateRol_Model
    {
        [Required]
        public string RoleName { get; set; }
    }
}
