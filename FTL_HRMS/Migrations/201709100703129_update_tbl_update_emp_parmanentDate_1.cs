namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_tbl_update_emp_parmanentDate_1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_Employee", "ParmanentDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_Employee", "ParmanentDate", c => c.DateTime(nullable: false));
        }
    }
}
