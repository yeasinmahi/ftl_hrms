using System.Collections.Generic;
using FTL_HRMS.Utility;

namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FTL_HRMS.Models.HRMSDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FTL_HRMS.Models.HRMSDbContext context)
        {
            List<string> scripts = new List<string>();

            string checkDepartmentTransferView = DbUtility.GetViewCheckQuery("DepartmentTransferView");
            scripts.Add(checkDepartmentTransferView);

            string executeDepartmentTransferView =
            @"create view DepartmentTransferView 
AS
select dt.EmployeeId as Code, e.Name,fd.Name as FromDesignation,td.Name as ToDesignation,dt.TransferDate from tbl_DepartmentTransfer as dt 
join tbl_Designation as fd on fd.Sl = dt.FromDesignationId 
join tbl_Designation as td on td.Sl =dt.ToDesignationId
join tbl_Employee as e on e.Sl = dt.EmployeeId";
            scripts.Add(executeDepartmentTransferView);

            string checkBranchTransferView = DbUtility.GetViewCheckQuery("BranchTransferView");
            scripts.Add(checkBranchTransferView);

            string executeBranchTransferView =
            @"create view BranchTransferView 
AS
select bt.EmployeeId as Code, e.Name,fb.Name as FromDesignation,tb.Name as ToDesignation,bt.TransferDate 
from tbl_BranchTransfer as bt 
join tbl_Branch as fb on fb.Sl = bt.FromBranchId 
join tbl_Branch as tb on tb.Sl =bt.ToBranchId
join tbl_Employee as e on e.Sl = bt.EmployeeId";
            scripts.Add(executeBranchTransferView);


            DbUtility.ExecuteSeedOperation(context,scripts);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
