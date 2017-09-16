namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_tbl_monthlyattendance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_MonthlyAttendance", "UpdatedBy", c => c.Int());
            AddColumn("dbo.tbl_MonthlyAttendance", "UpdateDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_MonthlyAttendance", "UpdateDate");
            DropColumn("dbo.tbl_MonthlyAttendance", "UpdatedBy");
        }
    }
}
