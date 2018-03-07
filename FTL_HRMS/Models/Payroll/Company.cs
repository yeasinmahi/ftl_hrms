using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models.Payroll
{
    [Table("tbl_Company")]
    public class Company
    {
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address cannot be empty")]
        public string Address { get; set; }

        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Invalid Email")]
        [StringLength(450)]
        [MaxLength(250)]
        public string Email { get; set; }

        [MaxLength(250)]
        public string Website { get; set; }

        [StringLength(450)]
        public string Phone { get; set; }

        [StringLength(450)]
        public string Mobile { get; set; }

        [StringLength(450)]
        public string AlternativeMobile { get; set; }

        [MaxLength(250)]
        public string RegistrationNo { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}",
        ApplyFormatInEditMode = true)]
        public DateTime RegistrationDate { get; set; }

        [MaxLength(250)]
        public string TINNumber { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}",
        ApplyFormatInEditMode = true)]
        public DateTime StartingDate { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}",
        ApplyFormatInEditMode = true)]
        public DateTime LastEarnLeaveCountDate { get; set; }
        
        [DataType(DataType.PhoneNumber, ErrorMessage = "Not a number")]
        [Range(0, 12, ErrorMessage = "Please use month between 0 to 12")]
        public int EarnLeaveDuration { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Not a number")]
        [Range(1, 30, ErrorMessage = "Please use day between 1 to 30")]
        public int EarnLeaveCountDay { get; set; }
    }
}