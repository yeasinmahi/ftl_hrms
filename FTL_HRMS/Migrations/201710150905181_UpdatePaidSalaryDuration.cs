namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePaidSalaryDuration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_PaidSalaryDuration", "GenerateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tbl_PaidSalaryDuration", "PaidDate", c => c.DateTime());
            AddColumn("dbo.tbl_PaidSalaryDuration", "IsPaid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_PaidSalaryDuration", "IsPaid");
            DropColumn("dbo.tbl_PaidSalaryDuration", "PaidDate");
            DropColumn("dbo.tbl_PaidSalaryDuration", "GenerateDate");
        }
    }
}
