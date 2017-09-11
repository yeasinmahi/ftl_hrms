namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changedeviceattandancedatetimetochecktime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_DeviceAttendance", "CheckTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.tbl_DeviceAttendance", "Datetime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_DeviceAttendance", "Datetime", c => c.DateTime(nullable: false));
            DropColumn("dbo.tbl_DeviceAttendance", "CheckTime");
        }
    }
}
