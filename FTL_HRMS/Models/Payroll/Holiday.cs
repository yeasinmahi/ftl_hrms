using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models.Payroll
{
    [Table("tbl_Holiday")]
    public class Holiday
    {
        [Key]
        public int Sl { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
        ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Remarks cannot be empty")]
        public string Remarks { get; set; }
    }
}