namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_EmployeeType", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.tbl_SourceOfHire", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_SourceOfHire", "Status");
            DropColumn("dbo.tbl_EmployeeType", "Status");
        }
    }
}
