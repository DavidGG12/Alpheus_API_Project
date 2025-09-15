using System.ComponentModel.DataAnnotations;

namespace Alpheus_API.Models.Users
{
    public class Tbl_Alpheus_Role_Model
    {
        public string IDRol { get; set; }
        public string NmRol { get; set; }
        public string StRol { get; set; }
        [DisplayFormat(DataFormatString = "dd/MM/yyyy HH:mm:ss")]
        public string UpDate { get; set; }
    }
}
