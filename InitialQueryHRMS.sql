USE [master]
GO
/****** Object:  Database [FTL_HRMS]    Script Date: 10/23/2017 3:41:21 PM ******/
CREATE DATABASE [FTL_HRMS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FTL_HRMS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\FTL_HRMS.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FTL_HRMS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\FTL_HRMS_log.ldf' , SIZE = 2112KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [FTL_HRMS] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FTL_HRMS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FTL_HRMS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FTL_HRMS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FTL_HRMS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FTL_HRMS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FTL_HRMS] SET ARITHABORT OFF 
GO
ALTER DATABASE [FTL_HRMS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FTL_HRMS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FTL_HRMS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FTL_HRMS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FTL_HRMS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FTL_HRMS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FTL_HRMS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FTL_HRMS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FTL_HRMS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FTL_HRMS] SET  ENABLE_BROKER 
GO
ALTER DATABASE [FTL_HRMS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FTL_HRMS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FTL_HRMS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FTL_HRMS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FTL_HRMS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FTL_HRMS] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [FTL_HRMS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FTL_HRMS] SET RECOVERY FULL 
GO
ALTER DATABASE [FTL_HRMS] SET  MULTI_USER 
GO
ALTER DATABASE [FTL_HRMS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FTL_HRMS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FTL_HRMS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FTL_HRMS] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [FTL_HRMS] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'FTL_HRMS', N'ON'
GO
USE [FTL_HRMS]
GO
/****** Object:  User [FUTURISTICTECH\Administrator]    Script Date: 10/23/2017 3:41:21 PM ******/
CREATE USER [FUTURISTICTECH\Administrator] FOR LOGIN [FUTURISTICTECH\Administrator] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [FUTURISTICTECH\Administrator]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [FUTURISTICTECH\Administrator]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [FUTURISTICTECH\Administrator]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [FUTURISTICTECH\Administrator]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [FUTURISTICTECH\Administrator]
GO
ALTER ROLE [db_datareader] ADD MEMBER [FUTURISTICTECH\Administrator]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [FUTURISTICTECH\Administrator]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ApplicationUsers]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationUsers](
	[Id] [nvarchar](128) NOT NULL,
	[CustomUserId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[RoleId] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.ApplicationUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityRoles]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.IdentityRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityUserClaims]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](max) NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[ApplicationUser_Id] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.IdentityUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityUserLogins]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUserLogins](
	[UserId] [nvarchar](128) NOT NULL,
	[LoginProvider] [nvarchar](max) NULL,
	[ProviderKey] [nvarchar](max) NULL,
	[ApplicationUser_Id] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.IdentityUserLogins] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityUserRoles]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUserRoles](
	[RoleId] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[IdentityRole_Id] [nvarchar](128) NULL,
	[ApplicationUser_Id] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.IdentityUserRoles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MenuItems]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ParentItemId] [int] NULL,
	[Name] [nvarchar](30) NOT NULL,
	[ControllerName] [nvarchar](max) NOT NULL,
	[ActionName] [nvarchar](max) NOT NULL,
	[AllFunctions] [nvarchar](max) NULL,
	[ViewNames] [nvarchar](max) NULL,
	[Remarks] [nvarchar](max) NULL,
	[MenuOrder] [int] NOT NULL,
	[IcnClass] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.MenuItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RolePermissions]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePermissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](128) NULL,
	[MenuItemIdList] [nvarchar](max) NULL,
	[CanView] [bit] NOT NULL,
	[CanEdit] [bit] NOT NULL,
	[CanDelete] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.RolePermissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_BonusAndPenalty]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_BonusAndPenalty](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[Amount] [float] NOT NULL,
	[Remarks] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.tbl_BonusAndPenalty] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Branch]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Branch](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[OpeningTime] [datetime] NOT NULL,
	[EndingTime] [datetime] NOT NULL,
	[IsLateCalculated] [bit] NOT NULL,
	[LateConsiderationTime] [float] NULL,
	[LateConsiderationDay] [float] NULL,
	[LateDeductionPercentage] [float] NULL,
	[IsOvertimeCalculated] [bit] NOT NULL,
	[OvertimeConsiderationTime] [float] NULL,
	[OvertimePaymentPercentage] [float] NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_Branch] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_BranchTransfer]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_BranchTransfer](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[FromBranchId] [int] NOT NULL,
	[ToBranchId] [int] NOT NULL,
	[TransferDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_BranchTransfer] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Company]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Company](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](250) NULL,
	[Website] [nvarchar](250) NULL,
	[Phone] [nvarchar](450) NULL,
	[Mobile] [nvarchar](450) NULL,
	[AlternativeMobile] [nvarchar](450) NULL,
	[RegistrationNo] [nvarchar](250) NULL,
	[RegistrationDate] [datetime] NOT NULL,
	[TINNumber] [nvarchar](250) NULL,
	[StartingDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_Company] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Department]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Department](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NULL,
	[Name] [nvarchar](250) NOT NULL,
	[DepartmentGroupId] [int] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdateDate] [datetime] NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_Department] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_DepartmentGroup]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_DepartmentGroup](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NULL,
	[Name] [nvarchar](250) NOT NULL,
	[CreatedBy] [int] NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdateDate] [datetime] NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_DepartmentGroup] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_DepartmentTransfer]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_DepartmentTransfer](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[FromDesignationId] [int] NOT NULL,
	[ToDesignationId] [int] NOT NULL,
	[TransferDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_DepartmentTransfer] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Designation]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Designation](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NULL,
	[Name] [nvarchar](250) NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[RoleName] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdateDate] [datetime] NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_Designation] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_DeviceAttendance]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_DeviceAttendance](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode] [nvarchar](15) NULL,
	[UserId] [int] NOT NULL,
	[CheckTime] [datetime] NOT NULL,
	[IsCalculated] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_DeviceAttendance] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_DisciplinaryAction]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_DisciplinaryAction](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[DisciplinaryActionTypeId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Remarks] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.tbl_DisciplinaryAction] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_DisciplinaryActionType]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_DisciplinaryActionType](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_dbo.tbl_DisciplinaryActionType] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Education]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Education](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[InstituteName] [nvarchar](250) NOT NULL,
	[Program] [nvarchar](250) NOT NULL,
	[Board] [nvarchar](250) NULL,
	[Result] [nvarchar](250) NOT NULL,
	[FromDate] [datetime] NOT NULL,
	[ToDate] [datetime] NOT NULL,
	[EmployeeId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_Education] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Employee]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Employee](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](15) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[FathersName] [nvarchar](250) NOT NULL,
	[MothersName] [nvarchar](250) NOT NULL,
	[Gender] [nvarchar](250) NOT NULL,
	[PresentAddress] [nvarchar](250) NOT NULL,
	[PermanentAddress] [nvarchar](250) NOT NULL,
	[Mobile] [nvarchar](450) NOT NULL,
	[Email] [nvarchar](250) NULL,
	[NIDorBirthCirtificate] [nvarchar](250) NOT NULL,
	[DrivingLicence] [nvarchar](250) NULL,
	[PassportNumber] [nvarchar](250) NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[DateOfJoining] [datetime] NOT NULL,
	[SourceOfHireId] [int] NOT NULL,
	[DesignationId] [int] NOT NULL,
	[EmployeeTypeId] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[GrossSalary] [float] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdateDate] [datetime] NULL,
	[IsSystemOrSuperAdmin] [bit] NOT NULL,
	[Status] [bit] NOT NULL,
	[ProbationStatus] [bit] NOT NULL,
	[IsSpecialEmployee] [bit] NOT NULL,
	[ParmanentDate] [datetime] NULL,
	[EmergencyMobile] [nvarchar](450) NULL,
	[RelationEmergencyMobile] [nvarchar](450) NULL,
	[BloodGroup] [nvarchar](max) NULL,
	[MedicalHistory] [nvarchar](max) NULL,
	[Height] [nvarchar](max) NULL,
	[Weight] [nvarchar](max) NULL,
	[ExtraCurricularActivities] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.tbl_Employee] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_EmployeeLeaveCountHistory]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_EmployeeLeaveCountHistory](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[PaidSalaryDurationId] [int] NOT NULL,
	[EarnLeaveDays] [float] NOT NULL,
	[WithoutPayLeaveDays] [float] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_EmployeeLeaveCountHistory] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_EmployeeSalaryDistribution]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_EmployeeSalaryDistribution](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[GrossSalary] [float] NOT NULL,
	[BasicSalary] [float] NOT NULL,
	[HouseRent] [float] NOT NULL,
	[MedicalAllowance] [float] NOT NULL,
	[LifeInsurance] [float] NOT NULL,
	[FoodAllowance] [float] NOT NULL,
	[Entertainment] [float] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_EmployeeSalaryDistribution] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_EmployeeType]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_EmployeeType](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_EmployeeType] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Experience]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Experience](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[InstituteName] [nvarchar](250) NOT NULL,
	[InstituteAddress] [nvarchar](250) NOT NULL,
	[Website] [nvarchar](250) NULL,
	[Phone] [nvarchar](450) NULL,
	[Designation] [nvarchar](250) NOT NULL,
	[FromDate] [datetime] NOT NULL,
	[ToDate] [datetime] NOT NULL,
	[EmployeeId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_Experience] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_FestivalBonus]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_FestivalBonus](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[BasedOn] [nvarchar](max) NOT NULL,
	[BonusPersentage] [float] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Remarks] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.tbl_FestivalBonus] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_FileStorage]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_FileStorage](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[Path] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_FileStorage] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_FilterAttendance]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_FilterAttendance](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[InTime] [datetime] NOT NULL,
	[OutTime] [datetime] NOT NULL,
	[IsCalculated] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_FilterAttendance] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Holiday]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Holiday](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Remarks] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.tbl_Holiday] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Images]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Images](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[Image] [varbinary](max) NOT NULL,
	[EmployeeId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_Images] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_LeaveCount]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_LeaveCount](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[LeaveTypeId] [int] NOT NULL,
	[AvailableDay] [float] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_LeaveCount] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_LeaveHistory]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_LeaveHistory](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[LeaveTypeId] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[FromDate] [datetime] NOT NULL,
	[ToDate] [datetime] NOT NULL,
	[Day] [int] NOT NULL,
	[Cause] [nvarchar](250) NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdateDate] [datetime] NULL,
	[Status] [nvarchar](max) NULL,
	[Remarks] [nvarchar](max) NULL,
	[IsSeen] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_LeaveHistory] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_LeaveType]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_LeaveType](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Day] [float] NOT NULL,
	[IsEditable] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_LeaveType] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Loan]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Loan](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[LoanAmount] [float] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LoanReason] [nvarchar](max) NOT NULL,
	[LoanDuration] [int] NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[Remarks] [nvarchar](max) NULL,
	[IsSeen] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_Loan] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_LoanCalculation]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_LoanCalculation](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[LoanId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[LoanAmount] [float] NOT NULL,
	[LoanDuration] [int] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_LoanCalculation] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_LoanCalculationHistory]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_LoanCalculationHistory](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[PaidSalaryDurationId] [int] NOT NULL,
	[LoanCalculationId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[LoanAmount] [float] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_LoanCalculationHistory] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_MonthlyAttendance]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_MonthlyAttendance](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdateDate] [datetime] NULL,
	[IsCalculated] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_MonthlyAttendance] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_MonthlySalarySheet]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_MonthlySalarySheet](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[PaidSalaryDurationId] [int] NOT NULL,
	[GrossSalary] [float] NOT NULL,
	[BasicSalary] [float] NOT NULL,
	[AbsentDay] [float] NOT NULL,
	[AbsentPanelty] [float] NOT NULL,
	[LateDay] [float] NOT NULL,
	[LatePenalty] [float] NOT NULL,
	[UnofficialDay] [float] NOT NULL,
	[UnofficialPenalty] [float] NOT NULL,
	[LeavePenalty] [float] NOT NULL,
	[OthersPenalty] [float] NOT NULL,
	[FestivalBonus] [float] NOT NULL,
	[OthersBonus] [float] NOT NULL,
	[AdjustmentAmount] [float] NOT NULL,
	[LoanAmount] [float] NOT NULL,
	[NetPay] [float] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_MonthlySalarySheet] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_PaidSalaryDuration]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_PaidSalaryDuration](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[FromDate] [datetime] NOT NULL,
	[ToDate] [datetime] NOT NULL,
	[WorkingDay] [float] NOT NULL,
	[GenerateDate] [datetime] NOT NULL,
	[PaidDate] [datetime] NULL,
	[IsPaid] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_PaidSalaryDuration] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_PerformanceIssue]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_PerformanceIssue](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_dbo.tbl_PerformanceIssue] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_PerformanceRating]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_PerformanceRating](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[Rating] [float] NOT NULL,
	[Date] [datetime] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[PerformanceIssueId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_PerformanceRating] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_PromotionHistory]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_PromotionHistory](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[FromDesignationId] [int] NOT NULL,
	[ToDesignationId] [int] NOT NULL,
	[PromotionDate] [datetime] NOT NULL,
	[FromSalary] [float] NOT NULL,
	[ToSalary] [float] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_PromotionHistory] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Resignation]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Resignation](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[ResignDate] [datetime] NOT NULL,
	[Reason] [nvarchar](max) NOT NULL,
	[Suggestion] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdateDate] [datetime] NULL,
	[Remarks] [nvarchar](max) NULL,
	[EmployeeId] [int] NOT NULL,
	[IsSeen] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_Resignation] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_SalaryAdjustment]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_SalaryAdjustment](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Amount] [float] NOT NULL,
	[Remarks] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.tbl_SalaryAdjustment] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_SalaryDistribution]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_SalaryDistribution](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[BasicSalary] [float] NOT NULL,
	[HouseRent] [float] NOT NULL,
	[MedicalAllowance] [float] NOT NULL,
	[LifeInsurance] [float] NOT NULL,
	[FoodAllowance] [float] NOT NULL,
	[Entertainment] [float] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_SalaryDistribution] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_SourceOfHire]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_SourceOfHire](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_SourceOfHire] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Weekend]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Weekend](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[BranchId] [int] NOT NULL,
	[Day] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_Weekend] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[BranchTransferView]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[BranchTransferView] 
