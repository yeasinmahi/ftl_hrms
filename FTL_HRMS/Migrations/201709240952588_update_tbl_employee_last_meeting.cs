namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_tbl_employee_last_meeting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Employee", "EmergencyMobile", c => c.String(nullable: false, maxLength: 450));
            AddColumn("dbo.tbl_Employee", "RelationEmergencyMobile", c => c.String(nullable: false, maxLength: 450));
            AddColumn("dbo.tbl_Employee", "BloodGroup", c => c.String(nullable: false));
            AddColumn("dbo.tbl_Employee", "MedicalHistory", c => c.String());
            AddColumn("dbo.tbl_Employee", "Height", c => c.Double(nullable: false));
            AddColumn("dbo.tbl_Employee", "Weight", c => c.Double(nullable: false));
            AddColumn("dbo.tbl_Employee", "ExtraCurricularActivities", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_Employee", "ExtraCurricularActivities");
            DropColumn("dbo.tbl_Employee", "Weight");
            DropColumn("dbo.tbl_Employee", "Height");
            DropColumn("dbo.tbl_Employee", "MedicalHistory");
            DropColumn("dbo.tbl_Employee", "BloodGroup");
            DropColumn("dbo.tbl_Employee", "RelationEmergencyMobile");
            DropColumn("dbo.tbl_Employee", "EmergencyMobile");
        }
    }
}
