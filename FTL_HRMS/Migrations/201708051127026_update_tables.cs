namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_tables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Employee", "EmployeeTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_Employee", "EmployeeType_Sl", c => c.Int());
            AddColumn("dbo.tbl_PerformanceRating", "Employee_Sl", c => c.Int());
            AddColumn("dbo.tbl_PerformanceRating", "PerformanceIssue_Sl", c => c.Int());
            CreateIndex("dbo.tbl_Employee", "EmployeeType_Sl");
            CreateIndex("dbo.tbl_PerformanceRating", "Employee_Sl");
            CreateIndex("dbo.tbl_PerformanceRating", "PerformanceIssue_Sl");
            AddForeignKey("dbo.tbl_Employee", "EmployeeType_Sl", "dbo.tbl_EmployeeType", "Sl");
            AddForeignKey("dbo.tbl_PerformanceRating", "Employee_Sl", "dbo.tbl_Employee", "Sl");
            AddForeignKey("dbo.tbl_PerformanceRating", "PerformanceIssue_Sl", "dbo.tbl_PerformanceIssue", "Sl");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_PerformanceRating", "PerformanceIssue_Sl", "dbo.tbl_PerformanceIssue");
            DropForeignKey("dbo.tbl_PerformanceRating", "Employee_Sl", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Employee", "EmployeeType_Sl", "dbo.tbl_EmployeeType");
            DropIndex("dbo.tbl_PerformanceRating", new[] { "PerformanceIssue_Sl" });
            DropIndex("dbo.tbl_PerformanceRating", new[] { "Employee_Sl" });
            DropIndex("dbo.tbl_Employee", new[] { "EmployeeType_Sl" });
            DropColumn("dbo.tbl_PerformanceRating", "PerformanceIssue_Sl");
            DropColumn("dbo.tbl_PerformanceRating", "Employee_Sl");
            DropColumn("dbo.tbl_Employee", "EmployeeType_Sl");
            DropColumn("dbo.tbl_Employee", "EmployeeTypeId");
        }
    }
}
