using FTL_HRMS.Models.Hr;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models.Payroll
{
    [Table("tbl_LoanCalculationHistory")]
    public class LoanCalculationHistory
    {
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Loan cannot be empty")]
        [ForeignKey("PaidSalaryDuration")]
        public int PaidSalaryDurationId { get; set; }

        [Required(ErrorMessage = "Loan cannot be empty")]
        [ForeignKey("LoanCalculation")]
        public int LoanCalculationId { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Loan amount cannot be empty")]
        public double LoanAmount { get; set; }

        public virtual PaidSalaryDuration PaidSalaryDuration { get; set; }
        public virtual LoanCalculation LoanCalculation { get; set; }
        public virtual Employee Employee { get; set; }
    }
}