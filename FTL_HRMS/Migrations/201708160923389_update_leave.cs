namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_leave : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_DepartmentTransfer", "Department_Sl", "dbo.tbl_Department");
            DropIndex("dbo.tbl_DepartmentTransfer", new[] { "Department_Sl" });
            AddColumn("dbo.tbl_DepartmentTransfer", "FromDesignationId", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_DepartmentTransfer", "ToDesignationId", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_LeaveHistory", "CreateDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.tbl_DepartmentTransfer", "FromDepartmentId");
            DropColumn("dbo.tbl_DepartmentTransfer", "ToDepartmentId");
            DropColumn("dbo.tbl_DepartmentTransfer", "Department_Sl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_DepartmentTransfer", "Department_Sl", c => c.Int());
            AddColumn("dbo.tbl_DepartmentTransfer", "ToDepartmentId", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_DepartmentTransfer", "FromDepartmentId", c => c.Int(nullable: false));
            DropColumn("dbo.tbl_LeaveHistory", "CreateDate");
            DropColumn("dbo.tbl_DepartmentTransfer", "ToDesignationId");
            DropColumn("dbo.tbl_DepartmentTransfer", "FromDesignationId");
            CreateIndex("dbo.tbl_DepartmentTransfer", "Department_Sl");
            AddForeignKey("dbo.tbl_DepartmentTransfer", "Department_Sl", "dbo.tbl_Department", "Sl");
        }
    }
}
