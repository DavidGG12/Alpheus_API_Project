using System.ComponentModel.DataAnnotations;

namespace Alpheus_API.Models.Requests.UserRequests.Put
{
    public class rq_Put_InfoUser_Model
    {
        [Required]
        public string IDUser { get; set; }
        [Required]
        public string NameOfUser { get; set; }
        [Required]
        public string LastNameUser1 { get; set; }
        public string? LastNameUser2 { get; set; }
        public string? BirthdayUser { get; set; }
    }
}
