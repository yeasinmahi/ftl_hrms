using FTL_HRMS.Models.Hr;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models.Payroll
{
    [Table("tbl_LoanCalculation")]
    public class LoanCalculation
    {
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Loan cannot be empty")]
        [ForeignKey("Loan")]
        public int LoanId { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Loan amount cannot be empty")]
        public double LoanAmount { get; set; }

        [Required(ErrorMessage = "Loan duration cannot be empty")]
        public int LoanDuration { get; set; }

        public virtual Loan Loan { get; set; }
        public virtual Employee Employee { get; set; }
    }
}