namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_tbl_update_emp_parmanentDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Employee", "ParmanentDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_Employee", "ParmanentDate");
        }
    }
}
