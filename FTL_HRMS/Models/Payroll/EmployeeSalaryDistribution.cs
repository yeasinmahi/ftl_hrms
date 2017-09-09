using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FTL_HRMS.Models.Hr;

namespace FTL_HRMS.Models
{
    [Table("tbl_EmployeeSalaryDistribution")]
    public class EmployeeSalaryDistribution
    {       
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Gross Salary cannot be empty")]
        public double GrossSalary { get; set; }

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

        public virtual Employee Employee { get; set; }
    }
}