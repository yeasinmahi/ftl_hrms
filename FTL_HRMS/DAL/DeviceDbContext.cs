using System.Data.Entity;
using FTL_HRMS.Models;
using FTL_HRMS.Models.Payroll;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FTL_HRMS.DAL
{
    public class DeviceDbContext : IdentityDbContext<ApplicationUser>
    {
        public DeviceDbContext():base("DeviceDbContext")
        {
            Database.SetInitializer<DeviceDbContext>(null);
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<DeviceAttendance> DeviceAttendance { get; set; }
        
    }
}