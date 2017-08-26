namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatebranchmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Branch", "OpeningTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.tbl_Branch", "OpentingTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_Branch", "OpentingTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.tbl_Branch", "OpeningTime");
        }
    }
}
