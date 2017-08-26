namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsalarydistribution : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_SalaryDistribution",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        GrossSalary = c.Double(nullable: false),
                        BasicSalary = c.Double(nullable: false),
                        HouseRent = c.Double(nullable: false),
                        MedicalAllowance = c.Double(nullable: false),
                        LifeInsurance = c.Double(nullable: false),
                        FoodAllowance = c.Double(nullable: false),
                        Entertainment = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Sl);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tbl_SalaryDistribution");
        }
    }
}
