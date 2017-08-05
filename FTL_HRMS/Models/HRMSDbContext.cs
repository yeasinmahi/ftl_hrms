using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}