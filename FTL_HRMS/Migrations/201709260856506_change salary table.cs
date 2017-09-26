namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesalarytable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_MonthlySalarySheet", "AbsentPanelty", c => c.Double(nullable: false));
            AddColumn("dbo.tbl_MonthlySalarySheet", "UnofficialDay", c => c.Double(nullable: false));
            AddColumn("dbo.tbl_MonthlySalarySheet", "UnofficialPenalty", c => c.Double(nullable: false));
            AddColumn("dbo.tbl_MonthlySalarySheet", "LeavePenalty", c => c.Double(nullable: false));
            DropColumn("dbo.tbl_MonthlySalarySheet", "DisciplinaryActionPenalty");
            DropColumn("dbo.tbl_MonthlySalarySheet", "PerformanceBonus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_MonthlySalarySheet", "PerformanceBonus", c => c.Double(nullable: false));
            AddColumn("dbo.tbl_MonthlySalarySheet", "DisciplinaryActionPenalty", c => c.Double(nullable: false));
            DropColumn("dbo.tbl_MonthlySalarySheet", "LeavePenalty");
            DropColumn("dbo.tbl_MonthlySalarySheet", "UnofficialPenalty");
            DropColumn("dbo.tbl_MonthlySalarySheet", "UnofficialDay");
            DropColumn("dbo.tbl_MonthlySalarySheet", "AbsentPanelty");
        }
    }
}
