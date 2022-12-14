using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models.Payroll
{
    [Table("tbl_FestivalBonus")]
    public class FestivalBonus
    {
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Based On cannot be empty")]
        public string BasedOn { get; set; }

        [Required(ErrorMessage = "Bonus Persentage cannot be empty")]
        public double BonusPersentage { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}",
        ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Remarks cannot be empty")]
        public string Remarks { get; set; }
    }
}