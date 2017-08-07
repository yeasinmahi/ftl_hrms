namespace FTL_HRMS.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class add_new_tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_BranchTransfer",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        FromBranchId = c.Int(nullable: false),
                        ToBranchId = c.Int(nullable: false),
                        TransferDate = c.DateTime(nullable: false),
                        Branch_Sl = c.Int(),
                        Employee_Sl = c.Int(),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Branch", t => t.Branch_Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.Employee_Sl)
                .Index(t => t.Branch_Sl)
                .Index(t => t.Employee_Sl);
            
            CreateTable(
                "dbo.tbl_Branch",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Sl);
            
            CreateTable(
                "dbo.tbl_DepartmentTransfer",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        FromDepartmentId = c.Int(nullable: false),
                        ToDepartmentId = c.Int(nullable: false),
                        TransferDate = c.DateTime(nullable: false),
                        Department_Sl = c.Int(),
                        Employee_Sl = c.Int(),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Department", t => t.Department_Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.Employee_Sl)
                .Index(t => t.Department_Sl)
                .Index(t => t.Employee_Sl);
            
            CreateTable(
                "dbo.tbl_DisciplinaryAction",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        DisciplinaryActionTypeId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Remarks = c.String(),
                        DisciplinaryActionType_Sl = c.Int(),
                        Employee_Sl = c.Int(),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_DisciplinaryActionType", t => t.DisciplinaryActionType_Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.Employee_Sl)
                .Index(t => t.DisciplinaryActionType_Sl)
                .Index(t => t.Employee_Sl);
            
            CreateTable(
                "dbo.tbl_DisciplinaryActionType",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Sl);
            
            CreateTable(
                "dbo.tbl_EmployeeType",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Sl);
            
            CreateTable(
                "dbo.tbl_PerformanceIssue",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Sl);
            
            CreateTable(
                "dbo.tbl_PerformanceRating",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        Rating = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        PerformanceIssueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Sl);
            
            CreateTable(
                "dbo.tbl_Resignation",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        ResignDate = c.DateTime(nullable: false),
                        Reason = c.String(nullable: false),
                        Suggestion = c.String(),
                        Status = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdateDate = c.DateTime(nullable: false),
                        Remarks = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        Employee_Sl = c.Int(),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.Employee_Sl)
                .Index(t => t.Employee_Sl);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_Resignation", "Employee_Sl", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_DisciplinaryAction", "Employee_Sl", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_DisciplinaryAction", "DisciplinaryActionType_Sl", "dbo.tbl_DisciplinaryActionType");
            DropForeignKey("dbo.tbl_DepartmentTransfer", "Employee_Sl", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_DepartmentTransfer", "Department_Sl", "dbo.tbl_Department");
            DropForeignKey("dbo.tbl_BranchTransfer", "Employee_Sl", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_BranchTransfer", "Branch_Sl", "dbo.tbl_Branch");
            DropIndex("dbo.tbl_Resignation", new[] { "Employee_Sl" });
            DropIndex("dbo.tbl_DisciplinaryAction", new[] { "Employee_Sl" });
            DropIndex("dbo.tbl_DisciplinaryAction", new[] { "DisciplinaryActionType_Sl" });
            DropIndex("dbo.tbl_DepartmentTransfer", new[] { "Employee_Sl" });
            DropIndex("dbo.tbl_DepartmentTransfer", new[] { "Department_Sl" });
            DropIndex("dbo.tbl_BranchTransfer", new[] { "Employee_Sl" });
            DropIndex("dbo.tbl_BranchTransfer", new[] { "Branch_Sl" });
            DropTable("dbo.tbl_Resignation");
            DropTable("dbo.tbl_PerformanceRating");
            DropTable("dbo.tbl_PerformanceIssue");
            DropTable("dbo.tbl_EmployeeType");
            DropTable("dbo.tbl_DisciplinaryActionType");
            DropTable("dbo.tbl_DisciplinaryAction");
            DropTable("dbo.tbl_DepartmentTransfer");
            DropTable("dbo.tbl_Branch");
            DropTable("dbo.tbl_BranchTransfer");
        }
    }
}
