using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models.Hr
{
    [Table("tbl_Employee")]
    public class Employee
    {
        public Employee()
        {
            Education = new HashSet<Education>();
            Experience = new HashSet<Experience>();
            Images = new HashSet<Images>();
        }

        [Key]
        public int Sl { get; set; }
        [MaxLength(15)]
        [Required(ErrorMessage ="Code can not be empty")]
        [Index(IsUnique =true)]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Father's Name cannot be empty")]
        [MaxLength(250)]
        public string FathersName { get; set; }

        [Required(ErrorMessage = "Mother's Name cannot be empty")]
        [MaxLength(250)]
        public string MothersName { get; set; }

        [Required(ErrorMessage = "Gender cannot be empty")]
        [MaxLength(250)]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Present Address cannot be empty")]
        [MaxLength(250)]
        public string PresentAddress { get; set; }

        [Required(ErrorMessage = "Permanent Address cannot be empty")]
        [MaxLength(250)]
        public string PermanentAddress { get; set; }

        [Required(ErrorMessage = "Mobile cannot be empty")]
        [StringLength(450)]
        public string Mobile { get; set; }

        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Invalid Email")]
        [StringLength(450)]
        [MaxLength(250)]
        public string Email { get; set; }

        [Required(ErrorMessage = "NID or Birth Cirtificate cannot be empty")]
        [MaxLength(250)]
        // ReSharper disable once InconsistentNaming
        public string NIDorBirthCirtificate { get; set; }

        [MaxLength(250)]
        public string DrivingLicence { get; set; }

        [MaxLength(250)]
        public string PassportNumber { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime DateOfJoining { get; set; }

        [Required(ErrorMessage = "Source Of Hire cannot be empty")]
        [ForeignKey("SourceOfHire")]
        public int SourceOfHireId { get; set; }

        [Required(ErrorMessage = "Designation cannot be empty")]
        [ForeignKey("Designation")]
        public int DesignationId { get; set; }

        [Required(ErrorMessage = "Employee Type cannot be empty")]
        [ForeignKey("EmployeeType")]
        public int EmployeeTypeId { get; set; }

        [Required(ErrorMessage = "Branch cannot be empty")]
        [ForeignKey("Branch")]
        public int BranchId { get; set; }

        public double GrossSalary { get; set; }

        [ForeignKey("CreateEmployee")]
        public int CreatedBy { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }
        
        public int? UpdatedBy { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime? UpdateDate { get; set; }

        public bool IsSystemOrSuperAdmin { get; set; }

        public bool Status { get; set; }

        public bool ProbationStatus { get; set; }

        public bool IsSpecialEmployee { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime? ParmanentDate { get; set; }
        
        [StringLength(450)]
        public string EmergencyMobile { get; set; }
        
        [StringLength(450)]
        public string RelationEmergencyMobile { get; set; }
        
        public string BloodGroup { get; set; }
        
        public string MedicalHistory { get; set; }

        public string Height { get; set; }

        public string Weight { get; set; }
        
        public string ExtraCurricularActivities { get; set; }


        public virtual Employee CreateEmployee { get; set; }
        public virtual SourceOfHire SourceOfHire { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual EmployeeType EmployeeType { get; set; }
        public virtual Branch Branch { get; set; }

        public virtual ICollection<Education> Education { get; set; }
        public virtual ICollection<Experience> Experience { get; set; }
        public virtual ICollection<Images> Images { get; set; }
    }
}