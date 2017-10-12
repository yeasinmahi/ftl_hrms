using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models.Hr
{
    [Table("tbl_Resignation")]
    public class Resignation
    {
        [Key]
        public int Sl { get; set; }

        [DataType(DataType.Date),
         DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}",
         ApplyFormatInEditMode = true)]
        public DateTime ResignDate { get; set; }

        [Required(ErrorMessage = "Reason cannot be empty")]
        public string Reason { get; set; }

        public string Suggestion { get; set; }

        public string Status { get; set; }

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
        
        public string Remarks { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        public virtual Employee UpdateEmployee { get; set; }
        public virtual Employee Employee { get; set; }
    }
}