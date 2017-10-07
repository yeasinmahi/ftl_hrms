using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FTL_HRMS.Models.Hr;

namespace FTL_HRMS.Models.Payroll
{
    [Table("tbl_MonthlySalarySheet")]
    public class MonthlySalarySheet
    {       
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        [ForeignKey("PaidSalaryDuration")]
        public int PaidSalaryDurationId { get; set; }

        [Required(ErrorMessage = "Gross Salary cannot be empty")]
        public double GrossSalary { get; set; }

        [Required(ErrorMessage = "Basic Salary cannot be empty")]
        public double BasicSalary { get; set; }

        [Required(ErrorMessage = "Absent Day cannot be empty")]
        public double AbsentDay { get; set; }

        [Required(ErrorMessage = "Absent Panelty cannot be empty")]
        public double AbsentPanelty { get; set; }

        [Required(ErrorMessage = "Late Day cannot be empty")]
        public double LateDay { get; set; }

        [Required(ErrorMessage = "Late Penalty cannot be empty")]
        public double LatePenalty { get; set; }

        [Required(ErrorMessage = "Unofficial Day cannot be empty")]
        public double UnofficialDay { get; set; }

        [Required(ErrorMessage = "Unofficial Penalty cannot be empty")]
        public double UnofficialPenalty { get; set; }

        [Required(ErrorMessage = "Leave Penalty cannot be empty")]
        public double LeavePenalty { get; set; }

        [Required(ErrorMessage = "Others Penalty cannot be empty")]
        public double OthersPenalty { get; set; }

        [Required(ErrorMessage = "Festival Bonus cannot be empty")]
        public double FestivalBonus { get; set; }

        [Required(ErrorMessage = "Others Bonus cannot be empty")]
        public double OthersBonus { get; set; }

        [Required(ErrorMessage = "Adjustment Amount cannot be empty")]
        public double AdjustmentAmount { get; set; }

        [Required(ErrorMessage = "Net Pay cannot be empty")]
        public double NetPay { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual PaidSalaryDuration PaidSalaryDuration { get; set; }
    }
}