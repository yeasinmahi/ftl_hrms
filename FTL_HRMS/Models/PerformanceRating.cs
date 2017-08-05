using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FTL_HRMS.Models
{
    [Table("tbl_PerformanceRating")]
    public class PerformanceRating
    {
        [Key]
        public int Sl { get; set; }
        
        public double Rating { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Performance Issue cannot be empty")]
        public int PerformanceIssueId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual PerformanceIssue PerformanceIssue { get; set; }
    }
}