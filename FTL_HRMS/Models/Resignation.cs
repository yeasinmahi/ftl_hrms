using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FTL_HRMS.Models
{
    [Table("tbl_Resignation")]
    public class Resignation
    {
        [Key]
        public int Sl { get; set; }

        [DataType(DataType.Date),
         DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
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

        public string UpdatedBy { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime UpdateDate { get; set; }
        
        public string Remarks { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

    }
}