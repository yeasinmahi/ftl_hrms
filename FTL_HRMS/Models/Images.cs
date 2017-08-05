using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FTL_HRMS.Models
{
    [Table("tbl_Images")]
    public class Images
    {
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Image cannot be empty")]
        public byte Image { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}