namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatebranch : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Branch", "OvertimePaymentPercentage", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_Branch", "OvertimePaymentPercentage");
        }
    }
}
