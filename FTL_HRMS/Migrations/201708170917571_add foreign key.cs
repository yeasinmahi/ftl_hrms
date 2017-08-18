namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addforeignkey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_Designation", "Department_Sl", "dbo.tbl_Department");
            DropForeignKey("dbo.tbl_Department", "DepartmentGroup_Sl", "dbo.tbl_DepartmentGroup");
            DropIndex("dbo.tbl_Designation", new[] { "Department_Sl" });
            DropIndex("dbo.tbl_Department", new[] { "DepartmentGroup_Sl" });
            DropColumn("dbo.tbl_Designation", "DepartmentId");
            DropColumn("dbo.tbl_Department", "DepartmentGroupId");
            RenameColumn(table: "dbo.tbl_Designation", name: "Department_Sl", newName: "DepartmentId");
            RenameColumn(table: "dbo.tbl_Department", name: "DepartmentGroup_Sl", newName: "DepartmentGroupId");
            AlterColumn("dbo.tbl_Designation", "DepartmentId", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_Department", "DepartmentGroupId", c => c.Int(nullable: false));
            CreateIndex("dbo.tbl_Designation", "DepartmentId");
            CreateIndex("dbo.tbl_Department", "DepartmentGroupId");
            AddForeignKey("dbo.tbl_Designation", "DepartmentId", "dbo.tbl_Department", "Sl", cascadeDelete: true);
            AddForeignKey("dbo.tbl_Department", "DepartmentGroupId", "dbo.tbl_DepartmentGroup", "Sl", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_Department", "DepartmentGroupId", "dbo.tbl_DepartmentGroup");
            DropForeignKey("dbo.tbl_Designation", "DepartmentId", "dbo.tbl_Department");
            DropIndex("dbo.tbl_Department", new[] { "DepartmentGroupId" });
            DropIndex("dbo.tbl_Designation", new[] { "DepartmentId" });
            AlterColumn("dbo.tbl_Department", "DepartmentGroupId", c => c.Int());
            AlterColumn("dbo.tbl_Designation", "DepartmentId", c => c.Int());
            RenameColumn(table: "dbo.tbl_Department", name: "DepartmentGroupId", newName: "DepartmentGroup_Sl");
            RenameColumn(table: "dbo.tbl_Designation", name: "DepartmentId", newName: "Department_Sl");
            AddColumn("dbo.tbl_Department", "DepartmentGroupId", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_Designation", "DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.tbl_Department", "DepartmentGroup_Sl");
            CreateIndex("dbo.tbl_Designation", "Department_Sl");
            AddForeignKey("dbo.tbl_Department", "DepartmentGroup_Sl", "dbo.tbl_DepartmentGroup", "Sl");
            AddForeignKey("dbo.tbl_Designation", "Department_Sl", "dbo.tbl_Department", "Sl");
        }
    }
}
