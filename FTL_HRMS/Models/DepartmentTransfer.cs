using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models
{
    [Table("tbl_DepartmentTransfer")]
    public class DepartmentTransfer
    {
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "From Designation cannot be empty")]
        public int FromDepartmentId { get; set; }

        [Required(ErrorMessage = "To Designation cannot be empty")]
        public int ToDepartmentId { get; set; }

        [DataType(DataType.Date),
         DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
         ApplyFormatInEditMode = true)]
        public DateTime TransferDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Department Department { get; set; }

    }
}