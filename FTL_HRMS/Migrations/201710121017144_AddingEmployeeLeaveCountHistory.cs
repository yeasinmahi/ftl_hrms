namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingEmployeeLeaveCountHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_EmployeeLeaveCountHistory",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        PaidSalaryDurationId = c.Int(nullable: false),
                        EarnLeaveDays = c.Double(nullable: false),
                        WithoutPayLeaveDays = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_PaidSalaryDuration", t => t.PaidSalaryDurationId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.PaidSalaryDurationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_EmployeeLeaveCountHistory", "PaidSalaryDurationId", "dbo.tbl_PaidSalaryDuration");
            DropForeignKey("dbo.tbl_EmployeeLeaveCountHistory", "EmployeeId", "dbo.tbl_Employee");
            DropIndex("dbo.tbl_EmployeeLeaveCountHistory", new[] { "PaidSalaryDurationId" });
            DropIndex("dbo.tbl_EmployeeLeaveCountHistory", new[] { "EmployeeId" });
            DropTable("dbo.tbl_EmployeeLeaveCountHistory");
        }
    }
}
