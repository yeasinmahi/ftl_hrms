using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models.Hr
{
    [Table("tbl_LeaveType")]
    public class LeaveType
    {
        [Key]
        public int Sl { get; set; }
        
        [Required(ErrorMessage = "Name cannot be empty")]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Day cannot be empty")]
        public double Day { get; set; }

        public bool IsEditable { get; set; }
    }
}