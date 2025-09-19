using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Alpheus_API.Models.Requests.UserRequests.Put
{
    public class rq_Put_User_Model
    {
        [Required]
        public string IDUser { get; set; }
        [Required]
        public string NameUser { get; set; }
        [Required]
        [PasswordPropertyText]
        public string PasswordUser { get; set; }
        [Required]
        [EmailAddress]
        public string EmailUser { get; set; }
        [Required]
        public string ValidateEmail { get; set; }
        [Required]
        public string IDInfoUser { get; set; }
        [Required]
        public string IDRole { get; set; }
        [Required]
        public string StateUser { get; set; }
    }
}
