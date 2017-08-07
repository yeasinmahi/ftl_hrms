using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models
{
    [Table("tbl_Education")]
    public class Education
    {
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Institute Name cannot be empty")]
        [MaxLength(250)]
        public string InstituteName { get; set; }

        [Required(ErrorMessage = "Program cannot be empty")]
        [MaxLength(250)]
        public string Program { get; set; }

        [MaxLength(250)]
        public string Board { get; set; }

        [Required(ErrorMessage = "Result cannot be empty")]
        [MaxLength(250)]
        public string Result { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime ToDate { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}