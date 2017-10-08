namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class somenullabledataadd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_SalaryAdjustment", "CreatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_SalaryAdjustment", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_SalaryAdjustment", "UpdatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Designation", "CreatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Department", "CreatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_DepartmentGroup", "CreatedBy", "dbo.tbl_Employee");
            DropIndex("dbo.tbl_Employee", new[] { "CreatedBy" });
            DropIndex("dbo.tbl_Designation", new[] { "CreatedBy" });
            DropIndex("dbo.tbl_Department", new[] { "CreatedBy" });
            DropIndex("dbo.tbl_DepartmentGroup", new[] { "CreatedBy" });
            DropIndex("dbo.tbl_SalaryAdjustment", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_SalaryAdjustment", new[] { "CreatedBy" });
            DropIndex("dbo.tbl_SalaryAdjustment", new[] { "UpdatedBy" });
            AlterColumn("dbo.tbl_Employee", "CreatedBy", c => c.Int());
            AlterColumn("dbo.tbl_Designation", "CreatedBy", c => c.Int());
            AlterColumn("dbo.tbl_Department", "CreatedBy", c => c.Int());
            AlterColumn("dbo.tbl_DepartmentGroup", "CreatedBy", c => c.Int());
            CreateIndex("dbo.tbl_Employee", "CreatedBy");
            CreateIndex("dbo.tbl_Designation", "CreatedBy");
            CreateIndex("dbo.tbl_Department", "CreatedBy");
            CreateIndex("dbo.tbl_DepartmentGroup", "CreatedBy");
            AddForeignKey("dbo.tbl_Designation", "CreatedBy", "dbo.tbl_Employee", "Sl");
            AddForeignKey("dbo.tbl_Department", "CreatedBy", "dbo.tbl_Employee", "Sl");
            AddForeignKey("dbo.tbl_DepartmentGroup", "CreatedBy", "dbo.tbl_Employee", "Sl");
            DropTable("dbo.tbl_SalaryAdjustment");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tbl_SalaryAdjustment",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        Remarks = c.String(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Sl);
            
            DropForeignKey("dbo.tbl_DepartmentGroup", "CreatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Department", "CreatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Designation", "CreatedBy", "dbo.tbl_Employee");
            DropIndex("dbo.tbl_DepartmentGroup", new[] { "CreatedBy" });
            DropIndex("dbo.tbl_Department", new[] { "CreatedBy" });
            DropIndex("dbo.tbl_Designation", new[] { "CreatedBy" });
            DropIndex("dbo.tbl_Employee", new[] { "CreatedBy" });
            AlterColumn("dbo.tbl_DepartmentGroup", "CreatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_Department", "CreatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_Designation", "CreatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_Employee", "CreatedBy", c => c.Int(nullable: false));
            CreateIndex("dbo.tbl_SalaryAdjustment", "UpdatedBy");
            CreateIndex("dbo.tbl_SalaryAdjustment", "CreatedBy");
            CreateIndex("dbo.tbl_SalaryAdjustment", "EmployeeId");
            CreateIndex("dbo.tbl_DepartmentGroup", "CreatedBy");
            CreateIndex("dbo.tbl_Department", "CreatedBy");
            CreateIndex("dbo.tbl_Designation", "CreatedBy");
            CreateIndex("dbo.tbl_Employee", "CreatedBy");
            AddForeignKey("dbo.tbl_DepartmentGroup", "CreatedBy", "dbo.tbl_Employee", "Sl", cascadeDelete: true);
            AddForeignKey("dbo.tbl_Department", "CreatedBy", "dbo.tbl_Employee", "Sl", cascadeDelete: true);
            AddForeignKey("dbo.tbl_Designation", "CreatedBy", "dbo.tbl_Employee", "Sl", cascadeDelete: true);
            AddForeignKey("dbo.tbl_SalaryAdjustment", "UpdatedBy", "dbo.tbl_Employee", "Sl");
            AddForeignKey("dbo.tbl_SalaryAdjustment", "EmployeeId", "dbo.tbl_Employee", "Sl", cascadeDelete: true);
            AddForeignKey("dbo.tbl_SalaryAdjustment", "CreatedBy", "dbo.tbl_Employee", "Sl", cascadeDelete: true);
        }
    }
}
