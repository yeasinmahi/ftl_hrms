using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FTL_HRMS.Models
{
    [Table("tbl_SourceOfHire")]
    public class SourceOfHire
    {
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [MaxLength(250)]
        public string Name { get; set; }

        public bool Status { get; set; }
    }
}