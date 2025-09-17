using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Alpheus_API.Models.Requests.UserRequests.Post
{
    public class rq_Post_RegisterInfoUser_Model
    {
        [Required]
        public string NameOfUser { get; set; }
        [Required]
        public string LastNameUser1 { get; set; }
        public string? LastNameUser2 { get; set; }
        public string? BirthdayUser { get; set; }
    }
}
