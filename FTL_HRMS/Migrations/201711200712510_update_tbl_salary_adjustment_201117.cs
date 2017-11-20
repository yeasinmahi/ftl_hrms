namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_tbl_salary_adjustment_201117 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_SalaryAdjustment", "Remarks", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_SalaryAdjustment", "Remarks", c => c.String(nullable: false));
        }
    }
}
