using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alpheus_API.Models.Users
{
    public class Tbl_Alpheus_User_Model
    {
        public string IDUser { get; set; }
        public string NmUser { get; set; }
        [DataType(DataType.Password)]
        public string PwdUser { get; set; }
        public string EmailUser { get; set; }
        public string ValidatedEmail { get; set; }
        public string InfoUserID { get; set; }
        public string RolID { get; set; }
        public string StUser { get; set; }
        [DisplayFormat(DataFormatString = "dd/MM/yyyy HH:mm:ss")]
        public string UpDate { get; set; }
    }
}
