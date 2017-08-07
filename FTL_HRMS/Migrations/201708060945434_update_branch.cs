namespace FTL_HRMS.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class update_branch : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Branch", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_Branch", "Status");
        }
    }
}
