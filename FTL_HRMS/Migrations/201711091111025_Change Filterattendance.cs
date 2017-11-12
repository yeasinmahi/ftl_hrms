namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFilterattendance : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_FilterAttendance", "EmployeeId", "dbo.tbl_Employee");
            DropIndex("dbo.tbl_FilterAttendance", new[] { "EmployeeId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.tbl_FilterAttendance", "EmployeeId");
            AddForeignKey("dbo.tbl_FilterAttendance", "EmployeeId", "dbo.tbl_Employee", "Sl", cascadeDelete: true);
        }
    }
}
