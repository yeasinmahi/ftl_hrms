namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_tbl_Employee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_DeviceAttendance", "EmployeeId", "dbo.tbl_Employee");
            DropIndex("dbo.tbl_DeviceAttendance", new[] { "EmployeeId" });
            RenameColumn(table: "dbo.tbl_DeviceAttendance", name: "EmployeeId", newName: "Employee_Sl");
            AddColumn("dbo.tbl_Employee", "ProbationStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.tbl_Employee", "IsSpecialEmployee", c => c.Boolean(nullable: false));
            AddColumn("dbo.tbl_DeviceAttendance", "EmployeeCode", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_DeviceAttendance", "Employee_Sl", c => c.Int());
            CreateIndex("dbo.tbl_DeviceAttendance", "Employee_Sl");
            AddForeignKey("dbo.tbl_DeviceAttendance", "Employee_Sl", "dbo.tbl_Employee", "Sl");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_DeviceAttendance", "Employee_Sl", "dbo.tbl_Employee");
            DropIndex("dbo.tbl_DeviceAttendance", new[] { "Employee_Sl" });
            AlterColumn("dbo.tbl_DeviceAttendance", "Employee_Sl", c => c.Int(nullable: false));
            DropColumn("dbo.tbl_DeviceAttendance", "EmployeeCode");
            DropColumn("dbo.tbl_Employee", "IsSpecialEmployee");
            DropColumn("dbo.tbl_Employee", "ProbationStatus");
            RenameColumn(table: "dbo.tbl_DeviceAttendance", name: "Employee_Sl", newName: "EmployeeId");
            CreateIndex("dbo.tbl_DeviceAttendance", "EmployeeId");
            AddForeignKey("dbo.tbl_DeviceAttendance", "EmployeeId", "dbo.tbl_Employee", "Sl", cascadeDelete: true);
        }
    }
}
