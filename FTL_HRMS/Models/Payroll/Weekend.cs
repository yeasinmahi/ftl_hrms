using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FTL_HRMS.Models.Hr;

namespace FTL_HRMS.Models.Payroll
{
    [Table("tbl_Weekend")]
    public class Weekend
    {       
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Branch cannot be empty")]
        [ForeignKey("Branch")]
        public int BranchId { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
        ApplyFormatInEditMode = true)]
        public DateTime Day { get; set; }

        public virtual Branch Branch { get; set; }
    }
}