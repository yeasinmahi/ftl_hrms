using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models.Payroll
{
    [Table("tbl_SalaryDistribution")]
    public class SalaryDistribution
    {       
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Basic Salary cannot be empty")]
        public double BasicSalary { get; set; }

        [Required(ErrorMessage = "House Rent cannot be empty")]
        public double HouseRent { get; set; }

        [Required(ErrorMessage = "Medical Allowance cannot be empty")]
        public double MedicalAllowance { get; set; }

        [Required(ErrorMessage = "Life Insurance cannot be empty")]
        public double LifeInsurance { get; set; }

        [Required(ErrorMessage = "Food Allowance cannot be empty")]
        public double FoodAllowance { get; set; }

        [Required(ErrorMessage = "Entertainment cannot be empty")]
        public double Entertainment { get; set; }
    }
}