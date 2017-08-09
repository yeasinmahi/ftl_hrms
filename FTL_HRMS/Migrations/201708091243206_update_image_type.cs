namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_image_type : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_Images", "Image", c => c.Binary(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_Images", "Image", c => c.Byte(nullable: false));
        }
    }
}