AS
select bt.EmployeeId as Code, e.Name,fb.Name as FromDesignation,tb.Name as ToDesignation,bt.TransferDate 
from tbl_BranchTransfer as bt 
join tbl_Branch as fb on fb.Sl = bt.FromBranchId 
join tbl_Branch as tb on tb.Sl =bt.ToBranchId
join tbl_Employee as e on e.Sl = bt.EmployeeId
GO
/****** Object:  View [dbo].[DepartmentTransferView]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[DepartmentTransferView] 
AS
select dt.EmployeeId as Code, e.Name,fd.Name as FromDesignation,td.Name as ToDesignation,dt.TransferDate from tbl_DepartmentTransfer as dt 
join tbl_Designation as fd on fd.Sl = dt.FromDesignationId 
join tbl_Designation as td on td.Sl =dt.ToDesignationId
join tbl_Employee as e on e.Sl = dt.EmployeeId
GO
/****** Object:  View [dbo].[PromotionHistoryView]    Script Date: 10/23/2017 3:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[PromotionHistoryView]
as
select e.Name, e.Code, fd.Name as FromDesignation, td.Name as ToDesignation, p.PromotionDate,p.FromSalary,p.ToSalary from tbl_PromotionHistory as p 
join tbl_Designation as fd on p.FromDesignationId=fd.Sl
join tbl_Designation as td on p.ToDesignationId = td.Sl
join tbl_Employee as e on e.Sl = p.EmployeeId
GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201710221005477_InitialCreate', N'FTL_HRMS.Migrations.Configuration', 0x1F8B0800000000000400ED7DDD72DCB892E6FD46EC3B2874B53BD163D9DDE74C9CE9B0674296AC769D912D85A46EEF9D83AA82241EB3C81A92A5B662629F6C2FF691F61516208B247E12408204FFCA151DE15611894422F121F19748FCBFFFF37FDFFEFBF77574F44CD22C4CE277C76F5EBD3E3E22F1325985F1E3BBE36DFEF0CF7F3BFEF77FFBEFFFEDED87D5FAFBD11F15DD2F8C8EE68CB377C74F79BEF9F5E4245B3E917590BD5A87CB34C99287FCD532599F04ABE4E4E7D7AFFFF5E4CD9B1342591C535E47476F6FB6711EAE49F183FE3C4BE225D9E4DB20FA94AC4894EDBED394DB82EBD1E7604DB24DB024EF8E2FEE2EBF7EBCF974FBEAFCF4F2F8E8340A032AC32D891E8E8F82384EF220A712FEFA7B466EF334891F6F37F44310DDBD6C08A57B08A28CEC24FFB521C756E2F5CFAC12274DC68AD5729BE5C9DA91E19B5F765A3991B3B7D2ED71AD35AAB70F54BFF90BAB75A1BB77C7EF93789B9DC6AB6B120751FE727C2497F9EB5994327A4EC36563BCBA0E5ED2248A5E492C7E3AAA087FAA8141F1C3FEFBE9E86C1BE5DB94BC8BC9364F83E8A7A3EBED7D142EFF83BCDC25DF48FC2EDE46112F301599A6091FE8A7EB34D990347FB9210FBB6ADC46C7472762BE1339639D8DCB53D66C11E7BFFC7C7CF499161EDC47A4C603A785DB3C49C96F2426699093D57590E724A5CDB9589142A34AE952591FD69B28792164B1B29569E6734E4BAF38B0BFEF686F7166C2B2574C686FA05DFAF8E853F0FD92C48FF9D3BB63FAE7F1D145F89DACAA2F3BAEBFC721B50034539E6EDD4B3D5D27B477D7C227B4E1DD99DC50BCA7DFB2E1A53F4B096BF8F72FDDDAAF64E3A5157FDFAC408930B96001A49C9F83E7F0B1403F58890AD2C74737242AC8B2A77053DA5CD9267C95B35CA4C9FA2689540324517EBD4DB6E992C99AA0C8EF82F491E4F8AA385502233E5AF0D622972DE824B89C452FBE4869AD84440E55E5ED4933E61847A2463CFC10F4317D55653B0C3B90B9A16A32D8CA377FEDC354B27F0D85FEFCD7D77D947A11E44FB4D9C729FC533262E1141E2B920E5FEE754A320AC3D3D58AFE611A92FB2A9FA4D4488C29C1A7E43E8C4C2DFE977ECAFDB00EC2C843752DFD78719EA4EFC3347F3AA3FF840FE1929B350CA7E4F3347CA6455D864BBA00F551BE055541966D9234FFBC5DDF7BE955F609FDD543A1E5CE33C292D7DF93302E64EDC8AD1CFBAF1E3E8669F7650BC9C2C7B818D2BAB2AAC67BC6A32BAFF769102F9FBA72F98DAEBDB3DB200AD2978EAB1BDD22637FD61452CE4576FB92E5647D95DE6EE9F7D3D53A8C2B1EEF133AE50C6277DCD2A9D336EBCA85FEBC2FF0EA871DADE8862CC3206A66B91DE50B76835F1BB57F58133A518F972FDE0630DBAABD5CA20C5DEEFB284956B47B6E371E360C2C5301B2A2E363F431A44B97C60EF456DC47123E3EE5BD17F36598623E7CA7ABB2B36D9A86CB2D35A3A7CB9C0EF979487CECF36097D5E558002EA7EBF56D45D3AC9FA52465C12CA7BB2EF6115B2D7511FA3D160D895EDA8EBB2ADC706F16592004E4E5D2F5C2F244CEFB3FABEDD22067950A6EFDA8A975F1B58C2A49550DD71DAA72D3D6A44C9112D0264FA057A740E5ACCFEFF457584ED42159EB6458A36AB2AA5280C655A78B75F0C88C0B206199044A27252992C9E9AE52F1136E734B8B94404BF304FA9616A83AEDDD5586D169E7AECC74D8B703CA1A6723C9BE8FD2D3E1CAD586B0256B3975EDB884F910AF7CB15A649734EB5910B159096DC6AE73F6821BEDD1E18AC182C2449072B7647464711E288B4E3B877342C7A5C202116A00E29CDA2D37268BECEA99FEA4E2FBD34ECDB19B862A36D7C1CB9A56AD6D0DDB2CFBD0E65298F638D94C2EE7C1700265590E3CFC2C57C6B1CEE76413A439C374D70D2B360DB0D461E053F4BDDDE06AB97BD4653DC82F87F44B423D95325F3490BA2F0C2B0C5B45E74961B11B0AA3C81C590F47ED7C49FA63763D9551749FC7EBBC3E1D879C2AE361C4818CD38F30E2147BA85D879D83F11FC2F8D7C6CE64FB35448031D251B6B7FCBBFD78B3F00A3928BD4465125F26F5BDA5D97AE89277912C239CD7A1ABE66E1AB934442665F7336EED90D072F02A721F4630C8C2EEEF08761872861C724ACB8A1977204A834101C9BDAF3D3A0E3EAA21B78E53BD18F3B2088C458728ADADE0D5E5B7397D73F3F9ADF21DEC3950D622CE28D936B7EDF1F4E6169A3CA6C17AF882DF2741BAF250ACCD9725A3B81AC1BF9976603FB77F122F6C5ADF686A7717C3F9285E39F5D49FD677BAAE509EB6B7BAB2C0080E160C286B1CC3D5EB910FEF18E106963AE3012A4059230F7675F1A3DD41F842EEB3D08B5FBE65587F4A6253297E3C36856D96C3103BEC10EBEE9CA50CB20607AE5666B3F2D472329965A683B984ECD59AF384781FC6C59D05CC66CFB4A1EBE0B5274356E7D5D70AAEA2639E1368F9AC07E80265EDE1A4B0F480BCA3FF660FECAE590BF7C92AF301323EAD103031F07357EC2EF1C467D7ECEDE6192DC321088083A321C0246A1C010D9DEB9E6ED3301889796AADCC0D914D6A8ED255EE0A0618A91B5AADCC15894DE29AAED3207796AC3741DC2A54D02EEBC1580165FD603EDFC3DC61DFAFD5F140D7264F23865C0AEA6732D805D1C730CB4B97EFCFC9007BF84D717E16FC8BCF03C50BA0F3D134A7AC5B88DDC2E5A2E5D4546570B0F8A009F4373DF51861E12EF1C96C8489AA0A3F70B26A20339C82FB99B4DADDC994F2946CC69A48D4980AC959DC27B52D6A256532D649A0C5D448CCD0D10DED395C9253DAC3E355E0788A53CD7E651E07A368308A7E429759FCAC329276F66A7E22CB6F9EAE1BB6BD4C874771982DC34D54ECFB9E2EDBDC3F53181C406C0071E7B8498ABA19B7490491F5178915EDC0072A031E6814D2AFBADCDC8883CDA40E3DE89C5EE39302C582F31C3D19A6269EDCB835EAEF687D0E2E349AB286D84D72F695BA24C1331DD8B7715EC7496AE538A5F039400028CBD720741D84AB32BCDDF936F5B228FC10A471D186E7C14B3D82B48C99F725CC9F926D4EE7D8AD39B67497D08111F69EB0526BE3B418B2B80E276A533A560D6280A9A49ACFA1BA40E64E63115409F7D59CCAE5608580B2A6E654F52549BF15FBA75D8375562AF122150353BB009A2C675F3EABBB1EB94339DB320FEFB76DFB8B9EDBA1DF0065F91ABDFD45A77D1F64E1D20BA78FC9362337A4F36B20BBA09BA75194FC1970C1A15BB2BB0C1FC822CEB6A9075E1749B2F22517ED98F45710C6EB162AEB36B9513BAB71766320D78EF7A63C9D86F90B92E5E17310158F56B4B15802838391824D02595D995CB57BF21E289AE49A36031CDB6BDE1B627E46EF8B3022AC790BF5382DB3B99C07D40365F95B583741FFC78FC1D58A8DE7635DC475730E9C86ABE67A2A651C32907ADDADE5CBB1898C12D6D32819326F9F6E079B328F83D5E8D16A7819AC16B19723CBAB6D3EF2D16797EE286056D7276122A8636A283BF5CE8F4914AE8256BEB6BBAC87BED8571F9ADA84AFD938759CEF35190F6001CAF265B80B35FB38BF3F7D0EC282BEC526663B63D92004349340B26220211AD7F955AD419B901C2128659D6E12B321EA64C23959DC7BE5E17C5B53D638B725BA9F1A2CB20FAB300FEE1BA77ACF9BF6056CDA1DAFF3590FA003CA9ADC48E03152DDD4CEE9B8AED65237C1361BC1448C1BF1AFB7F88DBDB9FB29F6F19610F737003B4C6B4C5E1B20013C67E8EC9B8198DC5465E8A7370A855958F314C7242D2224A150923E1EA181CC2CBCCF48849749D0EA509BE53B0C954059DE864AAAE0D375B9A8ED34F1F2385432996E4890196314F5B47BCFCA6EBC96BA68D6DBC0D16A806C33D4B51B5AF773E4A23080472C3E41B59F426A1F369F1560B0F56A322CA36FDB5E6D2FB7F45D92581C2CBEC632F97ADC7B32A3460773DBBE637348D3F6718806EC4A20A1F3DCB4981E21C42D09F5A2169DDB266641E4B3CBB7D80ED1F4FCC3EE88BE2CFFF70424E5CFDFBA78B109C605AB99D4D6F5BA2F62E5B1165F1D25ABB556B2DD40564ECED6D3250A4DE9E61B14E84CD8CAFABE3BF189C4DB454ED62EB6B4CA338AD5649DDDD56ADA0D844FAB995242A61EA5D44E2720BFF4F3A04A12E76C5C24A9A5F89E168FE5ADCF91CA8EA28B6D5C08D0FF6AF28F90FCC9FEDA9F9D5D6605AED2551323A9DDE0BB58C66751E02562593B03487BC053F4D2CD614E6172985002654DCA636EDCEDB2E1CE934670C5537A0338B5D55329732103690F7B5E6A69FA0D301B2DA22A3EB7C676DCCB09E2ED13214EAE639235E3B81CCC598FE6CCFF3A7B8A77044FEFD9DD9EEE0E37259FEB202651DE95D76561537D70B92671D05D9EDFE3E4E1215C8641D45DAA86971FD98AA35A3FACAEF2276A43FCF0922E057A90CB07A7D3D53FA8D565D73BBDED5A7B61F499B0C01AC3EC72A9A388691A0091E9064F90B6A75D1FA048F38E0F2A03A666DEA36490F42149D76CCEB1C8B2ADD33AE763FA4ACE7E981240FD6B002FD6360D7E13E4852C6D5BBCCC7F6872A0AC4AB59DECF2B8CF4BC99651EAE903BDF9A3C00D1C2EF4548A4D35903A0F168AED445540CD66AA884C8DA89092A5DB0841854B5A9EAF327B21653F988B1E3BE9946384D740F0E6C8EE65057A97B463D3D29C49BD01B6663A22B5EF6B29FB0857AE946608566EA3B557A5FF40E54A91DA30E5664A7B5D3C8628BFE12574B2C45CCE831106CA2AF5E3E9D2F0387EC3B7DBC747B6F5E1A5EC49DC48F1E8D03DF419CB5047AEBE260F83BA3673C6081C03A174C5CE82443D9CFDF0E5E84F7DF45446C17D9EF430EE74F6BF0EB3CC717C10731E7C78A0FE4C55D49407BD56F1F3DF3C7950948E429774F6D0BF810D62E685E2DCED5536ECCAB30736E724228DB1ED6E8658B3C1BD5A80FCD7928EEBCE6AB2DA8F019A6E2F61EFB0580AA3EFBE9FC2659A64C943FEEA34DB7C26F9AB2AE3AB92E5454AD9FD99A4DF5EF11C7F3A42E76BFAFDCFD87EFFCB9BFB875FFEF6D77F0956BFFCCB5FC82F7F1DDE0674E893AE47267E7CD3D06354461B037E139B6BDEAF3B32EE596C25557D195B25F18260C6CA3F8A2BAED34772355A9CD849AB078A5C81EF6D40729D480B0F2AF5552EFE39F662FFA6395C6DE3DC22F3382C9081B226E5A9E7E5E47BA0E05EBA65EDA8E139875E1D7709072A774F434C500BA932FED9E8BD4607550A43D5002F7B9F8B60A530FD4AD8426AAF87CF35B19F770D0EEF196043851F5E0FC0F29AC6EB01E88E74BAD950081730613340975E24659DE1F6D270B3EBB342AB3E1E2D5D64EC22D5B3FB5E8AEB3AC3D73E761046C3947296C40F61BA6E71F741F117CC32BAC85C7D0C321F41DE2D472E64B94D699FBCCD83F5A6F7D2AE9F929858DFBBF75E96B7A6B9FB33B908967438FB10B35C9DF95D26CB6FC936FF10174F38FD9E2F5DCF836A065EC4395D2E49965D503093D519BF146A672B98BD197853EB2C0AC235BCAB258D175F2BD2668A095328334B0D99FB85F7C730C6895A91EA452D29ACA2EEC85C4565CC7092EE28F582160456394B2A6F7B86450BF9DF342CD84E7FD770EA6760D6AD3F4F8752ACB55819C394F447106D7D17D50AFC459FF70FFE82EDF4C1DF761F7C98FD686840A76AA5DF9EC3D51073A45D41541B6341F50B21B4AD576DF64F76590F9B264059EFD966C253F75DF5177546DAFAD4BC94099C47EC9AF26B45D24C20C41465E62025BB4C194EB32C59868524D23E6B2586582F3A4B3F121F2054842A7B10FF0C22ED481481ECE9F92515E1DDF13F29EAD2B3AD2AC3B1ADF423327D732C03F66AE7F27054863D614E10D93258A98D4575B312BF508C131658260CD89236A3BD268C73B54384F132DC04915974291BB22731B1EA02E49473B2A1ED4DE533B705A6E4A68FA8E5D7C548EAB269E7ED09072B24DAA483072B3C74A70F9ED0A739AC40B07FFDEAD51BA5846E788285191257B0B63112700772A3008CF70F47624C9F058299E0D98E479AA19051C066976700BCD9353F0FC86D82143C4ED583419303065C45EC86375D1123C1CD22CE2068B3687D5E60FB2D4DB61B77C441D9CCB02B72B4C31E58D8C80034C934280A4D2D3147284A7E0C5874E87C1A7A81A2C621625428C2328D0045B8253082704E4F630FC132581003A4AC873E0661B90C007916988FBFE0B55767D8515CD36E1821A4ACA3AD87B9DA389B4F27CBD912B6D3B297533095B3B692CD728B430666C9CAD5BF9F35315780D1344ED12A9A6A31F02A5A6D27376B38A221D45E0DC5C007630A3B63741AC6D026CFC0889BAF39AC65E6A161DD2906E35578DA8586825BE0203CBE19345562C8AD6BA07D70265008D0330E2057DBA574935F0B198514046345E5844695B583A59B0010B5F20F01436DBB600AE72F868D6B12AB3F0ADF21ABDDE2A9BD1B4581B90189A59BD3E4D068AAC69076116A23174CB27CE3E1F23B952A2442B07B2D70545A10933599132A01E6F3B28EFA0A0C81467DDBCCC23E2ED6C123C9EC1894E820FC95242ED89399CE0A771AE107C09CA62D66813749E8AB878F618A188F796AEFE3B1C01CC0A020EAE47068ACC690E331D446A8F2B97CA3E1B288E27F1AAF76AF0C604FA1CDD920A44A395C006B296C56D613579701E08B6B418C20639F5ECB357106EF40B0DD0FC04E00AAB39C00C895406E8D9BB3F58CD8296C93E3641A0185F3DD2E2F3DC7EFE8BF19E589B097303D083E81D4097B9A42E6652CCD951802A5E6B69A87A914EBC06ED358AEB46873F48A51AE98195D76B156627894AAED8591A1C93515A4DE256E38ADE87B45695DC87C312A57617884CA2D8591A0CA33019734FC586FC863764B6B83545361B31AF711151900B388B69BC5F80FD4437E57C50151DA67567A44B1EEA596B9B880E0AB340EAC356D8A9D354CC14504A895F8D88E03DC346FEFF40870F8F99E19C31BACD038E0065B1337E19804B0C36CC91A3B66014C97A52FABF2C9E88D82E600425DA174823ABA6808F37035A7077FD74A0ED10F5CDB1C2313CC614A3DC33E1FD7E71904FD339D8FDB2B320EA667391FAF04289EB62F222B2A6F35EA1065CF6AF26A5072B5717330143D2B4CE3EB3300B4F1ED3A7384038FD8BB03CEF4B0FD60A8078400F00FD477463D415FC751FB84BEFD3162A9B947EF276AEC7FFC5060C86BEA15D0A305EEDDC254F82C4703448506843EA26D67311E5C841161210C8347747C337D1608D51CB50B8C0D85CC0ABDF67A0C005A7B8B618418DBFB8DAF8513487B86E7FC81393224E76A3873929EE6392D28405DF2D1E6D0E052207604A7A6A0B921D45C8D61606A6EB35960B599A7DB510AD042F86CC85C9009319F15260D1518008D86B6991B0E8B3F8D672310712F486CB803506CE49C3216952A0C0B46A57D30C5D799C685237A8317A4D602B2C586165CC0FCCCE3889BB5C6369A8F89AC6A8034920A796FA89CB5A9D456626860EE83B9445EEA31E4E90DA453B8CB83106868D4CDF716CF65122076DD052A105C49E0B4932E329CD7380C893E04E0A03698C7B8CB24C71A3595D61BDE2661BCF4820C85A1791BABB3205A6E236458345D061DA4385A577481C5CCCEB0996A31103E4DED351B73C757A2B05558E8143DB45F741645406B0CC0A64E0F95BCF42320926F1FD4D282D24F0589F8ED18733E043EDBAC332C85CED9968EB96D836BCB395AD67ACD2FD9464784C95D7C4874CB656BCCB2C1F24F16EB9AAA8D07794D3B63AD3897756A1DC0C1BD14CD61D06EB03F5EA5CE151CAF3FEC8D3FE9A724CE9FA217177F117D1608F70AB50BE40D45CD6A4A63AFC70050B6B7DB2C26326A3590DB63B68C03A0770ABB6858A94641E47CF7D7767529CDFAED1321089F26431E031839F21668040B9BA33135556438EC9ADA6E4EE694AF87C39418957B102CEFCF34D8A972E3207D6FA6BFD7247D48D2351B7B6EE8F7F8D16EB4F559209C2BD42E303714352B8B6DAFC70030B6B7DB2CECB55A0DEECB22CBB62EC895B30E8060A548C84ACB359A01A275F51A05D9BA7645596729EF78484F9375E274F6A2CD01E25A227682B5B6A079D9655B358600AFADCDE66195E55A60C3E5D932F68EDCB907CAC356680C28EF41903CA54EB81079E66CBD837ADEC1F170D51903D0330F8C77C3096F9D4C40C410706FF450326116643FABD983A9060380D3D43EB39833F015401E61E8B3F484CC291C58D8E519186DF33DA45830F6B479589DBEFE9E9134D3424D25055F5EE4A85008301600C0ABA26204502112905DBA1FE546D7BAEB30CB58A316CCB55D42A5053B9C40E6D4E780020CDA40ABBB5D87D30B8301396C50F13D4DAF6B4CE98C7E34935EEE6A9FAEFEB1CD8A88C2D8D045967C10D6E42C2E68B31537AB6908B232038C11C856C44832764C23A52AEE101E0CBC7B02DB29007696F368A516C8C9B4255FEFB89DC2DC1A29D418589CEF2CFB74C35AAAF8C2A6AD5FCFA2205CEB67DA3039043F89D271C2AD29C732E92EA8FCCDBA65212E93C730C6EBA624EF5F37BB722CBA29A8FAD30D130DAF9A82BA7FCD94C50CBA52FB42C837DA67BF5A9E7613C9204DEC285CACB6C474462FB8C1920F60C9E176C0143CE86B6D1F0AB4D23C39CD41D29D081F6F3EDD9EDFB3AFE43B6DE7251D8D927510C7495E30F895A2FB2C4A59F367EF8EF314F0B6605C6F495E55497E0BB82430BD16AC6051E4D84C0A14564D928547A9686A5C00717670B670100E50142642AA9553F5FE0EC8A84944F3F92D4DB61B23B31D854DD3ABED5257432E0DD95E6504226D9B95C9365EDF698F0949E187AE72E2122D7C16EBE0116CFD2AC192BFECD3570F1FC31492444C4661B179154B83C886C0C2EF2C596F8218EA69750A1A4906A920222BDFE77049F8BB04005799C4C613786E46E50A1039F3D5E0574788EC15C0530BDA2E02D05A4A81DC7D15F61011527A2824BE567C88D852CE05C9F2F039888A3102602DA5DBB8F131A5555E7CAA9D9314051862279158787E4CA2701540ED5FA7583834F88074C5C7E3C4302AC6781D1F14C6B93057A19E1516CB6588099547F1199157B8070DB21128DC38EA3BAF8ED0C2FF1389B78B9CAC018E4D928D877A794B65A6D2E0B80A3717746C05229BAD529C5E554BA590E07956BEBB26A6158D8DABE81602635BF5E7B170BD31CE286F1C6694F2319CCA4C2240F003274BC2699C6DCA252D8F4D0C9B25B46D1EA6EC47AA73318504C5D332A6B518CBCAE3669595B23DE1A0C8DDCE9A5993BB1D2C07B6BB4D2933DBDDE68F856DBDE5A0F0AA53240EDC72559D70547B20471C9534CD00B749843D0D89B2DE29A965E71799CA4A5CCFA7DA1CE1F8340B4D799343ACA68B0AA43355932A4CC7AF70553407AFAD55A3395945F06BA122DE39CFAE2503B5BE62FA4C90AE0CFB0058AE7D6BAC5AC5A114A62336D54C930756976EB703C973286515FB274E1A037360AA086534EB0EDCDC71E13EAC16A5D34A84164DE79BC67A6A0E383D6951739ED97BC7556436F65C991AD5CDE4AA7AE9BB32534061B61DCB4E7A73015E1BCC39C10DABB48141D60C4BBCA8E6B194A3C48D785CF53C8DA11C4723AA3CEB0883289CCFB3B666184CB9EB6B5054D5C508921A66B1DA0B37F09413BA68D37AFE0ADDA3C129BB8D66AA4395AF260C0154868A28C4A04E74A739365E0321453C243240452044342F4FDF1D2C0237836AC0DDD316EAA90FBCCC7801C80C9551A941C5680FDBACDCFAC54C79826754884CA2175FA28414019F181AB90CD469C4534843A711081130E7E9BB771A811BA01AE361AABB7A247703C422CE92435F4573464873165F0817EEFD824C2EDC457F2D34D797CE46D2967D9E68C981AFA17DBED8457F83CE1B453F0833E434A4869AC139408519FD31505C075514BB4AAFDF36D713A3ABD5E4F1AB2E8E6FBF5BEB52B977095A5D3529BA52550EBFAAAAB9F6AB28D5E9C7D80D4DE498DD0E5477B43B22A1B90FB5652720DCB20A46E472AAAC2EBE8A578DEA82AAF4B6720664100375E0346B08EE61AB321CE0C3AB56E1A81EFDE954F16CFBAA737603948BCE6C50019607A86EABCF5FCBB220BDE37C00BD3481D1DE1AC89DAA6EB4B79D153BF04258719B34EF96D873D917B5FACCA6F5B2D5BFD3B5ACB1F40B7994BA68DA168017A9074324DE1EB56F08C4CB958A70BA6DDF22AA77120AF2A66C764D18729BD46EF7A4722EAD5FD8737EC288CD2303B5BE92FA4C90260D8ECB58AEC3690CAB2B472DF9D6CF709A119CC46DEAD1101BEB04E7D128CAE8B28EE4DCAFCA1A836B541644A6AF0C400D294870B037E8066237985638FF7CA35A348FD8EB2AA23E62DF4D31EA93F532BFDD05043FBAC14CFD60424B6D30133CF922844D43034FE48432EDF0512991D5B143A8858E46C2917D0BDF448EAC9B7DF3BEADD606DDB817DF898754A57F485EAC02F8943CAF0EE0329081C300D546E0C4F6C2B95A01042E508A181C05E003DC1A9DD81FEB562A647CAE5BD28EE1B2178EEFB0DA2A1BD4AE29F5C168636D8427A33D69487823DA82C9EE9A410DEC2E6F199BEA861AEC5117FE9C4A19146ABA9771F17A35BEA98BA9B8EE55DD5EB4AC7B4617DF1BFCE91CB771D6F225578C32705B66FE943FE26699E1E54F40E7D87742854A235E0AE56A69BD8A8BE5DDAFB9B0BE3F89D21E7AC2837EB5D2A326079D17991E4ED4ABD2FECE225447E34B8BAA020DD7B7D1DC07571ECE86B678F6CF56659CEDECAEDA116DA6E1B93840C7D8C7E5840A239E97E36B69BBBC8FE5DD2F4C118F96A1F4677EEACC5257ED63671EF5A97DDD0C2E038EE2D042BFDAA7B220B5E2DED5126B6A7D598BAF20101FC2A445EB635A3D81D2F666134677785F24F44B4F1E3539B81F92E5B9208C46B13E48C827863C6A7360FF23F0B51A4083F6576D846A19DFB5E12A6388B862E7D76FCF35BC9C62D10F7ADA8D7869A5A3AE069D64030F7F4017832CCF830875313C10C26BA60C7563BA1DA47F0784BF66A486CFE90EA3447D0104C20F40666862955AA7177D08222B474043BEB4627BC302D090D3B31742DDB00F5FF0F7A12C018D9CF8F7DBEBF4AF286094D8467DFD296E2C95D9EDBB53FC7F7325ED96BE9B1207B5F99A30F480063101EB858A5942D673F5D98D35062559C2D26BC680AA32DEB554C5FEB26B090A5D6FAC9914BCBE9396A400F51A2D5595F1AEA5DD606357524988AF5741EF474525AB81E612528C7A4033A628F6423D3471ECB90A6842C799B8F8B917C562D3B3FC75D4F43AEDEDC92DE5B50E761FDE9E509225D9E4DB20FA94AC489455099F82CD268C1FB326E7EECBD1ED265852C1CFFEF9F6F8E8FB3A8AB377C74F79BEF9F5E4242B5867AFD6E1324DB2E4217FB54CD627C12A39F9F9F5EB7F3D79F3E6645DF238590A587B2B495B97547A394AA92C74F48A5C8469969F0779701F6454EB67ABB54226C688177557EBB72A0B0E03AFB61BCBC63C75AA7CECEF9D93E4DDE55756E4ABF3D34B255ABCC4A851E705AD211B738ACA12BED96D1C288FDB251BB9AA00FDDCB3006749B45DC7FA6702F4B9AB818D45F7E7B9F0DFF1DC680311914FF905CFA174F1E2394077564C1C4ED76530619E47F50DCFE586223BFD96896CEA8F783EDC8B613C27C34362365EAA8EF9EF786EDC733C3C33C32B3D365EAA64FC7795DBDB13A953C8DDEF44E97F924594BB35AAD31B66A0C8DEAEE380E8E6FAACFDF46F663B25B424EAE326260EEC5F9143F905CFE122C89FE82C41652424E0F97D4A34FC84043CBFDFE880CCAE3DF2ACAA6F782ED729C9688B9FAE56F40FC970C8690E5C093B92D0F255525DB4781F468A02CB6F2EE3471046F2D0517C7240D8E23C49DF8769FE7446FF091FD86C51861C4CE23032A5E133ED9797E1B20C0E248C51529A43EB0459B649D2FCF3767D2F23484E731B45AF1E8ADAAA83699DE0CAEFEF49181727672AC73A09CF930F7F23CF1DE4340749C5D7E80549CD0FD59B105A5A5C66CB75B39C2A0DCFB5791389E7A77F2949CFE9373A77CECA2D09C904F1098719878F19878EDB22BB7DC972B2BE4A6FB7F4FBE96ACD6288F37C610A87FE9207F956B2DED537973126B92FD77F003B25D1A9FE1B42979651B3CF25555E4E76B191BB314A6D2C29C9A54F13BA6E8E972FD020A624BACCFA4B3F40237F2D9183F5889264B58B1D2BD80FEEBBC3484E56743C8C6A7F47614497D2F05C3F92F0F1495A4755DFF05CBE005CBE3873F9F03D4F83B36D9A86CC53336517F69FC3BC3829165A5E4F3699D547BDBDD37EAF01DCA9C26C316832F6B3F2E8BE6E00A7BC2D66BA571BC2A63777A12C9090E080C67805B2E3BFBBD8DE4B6AFD2A2764B2924DAF9C8AE75CE44CE22CA42B99C262A9226B483A94711EBC588A3857DF8CB295704E56DB2248075DF0D0497A5EDC99960B01895CDAE1EA99FEA4F5D7B70544E180C32AB7B94D0C64EE655D072FCC16E8F46620EB6B6E3392DD357B2F214DAF8109C2FE1A73EFEFF64F13324A5DD3F1290ED3B42422AA5CCDD7C362A9CFC5D24CBABB21DA3FBAB7EB78A03ABB3EF38FD0D78B558CBEC3D7C987BE7AE8ABD6B75F9C3B2CC8C8A9D76A38EC6FD73D74AF3DED5ECD2B1AED0F3C752C30279EFABCFD74A6459CD1C2B739304594929C365E1FD360AD6CB8961F1D36FE922095CF0CCA4F2E9B94D93692B6D4AA6F0E87B2EC0E8AD20D9AAF0E2E1B89CAA7FAD6B723CAC82E049A30438E6E04101707570238FB5437F5E661329B6764DAB7AE9607A66D0D99E76335EB8CE04EAE9AEA72AC709F85B2D1A93F3AD8F5A724960FA5CA4FAD0EADB547D607CB3C50CFDDBD7BD4BAD7C2F9113D5697B1A7DEBA56B672779FF6B45D2D8F41215BD7C405D1C6E6EC8711B743FB4A6F8C743C1CD5F1411F92EA19F4D3CA7EDDB09B176A647E628ACB4802F3E3BF3B70DB6917189D8494C9A0F32C596F82B8C3FD000D03041EB539A76A6E7C9DDAFBF02C9DD234D18FBFED290B6FCDE694CF04620824BB6C2C3CB2C0F5C584F573226F308869EDB8AA1D5E4D7530238BCF90E32DF7D969904C59A8185542316532260978B7C7C3BE79878113C3642E83A7C10F1948765A90E9392B897B3CA09E93E77049B848781DB06BE68442AE8D45BFB8554F7FC41487D38D8CA432ACAA6F0EA7374F64F94DF54BE23EBB7855E9BDA9CC5E546321537D47AA3D36ADBC30E8443099835D855F23533C04B4546EF75DD46B2E6E7316B7DB9F93C16AB703091CBF56989DE621C5C8C747EADB5E9DCF92AC2C1D0E9610BCE66078D490A3325F98C241DE208D0B659D072FF2950431C961E91AE64FC936BF0E5E349C4182C9401C15B516896D3B2F04A8314CFA41F3D4CE73BE24E9B7623D2BF9D3F0DF1DAE2F9298B9A8032E35628A5B7F85EE8A555F5D269F2C973CED2CBF4DA6A754460D7871B0F36860E7E9301C6098CD613CF07DE1F67D90854B889F90E070F12ED966E486C8314CB8CFCE57034FA328F933502EBEABA90ED772C207B288B36DAAB295921C2C6592AC34A24A492E97B572FA2B08E3B5A25129693216E1826479F81C444548A0F646C0C806D1EF2DF9FBE9EAB4CF90D555AC74A4F2A38BA31D15FA9AA42CEC867254AE241ED6962A08B9E749DB4350CF04034053EE398C34D7811CC1A3FC3227A7ECF1E0273EFADA0583464E38205A58CC018DDD4DD702B8ADBA70BF9ABACD814BAFD5C73DDE5AFE9844E12AE8B0C9A3618040B036673FC0FD614649FE85E1D6CDDA3069D1B2A6CC73B04AF55BB8323B21C1C141E23908A3E03E22CAFE8698322D08958F01774350CBBD7A43DEB1B7E7F5E6E545B62E4E33A145F66115E60C0AF2B8D17C9F163E9AA751BA61A4FDA18039FB8F6869FC5E729CDA2E71D73E76166C335931E5A71FEFE2A7AF498EDE9EDD12A206AC2BBE4DC78EC14F4063AD17901B63B5C06CB3B0565472289435FF7D2C5BC564B82141265F78E2BFBB716B1E8394F9E99E8934B6A397DEE9D762F8B4653F9CDD38333EDBED60420C8C90D6C4C8A11FC3C28A55E62CBB6FFB619EDA9B806980B2B3FB0E8E9F3B44079EB0F7EF6A23D50FEA1652F2BC7AC84878FE44E2ED2227EBF608D6714060569F553B70C91BBE8EC73229158395A8A2934F19724B833D5B932651445295979CE6B01956F85EAA1CF9EF2E778FA28B6D5C6495AF770929788E7F84E44FF697C48EFB3CFCA488A1F12A551E84E03E3B4CB096F1591428810FEAAFD3E9FFE57BEE3ECEDBACAC3016C1CE630E6BB8EEC7203E573253DC4799DD19DE0E99E514E6F689900EF125EDBCF05DC5C8640E7DA5FFA9E3D4FDFE4EEF993F927A6CD57C76E5751DC484BD2507F0AB931C26B445FF06A27BB708E85DBD72A7F0AA131C2C529C3C3C84EC790A453A29A90D4F505220D9F11C02568090E2E051513C7B05B294921CCE2304EF3FF150424C729513E0282438A0BC7EC2177CF44F491D6B7BE33361D734A405C2EEDB6446B66B923E24EC3D96255964D9B6C30CD0C60931AAD9598C7D363D7E2BDD042C2C8197668259B9B5938E473F0DB52B4D5CECEDBE0D3941F73C099260AF4C8180F4E940334DD649B3E9D8C95941E2D56627D4CE620ED3E23946A3A8350FDCE21293DCF400CDB5F9EF2E358778355F27D3A76E7CBC5A626082E847C6DC3D19F7A248285850F3DD859B7A52ED7E4A7DBB7D7C64F35E9913FF7DE87D9D1F25CCBAAFAD5DBFA67916A7E7EC511C3A6B588759D6CD8618F960CC8885413F473DAC549943F5CDED48A03C14BAA47309F55C804F73F1908BD94187EC23B7FBE8C487B9AD2A7CCA8F4E7CCE49441463D27C9E14AABB04F95D918294B16913EAD798BD1F204F7E615A2985859BF2D43C15AB0E4DA467D1AFC5700BC435564CE562B6DB6C937588AB6CE18468313B8B392CD7BA6F28803B99CE3B8EBE664CE3DF3F9DEE6C75D45EEB274E8897F820E3C5053944DE3844DEF0DAB7D810DD61E272BAD94461F9D815E3D4A22F5939F433BB3CA3836EB286262D628ACBE2B8787D5DB960577D1D7A09E7239A7891E12C891FC2742DBBABC8692E8E0F59F66792AE3E0699122C824F71D85C22CB6DCA109507EB8D645FC524072959B87328DAB690D08A9F46A33085C31EEF9FC945B0A4FDEB43CC6E74CADBDB4AAACBE1F0F25BB2CD3FC44540B2DFF3A57C46AC24B7E00DC82CA7B9F8812E49965D508892D51930BF5493DD963AEAEAB4F93A19E3CEAF09CFA2205C7B5AA216BC3AAE51353CFAB1F69EA24433918B7BE3E280D17C76E4F547106D2166BBEF93C4D165F218C69E7054F0EA88230D8F7E9150144ABF3D878AD7B494E4748658E4F90F224DED8584C960E20B21DF48BC6A0F040D0344F36B73F6B4E602DF016AF30A10F68679DF4D7A9A65C9322CE6DCDABECE269C5FCBD5016653B92275DD96A4785929A65E66FBB57C860C800A72ACE018422DC6F4554BD14AC0BB207D049D993D4CF0BB1B3263FDE854731532281C2DB2CFDB287A77FC10447220038B0ADF9E8090C2A34E5A057EDDCD5690CBCE9ADC79C201B42ECCBB230465A61E50A891D30D8998695577F4D86BEF1D40BB690A164015B9F34C03D13025EFE9036827A71B808635432300A93CD9C3E26847ED611404394F1F44A598FB3E1CFAC061354963775D833026A94C52CF02775FEADF59F561172AF653B22251D6E4BB5D3E9175502826DB044BB6AAA41417619AE5E7411EDC071929498E8FAAB5059D13BF643959BF6204AF6EFF333A8BC262A3BA22F814C4E103C9F2BB844EBDDF1DFFFCFAF5DF8E8F4EA330C8D8165BF4707CF47D1DC5D9AFCB62F73488E3242FAAFEEEF829CF37BF9E9C644589D9AB75B84C932C79C85F2D93F549B04A4E7E7EFDE69793376F4EC86A7D2267DFB1457179FDAF15972C5B453CF8B8454935A767172F4E290A76D744C4B6A32B2D1916155C6EC803B7A890DB5CCEF81658883029DE1D874CB98599A81E0D585D07397BCAB0B116C7470C806CEBAB06E189913D7F82CB15232BF5D745BC22DFDF1DFF5791EDD7A3C5FFFADAE4FCE9A8B8F3FBEBD1EBA3FFED2C4079705816CD0E12F322C0A92393722BA564123F07E9F22948FFC73AF8FE3F9D3955A7C825AF87280972671EF511B20F81B85364B7F6A933766A1EFEDCB9632371A7CE6E35A933EA6A92A7EADE175CBAAD222A277EE16CB40F557FD813C350BEAA2722D8D45A8BECF738FCCF2D6DB13BAA43D63E9F82EF97247ECC9FDE1DBFF9AB73F9E5BEB85C3EC7F3E7BFBE76667A111477E87AE1FD29E98F376DD7626BD033DBEB94B06BAEF5BBC3BED913760BA7C702AAD77B0D6CFFD282EDEECC132F2CC6FE7C5E9C27E9FB30CD9FCEE83FE1039B0BFA07CA791A3ED3E9DE65B824852382DF3AB0B3D64D92E6D549A65FEECC365F3D142AEA3CD694BCFE9E847171D9AD23B7720975F5F0314C5B4C94C4DCDD264BE26D23373984CC9DC4A886BA2A1C6CBB896399BB9320CD8EBA9B0855BE4E850B1111BACC17FB9BE261BA5DEF33BC61E6692AA745562E50AFD2DB2DFD7EBA5A8771C5F33E746FA6EAEA517B0EF4E77DB9BAEFCC8A566E43586C857ADAD945AE603750FB50FB8735491FE9E8F3E23E3A63D8DF90326A5DCFC5BC8F9264457BF876635DC661D8ED5C19AB5BB63E587E24E1E353EE85D5177FAC3E7CCFD3E06C9BA6210B4B94169E6C611E12FB6AB8C3D2AB34E77BB2F0EA6501A19981B7DB92B8DA1036AD2A9FA2E938647C8857BE582D32161B870F88D5DE22169C923863FB9885B9E1255446794CC750381607F91D199E93D5B6082848975A74C65FBED3D681E722BB7AA63F695DFDA8B1E6E6559515D7EBE085793A78AABBEB988CB64EE7BABBE1B33551D0DE50ABD1A2175B774E36415A5C8F6AB3466AF2765A1CB0C328A87653DB049EC40A61EA7BC0831A8B0A80075B31A8AD28E6FA5D0CC68E8187D396433FFF91FA79B9C83C74F6FE3BFBA177FD28BDEB035D16EDD3847B116734DF3607A7941E8E0193C734587BE7FB3E09D2950B57DCFE5FB68D94BDAAEE27C3F51B725D5D51122F6C06F6CB71F672283C6EF6A373F5D2A7FA336DDF69312151C2FDCF56FDFDDAB69A7B5F3E085FC87D16BA9DE4A38EDAD9C55EDF871AFC4ED4C1808E634017EBE0518EFB34DFBEBBE6765D2992EEC338485FDAED714DB5C178F78D3D69B6798D78E5E9DE1DFD377B90A37DCCB60946F7BB66F6BAADFB0C9FB7931077495B119A9CDD04D8A1AAC59083C6EF59B2DE04F1BE5C1898FEF1742FEE9B739AE6F5E311731A31E4042C32515F9E3D8F2C3C5AD1E33F27FEF70D1AEE7EE6A98BCFFDF8C1D28134656F34F469939AEDE7C3B8EA7F5CEDE4A6AB30E838C2761246CA3EF9B1F69C3C874BA27B1271F6A8862FE5682FDAA076F377375E39E95D8F559EC8F29B270FAE366E47787084D932DC44C51AB57CDC75CFE031DE254E45B34C8C16A7E91A3EE3DF30C5DEE9ECE0C50A577E4F20EA61D9E07C4651BC145884EA035F379AAD2E47EFEEF023A36EA2403CBA5D4A0AD2B868F1F3E0A5EEA7ED2EE47C09F3A764CB9E1E6CC5100D5455077B82D0A91D067C49D26FC58AAAE34DAD4A455E8462ADEFE79E13E3D4C751E3CE5AD86297CF16A6A31B526F57088558EE5D187161DCBBB05143B777E126456CEFC24A0AD4DE8595149FBD87F1417C43783FFA1C452A595D29E7CFEDB67A0BC55C9334B3DCFF98DE3AA1132CC288ECE238ED09284637C4B40A4F7E2039D7783D2EE8A3ADBDB75B6CF30E84B588BD6CC45D6DF3396CE87D4CA27015ECCBB27E36E34FB3AFB2279A1FBDEF171A6DB765CA65ED24C2E9731016F48E8B6437D41C76346D36A0E3FA6D91B1174D59861EEC6DD18487BDD47DEBFA1E6F264D6DE38FEB4FAD8E36836DE6BF93EFE7F5AD4EF7FDDA1FAF4126B07C5EDCBFF94B82C3CEA72FB34775E923D6AE47DBC544BA2141A65E8E68B713C0F8D5E73A1D8C10B29B39981E1F9D7F5C23361B7B51AD7FF7E7D084D5AAC55CA5C8E525EAE4EC6D563BC3D01676FB355F9FA4EB81A4F176BD4360F023761434C03F9178BBC8C9BA0DA4A167376C9096F4E81FD2298B37452BA4F5C3F41232E39716113392384F9328AA9E29F53105295DDB3CF28BA28B6D5C30F5331FF823247FB2BFA6B7B261C02FBA6597E9E462199F4501E2B24D0757C64F14374FD1CBE19068928744FE1714F3DFCBE8F9A46AD721CA59C7ED13915FBA3AF488BDF2459D9E5FD7E97D56C4F7F6C2E63A88097B02ABDB2AA8E8B91E98540F727562F47B9C3C3C842CA07A67991A565E242B76F9BD70BA2A1EA8F1C24AF44CF320950746A7AB7FD0AECE9CF17C2DD27DF0F94C981F793FEEE3247D4858E4FE255964D9765F26591E0E56DBA8F02660976EF74487BBCA8CEF0F39FEFC40EA242D66070A873EC2CC50B1D7C9FEEDD78DDEFE87FBD8DA9E5121CEDBC9BF8F89EA5DE2CE05DDC96EF62E147F59234FAE83FECE3E6FB78F8F6C8A88E037B0A7C1210C6FAF5B94A3DBFBDE4E73D98B0E742AB00EB3ACA5F998E0D943F566BA3CDB46B45799556C2B3E12C5CF7F6BB3BD5D1E835CD239909FDE1EC46C3B1F0F07900573A9ECC8E29C44A4E9A91E6159353D6B8EE141090721119BBEF5A2CFE78944A52616EEA4ADAAAACE726227ADA2AAB86A56D71F3B6A570CF2E28D2D8FBDAF2DCD88C4C3A73D39DD6CA2B08C03CF14D05642954D7B21F151478B4970B3A5B527D3D5D1A7075E267E3EF6053B5CD4D14D6AE777E9705FE6D48EDD7A0F43291C021060F749E61480401AFA663CBF3C2B2C48F760778BAC78D2D661268F9DE4B55C6E03B16EDB733A4BE287305D777BF4F33AC8B23F9374F531C8EC77EA51BB3F64B94D29306FF360EDE739E622EC2E1CBDB52B3F2F2ABCFB33B90896D4447F8859AE6E4FD926CB6FC936FF1017C1867ECF975DB78B6A869D453B5D2E49965D50E091D5193FA56AD33359DF1E7215791605E1BEB8A0C2EBC3763B2E4C2D8C8F3F6E7F04D1D60FBB39AF0879EC5D268F61AB9963DB7D899E36108A7AD06FCFE1CA9725DE31A315FAE111F385906F245EEDCB0AA3E54B115EDE89E09CC23AC6D339CDB2641916C24A7B223B49A5E6A2E3F6119B30560AA8C4B825D1C3ABEAD3A76D94B3D0B14B5A2045D0B1DCC057BBFDE7A3D2F99EED4867CB60A5569B0ABDD2955FC92948D07C1465F8278535051761171DC2804D3359287EDA822A12C378196E8248ACB34486842CAB4FCD504E39271BDA37A83C72ED30653558544BAC194B9AB5D5FFED09070D2462CA8D1758F4B62DF7FAD5AB374AE3A9BC188F2191E0D43E9EB050D411531EB7FF360A2038A790313001BC2B57B013BECF1E15E73AE791C982A27AE3631C4CD40FD98B90683EEF0122EACACC0B10C59BE3E3A2A27CF61C86C62E6D8FF0013CF23E139094472207904C0A24DC29D9D8438B517C3F8DDAD37A66AC11AA4D830F3B5049128EB6CEE16036AA193ACC602663779A758E4EF42E2DD89BA5196B7DE4D8BE03AF901AE94634310DA246B1318795F3F4CC4CBDBFA615BC5B13EED106AD6BD30EB94B2BDDB41A074AAB6D79ECF3D5AF5DE90B4295B8A210CDD7D9DBA2A62E98C2787FD5714D51F54771B46E83107FFE2EC0A84CD85B6BA457524F6072012E2FDC7878FA4EA50A49BC24333148B5BCA214DCE7F99BA4A632B3B0498B75F048B279E0A7945590A0FA347BDCEC2A320BCCD4E3D86DB24D97E4EAE16398EA912310F16D2726ECED38A657523F4872432D27DC68782AE2059DC6AB5D34A35ECEB27A029424BAE8D023A7CDDE48C935C21439F619980CAE03ACF60256A30F8232AE46D8703CA0C456E4D89B8FA56FE11DFD37A33C67627B04990117D52669FE98122B340FC323628AC56A9AB8B3F1D8881ACEF1B8059E9AF69B0AA2EE92039EE68BA7AAF526E0E831AF714F955BE368B047E31F50A9598C8100C6A4D08E3338E29D0AE0063EF66D893920F6E754A02704F23C006FCF80A744791D077661B6640D10B34852CBD2694EF9643C4FD6900B2DAF211906954AE116D9FA422542AD3D0154AD22A65458E02921751EF3BF89E06FD0F95F3BC48D3EFFAB0428DED328A28DEC22ADCF036A5AF141A100AAD9034F5FB799E34F7D7E488B4480946F7E28F90746A74DB153C429FC9ED5A888558316CECB6402411721A920B2BD319AB6C0939AD247B79A176144582499E0B19F68173D418F135B1043F83E7B70F1B5C11437B65F070FA6038C660BA32918A59CA4CDEBB6B3019320B58C2829711F6025566916D86A668DF3405523AF2005FF79F648E22A33370C157F1AF7781B0AA5FD06DCC91D0B461AFD4C0247B56CE3026956DB64BCC42A92F667334CA8CE7C8C5285A55999A591E034BC6972C1D3C48CD308AED40753A3296F6C0F6AF674F74C462B2AA9089DE2C3FC21C3AA318F518961650CE3F1A3B7FC148CC459102DB7D18CC27348422B0012D2F6024B7C8D666350785CA91DC2D50AEC2D96D0B662041CB13C53C1D0BC16E0A0EC2674EDD14C19AED81CED56BD5037F51D3FD6E40701621B2B341E1EA5AC5383E51EB84C4D0D9FE3384B7580E8843CA53E2571FE14BDCCED4458115B1006489DFD08ADD6691683B38AAF11B62C0E68C1153AF6CEC64EEAD234DE3E1132134F02556E086042F2BE208CAFD49C0C128FB13D98924D0581E34CC55A627142D3B06B923E24E99A59EA1BFA3D7E9C87E153C4167B839A3A7BB3A7D66916564FC517F76591655B3DCE14424D13EF127F20DC99553825FCC9928E87C3345927B3DB1996A516D1A624CEDFC8C9559A878D93B135BF20295300DAC0712A5A616D2AE15114C8CD2D38CA017038C04D232CCA0DF7E4D32C06CE1B0DD06FF6EA91A81B47008F3E52F2381A613BF6808A296EBE2E187BAA6C26FDD7DF3392665A30F0A4E2031542821328AAACAC642DDF26B1177808E263DA0CEEC1F86730E46AE1CAE4DA693C13424BA7ABCA759865CC8AA8D2F70C17B17CD18C48497B0115A95298125996D1F051EEAB9EAEFEB1CDCA677067140241965D7CC745499CFD48A5540953E6D8111114801DA0B52FD01A7D7EAC606B8449F201293398339F6E98CA8B2F6C0EF7F52C0AC2B57EDE2C910B4DAAA4B59E3D174268A7CFBBD45EF022D761E0297459354CA172BB8D666964412E93C7301E1B4085105A00ED52F7114065D5660D2056A9B1F133D6F27D64F4A097829301CF1742BED132BE4EFC05949D988200F5B799BF7952D50353D4A04F9D7C28704DF3E4340749AB555FB22217619AE5E7411EDC07993A1766B96E495E892C3FCC5612989E6EBB5D3E9175F0EE78759FD0160FEE9996F2FBE8AB42A8D822B1E866DEAD94D924E90A6B282CA5541D41ADD72E415B9D5DBA85BF7024A81422A4EA4A1288ACC555AF0680A53589FAC21A1A7459BFA5C976632C7047612F754768C3C66ABBD4E9944BD3A2A3214182B00CFCA20562996C036349652B917B5F5C2D8F4BD496C6D158CAAADEA356CAA912746554E916FEE29BC94A2962B2AE2C910AD59D9B974034DDBA213077EF86CE52EE59B2DE0431641FEB145D493501BABB19AA0711D93B1DBA9AE7E4395C12FEA60520814CA22F5FA6B4950E3C6AA0960F10692500689D65D098061D215E169CB9D00736D75A2B80D666BA802C16C120577A45228848270A448B540E145F5BAB1D88D8A61E288F45B60B92E5E1731015D322401C295D278144662B948FDEAB16C9A76A0BE489ECC549A15DA132251243C112A5A5F48F4914AE02A823D429BAB26A024B117C9048A5143E5157104F83294B636CB8346349289322061183CBB21A0E91CA5662117B442DA9F8AC2DA148457016020080850814A6F20442B7A20D0AD5102205C12AF91389B78B9CAC01099A24A8CC2AD56E5E809B986A592A8DAEA200294E00E19E944E0281C82282406B1BF8942B15EAB0A79068073D85125F7A7577C2547C458328BF22B509A0389DAAE52B24DAE2154A4BE937C6E5F60D66B97DE3B0DC965D55D4122502A85091C6DECB44AF1B75ED26244305F214F8E29ACD5F6D910D89A9D88A0AB17E548E49D535A442A25D472A94A8D22DB34697D9628B59A2B2A9AF08A05040A54B446E8DBE3B7134B6FA8EC6D6ECBBF35597D277C755C6D27734B6D2778773B6D2EBAD69A5CC3A45D7C235815404B747ABAE4EEAAD448E4A5A93803BFCE01E7F5D99E693B2D90C393C70F9B47BA72762355CAA28798F99AA6A72346B2F3C989315DC57D5B95D5B44ED0DD47D2800D8952E329BB69ADBA8A0DA6042694047DC8F02E47DF25DFDB5BBDF5DAA5F6C6A3BE900CCD1AF22844D7C491BF0AE7C7795487E590895983CB966AE12F514C4D84B64EA5E2A3462A77102C7B0B8E8B3FACD38C033368F181CA547F1471931B4179B2C2A18A4FD8750413D0F12D81AE64B86EB97DD841F76A6581DC97E35B53840D5CBFC503E5E2EB36A4F8D3BB4B278366C686681D05E697EA358A838B4053C6C4BD7E7D3E6A606C87A696BE5BCBDCCAB3F4577AF72795C6EACAE4CD24755C5E3FE229FE620BF03A0C5E37A03A00542BDD0900F4121BAC92F6050404B8E4D8879BE25471F6DAF71E42A370A2C9E59DD55E2A28C3D56837D4A63C9B1072A11FC6ACCC0D090F6A204D06F88DB46D3BAC87456018B85A2DF01D413FBDE0B1C510577095A0135E99CABAF7A5E193B8289BCDF852EA80E84DF981795C8E19F709A31068DF2BA249A889AC49045382519C21CCD5F458A239DD6B70ED0153AB3A1D24617C1B2E228673F0B570BC77E5469B45306F25EECD4082AD13A449A17B3F65C7D9E73699D430556761F4F9FEA82DC395D14678F54CC8749D33AA3161AB0BB964E54ADAA47010A86A66CBD9EB76A3D2A045E083F097795717EAB8815BA81BA0F05017EB9456693B36D3715602BBF1FD5161C876D75D710F7A400D041BAD282D9E1D95D158D2D322A0122EBA3FAAAFB7491D7E014DDA9CA9C0BB4B1CEDAE7AF65D195799DD6C97A9C6A63A62630616FAD0D8D94465FF18E55B7B7B9E56178BFAD3E74F5ED3B8EE8B7CCE78602E1296EA8E2FAB7BA7D5595BBD6505611BAB2D0B26A8896B53D303D836A82EF246BEA6A7F53D96385814B2575DD4DB744BAABA1D4AE5D05D01BB7AEAD369D6AA3863297C76BFBC10268D870F775BCA948690BB4A62C0F85FAC1C2D4D587DB9868F93A688FBB1263AAD2F00A25A03BEC9B959EBAA8F6565AC1C27EC9CC873AECA3B5DB438B73570DF844A05E2BF61705FD2A04B840C86BC4741DD08B4A7036A8C553783DDA9F31546778760DD017F691364F58D25EC52CB56BBD59E9431DEAFD518C5A6C0F63C195146EBBCA75046FAF8EAD2EED63569096702F5FF9C28EE6166DA90BDBAD580FAA401CBEBBBDD7E4F55C7974F5580FDD5D5E169AB56AC0376D0085D8DFBEF1D475802BE04566D3BDEE6ED5B64FEEB00FB6CC4A05C083239063B3E559124170E86A7B21B99860F271D6DC5617B83489DD7190A8EF68400000C8FA5483589E080229A9B30A6C4F4500EA707A5DC253A7D00513283858830378508B9342F65B15768BE914C27FA66AD1449F07B48189532F54451337A2A889928634A7421C08C59EEE52BD2BA50ADA60570A147BBD77A508E12914A5EC52BD2B85C981D2494938AC4A861876A5C8D780264CB1B105D1DBB98B4B41428A6CBAE81FFAEAB1B0D92C7B1DA6B94E7B7B52C618D97DA03F4B7F984FC98A4459F1F5EDC9CD96E65E93F257B940A859BCA53C6352B83D364C2B9A45FC9054F1A925892A922AB98E1E9607D40007A7691E3E04CB9C262F099D41B085F81F41B42D0CEF3D592DE2AB6DBED9E6B4CA647D1F092B0C16E5DA54FEDB1345E6B7571BF62BF351052A66C8C690ABF8FD368C56B5DC1741944998D4B160E1B37F23F47BD99639FD3F797CA9397D4E6224A39DFAEAA8DF77840E5094597615DF06CFA48D6CB4775D92C760F942BF3F872BD661754CEC0D21AAFDED79183CA6C13ADBF168F2D39F14C3ABF5F77FFBFFC1345CA285060400, N'6.1.3-40302')
INSERT [dbo].[ApplicationUsers] ([Id], [CustomUserId], [IsActive], [RoleId], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'22424a78-fb6b-4db3-bf16-90747a08e92e', 2, 1, NULL, N'admin@futuristictech.xyz', 0, N'AGDgwfBPUVi8dGBrJwn6Rfa21INfA7prCotRdys1gL/K8nlQcbOzMlft3Vl2nBSSvA==', N'a8def7f9-5470-4bbe-bc72-14485f478190', N'01676272718', 0, 0, NULL, 0, 0, N'admin')
INSERT [dbo].[ApplicationUsers] ([Id], [CustomUserId], [IsActive], [RoleId], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c5575472-d22a-421f-976c-02792e888d99', 1, 1, NULL, N'systemadmin@futuristictech.xyz', 0, N'AEwOrX8+QMUhzSvj8cNmmFL4QI/2S7ujV36K5hKo5L3fmyLCOUdtK1GOAuVzrDSKaQ==', N'e1b189b4-30b7-497c-af26-dc54b79f25fa', NULL, 0, 0, NULL, 0, 0, N'futuristic')
INSERT [dbo].[IdentityRoles] ([Id], [Name]) VALUES (N'04e3d876-31ca-4e05-b6dd-193ea918dec0', N'System Admin')
INSERT [dbo].[IdentityRoles] ([Id], [Name]) VALUES (N'23826810-48ff-476b-b2d1-1ea893153048', N'Admin')
INSERT [dbo].[IdentityRoles] ([Id], [Name]) VALUES (N'48b64668-cea9-4606-89b3-0e2853df7014', N'Department Head')
INSERT [dbo].[IdentityRoles] ([Id], [Name]) VALUES (N'994d2a55-1e9f-4e40-bb40-149b9e4a77c6', N'Super Admin')
INSERT [dbo].[IdentityRoles] ([Id], [Name]) VALUES (N'b4f77c9b-2aa7-40e5-b563-b583a8828580', N'Employee')
INSERT [dbo].[IdentityUserRoles] ([RoleId], [UserId], [IdentityRole_Id], [ApplicationUser_Id]) VALUES (N'04e3d876-31ca-4e05-b6dd-193ea918dec0', N'c5575472-d22a-421f-976c-02792e888d99', NULL, NULL)
INSERT [dbo].[IdentityUserRoles] ([RoleId], [UserId], [IdentityRole_Id], [ApplicationUser_Id]) VALUES (N'994d2a55-1e9f-4e40-bb40-149b9e4a77c6', N'22424a78-fb6b-4db3-bf16-90747a08e92e', NULL, NULL)
SET IDENTITY_INSERT [dbo].[MenuItems] ON 

INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (1, NULL, N'Manage Menu', N'#', N'#', N'#', N'#', N'#', 1, N'fa fa-file-archive-o')
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (2, 1, N'Add Menu Item', N'MenuItems', N'Create', N'Create', N'Create', N'#', 1, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (3, 1, N'Menu List', N'MenuItems', N'Index', N'Index', N'Index', N'#', 2, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (4, NULL, N'Role Management', N'MenuItems', N'#', N'#', N'#', N'#', 2, N'fa fa-th')
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (5, 4, N'Add Role', N'Roles', N'AddRole', N'AddRole', N'AddRole', N'#', 1, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (6, 4, N'Role List', N'Roles', N'RoleList', N'RoleList', N'RoleList', N'#', 2, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (7, 4, N'Add User', N'Roles', N'UserManagement', N'UserManagement', N'AddUser', N'#', 3, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (8, NULL, N'Settings', N'#', N'#', N'#', N'#', N'#', 3, N'fa fa-home')
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (9, 8, N'Add Branch', N'Branches', N'Create', N'Create', N'Create', N'#', 2, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (10, 8, N'Branch List', N'Branches', N'Index', N'Index', N'Index', N'#', 3, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (11, NULL, N'Employee Record', N'#', N'#', N'#', N'#', N'#', 4, N'fa fa-user')
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (12, 11, N'Add Department Group', N'DepartmentGroups', N'Create', N'Create', N'Create', N'#', 1, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (13, 11, N'Department Group List', N'DepartmentGroups', N'Index', N'Index', N'Index', N'#', 2, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (14, 11, N'Add Department', N'Departments', N'Create', N'Create', N'Create', N'#', 3, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (15, 11, N'Department List', N'Departments', N'Index', N'Index', N'Index', N'#', 4, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (16, 11, N'Add Designation', N'Designations', N'Create', N'Create', N'Create', N'#', 5, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (17, 11, N'Designation List', N'Designations', N'Index', N'Index', N'Index', N'#', 6, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (18, 11, N'Add Employee Type', N'EmployeeTypes', N'Create', N'Create', N'Create', N'#', 9, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (19, 11, N'Employee Type List', N'EmployeeTypes', N'Index', N'Index', N'Index', N'#', 10, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (20, 11, N'Add Source Of Hire', N'SourceOfHires', N'Create', N'Create', N'Create', N'#', 7, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (21, 11, N'Source Of Hire List', N'SourceOfHires', N'Index', N'Index', N'Index', N'#', 8, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (22, 11, N'Add Employee', N'Employees', N'Create', N'Create', N'Create', N'#', 11, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (23, 11, N'Employee List', N'Employees', N'Index', N'Index', N'Index', N'#', 12, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (24, NULL, N'Transfer', N'#', N'#', N'#', N'#', N'#', 5, N'fa fa-exchange')
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (25, 24, N'Department Transfer', N'DepartmentTransfers', N'Create', N'Create', N'Create', N'#', 1, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (26, 24, N'Department Transfer History', N'DepartmentTransfers', N'Index', N'Index', N'Index', N'#', 2, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (27, 24, N'Branch Transfer', N'BranchTransfers', N'Create', N'Create', N'Create', N'#', 3, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (28, 24, N'Branch Transfer History', N'BranchTransfers', N'Index', N'Index', N'Index', N'Index', 4, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (29, NULL, N'Resignation', N'#', N'#', N'#', N'#', N'#', 6, N'fa fa-power-off')
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (30, 29, N'Application Form', N'Resignations', N'Create', N'Create', N'Create', N'#', 1, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (31, 29, N'Application List', N'Resignations', N'Index', N'Index', N'Index', N'Index', 2, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (32, NULL, N'Disciplinary Action', N'#', N'#', N'#', N'#', N'#', 7, N'fa fa-ban')
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (33, 32, N'Add Disciplinary Action Type', N'DisciplinaryActionTypes', N'Create', N'Create', N'Create', N'#', 1, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (34, 32, N'Disciplinary Action Type List', N'DisciplinaryActionTypes', N'Index', N'Index', N'Index', N'#', 2, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (35, 32, N'Add Disciplinary Action', N'DisciplinaryActions', N'Create', N'Create', N'Create', N'#', 3, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (36, 32, N'Disciplinary Action History', N'DisciplinaryActions', N'Index', N'Index', N'Index', N'#', 4, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (37, NULL, N'Leave', N'#', N'#', N'#', N'#', N'#', 8, N'fa fa-bell-slash')
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (38, NULL, N'Performance', N'#', N'#', N'#', N'#', N'#', 9, N'fa fa-bar-chart')
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (39, 38, N'Add Performance Issue', N'PerformanceIssues', N'Create', N'Create', N'Create', N'#', 1, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (40, 38, N'Performance Issue List', N'PerformanceIssues', N'Index', N'Index', N'Index', N'#', 2, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (41, 38, N'Add Performance Ratings', N'PerformanceRatings', N'Create', N'Create', N'Create', N'#', 3, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (42, 38, N'Performance Ratings History', N'PerformanceRatings', N'Index', N'Index', N'Index', N'#', 4, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (43, 37, N'Add Leave Type', N'LeaveTypes', N'Create', N'Create', N'Create', N'#', 1, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (44, 37, N'Leave Type List', N'LeaveTypes', N'Index', N'Index', N'Index', N'#', 2, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (45, 29, N'Resignation Approval', N'Resignations', N'ResignationApproval', N'ResignationApproval', N'ResignationApproval', N'#', 3, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (46, 37, N'Application Form', N'LeaveHistories', N'Create', N'Create', N'Create', N'#', 3, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (47, 37, N'Application List', N'LeaveHistories', N'Index', N'Index', N'Index', N'#', 4, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (48, 37, N'Leave Approval', N'LeaveHistories', N'LeaveApproval', N'LeaveApproval', N'LeaveApproval', N'#', 6, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (49, NULL, N'Reports', N'#', N'#', N'#', N'#', N'#', 10, N'fa fa-globe')
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (50, 49, N'Employee Report', N'Reports', N'EmployeeTypeReport', N'EmployeeTypeReport', N'EmployeeTypeReport', N'#', 1, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (51, 49, N'Resign Report', N'Reports', N'ResignReport', N'ResignReport', N'ResignReport', N'#', 2, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (52, 49, N'Department Transfer Report', N'Reports', N'DepartmentTransferReport', N'DepartmentTransferReport', N'DepartmentTransferReport', N'#', 3, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (53, 49, N'Branch Transfer Report', N'Reports', N'BranchTransferReport', N'BranchTransferReport', N'BranchTransferReport', N'#', 4, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (54, 49, N'Leave Report', N'Reports', N'LeaveReport', N'LeaveReport', N'LeaveReport', N'#', 5, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (55, 37, N'Leave Recommendation', N'LeaveHistories', N'LeaveRecommendation', N'LeaveRecommendation', N'LeaveRecommendation', N'#', 5, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (56, 8, N'Company', N'Companies', N'Edit', N'Edit', N'Edit', N'#', 1, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (57, 8, N'Salary Distribution', N'SalaryDistributions', N'Edit', N'Edit', N'Edit', N'#', 4, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (58, 8, N'Add Weekend', N'Weekends', N'Create', N'Create', N'Create', N'#', 5, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (59, 8, N'Weekend List', N'Weekends', N'Index', N'Index', N'Index', N'#', 6, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (60, 8, N'Add Holiday', N'Holidays', N'Create', N'Create', N'Create', N'#', 7, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (61, 8, N'Holiday List', N'Holidays', N'Index', N'Index', N'Index', N'#', 8, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (62, NULL, N'Promotion', N'#', N'#', N'#', N'#', N'#', 11, N'fa fa-sliders')
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (63, 62, N'Employee Promotion', N'PromotionHistories', N'Create', N'Create', N'Create', N'#', 1, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (64, 62, N'Promotion Histories', N'PromotionHistories', N'Index', N'Index', N'Index', N'#', 2, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (65, NULL, N'Probation', N'#', N'#', N'#', N'#', N'#', 12, N'fa fa-plus-square')
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (66, 65, N'Employee List', N'Probation', N'Index', N'Index', N'Index', N'#', 1, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (67, NULL, N'Attendance', N'#', N'#', N'#', N'#', N'#', 13, N'fa fa-barcode')
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (68, 67, N'Todays Attendance', N'DeviceAttendances', N'Index', N'Index', N'Index', N'#', 1, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (69, 67, N'Attendance By Department', N'MonthlyAttendances', N'EmployeeAttendenceReport', N'EmployeeAttendenceReport', N'EmployeeAttendenceReport', N'#', 2, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (70, 67, N'Attendance By Date Range', N'MonthlyAttendances', N'EmployeewiseAttendenceReport', N'EmployeewiseAttendenceReport', N'EmployeewiseAttendenceReport', N'#', -2, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (71, NULL, N'Bonus And Penalty', N'BonusAndPenalties', N'#', N'#', N'#', N'#', 14, N'fa fa-gift')
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (72, 71, N'Bonus And Penalty', N'BonusAndPenalties', N'Create', N'Create', N'Create', N'#', 1, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (73, 71, N'Bonus And Penalty List', N'BonusAndPenalties', N'Index', N'Index', N'Index', N'#', 2, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (74, NULL, N'Salary', N'#', N'#', N'#', N'#', N'#', 18, N'fa fa-money')
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (75, 74, N'Salary Calculation', N'SalarySheet', N'Index', N'Index', N'Index', N'#', 1, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (76, NULL, N'Festival Bonus', N'#', N'#', N'#', N'#', N'#', 15, N'fa fa-gift')
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (77, 76, N'Add Festival Bonus', N'FestivalBonus', N'Create', N'Create', N'Create', N'#', 1, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (78, 76, N'Festival Bonus List', N'FestivalBonus', N'Index', N'Index', N'Index', N'#', 2, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (79, NULL, N'FIle Storage', N'#', N'#', N'#', N'#', N'#', 19, N'fa fa-file')
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (80, 79, N'Upload File', N'FileStorages', N'Create', N'Create', N'Create', N'#', 1, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (81, 79, N'FIle List', N'FileStorages', N'Index', N'Index', N'Index', N'#', 2, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (82, NULL, N'Salary Adjustment', N'#', N'#', N'#', N'#', N'#', 17, N'fa fa-adjust')
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (83, 82, N'Add Salary Adjustment', N'SalaryAdjustments', N'Create', N'Create', N'Create', N'#', 1, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (84, 82, N'Salary Adjustment List', N'SalaryAdjustments', N'Index', N'Index', N'Index', N'#', 2, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (85, NULL, N'Loan', N'#', N'#', N'#', N'#', N'#', 16, N'fa fa-adjust')
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (86, 85, N'Loan Application', N'Loans', N'Create', N'Create', N'Create', N'#', 1, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (87, 85, N'Application List', N'Loans', N'Index', N'Index', N'Index', N'#', 2, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (88, 85, N'Loan Approval', N'Loans', N'LoanApproval', N'LoanApproval', N'LoanApproval', N'#', 3, NULL)
SET IDENTITY_INSERT [dbo].[MenuItems] OFF
SET IDENTITY_INSERT [dbo].[RolePermissions] ON 

INSERT [dbo].[RolePermissions] ([Id], [RoleId], [MenuItemIdList], [CanView], [CanEdit], [CanDelete]) VALUES (1, N'04e3d876-31ca-4e05-b6dd-193ea918dec0', N'1,2,3,4,5,6,7,8,9,10,56,57,58,59,60,61,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,45,32,33,34,35,36,37,43,44,46,47,48,55,38,39,40,41,42,49,50,51,52,53,54,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88', 1, 1, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [MenuItemIdList], [CanView], [CanEdit], [CanDelete]) VALUES (2, N'994d2a55-1e9f-4e40-bb40-149b9e4a77c6', N'8,9,10,56,57,58,59,60,61,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,45,32,33,34,35,36,37,43,44,48,55,38,39,40,41,42,49,50,51,52,53,54,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,88', 1, 1, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [MenuItemIdList], [CanView], [CanEdit], [CanDelete]) VALUES (3, N'b4f77c9b-2aa7-40e5-b563-b583a8828580', N'29,30,31,37,46,47,85,86,87', 1, 1, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [MenuItemIdList], [CanView], [CanEdit], [CanDelete]) VALUES (1003, N'23826810-48ff-476b-b2d1-1ea893153048', N'8,9,10,56,57,58,59,60,61,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,45,32,33,34,35,36,37,43,44,46,47,48,55,38,39,40,41,42,49,50,51,52,53,54,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88', 1, 1, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [MenuItemIdList], [CanView], [CanEdit], [CanDelete]) VALUES (1004, N'48b64668-cea9-4606-89b3-0e2853df7014', N'29,30,31,37,46,47,55,85,86,87', 1, 1, 1)
SET IDENTITY_INSERT [dbo].[RolePermissions] OFF
SET IDENTITY_INSERT [dbo].[tbl_Branch] ON 

INSERT [dbo].[tbl_Branch] ([Sl], [Name], [Address], [OpeningTime], [EndingTime], [IsLateCalculated], [LateConsiderationTime], [LateConsiderationDay], [LateDeductionPercentage], [IsOvertimeCalculated], [OvertimeConsiderationTime], [OvertimePaymentPercentage], [Status]) VALUES (1, N'Super Admin', N'Dhaka', CAST(N'2017-01-01 00:00:00.000' AS DateTime), CAST(N'2017-01-01 00:00:00.000' AS DateTime), 0, NULL, NULL, NULL, 0, NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[tbl_Branch] OFF
SET IDENTITY_INSERT [dbo].[tbl_Department] ON 

INSERT [dbo].[tbl_Department] ([Sl], [Code], [Name], [DepartmentGroupId], [CreatedBy], [CreateDate], [UpdatedBy], [UpdateDate], [Status]) VALUES (1, NULL, N'Super Admin', 1, NULL, CAST(N'2017-01-01 00:00:00.000' AS DateTime), NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[tbl_Department] OFF
SET IDENTITY_INSERT [dbo].[tbl_DepartmentGroup] ON 

INSERT [dbo].[tbl_DepartmentGroup] ([Sl], [Code], [Name], [CreatedBy], [CreateDate], [UpdatedBy], [UpdateDate], [Status]) VALUES (1, NULL, N'Super Admin', NULL, CAST(N'2017-01-01 00:00:00.000' AS DateTime), NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[tbl_DepartmentGroup] OFF
SET IDENTITY_INSERT [dbo].[tbl_Designation] ON 

INSERT [dbo].[tbl_Designation] ([Sl], [Code], [Name], [DepartmentId], [RoleName], [CreatedBy], [CreateDate], [UpdatedBy], [UpdateDate], [Status]) VALUES (1, NULL, N'Super Admin', 1, N'Super Admin', NULL, CAST(N'2017-01-01 00:00:00.000' AS DateTime), NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[tbl_Designation] OFF
SET IDENTITY_INSERT [dbo].[tbl_Employee] ON 

INSERT [dbo].[tbl_Employee] ([Sl], [Code], [Name], [FathersName], [MothersName], [Gender], [PresentAddress], [PermanentAddress], [Mobile], [Email], [NIDorBirthCirtificate], [DrivingLicence], [PassportNumber], [DateOfBirth], [DateOfJoining], [SourceOfHireId], [DesignationId], [EmployeeTypeId], [BranchId], [GrossSalary], [CreatedBy], [CreateDate], [UpdatedBy], [UpdateDate], [IsSystemOrSuperAdmin], [Status], [ProbationStatus], [IsSpecialEmployee], [ParmanentDate], [EmergencyMobile], [RelationEmergencyMobile], [BloodGroup], [MedicalHistory], [Height], [Weight], [ExtraCurricularActivities]) VALUES (1, N'SystemAdmin', N'System Admin', N'admin', N'admin', N'Male', N'Dhaka', N'Dhaka', N'01676272718', N'yeasinmahi72@gmail.com', N'1234567890123', NULL, NULL, CAST(N'2000-01-01 00:00:00.000' AS DateTime), CAST(N'2017-01-01 00:00:00.000' AS DateTime), 1, 1, 1, 1, 0, NULL, CAST(N'2017-01-01 00:00:00.000' AS DateTime), NULL, NULL, 1, 0, 0, 0, NULL, NULL, NULL, N'A+', NULL, NULL, NULL, NULL)
INSERT [dbo].[tbl_Employee] ([Sl], [Code], [Name], [FathersName], [MothersName], [Gender], [PresentAddress], [PermanentAddress], [Mobile], [Email], [NIDorBirthCirtificate], [DrivingLicence], [PassportNumber], [DateOfBirth], [DateOfJoining], [SourceOfHireId], [DesignationId], [EmployeeTypeId], [BranchId], [GrossSalary], [CreatedBy], [CreateDate], [UpdatedBy], [UpdateDate], [IsSystemOrSuperAdmin], [Status], [ProbationStatus], [IsSpecialEmployee], [ParmanentDate], [EmergencyMobile], [RelationEmergencyMobile], [BloodGroup], [MedicalHistory], [Height], [Weight], [ExtraCurricularActivities]) VALUES (2, N'SuperAdmin', N'Super Admin', N'admin', N'admin', N'Male', N'Dhaka', N'Dhaka', N'01676272718', N'yeasinmahi72@gmail.com', N'1234567890123', NULL, NULL, CAST(N'2000-01-01 00:00:00.000' AS DateTime), CAST(N'2017-01-01 00:00:00.000' AS DateTime), 1, 1, 1, 1, 0, NULL, CAST(N'2017-01-01 00:00:00.000' AS DateTime), NULL, NULL, 1, 0, 0, 0, NULL, NULL, NULL, N'A+', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[tbl_Employee] OFF
SET IDENTITY_INSERT [dbo].[tbl_EmployeeType] ON 

INSERT [dbo].[tbl_EmployeeType] ([Sl], [Name], [Status]) VALUES (1, N'Super Admin', 0)
INSERT [dbo].[tbl_EmployeeType] ([Sl], [Name], [Status]) VALUES (2, N'Full Time', 1)
INSERT [dbo].[tbl_EmployeeType] ([Sl], [Name], [Status]) VALUES (3, N'Part Time', 1)
INSERT [dbo].[tbl_EmployeeType] ([Sl], [Name], [Status]) VALUES (4, N'Contractual', 1)
SET IDENTITY_INSERT [dbo].[tbl_EmployeeType] OFF
SET IDENTITY_INSERT [dbo].[tbl_LeaveType] ON 

INSERT [dbo].[tbl_LeaveType] ([Sl], [Name], [Day], [IsEditable]) VALUES (1, N'Without Pay', 0, 0)
INSERT [dbo].[tbl_LeaveType] ([Sl], [Name], [Day], [IsEditable]) VALUES (2, N'Earn', 0, 0)
SET IDENTITY_INSERT [dbo].[tbl_LeaveType] OFF
SET IDENTITY_INSERT [dbo].[tbl_SourceOfHire] ON 

INSERT [dbo].[tbl_SourceOfHire] ([Sl], [Name], [Status]) VALUES (1, N'Super Admin', 0)
SET IDENTITY_INSERT [dbo].[tbl_SourceOfHire] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ApplicationUser_Id]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_ApplicationUser_Id] ON [dbo].[IdentityUserClaims]
(
	[ApplicationUser_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ApplicationUser_Id]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_ApplicationUser_Id] ON [dbo].[IdentityUserLogins]
(
	[ApplicationUser_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ApplicationUser_Id]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_ApplicationUser_Id] ON [dbo].[IdentityUserRoles]
(
	[ApplicationUser_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_IdentityRole_Id]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_IdentityRole_Id] ON [dbo].[IdentityUserRoles]
(
	[IdentityRole_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[RolePermissions]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CreatedBy]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_CreatedBy] ON [dbo].[tbl_BonusAndPenalty]
(
	[CreatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_BonusAndPenalty]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UpdatedBy]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_UpdatedBy] ON [dbo].[tbl_BonusAndPenalty]
(
	[UpdatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_BranchTransfer]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FromBranchId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_FromBranchId] ON [dbo].[tbl_BranchTransfer]
(
	[FromBranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ToBranchId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_ToBranchId] ON [dbo].[tbl_BranchTransfer]
(
	[ToBranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CreatedBy]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_CreatedBy] ON [dbo].[tbl_Department]
(
	[CreatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DepartmentGroupId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_DepartmentGroupId] ON [dbo].[tbl_Department]
(
	[DepartmentGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UpdatedBy]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_UpdatedBy] ON [dbo].[tbl_Department]
(
	[UpdatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CreatedBy]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_CreatedBy] ON [dbo].[tbl_DepartmentGroup]
(
	[CreatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UpdatedBy]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_UpdatedBy] ON [dbo].[tbl_DepartmentGroup]
(
	[UpdatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_DepartmentTransfer]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FromDesignationId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_FromDesignationId] ON [dbo].[tbl_DepartmentTransfer]
(
	[FromDesignationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ToDesignationId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_ToDesignationId] ON [dbo].[tbl_DepartmentTransfer]
(
	[ToDesignationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CreatedBy]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_CreatedBy] ON [dbo].[tbl_Designation]
(
	[CreatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DepartmentId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_DepartmentId] ON [dbo].[tbl_Designation]
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UpdatedBy]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_UpdatedBy] ON [dbo].[tbl_Designation]
(
	[UpdatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DisciplinaryActionTypeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_DisciplinaryActionTypeId] ON [dbo].[tbl_DisciplinaryAction]
(
	[DisciplinaryActionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_DisciplinaryAction]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_Education]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BranchId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_BranchId] ON [dbo].[tbl_Employee]
(
	[BranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Code]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Code] ON [dbo].[tbl_Employee]
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CreatedBy]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_CreatedBy] ON [dbo].[tbl_Employee]
(
	[CreatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DesignationId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_DesignationId] ON [dbo].[tbl_Employee]
(
	[DesignationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeTypeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeTypeId] ON [dbo].[tbl_Employee]
(
	[EmployeeTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SourceOfHireId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_SourceOfHireId] ON [dbo].[tbl_Employee]
(
	[SourceOfHireId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_EmployeeLeaveCountHistory]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PaidSalaryDurationId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_PaidSalaryDurationId] ON [dbo].[tbl_EmployeeLeaveCountHistory]
(
	[PaidSalaryDurationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_EmployeeSalaryDistribution]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_Experience]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CreatedBy]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_CreatedBy] ON [dbo].[tbl_FileStorage]
(
	[CreatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_FileStorage]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_FilterAttendance]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_Images]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_LeaveCount]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LeaveTypeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_LeaveTypeId] ON [dbo].[tbl_LeaveCount]
(
	[LeaveTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_LeaveHistory]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LeaveTypeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_LeaveTypeId] ON [dbo].[tbl_LeaveHistory]
(
	[LeaveTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UpdatedBy]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_UpdatedBy] ON [dbo].[tbl_LeaveHistory]
(
	[UpdatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_Loan]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UpdatedBy]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_UpdatedBy] ON [dbo].[tbl_Loan]
(
	[UpdatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_LoanCalculation]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LoanId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_LoanId] ON [dbo].[tbl_LoanCalculation]
(
	[LoanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_LoanCalculationHistory]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LoanCalculationId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_LoanCalculationId] ON [dbo].[tbl_LoanCalculationHistory]
(
	[LoanCalculationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PaidSalaryDurationId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_PaidSalaryDurationId] ON [dbo].[tbl_LoanCalculationHistory]
(
	[PaidSalaryDurationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_MonthlyAttendance]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UpdatedBy]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_UpdatedBy] ON [dbo].[tbl_MonthlyAttendance]
(
	[UpdatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_MonthlySalarySheet]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PaidSalaryDurationId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_PaidSalaryDurationId] ON [dbo].[tbl_MonthlySalarySheet]
(
	[PaidSalaryDurationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_PerformanceRating]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PerformanceIssueId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_PerformanceIssueId] ON [dbo].[tbl_PerformanceRating]
(
	[PerformanceIssueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_PromotionHistory]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FromDesignationId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_FromDesignationId] ON [dbo].[tbl_PromotionHistory]
(
	[FromDesignationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ToDesignationId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_ToDesignationId] ON [dbo].[tbl_PromotionHistory]
(
	[ToDesignationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_Resignation]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UpdatedBy]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_UpdatedBy] ON [dbo].[tbl_Resignation]
(
	[UpdatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CreatedBy]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_CreatedBy] ON [dbo].[tbl_SalaryAdjustment]
(
	[CreatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_SalaryAdjustment]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UpdatedBy]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_UpdatedBy] ON [dbo].[tbl_SalaryAdjustment]
(
	[UpdatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BranchId]    Script Date: 10/23/2017 3:41:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_BranchId] ON [dbo].[tbl_Weekend]
(
	[BranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IdentityUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.IdentityUserClaims_dbo.ApplicationUsers_ApplicationUser_Id] FOREIGN KEY([ApplicationUser_Id])
REFERENCES [dbo].[ApplicationUsers] ([Id])
GO
ALTER TABLE [dbo].[IdentityUserClaims] CHECK CONSTRAINT [FK_dbo.IdentityUserClaims_dbo.ApplicationUsers_ApplicationUser_Id]
GO
ALTER TABLE [dbo].[IdentityUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.IdentityUserLogins_dbo.ApplicationUsers_ApplicationUser_Id] FOREIGN KEY([ApplicationUser_Id])
REFERENCES [dbo].[ApplicationUsers] ([Id])
GO
ALTER TABLE [dbo].[IdentityUserLogins] CHECK CONSTRAINT [FK_dbo.IdentityUserLogins_dbo.ApplicationUsers_ApplicationUser_Id]
GO
ALTER TABLE [dbo].[IdentityUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.IdentityUserRoles_dbo.ApplicationUsers_ApplicationUser_Id] FOREIGN KEY([ApplicationUser_Id])
REFERENCES [dbo].[ApplicationUsers] ([Id])
GO
ALTER TABLE [dbo].[IdentityUserRoles] CHECK CONSTRAINT [FK_dbo.IdentityUserRoles_dbo.ApplicationUsers_ApplicationUser_Id]
GO
ALTER TABLE [dbo].[IdentityUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.IdentityUserRoles_dbo.IdentityRoles_IdentityRole_Id] FOREIGN KEY([IdentityRole_Id])
REFERENCES [dbo].[IdentityRoles] ([Id])
GO
ALTER TABLE [dbo].[IdentityUserRoles] CHECK CONSTRAINT [FK_dbo.IdentityUserRoles_dbo.IdentityRoles_IdentityRole_Id]
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RolePermissions_dbo.IdentityRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[IdentityRoles] ([Id])
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_dbo.RolePermissions_dbo.IdentityRoles_RoleId]
GO
ALTER TABLE [dbo].[tbl_BonusAndPenalty]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_BonusAndPenalty_dbo.tbl_Employee_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tbl_Employee] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_BonusAndPenalty] CHECK CONSTRAINT [FK_dbo.tbl_BonusAndPenalty_dbo.tbl_Employee_CreatedBy]
GO
ALTER TABLE [dbo].[tbl_BonusAndPenalty]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_BonusAndPenalty_dbo.tbl_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbl_Employee] ([Sl])
GO
ALTER TABLE [dbo].[tbl_BonusAndPenalty] CHECK CONSTRAINT [FK_dbo.tbl_BonusAndPenalty_dbo.tbl_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[tbl_BonusAndPenalty]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_BonusAndPenalty_dbo.tbl_Employee_UpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[tbl_Employee] ([Sl])
GO
ALTER TABLE [dbo].[tbl_BonusAndPenalty] CHECK CONSTRAINT [FK_dbo.tbl_BonusAndPenalty_dbo.tbl_Employee_UpdatedBy]
GO
ALTER TABLE [dbo].[tbl_BranchTransfer]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_BranchTransfer_dbo.tbl_Branch_FromBranchId] FOREIGN KEY([FromBranchId])
REFERENCES [dbo].[tbl_Branch] ([Sl])
GO
ALTER TABLE [dbo].[tbl_BranchTransfer] CHECK CONSTRAINT [FK_dbo.tbl_BranchTransfer_dbo.tbl_Branch_FromBranchId]
GO
ALTER TABLE [dbo].[tbl_BranchTransfer]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_BranchTransfer_dbo.tbl_Branch_ToBranchId] FOREIGN KEY([ToBranchId])
REFERENCES [dbo].[tbl_Branch] ([Sl])
GO
ALTER TABLE [dbo].[tbl_BranchTransfer] CHECK CONSTRAINT [FK_dbo.tbl_BranchTransfer_dbo.tbl_Branch_ToBranchId]
GO
ALTER TABLE [dbo].[tbl_BranchTransfer]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_BranchTransfer_dbo.tbl_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbl_Employee] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_BranchTransfer] CHECK CONSTRAINT [FK_dbo.tbl_BranchTransfer_dbo.tbl_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[tbl_Department]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_Department_dbo.tbl_DepartmentGroup_DepartmentGroupId] FOREIGN KEY([DepartmentGroupId])
REFERENCES [dbo].[tbl_DepartmentGroup] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_Department] CHECK CONSTRAINT [FK_dbo.tbl_Department_dbo.tbl_DepartmentGroup_DepartmentGroupId]
GO
ALTER TABLE [dbo].[tbl_Department]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_Department_dbo.tbl_Employee_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tbl_Employee] ([Sl])
GO
ALTER TABLE [dbo].[tbl_Department] CHECK CONSTRAINT [FK_dbo.tbl_Department_dbo.tbl_Employee_CreatedBy]
GO
ALTER TABLE [dbo].[tbl_Department]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_Department_dbo.tbl_Employee_UpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[tbl_Employee] ([Sl])
GO
ALTER TABLE [dbo].[tbl_Department] CHECK CONSTRAINT [FK_dbo.tbl_Department_dbo.tbl_Employee_UpdatedBy]
GO
ALTER TABLE [dbo].[tbl_DepartmentGroup]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_DepartmentGroup_dbo.tbl_Employee_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tbl_Employee] ([Sl])
GO
ALTER TABLE [dbo].[tbl_DepartmentGroup] CHECK CONSTRAINT [FK_dbo.tbl_DepartmentGroup_dbo.tbl_Employee_CreatedBy]
GO
ALTER TABLE [dbo].[tbl_DepartmentGroup]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_DepartmentGroup_dbo.tbl_Employee_UpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[tbl_Employee] ([Sl])
GO
ALTER TABLE [dbo].[tbl_DepartmentGroup] CHECK CONSTRAINT [FK_dbo.tbl_DepartmentGroup_dbo.tbl_Employee_UpdatedBy]
GO
ALTER TABLE [dbo].[tbl_DepartmentTransfer]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_DepartmentTransfer_dbo.tbl_Designation_FromDesignationId] FOREIGN KEY([FromDesignationId])
REFERENCES [dbo].[tbl_Designation] ([Sl])
GO
ALTER TABLE [dbo].[tbl_DepartmentTransfer] CHECK CONSTRAINT [FK_dbo.tbl_DepartmentTransfer_dbo.tbl_Designation_FromDesignationId]
GO
ALTER TABLE [dbo].[tbl_DepartmentTransfer]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_DepartmentTransfer_dbo.tbl_Designation_ToDesignationId] FOREIGN KEY([ToDesignationId])
REFERENCES [dbo].[tbl_Designation] ([Sl])
GO
ALTER TABLE [dbo].[tbl_DepartmentTransfer] CHECK CONSTRAINT [FK_dbo.tbl_DepartmentTransfer_dbo.tbl_Designation_ToDesignationId]
GO
ALTER TABLE [dbo].[tbl_DepartmentTransfer]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_DepartmentTransfer_dbo.tbl_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbl_Employee] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_DepartmentTransfer] CHECK CONSTRAINT [FK_dbo.tbl_DepartmentTransfer_dbo.tbl_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[tbl_Designation]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_Designation_dbo.tbl_Department_DepartmentId] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[tbl_Department] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_Designation] CHECK CONSTRAINT [FK_dbo.tbl_Designation_dbo.tbl_Department_DepartmentId]
GO
ALTER TABLE [dbo].[tbl_Designation]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_Designation_dbo.tbl_Employee_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tbl_Employee] ([Sl])
GO
ALTER TABLE [dbo].[tbl_Designation] CHECK CONSTRAINT [FK_dbo.tbl_Designation_dbo.tbl_Employee_CreatedBy]
GO
ALTER TABLE [dbo].[tbl_Designation]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_Designation_dbo.tbl_Employee_UpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[tbl_Employee] ([Sl])
GO
ALTER TABLE [dbo].[tbl_Designation] CHECK CONSTRAINT [FK_dbo.tbl_Designation_dbo.tbl_Employee_UpdatedBy]
GO
ALTER TABLE [dbo].[tbl_DisciplinaryAction]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_DisciplinaryAction_dbo.tbl_DisciplinaryActionType_DisciplinaryActionTypeId] FOREIGN KEY([DisciplinaryActionTypeId])
REFERENCES [dbo].[tbl_DisciplinaryActionType] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_DisciplinaryAction] CHECK CONSTRAINT [FK_dbo.tbl_DisciplinaryAction_dbo.tbl_DisciplinaryActionType_DisciplinaryActionTypeId]
GO
ALTER TABLE [dbo].[tbl_DisciplinaryAction]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_DisciplinaryAction_dbo.tbl_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbl_Employee] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_DisciplinaryAction] CHECK CONSTRAINT [FK_dbo.tbl_DisciplinaryAction_dbo.tbl_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[tbl_Education]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_Education_dbo.tbl_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbl_Employee] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_Education] CHECK CONSTRAINT [FK_dbo.tbl_Education_dbo.tbl_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[tbl_Employee]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_Employee_dbo.tbl_Branch_BranchId] FOREIGN KEY([BranchId])
REFERENCES [dbo].[tbl_Branch] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_Employee] CHECK CONSTRAINT [FK_dbo.tbl_Employee_dbo.tbl_Branch_BranchId]
GO
ALTER TABLE [dbo].[tbl_Employee]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_Employee_dbo.tbl_Designation_DesignationId] FOREIGN KEY([DesignationId])
REFERENCES [dbo].[tbl_Designation] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_Employee] CHECK CONSTRAINT [FK_dbo.tbl_Employee_dbo.tbl_Designation_DesignationId]
GO
ALTER TABLE [dbo].[tbl_Employee]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_Employee_dbo.tbl_Employee_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tbl_Employee] ([Sl])
GO
ALTER TABLE [dbo].[tbl_Employee] CHECK CONSTRAINT [FK_dbo.tbl_Employee_dbo.tbl_Employee_CreatedBy]
GO
ALTER TABLE [dbo].[tbl_Employee]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_Employee_dbo.tbl_EmployeeType_EmployeeTypeId] FOREIGN KEY([EmployeeTypeId])
REFERENCES [dbo].[tbl_EmployeeType] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_Employee] CHECK CONSTRAINT [FK_dbo.tbl_Employee_dbo.tbl_EmployeeType_EmployeeTypeId]
GO
ALTER TABLE [dbo].[tbl_Employee]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_Employee_dbo.tbl_SourceOfHire_SourceOfHireId] FOREIGN KEY([SourceOfHireId])
REFERENCES [dbo].[tbl_SourceOfHire] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_Employee] CHECK CONSTRAINT [FK_dbo.tbl_Employee_dbo.tbl_SourceOfHire_SourceOfHireId]
GO
ALTER TABLE [dbo].[tbl_EmployeeLeaveCountHistory]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_EmployeeLeaveCountHistory_dbo.tbl_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbl_Employee] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_EmployeeLeaveCountHistory] CHECK CONSTRAINT [FK_dbo.tbl_EmployeeLeaveCountHistory_dbo.tbl_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[tbl_EmployeeLeaveCountHistory]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_EmployeeLeaveCountHistory_dbo.tbl_PaidSalaryDuration_PaidSalaryDurationId] FOREIGN KEY([PaidSalaryDurationId])
REFERENCES [dbo].[tbl_PaidSalaryDuration] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_EmployeeLeaveCountHistory] CHECK CONSTRAINT [FK_dbo.tbl_EmployeeLeaveCountHistory_dbo.tbl_PaidSalaryDuration_PaidSalaryDurationId]
GO
ALTER TABLE [dbo].[tbl_EmployeeSalaryDistribution]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_EmployeeSalaryDistribution_dbo.tbl_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbl_Employee] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_EmployeeSalaryDistribution] CHECK CONSTRAINT [FK_dbo.tbl_EmployeeSalaryDistribution_dbo.tbl_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[tbl_Experience]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_Experience_dbo.tbl_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbl_Employee] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_Experience] CHECK CONSTRAINT [FK_dbo.tbl_Experience_dbo.tbl_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[tbl_FileStorage]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_FileStorage_dbo.tbl_Employee_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tbl_Employee] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_FileStorage] CHECK CONSTRAINT [FK_dbo.tbl_FileStorage_dbo.tbl_Employee_CreatedBy]
GO
ALTER TABLE [dbo].[tbl_FileStorage]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_FileStorage_dbo.tbl_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbl_Employee] ([Sl])
GO
ALTER TABLE [dbo].[tbl_FileStorage] CHECK CONSTRAINT [FK_dbo.tbl_FileStorage_dbo.tbl_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[tbl_FilterAttendance]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_FilterAttendance_dbo.tbl_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbl_Employee] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_FilterAttendance] CHECK CONSTRAINT [FK_dbo.tbl_FilterAttendance_dbo.tbl_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[tbl_Images]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_Images_dbo.tbl_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbl_Employee] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_Images] CHECK CONSTRAINT [FK_dbo.tbl_Images_dbo.tbl_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[tbl_LeaveCount]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_LeaveCount_dbo.tbl_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbl_Employee] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_LeaveCount] CHECK CONSTRAINT [FK_dbo.tbl_LeaveCount_dbo.tbl_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[tbl_LeaveCount]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_LeaveCount_dbo.tbl_LeaveType_LeaveTypeId] FOREIGN KEY([LeaveTypeId])
REFERENCES [dbo].[tbl_LeaveType] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_LeaveCount] CHECK CONSTRAINT [FK_dbo.tbl_LeaveCount_dbo.tbl_LeaveType_LeaveTypeId]
GO
ALTER TABLE [dbo].[tbl_LeaveHistory]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_LeaveHistory_dbo.tbl_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbl_Employee] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_LeaveHistory] CHECK CONSTRAINT [FK_dbo.tbl_LeaveHistory_dbo.tbl_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[tbl_LeaveHistory]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_LeaveHistory_dbo.tbl_Employee_UpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[tbl_Employee] ([Sl])
GO
ALTER TABLE [dbo].[tbl_LeaveHistory] CHECK CONSTRAINT [FK_dbo.tbl_LeaveHistory_dbo.tbl_Employee_UpdatedBy]
GO
ALTER TABLE [dbo].[tbl_LeaveHistory]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_LeaveHistory_dbo.tbl_LeaveType_LeaveTypeId] FOREIGN KEY([LeaveTypeId])
REFERENCES [dbo].[tbl_LeaveType] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_LeaveHistory] CHECK CONSTRAINT [FK_dbo.tbl_LeaveHistory_dbo.tbl_LeaveType_LeaveTypeId]
GO
ALTER TABLE [dbo].[tbl_Loan]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_Loan_dbo.tbl_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbl_Employee] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_Loan] CHECK CONSTRAINT [FK_dbo.tbl_Loan_dbo.tbl_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[tbl_Loan]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_Loan_dbo.tbl_Employee_UpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[tbl_Employee] ([Sl])
GO
ALTER TABLE [dbo].[tbl_Loan] CHECK CONSTRAINT [FK_dbo.tbl_Loan_dbo.tbl_Employee_UpdatedBy]
GO
ALTER TABLE [dbo].[tbl_LoanCalculation]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_LoanCalculation_dbo.tbl_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbl_Employee] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_LoanCalculation] CHECK CONSTRAINT [FK_dbo.tbl_LoanCalculation_dbo.tbl_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[tbl_LoanCalculation]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_LoanCalculation_dbo.tbl_Loan_LoanId] FOREIGN KEY([LoanId])
REFERENCES [dbo].[tbl_Loan] ([Sl])
GO
ALTER TABLE [dbo].[tbl_LoanCalculation] CHECK CONSTRAINT [FK_dbo.tbl_LoanCalculation_dbo.tbl_Loan_LoanId]
GO
ALTER TABLE [dbo].[tbl_LoanCalculationHistory]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_LoanCalculationHistory_dbo.tbl_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbl_Employee] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_LoanCalculationHistory] CHECK CONSTRAINT [FK_dbo.tbl_LoanCalculationHistory_dbo.tbl_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[tbl_LoanCalculationHistory]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_LoanCalculationHistory_dbo.tbl_LoanCalculation_LoanCalculationId] FOREIGN KEY([LoanCalculationId])
REFERENCES [dbo].[tbl_LoanCalculation] ([Sl])
GO
ALTER TABLE [dbo].[tbl_LoanCalculationHistory] CHECK CONSTRAINT [FK_dbo.tbl_LoanCalculationHistory_dbo.tbl_LoanCalculation_LoanCalculationId]
GO
ALTER TABLE [dbo].[tbl_LoanCalculationHistory]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_LoanCalculationHistory_dbo.tbl_PaidSalaryDuration_PaidSalaryDurationId] FOREIGN KEY([PaidSalaryDurationId])
REFERENCES [dbo].[tbl_PaidSalaryDuration] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_LoanCalculationHistory] CHECK CONSTRAINT [FK_dbo.tbl_LoanCalculationHistory_dbo.tbl_PaidSalaryDuration_PaidSalaryDurationId]
GO
ALTER TABLE [dbo].[tbl_MonthlyAttendance]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_MonthlyAttendance_dbo.tbl_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbl_Employee] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_MonthlyAttendance] CHECK CONSTRAINT [FK_dbo.tbl_MonthlyAttendance_dbo.tbl_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[tbl_MonthlyAttendance]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_MonthlyAttendance_dbo.tbl_Employee_UpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[tbl_Employee] ([Sl])
GO
ALTER TABLE [dbo].[tbl_MonthlyAttendance] CHECK CONSTRAINT [FK_dbo.tbl_MonthlyAttendance_dbo.tbl_Employee_UpdatedBy]
GO
ALTER TABLE [dbo].[tbl_MonthlySalarySheet]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_MonthlySalarySheet_dbo.tbl_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbl_Employee] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_MonthlySalarySheet] CHECK CONSTRAINT [FK_dbo.tbl_MonthlySalarySheet_dbo.tbl_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[tbl_MonthlySalarySheet]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_MonthlySalarySheet_dbo.tbl_PaidSalaryDuration_PaidSalaryDurationId] FOREIGN KEY([PaidSalaryDurationId])
REFERENCES [dbo].[tbl_PaidSalaryDuration] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_MonthlySalarySheet] CHECK CONSTRAINT [FK_dbo.tbl_MonthlySalarySheet_dbo.tbl_PaidSalaryDuration_PaidSalaryDurationId]
GO
ALTER TABLE [dbo].[tbl_PerformanceRating]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_PerformanceRating_dbo.tbl_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbl_Employee] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_PerformanceRating] CHECK CONSTRAINT [FK_dbo.tbl_PerformanceRating_dbo.tbl_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[tbl_PerformanceRating]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_PerformanceRating_dbo.tbl_PerformanceIssue_PerformanceIssueId] FOREIGN KEY([PerformanceIssueId])
REFERENCES [dbo].[tbl_PerformanceIssue] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_PerformanceRating] CHECK CONSTRAINT [FK_dbo.tbl_PerformanceRating_dbo.tbl_PerformanceIssue_PerformanceIssueId]
GO
ALTER TABLE [dbo].[tbl_PromotionHistory]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_PromotionHistory_dbo.tbl_Designation_FromDesignationId] FOREIGN KEY([FromDesignationId])
REFERENCES [dbo].[tbl_Designation] ([Sl])
GO
ALTER TABLE [dbo].[tbl_PromotionHistory] CHECK CONSTRAINT [FK_dbo.tbl_PromotionHistory_dbo.tbl_Designation_FromDesignationId]
GO
ALTER TABLE [dbo].[tbl_PromotionHistory]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_PromotionHistory_dbo.tbl_Designation_ToDesignationId] FOREIGN KEY([ToDesignationId])
REFERENCES [dbo].[tbl_Designation] ([Sl])
GO
ALTER TABLE [dbo].[tbl_PromotionHistory] CHECK CONSTRAINT [FK_dbo.tbl_PromotionHistory_dbo.tbl_Designation_ToDesignationId]
GO
ALTER TABLE [dbo].[tbl_PromotionHistory]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_PromotionHistory_dbo.tbl_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbl_Employee] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_PromotionHistory] CHECK CONSTRAINT [FK_dbo.tbl_PromotionHistory_dbo.tbl_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[tbl_Resignation]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_Resignation_dbo.tbl_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbl_Employee] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_Resignation] CHECK CONSTRAINT [FK_dbo.tbl_Resignation_dbo.tbl_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[tbl_Resignation]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_Resignation_dbo.tbl_Employee_UpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[tbl_Employee] ([Sl])
GO
ALTER TABLE [dbo].[tbl_Resignation] CHECK CONSTRAINT [FK_dbo.tbl_Resignation_dbo.tbl_Employee_UpdatedBy]
GO
ALTER TABLE [dbo].[tbl_SalaryAdjustment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_SalaryAdjustment_dbo.tbl_Employee_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tbl_Employee] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_SalaryAdjustment] CHECK CONSTRAINT [FK_dbo.tbl_SalaryAdjustment_dbo.tbl_Employee_CreatedBy]
GO
ALTER TABLE [dbo].[tbl_SalaryAdjustment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_SalaryAdjustment_dbo.tbl_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbl_Employee] ([Sl])
GO
ALTER TABLE [dbo].[tbl_SalaryAdjustment] CHECK CONSTRAINT [FK_dbo.tbl_SalaryAdjustment_dbo.tbl_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[tbl_SalaryAdjustment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_SalaryAdjustment_dbo.tbl_Employee_UpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[tbl_Employee] ([Sl])
GO
ALTER TABLE [dbo].[tbl_SalaryAdjustment] CHECK CONSTRAINT [FK_dbo.tbl_SalaryAdjustment_dbo.tbl_Employee_UpdatedBy]
GO
ALTER TABLE [dbo].[tbl_Weekend]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tbl_Weekend_dbo.tbl_Branch_BranchId] FOREIGN KEY([BranchId])
REFERENCES [dbo].[tbl_Branch] ([Sl])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_Weekend] CHECK CONSTRAINT [FK_dbo.tbl_Weekend_dbo.tbl_Branch_BranchId]
GO
USE [master]
GO
ALTER DATABASE [FTL_HRMS] SET  READ_WRITE 
GO
