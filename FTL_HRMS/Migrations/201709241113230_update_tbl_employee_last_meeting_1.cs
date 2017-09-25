namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_tbl_employee_last_meeting_1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_Employee", "Height", c => c.String());
            AlterColumn("dbo.tbl_Employee", "Weight", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_Employee", "Weight", c => c.Double(nullable: false));
            AlterColumn("dbo.tbl_Employee", "Height", c => c.Double(nullable: false));
        }
    }
}
