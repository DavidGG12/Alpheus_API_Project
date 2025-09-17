using System.ComponentModel.DataAnnotations;

namespace Alpheus_API.Models.Requests.UserRequests.Put
{
    public class rq_Put_Rol_Model
    {
        [Required]
        public string IDRol { get; set; }
        [Required]
        public string NameRol { get; set; }
        [Required]
        public string StateRol { get; set; }
    }
}
