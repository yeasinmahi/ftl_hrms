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

            string checkPromotionHistoryView = DbUtility.GetViewCheckQuery("PromotionHistoryView");
            scripts.Add(checkPromotionHistoryView);

            string executePromotionHistoryView =
            @"create view PromotionHistoryView
as
select e.Name, e.Code, fd.Name as FromDesignation, td.Name as ToDesignation, p.PromotionDate,p.FromSalary,p.ToSalary from tbl_PromotionHistory as p 
join tbl_Designation as fd on p.FromDesignationId=fd.Sl
join tbl_Designation as td on p.ToDesignationId = td.Sl
join tbl_Employee as e on e.Sl = p.EmployeeId";
            scripts.Add(executePromotionHistoryView);

            string checkFilterAttendanceView = DbUtility.GetViewCheckQuery("FilterAttendanceView");
            scripts.Add(checkFilterAttendanceView);

            string executeFilterAttendanceView =
            @"CREATE view FilterAttendanceView
as
SELECT        e.Name, e.Code, m.EmployeeId, m.Date, f.InTime, f.OutTime, m.Status
FROM            dbo.tbl_Employee AS e INNER JOIN
                         dbo.tbl_MonthlyAttendance AS m ON e.Sl = m.EmployeeId LEFT OUTER JOIN
                         dbo.tbl_FilterAttendance AS f ON m.EmployeeId = f.EmployeeId AND m.Date = f.Date
WHERE        (e.Status = 1)";
            scripts.Add(executeFilterAttendanceView);


            DbUtility.ExecuteSeedOperation(context, scripts);
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
