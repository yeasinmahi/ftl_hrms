namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_tbl_BonusPenalty_festbl_fileStrg_16917 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_BonusAndPenalty",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Type = c.String(nullable: false),
                        Amount = c.Double(nullable: false),
                        Remarks = c.String(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.CreatedBy, cascadeDelete: false)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.tbl_Employee", t => t.UpdatedBy)
                .Index(t => t.EmployeeId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.tbl_FestibleBonus",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        BasedOn = c.String(nullable: false),
                        BonusPersentage = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Remarks = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Sl);
            
            CreateTable(
                "dbo.tbl_FileStorage",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Path = c.String(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.CreatedBy, cascadeDelete: false)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.CreatedBy);
            
            CreateIndex("dbo.tbl_MonthlyAttendance", "UpdatedBy");
            AddForeignKey("dbo.tbl_MonthlyAttendance", "UpdatedBy", "dbo.tbl_Employee", "Sl");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_MonthlyAttendance", "UpdatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_FileStorage", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_FileStorage", "CreatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_BonusAndPenalty", "UpdatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_BonusAndPenalty", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_BonusAndPenalty", "CreatedBy", "dbo.tbl_Employee");
            DropIndex("dbo.tbl_MonthlyAttendance", new[] { "UpdatedBy" });
            DropIndex("dbo.tbl_FileStorage", new[] { "CreatedBy" });
            DropIndex("dbo.tbl_FileStorage", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_BonusAndPenalty", new[] { "UpdatedBy" });
            DropIndex("dbo.tbl_BonusAndPenalty", new[] { "CreatedBy" });
            DropIndex("dbo.tbl_BonusAndPenalty", new[] { "EmployeeId" });
            DropTable("dbo.tbl_FileStorage");
            DropTable("dbo.tbl_FestibleBonus");
            DropTable("dbo.tbl_BonusAndPenalty");
        }
    }
}
