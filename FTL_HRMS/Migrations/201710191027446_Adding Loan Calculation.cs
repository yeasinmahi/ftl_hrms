namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingLoanCalculation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_LoanCalculation",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        LoanId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        LoanAmount = c.Double(nullable: false),
                        LoanDuration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Loan", t => t.LoanId, cascadeDelete: false)
                .Index(t => t.LoanId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.tbl_LoanCalculationHistory",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        PaidSalaryDurationId = c.Int(nullable: false),
                        LoanCalculationId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        LoanAmount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_LoanCalculation", t => t.LoanCalculationId, cascadeDelete: false)
                .ForeignKey("dbo.tbl_PaidSalaryDuration", t => t.PaidSalaryDurationId, cascadeDelete: true)
                .Index(t => t.PaidSalaryDurationId)
                .Index(t => t.LoanCalculationId)
                .Index(t => t.EmployeeId);
            
            AddColumn("dbo.tbl_MonthlySalarySheet", "LoanAmount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_LoanCalculationHistory", "PaidSalaryDurationId", "dbo.tbl_PaidSalaryDuration");
            DropForeignKey("dbo.tbl_LoanCalculationHistory", "LoanCalculationId", "dbo.tbl_LoanCalculation");
            DropForeignKey("dbo.tbl_LoanCalculationHistory", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_LoanCalculation", "LoanId", "dbo.tbl_Loan");
            DropForeignKey("dbo.tbl_LoanCalculation", "EmployeeId", "dbo.tbl_Employee");
            DropIndex("dbo.tbl_LoanCalculationHistory", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_LoanCalculationHistory", new[] { "LoanCalculationId" });
            DropIndex("dbo.tbl_LoanCalculationHistory", new[] { "PaidSalaryDurationId" });
            DropIndex("dbo.tbl_LoanCalculation", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_LoanCalculation", new[] { "LoanId" });
            DropColumn("dbo.tbl_MonthlySalarySheet", "LoanAmount");
            DropTable("dbo.tbl_LoanCalculationHistory");
            DropTable("dbo.tbl_LoanCalculation");
        }
    }
}
