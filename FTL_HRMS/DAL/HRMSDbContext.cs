using System.Data.Entity;
using FTL_HRMS.Models;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Models.Payroll;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FTL_HRMS.DAL
{
    public class HRMSDbContext : IdentityDbContext<ApplicationUser>
    {
        public HRMSDbContext():base("HRMSDbContext")
        {
            Database.SetInitializer<HRMSDbContext>(null);
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }
        public DbSet<PerformanceRating> PerformanceRating { get; set; }
        public DbSet<PerformanceIssue> PerformanceIssue { get; set; }
        public DbSet<DisciplinaryAction> DisciplinaryAction { get; set; }
        public DbSet<DisciplinaryActionType> DisciplinaryActionType { get; set; }
        public DbSet<Resignation> Resignation { get; set; }
        public DbSet<BranchTransfer> BranchTransfer { get; set; }
        public DbSet<DepartmentTransfer> DepartmentTransfer { get; set; }
        public DbSet<EmployeeType> EmployeeType { get; set; }
        public DbSet<Designation> Designation { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<DepartmentGroup> DepartmentGroup { get; set; }
        public DbSet<SourceOfHire> SourceOfHire { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Experience> Experience { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Images> Images { get; set; }

        public DbSet<Company> Company { get; set; }
        public DbSet<DeviceAttendance> DeviceAttendance { get; set; }
        public DbSet<FilterAttendance> FilterAttendance { get; set; }
        public DbSet<Holiday> Holiday { get; set; }
        public DbSet<MonthlyAttendance> MonthlyAttendance { get; set; }
        public DbSet<MonthlySalarySheet> MonthlySalarySheet { get; set; }
        public DbSet<PaidSalaryDuration> PaidSalaryDuration { get; set; }
        public DbSet<SalaryDistribution> SalaryDistribution { get; set; }
        public DbSet<EmployeeSalaryDistribution> EmployeeSalaryDistribution { get; set; }
        public DbSet<Weekend> Weekend { get; set; }
        public DbSet<BonusAndPenalty> BonusAndPenalty { get; set; }
        public DbSet<SalaryAdjustment> SalaryAdjustment { get; set; }
        public DbSet<FestivalBonus> FestivalBonus { get; set; }
        public DbSet<FileStorage> FileStorage { get; set; }
        public DbSet<EmployeeLeaveCountHistory> EmployeeLeaveCountHistory { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveCount> LeaveCounts { get; set; }
        public DbSet<LeaveHistory> LeaveHistories { get; set; }
        public DbSet<PromotionHistory> PromotionHistories { get; set; }
        public DbSet<Loan> Loan { get; set; }
        public DbSet<LoanCalculation> LoanCalculation { get; set; }
        public DbSet<LoanCalculationHistory> LoanCalculationHistory { get; set; }
        public DbSet<Subscription> Subscription { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
    }
}