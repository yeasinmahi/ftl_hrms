using FTL_HRMS.Models.Hr;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models.Payroll
{
    [Table("tbl_SalaryAdjustment")]
    public class SalaryAdjustment
    {
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}",
        ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Amount cannot be empty")]
        public double Amount { get; set; }

       
        public string Remarks { get; set; }

        [ForeignKey("CreateEmployee")]
        public int CreatedBy { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }

        [ForeignKey("UpdateEmployee")]
        public int? UpdatedBy { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime? UpdateDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Employee CreateEmployee { get; set; }
        public virtual Employee UpdateEmployee { get; set; }
    }
}