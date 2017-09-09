using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models.Hr
{
    [Table("tbl_BranchTransfer")]
    public class BranchTransfer
    {
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "From Branch cannot be empty")]
        [ForeignKey("FromBranch")]
        public int FromBranchId { get; set; }

        [Required(ErrorMessage = "To Branch cannot be empty")]
        [ForeignKey("ToBranch")]
        public int ToBranchId { get; set; }

        [DataType(DataType.Date),
         DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
         ApplyFormatInEditMode = true)]
        public DateTime TransferDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Branch FromBranch { get; set; }
        public virtual Branch ToBranch { get; set; }
    }
}