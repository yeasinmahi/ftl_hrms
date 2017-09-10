using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FTL_HRMS.Models.Hr
{
    [Table("tbl_PromotionHistory")]
    public class PromotionHistory
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
         DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
         ApplyFormatInEditMode = true)]
        public DateTime PromotionDate { get; set; }

        public double FromSalary { get; set; }
        public double ToSalary { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Designation FromDesignation { get; set; }
        public virtual Designation ToDesignation { get; set; }

    }
}