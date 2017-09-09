namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_days_type : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_LeaveCount", "AvailableDay", c => c.Double(nullable: false));
            AlterColumn("dbo.tbl_LeaveType", "Day", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_LeaveType", "Day", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_LeaveCount", "AvailableDay", c => c.Int(nullable: false));
        }
    }
}
