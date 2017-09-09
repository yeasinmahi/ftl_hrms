namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_IsEditable_in_LeaveType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_LeaveType", "IsEditable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_LeaveType", "IsEditable");
        }
    }
}
