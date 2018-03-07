namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecompanytable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Company", "LastEarnLeaveCountDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tbl_Company", "EarnLeaveDuration", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_Company", "EarnLeaveCountDay", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_Branch", "LateConsiderationTime", c => c.Double(nullable: false));
            AlterColumn("dbo.tbl_Branch", "LateConsiderationDay", c => c.Double(nullable: false));
            AlterColumn("dbo.tbl_Branch", "LateDeductionPercentage", c => c.Double(nullable: false));
            AlterColumn("dbo.tbl_Branch", "OvertimeConsiderationTime", c => c.Double(nullable: false));
            AlterColumn("dbo.tbl_Branch", "OvertimePaymentPercentage", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_Branch", "OvertimePaymentPercentage", c => c.Double());
            AlterColumn("dbo.tbl_Branch", "OvertimeConsiderationTime", c => c.Double());
            AlterColumn("dbo.tbl_Branch", "LateDeductionPercentage", c => c.Double());
            AlterColumn("dbo.tbl_Branch", "LateConsiderationDay", c => c.Double());
            AlterColumn("dbo.tbl_Branch", "LateConsiderationTime", c => c.Double());
            DropColumn("dbo.tbl_Company", "EarnLeaveCountDay");
            DropColumn("dbo.tbl_Company", "EarnLeaveDuration");
            DropColumn("dbo.tbl_Company", "LastEarnLeaveCountDate");
        }
    }
}
