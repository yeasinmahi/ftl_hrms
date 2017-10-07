namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMonthlySalarySheet : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.CreatedBy, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.tbl_Employee", t => t.UpdatedBy)
                .Index(t => t.EmployeeId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            AddColumn("dbo.tbl_MonthlySalarySheet", "AdjustmentAmount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_SalaryAdjustment", "UpdatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_SalaryAdjustment", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_SalaryAdjustment", "CreatedBy", "dbo.tbl_Employee");
            DropIndex("dbo.tbl_SalaryAdjustment", new[] { "UpdatedBy" });
            DropIndex("dbo.tbl_SalaryAdjustment", new[] { "CreatedBy" });
            DropIndex("dbo.tbl_SalaryAdjustment", new[] { "EmployeeId" });
            DropColumn("dbo.tbl_MonthlySalarySheet", "AdjustmentAmount");
            DropTable("dbo.tbl_SalaryAdjustment");
        }
    }
}
