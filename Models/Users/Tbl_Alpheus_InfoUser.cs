using System.ComponentModel.DataAnnotations;

namespace Alpheus_API.Models.Users
{
    public class Tbl_Alpheus_InfoUser
    {
        public string IDInfo { get; set; }
        public string NmUser { get; set; }
        public string LstNmUser1 { get; set; }
        public string LstNmUser2 { get; set; }
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public string BirthdayUser { get; set; }
    }
}
