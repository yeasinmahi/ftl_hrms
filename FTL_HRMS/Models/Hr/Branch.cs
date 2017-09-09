using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models.Hr
{
    [Table("tbl_Branch")]
    public class Branch
    {
        [Key]
        public int Sl { get; set; }
        
        [Required(ErrorMessage = "Name cannot be empty")]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address cannot be empty")]
        public string Address { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime OpeningTime { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime EndingTime { get; set; }

        [Required(ErrorMessage = "IsLateCalculated cannot be empty")]
        public bool IsLateCalculated { get; set; }

        public double? LateConsiderationTime { get; set; }

        public double? LateConsiderationDay { get; set; }

        public double? LateDeductionPercentage { get; set; }

        [Required(ErrorMessage = "IsOvertimeCalculated cannot be empty")]
        public bool IsOvertimeCalculated { get; set; }

        public double? OvertimeConsiderationTime { get; set; }

        public double? OvertimePaymentPercentage { get; set; }

        public bool Status { get; set; }
    }
}