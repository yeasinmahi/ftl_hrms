--FTL_HRMS
delete from [dbo].[ApplicationUsers] where CustomUserId > 2
delete from [dbo].[IdentityUserRoles] where UserId != 'c5575472-d22a-421f-976c-02792e888d99' and UserId != '22424a78-fb6b-4db3-bf16-90747a08e92e'
delete from [dbo].[tbl_BonusAndPenalty]
DBCC CHECKIDENT ([tbl_BonusAndPenalty], RESEED, 0)
delete from [dbo].[tbl_BranchTransfer]
DBCC CHECKIDENT ([tbl_BranchTransfer], RESEED, 0)
delete from [dbo].[tbl_Company]
DBCC CHECKIDENT ([tbl_Company], RESEED, 0)
delete from [dbo].[tbl_DepartmentTransfer]
DBCC CHECKIDENT ([tbl_DepartmentTransfer], RESEED, 0)
delete from [dbo].[tbl_DeviceAttendance]
DBCC CHECKIDENT ([tbl_DeviceAttendance], RESEED, 0)
delete from [dbo].[tbl_DisciplinaryAction]
DBCC CHECKIDENT ([tbl_DisciplinaryAction], RESEED, 0)
delete from [dbo].[tbl_DisciplinaryActionType]
DBCC CHECKIDENT ([tbl_DisciplinaryActionType], RESEED, 0)
delete from [dbo].[tbl_Education]
DBCC CHECKIDENT ([tbl_Education], RESEED, 0)
delete from [dbo].[tbl_EmployeeLeaveCountHistory]
DBCC CHECKIDENT ([tbl_EmployeeLeaveCountHistory], RESEED, 0)
delete from [dbo].[tbl_EmployeeSalaryDistribution]
DBCC CHECKIDENT ([tbl_EmployeeSalaryDistribution], RESEED, 0)
delete from [dbo].[tbl_EmployeeType] where Name != 'Super Admin' and Sl > 4
DBCC CHECKIDENT ([tbl_EmployeeType], RESEED, 4)
delete from [dbo].[tbl_Experience]
DBCC CHECKIDENT ([tbl_Experience], RESEED, 0)
delete from [dbo].[tbl_FestivalBonus]
DBCC CHECKIDENT ([tbl_FestivalBonus], RESEED, 0)
delete from [dbo].[tbl_FileStorage]
DBCC CHECKIDENT ([tbl_FileStorage], RESEED, 0)
delete from [dbo].[tbl_FilterAttendance]
DBCC CHECKIDENT ([tbl_FilterAttendance], RESEED, 0)
delete from [dbo].[tbl_Holiday]
DBCC CHECKIDENT ([tbl_Holiday], RESEED, 0)
delete from [dbo].[tbl_Images]
DBCC CHECKIDENT ([tbl_Images], RESEED, 0)
delete from [dbo].[tbl_LeaveCount]
DBCC CHECKIDENT ([tbl_LeaveCount], RESEED, 0)
delete from [dbo].[tbl_LeaveHistory]
DBCC CHECKIDENT ([tbl_LeaveHistory], RESEED, 0)
delete from [dbo].[tbl_LeaveType] where Name != 'Without Pay' and Name != 'Earn' and Sl > 2
DBCC CHECKIDENT ([tbl_LeaveType], RESEED, 2)
delete from [dbo].[tbl_LoanCalculationHistory]
DBCC CHECKIDENT ([tbl_LoanCalculationHistory], RESEED, 0)
delete from [dbo].[tbl_LoanCalculation]
DBCC CHECKIDENT ([tbl_LoanCalculation], RESEED, 0)
delete from [dbo].[tbl_Loan]
DBCC CHECKIDENT ([tbl_Loan], RESEED, 0)
delete from [dbo].[tbl_MonthlyAttendance]
DBCC CHECKIDENT ([tbl_MonthlyAttendance], RESEED, 0)
delete from [dbo].[tbl_PaidSalaryDuration]
DBCC CHECKIDENT ([tbl_PaidSalaryDuration], RESEED, 0)
delete from [dbo].[tbl_MonthlySalarySheet]
DBCC CHECKIDENT ([tbl_MonthlySalarySheet], RESEED, 0)
delete from [dbo].[tbl_PerformanceRating]
DBCC CHECKIDENT ([tbl_PerformanceRating], RESEED, 0)
delete from [dbo].[tbl_PerformanceIssue]
DBCC CHECKIDENT ([tbl_PerformanceIssue], RESEED, 0)
delete from [dbo].[tbl_PromotionHistory]
DBCC CHECKIDENT ([tbl_PromotionHistory], RESEED, 0)
delete from [dbo].[tbl_Resignation]
DBCC CHECKIDENT ([tbl_Resignation], RESEED, 0)
delete from [dbo].[tbl_SalaryAdjustment]
DBCC CHECKIDENT ([tbl_SalaryAdjustment], RESEED, 0)
delete from [dbo].[tbl_SalaryDistribution]
DBCC CHECKIDENT ([tbl_SalaryDistribution], RESEED, 0)
delete from [dbo].[tbl_Weekend]
DBCC CHECKIDENT ([tbl_Weekend], RESEED, 0)
delete from [dbo].[tbl_SourceOfHire] where Name != 'Super Admin' and Name != 'Bd Jobs' and Sl > 2
DBCC CHECKIDENT ([tbl_SourceOfHire], RESEED, 2)
delete from [dbo].[tbl_Designation] where Name != 'Super Admin'
DBCC CHECKIDENT ([tbl_Designation], RESEED, 1)
delete from [dbo].[tbl_Department] where Name != 'Super Admin'
DBCC CHECKIDENT ([tbl_Department], RESEED, 1)
delete from [dbo].[tbl_DepartmentGroup] where Name != 'Super Admin'
DBCC CHECKIDENT ([tbl_DepartmentGroup], RESEED, 1)
delete from [dbo].[tbl_Branch] where Name != 'Super Admin'
DBCC CHECKIDENT ([tbl_Branch], RESEED, 1)
delete from [dbo].[tbl_Employee] where Name != 'System Admin' and Name != 'Super Admin'
DBCC CHECKIDENT ([tbl_Employee], RESEED, 2)


--ZKAccess
Update [dbo].[CHECKINOUT] Set IsCalculated = 0