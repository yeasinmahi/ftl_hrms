namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedeviceattendancemodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_DeviceAttendance", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_DeviceAttendance", "UserId");
        }
    }
}
