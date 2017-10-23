namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingSubscription : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_Subscription",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Sl);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tbl_Subscription");
        }
    }
}
