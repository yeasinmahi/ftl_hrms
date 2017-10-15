using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models.Payroll
{
    [Table("tbl_PaidSalaryDuration")]
    public class PaidSalaryDuration
    {       
        [Key]
        public int Sl { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}",
        ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}",
        ApplyFormatInEditMode = true)]
        public DateTime ToDate { get; set; }

        [Required(ErrorMessage = "Working Day cannot be empty")]
        public double WorkingDay { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}",
        ApplyFormatInEditMode = true)]
        public DateTime GenerateDate { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}",
        ApplyFormatInEditMode = true)]
        public DateTime? PaidDate { get; set; }

        public bool IsPaid { get; set; }
    }
}