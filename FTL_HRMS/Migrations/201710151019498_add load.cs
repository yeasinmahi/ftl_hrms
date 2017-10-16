namespace FTL_HRMS.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class addload : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_Loan",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        LoanAmount = c.Double(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LoanReason = c.String(nullable: false),
                        LoanDuration = c.Int(nullable: false),
                        Status = c.String(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdatedBy = c.Int(),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Employee", t => t.UpdatedBy)
                .Index(t => t.EmployeeId)
                .Index(t => t.UpdatedBy);
            
            AddColumn("dbo.tbl_PaidSalaryDuration", "GenerateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tbl_PaidSalaryDuration", "PaidDate", c => c.DateTime());
            AddColumn("dbo.tbl_PaidSalaryDuration", "IsPaid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_Loan", "UpdatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Loan", "EmployeeId", "dbo.tbl_Employee");
            DropIndex("dbo.tbl_Loan", new[] { "UpdatedBy" });
            DropIndex("dbo.tbl_Loan", new[] { "EmployeeId" });
            DropColumn("dbo.tbl_PaidSalaryDuration", "IsPaid");
            DropColumn("dbo.tbl_PaidSalaryDuration", "PaidDate");
            DropColumn("dbo.tbl_PaidSalaryDuration", "GenerateDate");
            DropTable("dbo.tbl_Loan");
        }
    }
}
