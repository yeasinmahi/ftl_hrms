namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_tbl_PromotionHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_PromotionHistory",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        FromDesignationId = c.Int(nullable: false),
                        ToDesignationId = c.Int(nullable: false),
                        PromotionDate = c.DateTime(nullable: false),
                        FromSalary = c.Double(nullable: false),
                        ToSalary = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Designation", t => t.FromDesignationId, cascadeDelete: false)
                .ForeignKey("dbo.tbl_Designation", t => t.ToDesignationId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.FromDesignationId)
                .Index(t => t.ToDesignationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_PromotionHistory", "ToDesignationId", "dbo.tbl_Designation");
            DropForeignKey("dbo.tbl_PromotionHistory", "FromDesignationId", "dbo.tbl_Designation");
            DropForeignKey("dbo.tbl_PromotionHistory", "EmployeeId", "dbo.tbl_Employee");
            DropIndex("dbo.tbl_PromotionHistory", new[] { "ToDesignationId" });
            DropIndex("dbo.tbl_PromotionHistory", new[] { "FromDesignationId" });
            DropIndex("dbo.tbl_PromotionHistory", new[] { "EmployeeId" });
            DropTable("dbo.tbl_PromotionHistory");
        }
    }
}
