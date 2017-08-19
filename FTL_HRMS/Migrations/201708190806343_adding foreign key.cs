namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingforeignkey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_LeaveCount", "Employee_Sl", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_LeaveCount", "LeaveType_Sl", "dbo.tbl_LeaveType");
            DropIndex("dbo.tbl_LeaveCount", new[] { "Employee_Sl" });
            DropIndex("dbo.tbl_LeaveCount", new[] { "LeaveType_Sl" });
            DropColumn("dbo.tbl_LeaveCount", "EmployeeId");
            DropColumn("dbo.tbl_LeaveCount", "LeaveTypeId");
            RenameColumn(table: "dbo.tbl_LeaveCount", name: "Employee_Sl", newName: "EmployeeId");
            RenameColumn(table: "dbo.tbl_LeaveCount", name: "LeaveType_Sl", newName: "LeaveTypeId");
            AlterColumn("dbo.tbl_LeaveCount", "EmployeeId", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_LeaveCount", "LeaveTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.tbl_LeaveCount", "EmployeeId");
            CreateIndex("dbo.tbl_LeaveCount", "LeaveTypeId");
            AddForeignKey("dbo.tbl_LeaveCount", "EmployeeId", "dbo.tbl_Employee", "Sl", cascadeDelete: true);
            AddForeignKey("dbo.tbl_LeaveCount", "LeaveTypeId", "dbo.tbl_LeaveType", "Sl", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_LeaveCount", "LeaveTypeId", "dbo.tbl_LeaveType");
            DropForeignKey("dbo.tbl_LeaveCount", "EmployeeId", "dbo.tbl_Employee");
            DropIndex("dbo.tbl_LeaveCount", new[] { "LeaveTypeId" });
            DropIndex("dbo.tbl_LeaveCount", new[] { "EmployeeId" });
            AlterColumn("dbo.tbl_LeaveCount", "LeaveTypeId", c => c.Int());
            AlterColumn("dbo.tbl_LeaveCount", "EmployeeId", c => c.Int());
            RenameColumn(table: "dbo.tbl_LeaveCount", name: "LeaveTypeId", newName: "LeaveType_Sl");
            RenameColumn(table: "dbo.tbl_LeaveCount", name: "EmployeeId", newName: "Employee_Sl");
            AddColumn("dbo.tbl_LeaveCount", "LeaveTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_LeaveCount", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.tbl_LeaveCount", "LeaveType_Sl");
            CreateIndex("dbo.tbl_LeaveCount", "Employee_Sl");
            AddForeignKey("dbo.tbl_LeaveCount", "LeaveType_Sl", "dbo.tbl_LeaveType", "Sl");
            AddForeignKey("dbo.tbl_LeaveCount", "Employee_Sl", "dbo.tbl_Employee", "Sl");
        }
    }
}
