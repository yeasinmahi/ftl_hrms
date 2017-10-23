using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models.Hr
{
    [Table("tbl_SourceOfHire")]
    public class SourceOfHire
    {
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [MaxLength(250)]
        public string Name { get; set; }

        [DefaultValue(true)]
        public bool Status { get; set; }
    }
}