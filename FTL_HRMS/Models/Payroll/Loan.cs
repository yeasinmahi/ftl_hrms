using FTL_HRMS.Models.Hr;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FTL_HRMS.Models.Payroll
{
    [Table("tbl_Loan")]
    public class Loan
    {
        [Key]

        public int Sl { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Loan amount cannot be empty")]
        public double LoanAmount { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}",
        ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }

        [Required(ErrorMessage = "Reason cannot be empty")]
        public string LoanReason { get; set; }

        [Required(ErrorMessage = "Loan duration cannot be empty")]
        public int LoanDuration { get; set; }

        [Required(ErrorMessage = "Status cannot be empty")]
        public string Status { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}",
        ApplyFormatInEditMode = true)]
        public DateTime? UpdateDate { get; set; }

        [ForeignKey("UpdateEmployee")]
        public int? UpdatedBy { get; set; }

        public string Remarks { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Employee UpdateEmployee { get; set; }
    }
}