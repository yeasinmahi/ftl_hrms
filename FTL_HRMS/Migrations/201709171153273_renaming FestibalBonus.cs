namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renamingFestibalBonus : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.tbl_FestibleBonus", newName: "tbl_FestivalBonus");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.tbl_FestivalBonus", newName: "tbl_FestibleBonus");
        }
    }
}
