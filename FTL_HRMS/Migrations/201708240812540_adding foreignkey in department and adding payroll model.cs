namespace FTL_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingforeignkeyindepartmentandaddingpayrollmodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_LeaveCount", "Employee_Sl", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_LeaveCount", "LeaveType_Sl", "dbo.tbl_LeaveType");
            DropIndex("dbo.tbl_LeaveCount", new[] { "Employee_Sl" });
            DropIndex("dbo.tbl_LeaveCount", new[] { "LeaveType_Sl" });
            DropColumn("dbo.tbl_LeaveCount", "EmployeeId");
            DropColumn("dbo.tbl_LeaveCount", "LeaveTypeId");
            RenameColumn(table: "dbo.tbl_LeaveCount", name: "Employee_Sl", newName: "EmployeeId");
            RenameColumn(table: "dbo.tbl_LeaveCount", name: "LeaveType_Sl", newName: "LeaveTypeId");
            CreateTable(
                "dbo.tbl_Company",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Address = c.String(nullable: false),
                        Email = c.String(maxLength: 250),
                        Website = c.String(maxLength: 250),
                        Phone = c.String(maxLength: 450),
                        Mobile = c.String(maxLength: 450),
                        AlternativeMobile = c.String(maxLength: 450),
                        RegistrationNo = c.String(maxLength: 250),
                        RegistrationDate = c.DateTime(nullable: false),
                        TINNumber = c.String(maxLength: 250),
                        StartingDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Sl);
            
            CreateTable(
                "dbo.tbl_DeviceAttendance",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Datetime = c.DateTime(nullable: false),
                        IsCalculated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.tbl_EmployeeSalaryDistribution",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        GrossSalary = c.Double(nullable: false),
                        BasicSalary = c.Double(nullable: false),
                        HouseRent = c.Double(nullable: false),
                        MedicalAllowance = c.Double(nullable: false),
                        LifeInsurance = c.Double(nullable: false),
                        FoodAllowance = c.Double(nullable: false),
                        Entertainment = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.tbl_FilterAttendance",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        InTime = c.DateTime(nullable: false),
                        OutTime = c.DateTime(nullable: false),
                        IsCalculated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.tbl_Holiday",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Remarks = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Sl);
            
            CreateTable(
                "dbo.tbl_MonthlyAttendance",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Status = c.String(nullable: false),
                        IsCalculated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.tbl_MonthlySalarySheet",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        PaidSalaryDurationId = c.Int(nullable: false),
                        GrossSalary = c.Double(nullable: false),
                        BasicSalary = c.Double(nullable: false),
                        AbsentDay = c.Double(nullable: false),
                        LateDay = c.Double(nullable: false),
                        LatePenalty = c.Double(nullable: false),
                        DisciplinaryActionPenalty = c.Double(nullable: false),
                        OthersPenalty = c.Double(nullable: false),
                        FestivalBonus = c.Double(nullable: false),
                        PerformanceBonus = c.Double(nullable: false),
                        OthersBonus = c.Double(nullable: false),
                        NetPay = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_PaidSalaryDuration", t => t.PaidSalaryDurationId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.PaidSalaryDurationId);
            
            CreateTable(
                "dbo.tbl_PaidSalaryDuration",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        WorkingDay = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Sl);
            
            CreateTable(
                "dbo.tbl_Weekend",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        BranchId = c.Int(nullable: false),
                        Day = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Branch", t => t.BranchId, cascadeDelete: true)
                .Index(t => t.BranchId);
            
            AlterColumn("dbo.tbl_LeaveCount", "EmployeeId", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_LeaveCount", "LeaveTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.tbl_Department", "CreatedBy");
            CreateIndex("dbo.tbl_Department", "UpdatedBy");
            CreateIndex("dbo.tbl_LeaveCount", "EmployeeId");
            CreateIndex("dbo.tbl_LeaveCount", "LeaveTypeId");
            AddForeignKey("dbo.tbl_Department", "CreatedBy", "dbo.tbl_Employee", "Sl", cascadeDelete: false);
            AddForeignKey("dbo.tbl_Department", "UpdatedBy", "dbo.tbl_Employee", "Sl", cascadeDelete: false);
            AddForeignKey("dbo.tbl_LeaveCount", "EmployeeId", "dbo.tbl_Employee", "Sl", cascadeDelete: true);
            AddForeignKey("dbo.tbl_LeaveCount", "LeaveTypeId", "dbo.tbl_LeaveType", "Sl", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_LeaveCount", "LeaveTypeId", "dbo.tbl_LeaveType");
            DropForeignKey("dbo.tbl_LeaveCount", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Weekend", "BranchId", "dbo.tbl_Branch");
            DropForeignKey("dbo.tbl_MonthlySalarySheet", "PaidSalaryDurationId", "dbo.tbl_PaidSalaryDuration");
            DropForeignKey("dbo.tbl_MonthlySalarySheet", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_MonthlyAttendance", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_FilterAttendance", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_EmployeeSalaryDistribution", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_DeviceAttendance", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Department", "UpdatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Department", "CreatedBy", "dbo.tbl_Employee");
            DropIndex("dbo.tbl_Weekend", new[] { "BranchId" });
            DropIndex("dbo.tbl_MonthlySalarySheet", new[] { "PaidSalaryDurationId" });
            DropIndex("dbo.tbl_MonthlySalarySheet", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_MonthlyAttendance", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_LeaveCount", new[] { "LeaveTypeId" });
            DropIndex("dbo.tbl_LeaveCount", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_FilterAttendance", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_EmployeeSalaryDistribution", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_DeviceAttendance", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_Department", new[] { "UpdatedBy" });
            DropIndex("dbo.tbl_Department", new[] { "CreatedBy" });
            AlterColumn("dbo.tbl_LeaveCount", "LeaveTypeId", c => c.Int());
            AlterColumn("dbo.tbl_LeaveCount", "EmployeeId", c => c.Int());
            DropTable("dbo.tbl_Weekend");
            DropTable("dbo.tbl_PaidSalaryDuration");
            DropTable("dbo.tbl_MonthlySalarySheet");
            DropTable("dbo.tbl_MonthlyAttendance");
            DropTable("dbo.tbl_Holiday");
            DropTable("dbo.tbl_FilterAttendance");
            DropTable("dbo.tbl_EmployeeSalaryDistribution");
            DropTable("dbo.tbl_DeviceAttendance");
            DropTable("dbo.tbl_Company");
            RenameColumn(table: "dbo.tbl_LeaveCount", name: "LeaveTypeId", newName: "LeaveType_Sl");
            RenameColumn(table: "dbo.tbl_LeaveCount", name: "EmployeeId", newName: "Employee_Sl");
            AddColumn("dbo.tbl_LeaveCount", "LeaveTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_LeaveCount", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.tbl_LeaveCount", "LeaveType_Sl");
            CreateIndex("dbo.tbl_LeaveCount", "Employee_Sl");
            AddForeignKey("dbo.tbl_LeaveCount", "LeaveType_Sl", "dbo.tbl_LeaveType", "Sl");
            AddForeignKey("dbo.tbl_LeaveCount", "Employee_Sl", "dbo.tbl_Employee", "Sl");
        }
    }
}
