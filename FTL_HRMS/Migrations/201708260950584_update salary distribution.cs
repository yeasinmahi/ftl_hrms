namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesalarydistribution : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tbl_SalaryDistribution", "GrossSalary");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_SalaryDistribution", "GrossSalary", c => c.Double(nullable: false));
        }
    }
}
