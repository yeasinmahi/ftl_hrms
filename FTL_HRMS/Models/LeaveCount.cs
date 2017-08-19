using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models
{
    [Table("tbl_LeaveCount")]
    public class LeaveCount
    {
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Leave Type cannot be empty")]
        public int LeaveTypeId { get; set; }

        [Required(ErrorMessage = "Day cannot be empty")]
        public int AvailableDay { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual LeaveType LeaveType { get; set; }
    }
}