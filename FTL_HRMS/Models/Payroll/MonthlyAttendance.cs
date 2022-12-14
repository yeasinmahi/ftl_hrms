using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FTL_HRMS.Models.Hr;
using System.ComponentModel;

namespace FTL_HRMS.Models.Payroll
{
    [Table("tbl_MonthlyAttendance")]
    public class MonthlyAttendance
    {       
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Status cannot be empty")]
        public string Status { get; set; }

        [ForeignKey("UpdateEmployee")]
        public int? UpdatedBy { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime? UpdateDate { get; set; }

        [DefaultValue(false)]
        public bool IsCalculated { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Employee UpdateEmployee { get; set; }
    }
}