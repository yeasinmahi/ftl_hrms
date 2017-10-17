namespace FTL_HRMS.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_BonusAndPenalty",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Type = c.String(nullable: false),
                        Amount = c.Double(nullable: false),
                        Remarks = c.String(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.CreatedBy, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.tbl_Employee", t => t.UpdatedBy)
                .Index(t => t.EmployeeId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.tbl_Employee",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 15),
                        Name = c.String(nullable: false, maxLength: 250),
                        FathersName = c.String(nullable: false, maxLength: 250),
                        MothersName = c.String(nullable: false, maxLength: 250),
                        Gender = c.String(nullable: false, maxLength: 250),
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
                        EmployeeTypeId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                        GrossSalary = c.Double(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdateDate = c.DateTime(),
                        IsSystemOrSuperAdmin = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                        ProbationStatus = c.Boolean(nullable: false),
                        IsSpecialEmployee = c.Boolean(nullable: false),
                        ParmanentDate = c.DateTime(),
                        EmergencyMobile = c.String(maxLength: 450),
                        RelationEmergencyMobile = c.String(maxLength: 450),
                        BloodGroup = c.String(),
                        MedicalHistory = c.String(),
                        Height = c.String(),
                        Weight = c.String(),
                        ExtraCurricularActivities = c.String(),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Branch", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Employee", t => t.CreatedBy)
                .ForeignKey("dbo.tbl_Designation", t => t.DesignationId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_EmployeeType", t => t.EmployeeTypeId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_SourceOfHire", t => t.SourceOfHireId, cascadeDelete: true)
                .Index(t => t.Code, unique: true)
                .Index(t => t.SourceOfHireId)
                .Index(t => t.DesignationId)
                .Index(t => t.EmployeeTypeId)
                .Index(t => t.BranchId)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.tbl_Branch",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Address = c.String(nullable: false),
                        OpeningTime = c.DateTime(nullable: false),
                        EndingTime = c.DateTime(nullable: false),
                        IsLateCalculated = c.Boolean(nullable: false),
                        LateConsiderationTime = c.Double(),
                        LateConsiderationDay = c.Double(),
                        LateDeductionPercentage = c.Double(),
                        IsOvertimeCalculated = c.Boolean(nullable: false),
                        OvertimeConsiderationTime = c.Double(),
                        OvertimePaymentPercentage = c.Double(),
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
                        RoleName = c.String(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdateDate = c.DateTime(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.CreatedBy, cascadeDelete: false)
                .ForeignKey("dbo.tbl_Department", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Employee", t => t.UpdatedBy)
                .Index(t => t.DepartmentId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
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
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.CreatedBy, cascadeDelete: false)
                .ForeignKey("dbo.tbl_DepartmentGroup", t => t.DepartmentGroupId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Employee", t => t.UpdatedBy)
                .Index(t => t.DepartmentGroupId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
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
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.CreatedBy, cascadeDelete: false)
                .ForeignKey("dbo.tbl_Employee", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
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
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.tbl_EmployeeType",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Sl);
            
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
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.tbl_Images",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        Image = c.Binary(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.tbl_SourceOfHire",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Sl);
            
            CreateTable(
                "dbo.tbl_BranchTransfer",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        FromBranchId = c.Int(nullable: false),
                        ToBranchId = c.Int(nullable: false),
                        TransferDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Branch", t => t.FromBranchId, cascadeDelete: false)
                .ForeignKey("dbo.tbl_Branch", t => t.ToBranchId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.FromBranchId)
                .Index(t => t.ToBranchId);
            
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
                "dbo.tbl_DepartmentTransfer",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        FromDesignationId = c.Int(nullable: false),
                        ToDesignationId = c.Int(nullable: false),
                        TransferDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Designation", t => t.FromDesignationId, cascadeDelete: false)
                .ForeignKey("dbo.tbl_Designation", t => t.ToDesignationId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.FromDesignationId)
                .Index(t => t.ToDesignationId);
            
            CreateTable(
                "dbo.tbl_DeviceAttendance",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeCode = c.String(maxLength: 15),
                        CheckTime = c.DateTime(nullable: false),
                        IsCalculated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Sl);
            
            CreateTable(
                "dbo.tbl_DisciplinaryAction",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        DisciplinaryActionTypeId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_DisciplinaryActionType", t => t.DisciplinaryActionTypeId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.DisciplinaryActionTypeId);
            
            CreateTable(
                "dbo.tbl_DisciplinaryActionType",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Sl);
            
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
                "dbo.tbl_FestivalBonus",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        BasedOn = c.String(nullable: false),
                        BonusPersentage = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Remarks = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Sl);
            
            CreateTable(
                "dbo.tbl_FileStorage",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Path = c.String(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.CreatedBy, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.CreatedBy);
            
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
                "dbo.tbl_LeaveCount",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        LeaveTypeId = c.Int(nullable: false),
                        AvailableDay = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_LeaveType", t => t.LeaveTypeId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.LeaveTypeId);
            
            CreateTable(
                "dbo.tbl_LeaveType",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Day = c.Double(nullable: false),
                        IsEditable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Sl);
            
            CreateTable(
                "dbo.tbl_LeaveHistory",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        LeaveTypeId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        Day = c.Int(nullable: false),
                        Cause = c.String(nullable: false, maxLength: 250),
                        UpdatedBy = c.Int(),
                        UpdateDate = c.DateTime(),
                        Status = c.String(),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_LeaveType", t => t.LeaveTypeId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Employee", t => t.UpdatedBy)
                .Index(t => t.EmployeeId)
                .Index(t => t.LeaveTypeId)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentItemId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 30),
                        ControllerName = c.String(nullable: false),
                        ActionName = c.String(nullable: false),
                        AllFunctions = c.String(),
                        ViewNames = c.String(),
                        Remarks = c.String(),
                        MenuOrder = c.Int(nullable: false),
                        IcnClass = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_MonthlyAttendance",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Status = c.String(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdateDate = c.DateTime(),
                        IsCalculated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Employee", t => t.UpdatedBy)
                .Index(t => t.EmployeeId)
                .Index(t => t.UpdatedBy);
            
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
                        AbsentPanelty = c.Double(nullable: false),
                        LateDay = c.Double(nullable: false),
                        LatePenalty = c.Double(nullable: false),
                        UnofficialDay = c.Double(nullable: false),
                        UnofficialPenalty = c.Double(nullable: false),
                        LeavePenalty = c.Double(nullable: false),
                        OthersPenalty = c.Double(nullable: false),
                        FestivalBonus = c.Double(nullable: false),
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
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_PerformanceIssue", t => t.PerformanceIssueId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.PerformanceIssueId);
            
            CreateTable(
                "dbo.tbl_PromotionHistory",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        FromDesignationId = c.Int(nullable: false),
                        ToDesignationId = c.Int(nullable: false),
                        PromotionDate = c.DateTime(nullable: false),
                        FromSalary = c.Double(nullable: false),
                        ToSalary = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Designation", t => t.FromDesignationId, cascadeDelete: false)
                .ForeignKey("dbo.tbl_Designation", t => t.ToDesignationId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.FromDesignationId)
                .Index(t => t.ToDesignationId);
            
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
                        UpdatedBy = c.Int(),
                        UpdateDate = c.DateTime(),
                        Remarks = c.String(),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Employee", t => t.UpdatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.RolePermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.String(maxLength: 128),
                        MenuItemIdList = c.String(),
                        CanView = c.Boolean(nullable: false),
                        CanEdit = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityRoles", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.tbl_SalaryAdjustment",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        Remarks = c.String(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Sl)
                .ForeignKey("dbo.tbl_Employee", t => t.CreatedBy, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Employee", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.tbl_Employee", t => t.UpdatedBy)
                .Index(t => t.EmployeeId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.tbl_SalaryDistribution",
                c => new
                    {
                        Sl = c.Int(nullable: false, identity: true),
                        BasicSalary = c.Double(nullable: false),
                        HouseRent = c.Double(nullable: false),
                        MedicalAllowance = c.Double(nullable: false),
                        LifeInsurance = c.Double(nullable: false),
                        FoodAllowance = c.Double(nullable: false),
                        Entertainment = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Sl);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CustomUserId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        RoleId = c.String(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_Weekend", "BranchId", "dbo.tbl_Branch");
            DropForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.tbl_SalaryAdjustment", "UpdatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_SalaryAdjustment", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_SalaryAdjustment", "CreatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.RolePermissions", "RoleId", "dbo.IdentityRoles");
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.tbl_Resignation", "UpdatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Resignation", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_PromotionHistory", "ToDesignationId", "dbo.tbl_Designation");
            DropForeignKey("dbo.tbl_PromotionHistory", "FromDesignationId", "dbo.tbl_Designation");
            DropForeignKey("dbo.tbl_PromotionHistory", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_PerformanceRating", "PerformanceIssueId", "dbo.tbl_PerformanceIssue");
            DropForeignKey("dbo.tbl_PerformanceRating", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_MonthlySalarySheet", "PaidSalaryDurationId", "dbo.tbl_PaidSalaryDuration");
            DropForeignKey("dbo.tbl_MonthlySalarySheet", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_MonthlyAttendance", "UpdatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_MonthlyAttendance", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_LeaveHistory", "UpdatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_LeaveHistory", "LeaveTypeId", "dbo.tbl_LeaveType");
            DropForeignKey("dbo.tbl_LeaveHistory", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_LeaveCount", "LeaveTypeId", "dbo.tbl_LeaveType");
            DropForeignKey("dbo.tbl_LeaveCount", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_FilterAttendance", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_FileStorage", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_FileStorage", "CreatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_EmployeeSalaryDistribution", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_DisciplinaryAction", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_DisciplinaryAction", "DisciplinaryActionTypeId", "dbo.tbl_DisciplinaryActionType");
            DropForeignKey("dbo.tbl_DepartmentTransfer", "ToDesignationId", "dbo.tbl_Designation");
            DropForeignKey("dbo.tbl_DepartmentTransfer", "FromDesignationId", "dbo.tbl_Designation");
            DropForeignKey("dbo.tbl_DepartmentTransfer", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_BranchTransfer", "ToBranchId", "dbo.tbl_Branch");
            DropForeignKey("dbo.tbl_BranchTransfer", "FromBranchId", "dbo.tbl_Branch");
            DropForeignKey("dbo.tbl_BranchTransfer", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_BonusAndPenalty", "UpdatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_BonusAndPenalty", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_BonusAndPenalty", "CreatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Employee", "SourceOfHireId", "dbo.tbl_SourceOfHire");
            DropForeignKey("dbo.tbl_Images", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Experience", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Employee", "EmployeeTypeId", "dbo.tbl_EmployeeType");
            DropForeignKey("dbo.tbl_Education", "EmployeeId", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Employee", "DesignationId", "dbo.tbl_Designation");
            DropForeignKey("dbo.tbl_Designation", "UpdatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Designation", "DepartmentId", "dbo.tbl_Department");
            DropForeignKey("dbo.tbl_Department", "UpdatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Department", "DepartmentGroupId", "dbo.tbl_DepartmentGroup");
            DropForeignKey("dbo.tbl_DepartmentGroup", "UpdatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_DepartmentGroup", "CreatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Department", "CreatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Designation", "CreatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Employee", "CreatedBy", "dbo.tbl_Employee");
            DropForeignKey("dbo.tbl_Employee", "BranchId", "dbo.tbl_Branch");
            DropIndex("dbo.tbl_Weekend", new[] { "BranchId" });
            DropIndex("dbo.IdentityUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.tbl_SalaryAdjustment", new[] { "UpdatedBy" });
            DropIndex("dbo.tbl_SalaryAdjustment", new[] { "CreatedBy" });
            DropIndex("dbo.tbl_SalaryAdjustment", new[] { "EmployeeId" });
            DropIndex("dbo.IdentityUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.RolePermissions", new[] { "RoleId" });
            DropIndex("dbo.tbl_Resignation", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_Resignation", new[] { "UpdatedBy" });
            DropIndex("dbo.tbl_PromotionHistory", new[] { "ToDesignationId" });
            DropIndex("dbo.tbl_PromotionHistory", new[] { "FromDesignationId" });
            DropIndex("dbo.tbl_PromotionHistory", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_PerformanceRating", new[] { "PerformanceIssueId" });
            DropIndex("dbo.tbl_PerformanceRating", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_MonthlySalarySheet", new[] { "PaidSalaryDurationId" });
            DropIndex("dbo.tbl_MonthlySalarySheet", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_MonthlyAttendance", new[] { "UpdatedBy" });
            DropIndex("dbo.tbl_MonthlyAttendance", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_LeaveHistory", new[] { "UpdatedBy" });
            DropIndex("dbo.tbl_LeaveHistory", new[] { "LeaveTypeId" });
            DropIndex("dbo.tbl_LeaveHistory", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_LeaveCount", new[] { "LeaveTypeId" });
            DropIndex("dbo.tbl_LeaveCount", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_FilterAttendance", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_FileStorage", new[] { "CreatedBy" });
            DropIndex("dbo.tbl_FileStorage", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_EmployeeSalaryDistribution", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_DisciplinaryAction", new[] { "DisciplinaryActionTypeId" });
            DropIndex("dbo.tbl_DisciplinaryAction", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_DepartmentTransfer", new[] { "ToDesignationId" });
            DropIndex("dbo.tbl_DepartmentTransfer", new[] { "FromDesignationId" });
            DropIndex("dbo.tbl_DepartmentTransfer", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_BranchTransfer", new[] { "ToBranchId" });
            DropIndex("dbo.tbl_BranchTransfer", new[] { "FromBranchId" });
            DropIndex("dbo.tbl_BranchTransfer", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_Images", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_Experience", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_Education", new[] { "EmployeeId" });
            DropIndex("dbo.tbl_DepartmentGroup", new[] { "UpdatedBy" });
            DropIndex("dbo.tbl_DepartmentGroup", new[] { "CreatedBy" });
            DropIndex("dbo.tbl_Department", new[] { "UpdatedBy" });
            DropIndex("dbo.tbl_Department", new[] { "CreatedBy" });
            DropIndex("dbo.tbl_Department", new[] { "DepartmentGroupId" });
            DropIndex("dbo.tbl_Designation", new[] { "UpdatedBy" });
            DropIndex("dbo.tbl_Designation", new[] { "CreatedBy" });
            DropIndex("dbo.tbl_Designation", new[] { "DepartmentId" });
            DropIndex("dbo.tbl_Employee", new[] { "CreatedBy" });
            DropIndex("dbo.tbl_Employee", new[] { "BranchId" });
            DropIndex("dbo.tbl_Employee", new[] { "EmployeeTypeId" });
            DropIndex("dbo.tbl_Employee", new[] { "DesignationId" });
            DropIndex("dbo.tbl_Employee", new[] { "SourceOfHireId" });
            DropIndex("dbo.tbl_Employee", new[] { "Code" });
            DropIndex("dbo.tbl_BonusAndPenalty", new[] { "UpdatedBy" });
            DropIndex("dbo.tbl_BonusAndPenalty", new[] { "CreatedBy" });
            DropIndex("dbo.tbl_BonusAndPenalty", new[] { "EmployeeId" });
            DropTable("dbo.tbl_Weekend");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.tbl_SalaryDistribution");
            DropTable("dbo.tbl_SalaryAdjustment");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.RolePermissions");
            DropTable("dbo.tbl_Resignation");
            DropTable("dbo.tbl_PromotionHistory");
            DropTable("dbo.tbl_PerformanceRating");
            DropTable("dbo.tbl_PerformanceIssue");
            DropTable("dbo.tbl_PaidSalaryDuration");
            DropTable("dbo.tbl_MonthlySalarySheet");
            DropTable("dbo.tbl_MonthlyAttendance");
            DropTable("dbo.MenuItems");
            DropTable("dbo.tbl_LeaveHistory");
            DropTable("dbo.tbl_LeaveType");
            DropTable("dbo.tbl_LeaveCount");
            DropTable("dbo.tbl_Holiday");
            DropTable("dbo.tbl_FilterAttendance");
            DropTable("dbo.tbl_FileStorage");
            DropTable("dbo.tbl_FestivalBonus");
            DropTable("dbo.tbl_EmployeeSalaryDistribution");
            DropTable("dbo.tbl_DisciplinaryActionType");
            DropTable("dbo.tbl_DisciplinaryAction");
            DropTable("dbo.tbl_DeviceAttendance");
            DropTable("dbo.tbl_DepartmentTransfer");
            DropTable("dbo.tbl_Company");
            DropTable("dbo.tbl_BranchTransfer");
            DropTable("dbo.tbl_SourceOfHire");
            DropTable("dbo.tbl_Images");
            DropTable("dbo.tbl_Experience");
            DropTable("dbo.tbl_EmployeeType");
            DropTable("dbo.tbl_Education");
            DropTable("dbo.tbl_DepartmentGroup");
            DropTable("dbo.tbl_Department");
            DropTable("dbo.tbl_Designation");
            DropTable("dbo.tbl_Branch");
            DropTable("dbo.tbl_Employee");
            DropTable("dbo.tbl_BonusAndPenalty");
        }
    }
}
