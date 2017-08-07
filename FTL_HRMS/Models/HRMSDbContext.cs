using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FTL_HRMS.Models
{
    public class HRMSDbContext : IdentityDbContext<ApplicationUser>
    {
        public HRMSDbContext():base("FTL_HRMS")
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }

        public System.Data.Entity.DbSet<FTL_HRMS.Models.Branch> Branches { get; set; }
    }
}