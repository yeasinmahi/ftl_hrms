namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_Company", "LastEarnLeaveCountDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_Company", "LastEarnLeaveCountDate", c => c.DateTime(nullable: false));
        }
    }
}
