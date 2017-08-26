namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateemployeemodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Branch", "OpentingTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.tbl_Branch", "EndingTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.tbl_Branch", "IsLateCalculated", c => c.Boolean(nullable: false));
            AddColumn("dbo.tbl_Branch", "LateConsiderationTime", c => c.Double());
            AddColumn("dbo.tbl_Branch", "LateConsiderationDay", c => c.Double());
            AddColumn("dbo.tbl_Branch", "LateDeductionPercentage", c => c.Double());
            AddColumn("dbo.tbl_Branch", "IsOvertimeCalculated", c => c.Boolean(nullable: false));
            AddColumn("dbo.tbl_Branch", "OvertimeConsiderationTime", c => c.Double());
            AddColumn("dbo.tbl_Employee", "IsSystemOrSuperAdmin", c => c.Boolean(nullable: false));
            CreateIndex("dbo.tbl_Employee", "CreatedBy");
            AddForeignKey("dbo.tbl_Employee", "CreatedBy", "dbo.tbl_Employee", "Sl");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_Employee", "CreatedBy", "dbo.tbl_Employee");
            DropIndex("dbo.tbl_Employee", new[] { "CreatedBy" });
            DropColumn("dbo.tbl_Employee", "IsSystemOrSuperAdmin");
            DropColumn("dbo.tbl_Branch", "OvertimeConsiderationTime");
            DropColumn("dbo.tbl_Branch", "IsOvertimeCalculated");
            DropColumn("dbo.tbl_Branch", "LateDeductionPercentage");
            DropColumn("dbo.tbl_Branch", "LateConsiderationDay");
            DropColumn("dbo.tbl_Branch", "LateConsiderationTime");
            DropColumn("dbo.tbl_Branch", "IsLateCalculated");
            DropColumn("dbo.tbl_Branch", "EndingTime");
            DropColumn("dbo.tbl_Branch", "OpentingTime");
        }
    }
}
