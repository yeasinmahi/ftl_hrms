namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifysomemodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_DeviceAttendance", "Employee_Sl", "dbo.tbl_Employee");
            DropIndex("dbo.tbl_DeviceAttendance", new[] { "Employee_Sl" });
            AlterColumn("dbo.tbl_Employee", "Code", c => c.String(maxLength: 15));
            AlterColumn("dbo.tbl_DeviceAttendance", "EmployeeCode", c => c.String());
            CreateIndex("dbo.tbl_Employee", "Code", unique: true);
            DropColumn("dbo.tbl_Employee", "ParmanentDate");
            DropColumn("dbo.tbl_DeviceAttendance", "Employee_Sl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_DeviceAttendance", "Employee_Sl", c => c.Int());
            AddColumn("dbo.tbl_Employee", "ParmanentDate", c => c.DateTime());
            DropIndex("dbo.tbl_Employee", new[] { "Code" });
            AlterColumn("dbo.tbl_DeviceAttendance", "EmployeeCode", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_Employee", "Code", c => c.String());
            CreateIndex("dbo.tbl_DeviceAttendance", "Employee_Sl");
            AddForeignKey("dbo.tbl_DeviceAttendance", "Employee_Sl", "dbo.tbl_Employee", "Sl");
        }
    }
}
