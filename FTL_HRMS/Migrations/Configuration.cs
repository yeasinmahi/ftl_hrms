using System.Collections.Generic;
using FTL_HRMS.DAL;
using FTL_HRMS.Utility;

namespace FTL_HRMS.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<HRMSDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HRMSDbContext context)
        {
            DbUtility.ReadyStartupView(context);
        }
    }
}
