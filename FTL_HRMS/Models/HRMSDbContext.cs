using System.Data.Entity;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Models.Payroll;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FTL_HRMS.Models
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }

        public System.Data.Entity.DbSet<Branch> Branches { get; set; }

        public System.Data.Entity.DbSet<LeaveType> LeaveTypes { get; set; }

        public System.Data.Entity.DbSet<LeaveCount> LeaveCounts { get; set; }

        public System.Data.Entity.DbSet<LeaveHistory> LeaveHistories { get; set; }
    }
}