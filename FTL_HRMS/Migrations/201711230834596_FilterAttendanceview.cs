namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FilterAttendanceview : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FilterAttendanceView",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        Date = c.DateTime(nullable: false),
                        InTime = c.DateTime(),
                        OutTime = c.DateTime(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FilterAttendanceView");
        }
    }
}
