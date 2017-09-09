using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models.Hr
{
    [Table("tbl_Designation")]
    public class Designation
    {
        [Key]
        public int Sl { get; set; }

        public string Code { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Department cannot be empty")]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Role Name cannot be empty")]
        public string RoleName { get; set; }

        [ForeignKey("CreateEmployee")]
        public int CreatedBy { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }

        [ForeignKey("UpdateEmployee")]
        public int? UpdatedBy { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime? UpdateDate { get; set; }

        public bool Status { get; set; }
        
        public virtual Employee CreateEmployee { get; set; }
        public virtual Employee UpdateEmployee { get; set; }
        public virtual Department Department { get; set; }
    }
}