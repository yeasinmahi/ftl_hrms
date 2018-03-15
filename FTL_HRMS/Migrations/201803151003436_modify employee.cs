namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyemployee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_Employee", "EmergencyMobile", c => c.String(nullable: false, maxLength: 450));
            AlterColumn("dbo.tbl_Employee", "RelationEmergencyMobile", c => c.String(nullable: false, maxLength: 450));
            AlterColumn("dbo.tbl_Employee", "BloodGroup", c => c.String(nullable: false));
            AlterColumn("dbo.tbl_Employee", "MedicalHistory", c => c.String(nullable: false));
            AlterColumn("dbo.tbl_Employee", "Height", c => c.String(nullable: false));
            AlterColumn("dbo.tbl_Employee", "Weight", c => c.String(nullable: false));
            AlterColumn("dbo.tbl_Employee", "ExtraCurricularActivities", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_Employee", "ExtraCurricularActivities", c => c.String());
            AlterColumn("dbo.tbl_Employee", "Weight", c => c.String());
            AlterColumn("dbo.tbl_Employee", "Height", c => c.String());
            AlterColumn("dbo.tbl_Employee", "MedicalHistory", c => c.String());
            AlterColumn("dbo.tbl_Employee", "BloodGroup", c => c.String());
            AlterColumn("dbo.tbl_Employee", "RelationEmergencyMobile", c => c.String(maxLength: 450));
            AlterColumn("dbo.tbl_Employee", "EmergencyMobile", c => c.String(maxLength: 450));
        }
    }
}
