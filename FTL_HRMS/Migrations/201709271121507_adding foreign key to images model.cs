namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingforeignkeytoimagesmodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_Images", "Employee_Sl", "dbo.tbl_Employee");
            DropIndex("dbo.tbl_Images", new[] { "Employee_Sl" });
            DropColumn("dbo.tbl_Images", "EmployeeId");
            RenameColumn(table: "dbo.tbl_Images", name: "Employee_Sl", newName: "EmployeeId");
            AlterColumn("dbo.tbl_Images", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.tbl_Images", "EmployeeId");
            AddForeignKey("dbo.tbl_Images", "EmployeeId", "dbo.tbl_Employee", "Sl", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_Images", "EmployeeId", "dbo.tbl_Employee");
            DropIndex("dbo.tbl_Images", new[] { "EmployeeId" });
            AlterColumn("dbo.tbl_Images", "EmployeeId", c => c.Int());
            RenameColumn(table: "dbo.tbl_Images", name: "EmployeeId", newName: "Employee_Sl");
            AddColumn("dbo.tbl_Images", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.tbl_Images", "Employee_Sl");
            AddForeignKey("dbo.tbl_Images", "Employee_Sl", "dbo.tbl_Employee", "Sl");
        }
    }
}
