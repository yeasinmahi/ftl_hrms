namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_leave : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Employee", "BranchId", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_Employee", "Branch_Sl", c => c.Int());
            AddColumn("dbo.tbl_DepartmentTransfer", "FromDesignationId", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_DepartmentTransfer", "ToDesignationId", c => c.Int(nullable: false));
            CreateIndex("dbo.tbl_Employee", "Branch_Sl");
            AddForeignKey("dbo.tbl_Employee", "Branch_Sl", "dbo.tbl_Branch", "Sl");
            DropColumn("dbo.tbl_DepartmentTransfer", "FromDepartmentId");
            DropColumn("dbo.tbl_DepartmentTransfer", "ToDepartmentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_DepartmentTransfer", "ToDepartmentId", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_DepartmentTransfer", "FromDepartmentId", c => c.Int(nullable: false));
            DropForeignKey("dbo.tbl_Employee", "Branch_Sl", "dbo.tbl_Branch");
            DropIndex("dbo.tbl_Employee", new[] { "Branch_Sl" });
            DropColumn("dbo.tbl_DepartmentTransfer", "ToDesignationId");
            DropColumn("dbo.tbl_DepartmentTransfer", "FromDesignationId");
            DropColumn("dbo.tbl_Employee", "Branch_Sl");
            DropColumn("dbo.tbl_Employee", "BranchId");
        }
    }
}
