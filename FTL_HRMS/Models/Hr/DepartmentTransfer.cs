using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models.Hr
{
    [Table("tbl_DepartmentTransfer")]
    public class DepartmentTransfer
    {
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "From Designation cannot be empty")]
        [ForeignKey("FromDesignation")]
        public int FromDesignationId { get; set; }

        [Required(ErrorMessage = "To Designation cannot be empty")]
        [ForeignKey("ToDesignation")]
        public int ToDesignationId { get; set; }

        [DataType(DataType.Date),
         DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}",
         ApplyFormatInEditMode = true)]
        public DateTime TransferDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Designation FromDesignation { get; set; }
        public virtual Designation ToDesignation { get; set; }

    }
}