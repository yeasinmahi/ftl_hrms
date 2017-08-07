namespace FTL_HRMS.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class basic_setup_by_manas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_Department",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(nullable: false, maxLength: 250),
                        DepartmentGroupId = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdateDate = c.DateTime(),
                        Status = c.Boolean(nullable: false),
                        DepartmentGroup_Sl = c.Int(),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_DepartmentGroup", t => t.DepartmentGroup_Sl)
                .Index(t => t.DepartmentGroup_Sl);
            
            CreateTable(
                "dbo.tbl_DepartmentGroup",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(nullable: false, maxLength: 250),
                        CreatedBy = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdateDate = c.DateTime(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Sl);
            
            CreateTable(
                "dbo.tbl_Designation",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(nullable: false, maxLength: 250),
                        DepartmentId = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdateDate = c.DateTime(),
                        Status = c.Boolean(nullable: false),
                        Department_Sl = c.Int(),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Department", t => t.Department_Sl)
                .Index(t => t.Department_Sl);
            
            CreateTable(
                "dbo.tbl_Education",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        InstituteName = c.String(nullable: false, maxLength: 250),
                        Program = c.String(nullable: false, maxLength: 250),
                        Board = c.String(maxLength: 250),
                        Result = c.String(nullable: false, maxLength: 250),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        Employee_Sl = c.Int(),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.Employee_Sl)
                .Index(t => t.Employee_Sl);
            
            CreateTable(
                "dbo.tbl_Employee",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(nullable: false, maxLength: 250),
                        FathersName = c.String(nullable: false, maxLength: 250),
                        MothersName = c.String(nullable: false, maxLength: 250),
                        PresentAddress = c.String(nullable: false, maxLength: 250),
                        PermanentAddress = c.String(nullable: false, maxLength: 250),
                        Mobile = c.String(nullable: false, maxLength: 450),
                        Email = c.String(maxLength: 250),
                        NIDorBirthCirtificate = c.String(nullable: false, maxLength: 250),
                        DrivingLicence = c.String(maxLength: 250),
                        PassportNumber = c.String(maxLength: 250),
                        DateOfBirth = c.DateTime(nullable: false),
                        DateOfJoining = c.DateTime(nullable: false),
                        SourceOfHireId = c.Int(nullable: false),
                        DesignationId = c.Int(nullable: false),
                        GrossSalary = c.Double(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdateDate = c.DateTime(),
                        Status = c.Boolean(nullable: false),
                        Designation_Sl = c.Int(),
                        SourceOfHire_Sl = c.Int(),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Designation", t => t.Designation_Sl)
                .ForeignKey("dbo.tbl_SourceOfHire", t => t.SourceOfHire_Sl)
                .Index(t => t.Designation_Sl)
                .Index(t => t.SourceOfHire_Sl);
            
            CreateTable(
                "dbo.tbl_Experience",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        InstituteName = c.String(nullable: false, maxLength: 250),
                        InstituteAddress = c.String(nullable: false, maxLength: 250),
                        Website = c.String(maxLength: 250),
                        Phone = c.String(maxLength: 450),
                        Designation = c.String(nullable: false, maxLength: 250),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        Employee_Sl = c.Int(),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.Employee_Sl)
                .Index(t => t.Employee_Sl);
            
            CreateTable(
                "dbo.tbl_Images",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        Image = c.Byte(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        Employee_Sl = c.Int(),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.Employee_Sl)
                .Index(t => t.Employee_Sl);
            
            CreateTable(
                "dbo.tbl_SourceOfHire",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Sl);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_Employee", "SourceOfHire_Sl", "dbo.tbl_SourceOfHire");
            DropForeignKey("dbo.tbl_Images", "Employee_Sl", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Experience", "Employee_Sl", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Education", "Employee_Sl", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Employee", "Designation_Sl", "dbo.tbl_Designation");
            DropForeignKey("dbo.tbl_Designation", "Department_Sl", "dbo.tbl_Department");
            DropForeignKey("dbo.tbl_Department", "DepartmentGroup_Sl", "dbo.tbl_DepartmentGroup");
            DropIndex("dbo.tbl_Images", new[] { "Employee_Sl" });
            DropIndex("dbo.tbl_Experience", new[] { "Employee_Sl" });
            DropIndex("dbo.tbl_Employee", new[] { "SourceOfHire_Sl" });
            DropIndex("dbo.tbl_Employee", new[] { "Designation_Sl" });
            DropIndex("dbo.tbl_Education", new[] { "Employee_Sl" });
            DropIndex("dbo.tbl_Designation", new[] { "Department_Sl" });
            DropIndex("dbo.tbl_Department", new[] { "DepartmentGroup_Sl" });
            DropTable("dbo.tbl_SourceOfHire");
            DropTable("dbo.tbl_Images");
            DropTable("dbo.tbl_Experience");
            DropTable("dbo.tbl_Employee");
            DropTable("dbo.tbl_Education");
            DropTable("dbo.tbl_Designation");
            DropTable("dbo.tbl_DepartmentGroup");
            DropTable("dbo.tbl_Department");
        }
    }
}
