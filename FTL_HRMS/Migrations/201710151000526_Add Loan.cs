namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLoan : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tbl_PaidSalaryDuration", "GenerateDate");
            DropColumn("dbo.tbl_PaidSalaryDuration", "PaidDate");
            DropColumn("dbo.tbl_PaidSalaryDuration", "IsPaid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_PaidSalaryDuration", "IsPaid", c => c.Boolean(nullable: false));
            AddColumn("dbo.tbl_PaidSalaryDuration", "PaidDate", c => c.DateTime());
            AddColumn("dbo.tbl_PaidSalaryDuration", "GenerateDate", c => c.DateTime(nullable: false));
        }
    }
}
