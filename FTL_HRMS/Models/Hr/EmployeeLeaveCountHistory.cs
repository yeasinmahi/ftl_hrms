using FTL_HRMS.Models.Payroll;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models.Hr
{
    [Table("tbl_EmployeeLeaveCountHistory")]
    public class EmployeeLeaveCountHistory
    {
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Paid Salary Duration cannot be empty")]
        [ForeignKey("PaidSalaryDuration")]
        public int PaidSalaryDurationId { get; set; }

        [Required(ErrorMessage = "Earn Leave Days cannot be empty")]
        public double EarnLeaveDays { get; set; }

        [Required(ErrorMessage = "Without Pay Leave Days cannot be empty")]
        public double WithoutPayLeaveDays { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual PaidSalaryDuration PaidSalaryDuration { get; set; }
    }
}