namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_leave_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_LeaveHistory", "Remarks", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_LeaveHistory", "Remarks");
        }
    }
}
