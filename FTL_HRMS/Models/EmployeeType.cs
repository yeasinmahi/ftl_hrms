using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models
{
    [Table("tbl_EmployeeType")]
    public class EmployeeType
    {
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [MaxLength(250)]
        public string Name { get; set; }

        public bool Status { get; set; }
    }
}