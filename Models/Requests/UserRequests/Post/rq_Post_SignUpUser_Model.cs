using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Alpheus_API.Models.Requests.UserRequests.Post
{
    public class rq_Post_SignUpUser_Model
    {
        [Required]
        public string NameUser { get; set; }
        [Required]
        [PasswordPropertyText]
        public string PasswordUser { get; set; }
        [Required]
        [EmailAddress]
        public string EmailUser { get; set; }
        [Required]
        public string IDRole { get; set; }
    }
}
