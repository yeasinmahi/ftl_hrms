namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changebrachmodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_Branch", "LateConsiderationTime", c => c.Double(nullable: false));
            AlterColumn("dbo.tbl_Branch", "LateConsiderationDay", c => c.Double(nullable: false));
            AlterColumn("dbo.tbl_Branch", "LateDeductionPercentage", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_Branch", "LateDeductionPercentage", c => c.Double());
            AlterColumn("dbo.tbl_Branch", "LateConsiderationDay", c => c.Double());
            AlterColumn("dbo.tbl_Branch", "LateConsiderationTime", c => c.Double());
        }
    }
}
