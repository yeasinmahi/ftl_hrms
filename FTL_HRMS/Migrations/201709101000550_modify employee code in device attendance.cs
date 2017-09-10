namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyemployeecodeindeviceattendance : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.tbl_Employee", new[] { "Code" });
            AlterColumn("dbo.tbl_Employee", "Code", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.tbl_DeviceAttendance", "EmployeeCode", c => c.String(maxLength: 15));
            CreateIndex("dbo.tbl_Employee", "Code", unique: true);
            CreateIndex("dbo.tbl_DeviceAttendance", "EmployeeCode", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.tbl_DeviceAttendance", new[] { "EmployeeCode" });
            DropIndex("dbo.tbl_Employee", new[] { "Code" });
            AlterColumn("dbo.tbl_DeviceAttendance", "EmployeeCode", c => c.String());
            AlterColumn("dbo.tbl_Employee", "Code", c => c.String(maxLength: 15));
            CreateIndex("dbo.tbl_Employee", "Code", unique: true);
        }
    }
}
