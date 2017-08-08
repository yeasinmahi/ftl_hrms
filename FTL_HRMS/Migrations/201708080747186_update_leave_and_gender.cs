namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_leave_and_gender : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_LeaveCount",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        LeaveTypeId = c.Int(nullable: false),
                        AvailableDay = c.Int(nullable: false),
                        Employee_Sl = c.Int(),
                        LeaveType_Sl = c.Int(),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.Employee_Sl)
                .ForeignKey("dbo.tbl_LeaveType", t => t.LeaveType_Sl)
                .Index(t => t.Employee_Sl)
                .Index(t => t.LeaveType_Sl);
            
            CreateTable(
                "dbo.tbl_LeaveType",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Day = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Sl);
            
            CreateTable(
                "dbo.tbl_LeaveHistory",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        LeaveTypeId = c.Int(nullable: false),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        Day = c.Int(nullable: false),
                        Cause = c.String(nullable: false, maxLength: 250),
                        UpdatedBy = c.Int(),
                        UpdateDate = c.DateTime(),
                        Status = c.String(),
                        Employee_Sl = c.Int(),
                        LeaveType_Sl = c.Int(),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.Employee_Sl)
                .ForeignKey("dbo.tbl_LeaveType", t => t.LeaveType_Sl)
                .Index(t => t.Employee_Sl)
                .Index(t => t.LeaveType_Sl);
            
            AddColumn("dbo.tbl_Employee", "Gender", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_LeaveHistory", "LeaveType_Sl", "dbo.tbl_LeaveType");
            DropForeignKey("dbo.tbl_LeaveHistory", "Employee_Sl", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_LeaveCount", "LeaveType_Sl", "dbo.tbl_LeaveType");
            DropForeignKey("dbo.tbl_LeaveCount", "Employee_Sl", "dbo.tbl_Employee");
            DropIndex("dbo.tbl_LeaveHistory", new[] { "LeaveType_Sl" });
            DropIndex("dbo.tbl_LeaveHistory", new[] { "Employee_Sl" });
            DropIndex("dbo.tbl_LeaveCount", new[] { "LeaveType_Sl" });
            DropIndex("dbo.tbl_LeaveCount", new[] { "Employee_Sl" });
            DropColumn("dbo.tbl_Employee", "Gender");
            DropTable("dbo.tbl_LeaveHistory");
            DropTable("dbo.tbl_LeaveType");
            DropTable("dbo.tbl_LeaveCount");
        }
    }
}
