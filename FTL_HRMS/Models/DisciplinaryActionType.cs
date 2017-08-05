using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FTL_HRMS.Models
{
    [Table("tbl_DisciplinaryActionType")]
    public class DisciplinaryActionType
    {
        [Key]
        public int Sl { get; set; }
        
        [Required(ErrorMessage = "Name cannot be empty")]
        [RegularExpression("^([a-zA-Z .-]*)$", ErrorMessage = "Enter only alphabets")]
        [MaxLength(250)]
        public string Name { get; set; }
    }
}