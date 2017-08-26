namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_createdBy_updatedBy : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.tbl_Designation", "CreatedBy");
            CreateIndex("dbo.tbl_Designation", "UpdatedBy");
            CreateIndex("dbo.tbl_DepartmentGroup", "CreatedBy");
            CreateIndex("dbo.tbl_DepartmentGroup", "UpdatedBy");
            CreateIndex("dbo.tbl_LeaveHistory", "UpdatedBy");
            CreateIndex("dbo.tbl_Resignation", "UpdatedBy");
            AddForeignKey("dbo.tbl_Designation", "CreatedBy", "dbo.tbl_Employee", "Sl", cascadeDelete: false);
            AddForeignKey("dbo.tbl_DepartmentGroup", "CreatedBy", "dbo.tbl_Employee", "Sl", cascadeDelete: false);
            AddForeignKey("dbo.tbl_DepartmentGroup", "UpdatedBy", "dbo.tbl_Employee", "Sl");
            AddForeignKey("dbo.tbl_Designation", "UpdatedBy", "dbo.tbl_Employee", "Sl");
            AddForeignKey("dbo.tbl_LeaveHistory", "UpdatedBy", "dbo.tbl_Employee", "Sl");
            AddForeignKey("dbo.tbl_Resignation", "UpdatedBy", "dbo.tbl_Employee", "Sl");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_Resignation", "UpdatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_LeaveHistory", "UpdatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Designation", "UpdatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_DepartmentGroup", "UpdatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_DepartmentGroup", "CreatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Designation", "CreatedBy", "dbo.tbl_Employee");
            DropIndex("dbo.tbl_Resignation", new[] { "UpdatedBy" });
            DropIndex("dbo.tbl_LeaveHistory", new[] { "UpdatedBy" });
            DropIndex("dbo.tbl_DepartmentGroup", new[] { "UpdatedBy" });
            DropIndex("dbo.tbl_DepartmentGroup", new[] { "CreatedBy" });
            DropIndex("dbo.tbl_Designation", new[] { "UpdatedBy" });
            DropIndex("dbo.tbl_Designation", new[] { "CreatedBy" });
        }
    }
}
