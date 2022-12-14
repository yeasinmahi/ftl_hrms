USE [master]
GO
/****** Object:  Database [FTL_HRMS]    Script Date: 1/28/2018 2:50:55 PM ******/
CREATE DATABASE [FTL_HRMS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FTL_HRMS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\FTL_HRMS.mdf' , SIZE = 7360KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FTL_HRMS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\FTL_HRMS_log.ldf' , SIZE = 114432KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
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
/****** Object:  User [su]    Script Date: 1/28/2018 2:50:55 PM ******/
CREATE USER [su] FOR LOGIN [su] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [FUTURISTICTECH\Administrator]    Script Date: 1/28/2018 2:50:55 PM ******/
CREATE USER [FUTURISTICTECH\Administrator] FOR LOGIN [FUTURISTICTECH\Administrator] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [su]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [su]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [su]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [su]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [su]
GO
ALTER ROLE [db_datareader] ADD MEMBER [su]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [su]
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
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[ApplicationUsers]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[IdentityRoles]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[IdentityUserClaims]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[IdentityUserLogins]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[IdentityUserRoles]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[MenuItems]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[RolePermissions]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_BonusAndPenalty]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_Branch]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_BranchTransfer]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_Company]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_Department]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_DepartmentGroup]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_DepartmentTransfer]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_Designation]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_DeviceAttendance]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_DisciplinaryAction]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_DisciplinaryActionType]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_Education]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_Employee]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_EmployeeLeaveCountHistory]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_EmployeeSalaryDistribution]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_EmployeeType]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_Experience]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_FestivalBonus]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_FileStorage]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_FilterAttendance]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_Holiday]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_Images]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_LeaveCount]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_LeaveHistory]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_LeaveType]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_Loan]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_LoanCalculation]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_LoanCalculationHistory]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_MonthlyAttendance]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_MonthlySalarySheet]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_PaidSalaryDuration]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_PerformanceIssue]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_PerformanceRating]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_PromotionHistory]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_Resignation]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_SalaryAdjustment]    Script Date: 1/28/2018 2:50:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_SalaryAdjustment](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Amount] [float] NOT NULL,
	[Remarks] [nvarchar](max) NULL,
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
/****** Object:  Table [dbo].[tbl_SalaryDistribution]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_SourceOfHire]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  Table [dbo].[tbl_Subscription]    Script Date: 1/28/2018 2:50:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Subscription](
	[Sl] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.tbl_Subscription] PRIMARY KEY CLUSTERED 
(
	[Sl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Weekend]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  View [dbo].[BranchTransferView]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  View [dbo].[DepartmentTransferView]    Script Date: 1/28/2018 2:50:56 PM ******/
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
/****** Object:  View [dbo].[FilterAttendanceView]    Script Date: 1/28/2018 2:50:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[FilterAttendanceView]
as
SELECT        e.Name, e.Code, m.EmployeeId, m.Date, f.InTime, f.OutTime, m.Status
FROM            dbo.tbl_Employee AS e INNER JOIN
                         dbo.tbl_MonthlyAttendance AS m ON e.Sl = m.EmployeeId LEFT OUTER JOIN
                         dbo.tbl_FilterAttendance AS f ON m.EmployeeId = f.EmployeeId AND m.Date = f.Date
WHERE        (e.Status = 1)
GO
/****** Object:  View [dbo].[PromotionHistoryView]    Script Date: 1/28/2018 2:50:56 PM ******/
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
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201710231032476_Adding Subscription', N'FTL_HRMS.Migrations.Configuration', 0x1F8B0800000000000400ED7DD96E24B992E5FB00F30F829E661AD55266D5BD8DDB85CC6E28B554C66D292548AACA79135C1E94E4373DDCA37D51A5D0982F9B87F9A4F985217DE562DCDCE95B64A0802C85D368341A0F8D9BD1F8FFFECFFFFDF0EFDF37E1C12B4AD2208E3E1EBE3F7A777880223F5E07D1F3C7C33C7BFAE7BF1DFEFBBFFDF7FFF6E17CBDF97EF0474DF70BA1C339A3F4E3E14B966D7F3D3E4EFD17B4F1D2A34DE027711A3F65477EBC39F6D6F1F1CFEFDEFDEBF1FBF7C708B338C4BC0E0E3EDCE651166C50F103FF3C8D231F6DB3DC0BAFE2350AD3EA3B4EB92BB81E7CF13628DD7A3EFA7878717FF9F0F9F6EAEEE8ECE4F2F0E0240C3C2CC31D0A9F0E0FBC288A332FC312FEFA7B8AEEB2248E9EEFB6F88317DEBF6D11A67BF2C2145592FFDA929B56E2DDCFA412C76DC69A959FA759BCB164F8FE974A2BC77CF64EBA3D6CB486F5768EF59BBD915A17BAFB78F8298EF2F4245ADFA0C80BB3B7C303BECC5F4FC384D0531A2E1BE3E8C67B4BE2303CE258FC745013FED40003E387FCF7D3C1691E6679823E4628CF122FFCE9E0267F0C03FF3FD0DB7DFC0D451FA33C0C6981B1C8388DF9803FDD24F11625D9DB2D7AAAAA71171E1E1CB3F98EF98C4D362A4F59B35594FDF2F3E1C1175CB8F718A2060F9416EEB23841BFA108255E86D6375E96A10437E76A8D0A8D0AA573659D6FB661FC86D06AAD2B53CDE70C975E73207FDFE3DE62CD8464AF99E0DE80BBF4E1C195F7FD1245CFD9CBC743FCE7E1C145F01DADEB2F15D7DFA3005B009C294B72FB524F3631EEDD8DF0316E787B26B718EFC9B7747CE94F13441AFED35BBFF62BD93869C5DFB76B5022935CB0005CCE2FDE6BF05CA01FAC440DE9C3835B141664E94BB02D6D2E6F131EF82C1749BCB98D43D10071940F77719EF844D6D888FCDE4B9E51665E15AB4A98886F2C786791CB16B4129CCF22179FA5D456822387AAF2E1B81D739423512B9EF910F43939AAB3ED871DC8DC6035296CE5FBBF0E612AC9BF8A427FFEEBBB214ABDF0B217DCECD3147E154F583886C71A25E3977B93A014C3F064BDC67FA886E4A1CA47093612534A70153F06A1AAC5FF324CB9E71B2F081D5457D38F576771F22948B29753FC4FF014F8D4AC613C259F25C12B2EEA32F0F102D445F91A547969BA8D93EC4BBE7974D2ABF413FAEBA742CBBD678425AFBFC74154C8DA935B39F65F3F7D0E92FECB169406CF5131A4F565558FF784475F5E9F122FF25FFA72F90DAFBDD33B2FF492B79EAB1BD9226377D6145CCE557AF7966668739DDCE5F8FBC97A1344358F4F319E727A913D6EF1D4294FFB72C13F1F0BBCBA61872BBA457EE085ED2CB7A77C5E35F87551FBF906E1897AE4BF391BC074ABF672893276B99FC2385EE3EE996F1D6C1868A602688DC7C7F07380972EAD1D18ACB8CF28787EC9062FE6EB38C59C7FC7ABB2D33C49023FC766F4C4CFF0909F05C8C53E8FE9B2BA1C0BC0E574B3BEAD69DAF53397242C98F974DBC5BEC1564B53847C8F45422297B6E7AE0A35DCAB4566080179A974B9B03491F5FECF3AF71572D6A9E0D68F98DA14DFC82892D4D5B0DDA12A376D55CA6429016DD20472753254D6FAFC8E7F05E5441D92B54986352A268B2A05686C75BADA78CFC4B800129649A0745C9220199F6E2B153DE156B7344B09B4344D206F6986AAD7DE5D6D18AD76EECA4CFB7D3BA0AC693692F4FB28031DAE5C6F1159B29653D79E4B98F368ED8AD52ABDC4594FBD90CC4A7033F69DB317DC708F0ED6041618268C94D592D192C599272C3AF51CCE101E970A0B84B00188326CB7EC98ACD2EB57FC138BEF4E3B0DC77E1AAAD9DC786F1B5CB5AE35ECB2EC333697CCB4C7CA665239F78613284B73E0E166B9328D753E435B2FC908A6FB6E58916980A60E239FA2EFEC0657C7DDA33EEB417A39245F12CAA984F9A282D47E615863582B3A4D0A8BDD522845A6C806386AA74B921FB3CBA994A2BB3C5EA7F56939E4D419F7230E649C7E8411A7D843ED3BECEC8DFF18C6BF31762ADB2F21028C918CB2BBE5AFF6E3D5C20BE4A0F41C954A7C9ED4F59666E7A18BDF45D28C704E87AE86BB6AE49210A9943DCCB85521A1E3E055E4DE8F609085DDDD116C3FE48C39E49496D564DC81281506052477BEF6E839F888865C3B4E0D62CCCB224C2C3A44A96D05A72EBFEDE99B9DCF6F9D6F6FCF81B256518AC9F24CB7C733985B68FC9C789BF10BFE147BC9DA41B13A5F9614E36A02FF66DC81DDDCFE899DB0E97CA3A9DB5D0CEBA378E1D4537E5ADFEBBA4279DADEE9CA0221D85B30A0AC690CD7A0473EB463841D589A8C7BA800654D3CD835C54F7607E12B7A4C03277EF99A61FD258E54A5B8F1D864B659F643ECB843ACBD739630C82A1CB83A99CDDA53CBCA649699F6E612B2571BCA13E253101577164C367BE60D5D0BAF3D1EB232AFBE4E70651DF3AC404B67DD4317286B072785A507E43DFE377D2277CD3AB84FD699F7907169858089819BBB62F7B1233E55B3779B67740C87C0000E8E8600938871042474B67BBA6DC398484C534B656E8974525394B672D7303091BAA595CA5C93E8246EE87A0D72A7F166EB459D42055559F7C60A28EB07F3F91EE70EFB6EAD8E47BA36791212E46250BFA2D12E883E076956BA7C7F8947D8C36F8B73B3E05F7D19295E009E8F261966DD41EC0E2E171DA7A62283BDC5074DA0BBE9A9C3080BF7B14B66134C5445F88193550599E214DCCDA455EF4E2694276453D684A336A9109FC57E52DBA1565C26659D185A931AB1197ABAA1BD063E3AC13D3C5A7B96A738F5EC97E7B1378A0AA3E8267499C6CF2A45496FAFE617E47F7374DDB0EB653A731407A91F6CC362DFF7C4EF72FF4C60B007B102C4BDE32609EA26DC661144D65D245663073E5019F04023903EC87253238E692671E831CEE9343E29502C38CF919399D4C4911BB744FD3DADCFDE854652D618BB49D6BE5297C87BC5037B1E654D9CA44E8E53029F3D0480B25C0D42375EB02EC3DB9DE5899345E1B99744451B9E796FCD08D23166DED7207B89F30CCFB13B73ECE82E210323EC3DA1A596C6695164B11D4EC4A6B4AC1AC4C0A492623E8BEA02997B8D455025EC57732297BD1502CA9A9B53D5D738F956EC9FF60DD659ABC48954044CDD0268929C43F9AC563DB24239D9320F1EF3AEFD45CE6DDF6F80B25C8DDEEEA2D37EF2D2C077C2E9739CA7E816F57E0DA40ABA791286F19F1E151CBA23BBCBE009ADA2344F1CF0BA88E3B52BB970C7C4BFBC20DA745059BFC98DD85995B31B05B974BC57E5E935CC5FA0340B5EBDB078B4A28BC56218EC8D146C12D0FA5AE5AA3D90F740D12437B819E0D85ECBDE1073337A5F042122CD5BA8C76A994DE5DCA31E28CBDDC2BA0DFA3F7D0CAE4E6C1C1FEB1A5C37A7C0A9B86A2EA712C62105A9D3DD5ABA1C9DC846C23A1A2503E2EDD3EF6093E7B1B71A035A0D2783D52A727264799D67131F7DF6E98E0C66657D1226823AA684B257EFFC1C87C1DAEBE46B5B65DDF7C5A1FAD0DC267CEDC6A9E57CAFCDB8070B50962BC35DA8D9C5F9FDC9AB1714F41D3631BB19CB1621A0990492050309D1D8CEAF1A0DEA84A40841299B7495982D512F134EC962DF2BF7E7DB92B2A6B92DD1FFD460959EAF83CC7B6C9DEA1D6FDA17B0E976BC4E67DD830E286B762381C34875733BA7A3BA5A47DD78793A81899836E2DF60F11B0773F713ECE31D42F66F00F698D6A8BC36400278CED0DB37C360725397219FDE08146A61D5531C95B40621099992E4F10815646AE15D4622BC8CBD4E87DA24DF7EA804CA72365462059F6CCA456DAF8997C3A192C8748BBC5419A368A0DD7B5276EBB5D447B3CE068E4E036497A1AEDBD0BA9B231786013C62D109A2FD645287B0F9A40085AD179361195DDBF67A7BB9A3EF12C7626FF12596C9D5E3DEB319357A98DBEE1D9B429AB48F433460570209ADE7A6C5F4C840DC92502E6AD1B9756216442EBB7C87ED1049CFDFEF8EC8CB727F4F8053FEF2AD8B139BA05CB0AA49755DAFFF22961F6BCDAB2364D5D68AB71B8695E3B30D74894252BAFA06857126D3CABABE3B7185A27C95A18D8D2DADF34C62354967B7B59A7A03E1D26A269890A84728B5D709C82FC33CA8124719191751A2297EA0C56379EB73A2B2C3F0228F0A01865F4DFE11A03FC95FBBB3B34BACC075B26E6324751B7C577E741A7A4E2296753380B807BC846FFD1CE60426FB092550D6AC3CE6A6DD2E1BEF3C6902573CA13780535B3995301752900EB0E7259626DF00D3D11A54C5E5D658C5BD9C20DEBD2064E53AC659338ACBDE9C0D68CEDCAFB3E77847F0E491DCEDE9EF7053F2B9F12214667D795D1636D505971B1479FDE5F93D8A9F9E023FF0C2FE52B5BCDCC8561CD5BA61759DBD601BE286177729D0815C2E389DACFF81AD2EB9DEE96CD7DA09A32F8804D61867974B1C4554D300884C367882B403EDFA0045AA777C8C3298D4CC79940C943CC5C986CC3956699A5BAD733E27477CF6FD9400EA5F2378B17669F05B2F2B64E9DAE265FE7D930365D5AAED6597A77D5E8AB78C5C4F1FE9CD1F016EE07021A7126CAA82D47AB0106CA75105C46CAA8AF0D4061512B2F41B21B07071C7F355622FB8EC7B733160279D738CF00608CE1CD99DAC40EFE36E6C3A9A33AE37C0D64C4624F67D29E510E1CA85D214C1CA75B4FAAA0C1FA85C28521AA65C4DA9AF8BC310E5B7B484569698CAB937C24059A57E1C5D1A9EC66FF82E7F7E265B1F4ECA9EC58D14870EDD639FB18C75E4EA6AF230AA6B33658CC031104A17EC2C4834C0D90F5D8EFCD4474EA514DCE5490FE18E67FF9B204D2DC70736E7DE8707EACF58456D79D06B153FFFCD910745E9287489670FC31B582F225E28D6DD5E6443AE3C3B60738642D41ADBFE6688341BDCAB19C83F947454771693C57E0CD0F47B09BBC262298CBCFB5E057E12A7F1537674926EBFA0ECA8CE7854B2BC4830BB3FE3E4DB11CDF1A703E37C6DBFFFD9B4DFFFF2FEF1E997BFFDF55FBCF52FFFF217F4CB5FC7B7013DFAA4ED91891BDF34E3312AC58D01BF894D35EF4345463D8B2DA48A2F638B244E104C58B94771CD75FE48AE478B633D69FD40912DF09D0D48B61369E641A5A1CA357F8EBDD8BF690F57BB38B7F03CF60B64A0AC5979EA3939F91E29B8976C593B6978CEB157C77DC281F2DD53111354432A8C7F3A7AA7D14185C28C6A602EFB908B60A130F94A5843AAAF87CB35B19B770DF6EF1998860ADFBF1E60CA6B1EAF079877A4FC31F59360DBE1E881CEBAEF3740599A7750DD6CF50CF99AF8C9768BDBA9D005591ED8E083CBBAC0BDC7F1965EA785565DBC68BB4AC92DBB57FB8D36DB45A8AB430E2F08C729E5348E9E8264D3E1628CE04C9AA67FC6C9FAB397BA780140731E87FC3CC17DF22EF336DBC14BBB798923F425DF3CB617FDC628CB59D3DCFF195F783EB6D9E711C9D59BDF65EC7F8BF3EC3C2ADEF7FA3DF36D0F0B1B064EC439F17D94A61718CC687D4AAF93BBD90A626F46DEF13C0DBD60036F7972E3C5434DDAAE3F600A61D92121B38F86F01C4466A2D6A472514B0AADA81599ADA8849999A415A55CD082402B6749E56C43B96821F73BCA05DBF96F29CFFD8054BB2FECE8C492B41629639C92FEF0C2DC75519DC05FF479F7E02FD8CE1FFC5D0F49C639AC800674AC56FCED35588F3147AA0AC2DA980AAA5F11C26DBDEEB2B95665DDEF0C00657D223B4D2FFD8F5CDE2C57FEAA69442913388FA89AF2A1266927106C8A3073E0926DA60C27691AFB412109B7095F8BC1D60BCFD20FD8D72905A1CA1E44BF91893B12466040A63758848F87FF24A84BCEB6AE0CC5B6D60FCBF4FD210FD8EBCA1FE6A08C89433C6452DF5B8B8D8575B366BF608C23127528F0C89236C5BD268832B14304911F6CBD502D3A97CDB02711B19A02F89433B4C5ED8DE553B78549C96D1F11CB6F8AE1D4A5D3CE87630A568668E34EA5B4F0901D4D39429FE424CB80FDBBA3A3F74209FDF0040B3326AE606D9B48409DD64E0230FAF28021C6E459209831D71ECC91A6286412B0E9E519016F7ACD2F03725B2F01CFDAE56090E480015713DBE14D56C44470D388330ADA345A5F16D87E4BE27C6B8F38289B1A76458E6ED8030B9B18802A994645A1AA25960845CEC9C5141D32879741A028F19699148AB04C1340116E091341288FB8A987601E2C060324AF87210661BE0C00791A984FBFE0D55767DC515CD26E26427059275B0F53B5B1369F5696B3236CE7652FE7602A176D25DBE516850C93252B55FF61D6C454014AD33847ABA8AAC5C8AB68B19DECACE18486507A6FD8043E26A6B03746E7610C75F28C8CB8E59AC346661A1ADA9D62309889A35D6828F2891984A73783AA4A8CB9750DB48F990964A2374D03C875EE73611EA49011484130D654566814595B58BA1900512AFF183094B68B49E1F4ADC1694D62FD47E13BA4B55B34B573A3C8305720B174739A1D1A55D518D32E426D648349926F3A5C7EC75205887909410A1C9116C4644366854A80F9B2ACA3BC0263A051DE368BB08FAB8DF78C523D06393A087F25890DF678A68BC29D44F8113027698B45E08D13FAFAE97390188CC734B5F3F198610E609011757638545663CCF1186A23A3F2A97C93E1B278E2E1245A574F50989E42ABB34148E572D8005653D8A2ACA7595D4680AF590B9A0832F5E9355F136BF08E04DBDD00EC0CA0BAC809005F09C3AD7175B681113B876D7233992640E172B7CB4BCFF17BFC6F8A791AD84B981E041F436A853D4921CB3296EA4A8C8152755B2DC354B27520B76934575AA43906C52855CC822EBB682B313E4AC5F63291A1CD3517A4DEC77638ADE907456953C87231CA57617C84F22D6522419D67062E69E663BD228FDA2DAD0B5255852D6ADC37A8C808983568BB458CFF403DF847772C10257D83674014CB9EF1598A0B887995A681B5A44D4D670D737011016AC5BEC4640137C9C34C03021C7EDB69C1F0062B340DB8C1D6349B70CC02D841EA93C68E48745BBFF465153E29BD518C3980501728ADA06E5C348479B89AF383BF6D25C7E807B66D6E2213CC614E3D433F1F97E71905FD0B9D8FEB2B320DA617391FAF05B844DE2B2A222B0A0F79CA10A5CFAAF26A10727571735014BD284C9BD76704689BB7EBC2117EE305EB2A0C7E9E985D2C30613201EA012100FC03F55D504F90D771D23E216F7F13B1C4DC93F713F16108F3A1409157D52BA0172DECBB85AAF0458E0606151A11FA066DBB88F1E02208110961E83D1BC737936781504D51DBC05851C8A2D0ABAFC708A0D5B7988910537BBFD1B5B002E9C0F05C3E302786E4520D678692932CC3057946977CA43924B864882DC129296869085557631C98AADB6C11586DE7E97A9402B4103E5B321B6442CC17854945054640A3A26D9686C3E24FE5D908443C08125BEE00145B39E78C45A10AE38251681F93E29B4CD3C2D1788317A49602B2C386165CC0F2CCE3849BB5CA365A8E89AC6B60682405F2C150B9685329ADC4D8C0DC05736978A94791673090CEE12E8F814063A36EB9B7782E63CF60D79DA102C1157B563BE92CC3658DC390E863000E6A83658CBB447253A326D23AC3DB2C8C975C90B130B46C6375EA857E1E1A8645936590418AA2B5451758CCE20C9BAA1623E153D55E8B317774250A5B650A9DA2870E8BCEA208688D01D8D4F9A192967E0244D2ED63B4B4C0F47341A2F9768C3A9F013EBBAC3334852ED9964EB96D63D6964BB4ACCD9A9FB38D9608E3BBF898E8E6CB96986585E59F2DD625559B0EF2927636B5E254D6B975000BF752630EA37683DDF12AB5AEE074FD6167FC49AFE2287B09DF6CFC45E45920DC0BD436905714B5A8298DBE1E234059DF6E8B98C888D530DC1ED3651C01BD73D84533956A12442E777FADAA4B69D6EF5E1032F06952E451809122EF8046B0B0251A535545C6C3AEAAED96644EE97A584C898D728F82E5DD99065B556E1AA4EFCCF4F706254F71B22163CF2DFE1E3DEB8DB63C0B847381DA06E68AA21665B1F5F51801C6FA765B84BD16AB417D59A5696E835C3EEB0808168A84AC345FA305205A56AF49902D6B5723EBCCE59D0EE949BC89ADCE5EA439405C73C456B09616B42CBBACABC618E0D5B5D932AC325F0BD37079BA8C832377E981F24C2B3405947720489E5027B31079EA6C83837AD9C1F1CCAA3305A0171E18EF96125E3B99808821E0DECAA1A4C22CC87E51B307550D4600A7AA7D163167A02B60788421CF321032E77060A1976764B42DF7906245D8E3E621757AF83D45492A859A480ABEBC4851192140590000AF9A8A1040857040B6E97E981B5EEB6E8234258D5A309776099116EC700C99559F030A5068C358DDDD3A9C5C181390C306D5BCA7C9756D523AA19FCCA497BBDA27EB7FE4691151D8347491261F84353E8B0DDA74C52D6A1A62589911C608C356349164EA98464255EC213C1A787704B67300EC22E7D1422D0C27D39A7C83E3760E736B43A1A6C0E27267D9275BD252C517326D7D380DBD60239F69C3E410FC384ACB09B7A41CCDA4BBA07237EBE685B88C9F83C85C3725F9F0BAA9CAD1E8A6A01A4E37443473D514D4C36BA62C66D495DA5784BEE13EFBA079DA8D2583345151D8586D8EE9825E7083251FC192C3ED6052F0A8AFB59D1768C579329C032595089F6FAFEECE1EC957F41DB7B38F47A378E345519C150C7EC5E83E0D13D2FCE9C7C32C01BC2D08D73B94D555E2DF022E0954AF050B586439B6930281559BA4E1512A1A1B17409C0ACE1A0ECC018AC08449D572AADFDF0119B589C67C7E4BE27CAB645651E834BDCE7D590DA934C3F62A231049DBAC4CD6F1FA8E7B4C800A3F74911395A8E1B3DA78CF60EBD7099AFC659FBE7EFA1C2490246CB21116DB57B124886C0934FC4EE3CDD68BA09ED6A4182349211544A4E5FB1AF888BE4B0070E549743C81E76644AE0091355F097E658486BD02786A41DA45005A4D2990BBAFC01E2232941E0A892F151F22D6947381D22C78F5C2628C005873E93A6E744C6991179DAAE7C4450186D871241A9E9FE330587B50FB37291A0E2D3E205DD1F1384D181563BC8C8F11C6A93057819C952996CB1013228FE2B3415EE61E34C886A1B0E328EFBC32420DFF2B14E5AB0C6D008E6D928E8778794B6426D29871656E2EC8D832443A5B2538BD8A964A2031E759FBEEAA98D6343AAEAC5B088C6DD19F47C3F55639A3BCB59851F2C77022338EC0801F3859624EE374532E6E79AC62D82EA175F330613F529C8B0924463C35635A87B1EC2E7F4CFD24D8CA3832C91A5EE5D1B5C844D8EAB068946A974EDD2AD56E9805DB6A834BCDB6DA48D2B06DB62F045E4D0AC7815AFA8A93977A3FE580A2E2A62CE0960BB33FC25136BB2E8DECF4825558D5CBF9D41B2D149F76D1CA6F98B0D5B45101773EAB5285EA2817AE8AE410B7B36A24A7B406FC3AA88876F4D36B49412DAF983C13A42BC59E8229D7A13556AF088D14262356D54C920756976CE7C490E758CA2AF662AC3406E630A9229451AD3B70A3C886FBB85AE44E3E0DB4A83A2B55D6537258EA488B92B3D1C13BAE20B3B2E7F2D446DD8CAFAA93BECB330514A6DBFDECA5371BE075C19C15DC4C953632C8DA618916553D96529466231E553D476328C551892AC73A32419499FFB4B4662698B2D7D7A8A86A8A612455CC62A59777E029277469A7F3FC15BA9363A6EC2E9AA90F681E541802A814151188419DC84E8674BC46420A7BE0A4800A4368D0BC347D7FB030DC14AA0177623BA8A7393C53E30520535446A40615233DB8D3721B1633E569A052213C895C7C8E1252047CFAA8E43252A7614F34159D86213480394DDFBFD330DC00D5280F66EDD5C3B92E182CE23439E45554678434A7F1ABB0E13E2CC8F8C26DF4D7417343E96C226DE9E7899A1CE635D4CF17FBE86FD47923EB53A1869C845451333807A830A56F8711D7511545AEE5CBB7CDE5C4C6D56AF3B85517C577D8AD75AEDCFBD8585D0DA971A5EA1C6E55D5701D5651A20391B21BAAC84D763B8CBAA3DEA9C998FB585B760CC235AB60835C569595C56A71AA51598096C156CE800C6CD00F33CD2A0285E8AA0C070B71AA553842C8703A15BCE41E648E7380728D332B5460CA0354B7D67FB0635990DECDFC099D3481D2DE2AC8ADAAAEB4B7BD153BF2425870C154EF96E873E917B5F2CCAAF5B2D657D4B6ACA9F40B79A7DA685A17CCD7500F8AA8BE036A5F11D4972AD5C081B77B8B889E4E46905765D36B42915BA576BD57967569C3C29EF23936D83C5250CB2B29CF046952E1046DCA753C8D99EACA524BAEF5339E661887739D7A24C4CA3AC179248A52BABF1B721E5665ADC1552A0B22935706A08614C438EB2B7403B11B4D2B94AFBF522D2D9D51458487D67B2A4678395EE0575D6670A31B93A91F4CA8A98DC9048FBF54A1D3D0C81339A64C3D7C444AC3EAE821D4414713E148BF85AF2237AC9B7EF3BEABD646DDB867DF9C8754257F949EAD02F82C3DAD0EE0629182C308D536C089EEB574B10206B83052C4E828001FF396E844FFF0B75021E5D3DF9C761417C7CCF88EABADB241F59A121F9F56D686797EDA918698F7A63598ECAF19A381DDE65D6455DD8C067BA3CB8356A58C0A35D92BBBE67A55BECF6B5271D90BBD836859F624AF796F70A773B38DB38EAFC29A28C36CCBCC9DF227DC2C53BC220AE8DCF4CD51A6D206AF8E52B5D45EEB35E53DACB9D0BE6569A43DE3098FF10B980E3539EABC48F508A35C95FA371BA13A2A5F6D1415A8B80A6ECC7D74E599D9D00E4F08EAAA6C663BFBAB76429BA9787A0ED0B1E943754C850D9EAAA36BA90B0460CA7B58981A3C8066A43FF5B3699ABA4A1F4E73A84FE94B6970197044880EFA953EBB05A9D5EC8D2EB6A6DA57BAE80A02B126545AD43ECC35102875EF3F99E8CEDC17C9F8D528879A1CDD0F49F3F49089464D7D900C9F2B72A8CD91FD8FC0976F000DEA5FC861AAA57C2387AA8C227A8B9EDFB03D57F10A8B463FC6D36E83575B7AEA6AD44936F08808743148F3D4085317C56323B466CAB039AADB41F23745E86B4662289EFE308AC5D74420FC00648A2616A9657A918733D2720434E44A2BBAF730000D593DA1C1D4CDF4110DFA3E9426389215FF617B9DFC450613257651DF708A9B4A657AFB6EF59680BA927A4BDF4F89A3DA7C49487B408326C1EF998A69C2DF53F5A9C61A85923421EE2563405D19E75AAA637FE9B50485C157D68C0B84DF4B4B5CB07B8996EACA38D75235D8E89554129AD7ABA077A3A292D54873092EDE3DA01955447CA61E9298F8540524A1E3545CDCDC8B2271EE49FE26027B93F6E1F80EF3DA78D5870FC798C447DB2CF7C2AB788DC2B44EB8F2B6DB207A4EDB9CD59783BBADE763C14FFFF9EEF0E0FB268CD28F872F59B6FDF5F8382D58A7479BC04FE2347ECA8EFC7873ECADE3E39FDFBDFBD7E3F7EF8F37258F639FC1DA074EDAA6A4D2CB914B2561A8D7E82248D2ECCCCBBC472FC55A3F5D6F043236DE3CABBB46BF7559704879B1DD4836E2A953E7237F574E92F7970FA4C8A3B3934B21F23CC7A855E705AE2119738ACA22BAD9751C308F3B9F8C5C75B07FEA8981D338CC3791FCC90179EE7A60232F05D05CE8EFE6DC700321964FF9C59C43E9E2457380EEACA8389C6CCAC0C4348FFA9B39975B8CECE45BCAB2693E9AF3A15E1FA339291E25D3F112754C7F37E7463DED433353BCF8A3E3254A467F17B97D38E63A05DFFD8E85FEC75944BE5B1B757AC50CD4B0B7CB3818747379D661FA37B19D1C5A62F1A1141507F22FCBA1FC62CEE1C2CB5EF02C4164C42498F3BB8A25FC9804737EBFE101995C7BA459D5DFCCB9DC2428C52D7EB25EE33F38C3C1A7597045E44842CA5748B5D1E263100A0A2CBFD98C1F5E10F24347F1C90261ABB338F91424D9CB29FE277822B3451E723089C5C89404AFB85F5E067E191C8819A3B8348BD6F1D2741B27D9977CF3C823884FB31B45AF9F8ADA8A83699360CBEFEF711015276722C726C99C271DFE869F3BF0691692B22FDB3392AA1FBD5721B4B4B8C496CB6639759A39D7F67D259A9FFCD52539A7DFF0DC392DB72438134427EC671C2E661C326EABF4EE2DCDD0E63AB9CBF1F793F586C410A7F9C21416FD25F3B29CB3DEF5379B31267E2CD77F003B21D1AAFE5B84979661BBCFC5559E4FB6B191D5182536169764D3A7115E3747FE1B3488098936B3FED20F50C95F4A64613DC2385E57B16319FB417DB718C9D11A8F8761E3EFC88CE85C9A39D7CF28787EE1D651F537732E5F012E5FADB99C7FCF12EF344F9280786A26E4C2FE6B901527C54CCBCBC966B3FA68B677BAEF35803B55265B0C928CC3AC3CFAAF1BC0296F8799EEF51691E9CD7DC00BC42458A0315A83ECE8EF36B6F7125BBFDA0919AD79D3CBA79A732E72C6511AE0954C61B1449125243DCA38F3DE34459C89EF4FE94A3843EBBC08D281173C78929E1577A6F94240229B76B87EC53F71FDE56D015158E0B0CEAD6E1305997D5937DE1BB10532BD29C8869ADB4C6477D5DE4B86A657C1C4C0FE2A73EFEEF64F1B324A5CD3D12916D3B43844A25CEDD7FD6269C8C5D242BABB22DABF716F97F130EAECF2CC3F425F2F5631F20EDF24EFFBEABEAF6ADF7EB1EEB02023AB5E2BE1B0BB5D77DFBD76B47BB5AF68743FF094B13039F194E71DA633ADA214179E67C014914BB2DA787D4EBC8DB0E15A7EB4D8F88BBD843F33283FD96C52A679C86DA9D5DF2C0E65C91D14A11BB45F2D5C3662914FFD6D684794895D082461862CDD08202E16AE0470F6B96EEA2DC364B6CFC8746F5D290F93B655645E8ED56C32823BB962AACDB1C2631AF046A7F96861D75FE2883F942A3F753AB4961E59EF2DF3483DB77AF7A873AF85F31BF45859C6817AEB46D8CAAD3EED68BB6A1E83326C5D151783365667DF8FB83DDA977B63A4E7E1A88C8FF121A99CC130ADECD60DBB7DA186E7C7A6D88C24303FFABB05B74ABBC0E8C4A4CC069DA7F166EB453DEE07481818E0519A73AEE6C6D5A9BD0BCFD2394D13DDF8DB9E90F0D6644EF98A208640B2CDC6C233095C5F4C58BFC4FC06039BD68DABD8E1C5540B33B2FA0239DE529FAD06C984848A1125645366639280777B1CEC9BF718384D982C65F054F82103C9560B32396721718707D433F41AF8888A84D703BB6A4E46C8D5B11816B7E2E90F9B6271BA91A2848755FDCDE2F4E605F9DF44BF24EAB38D5795DC9B4AED45351532C577A4BA6353CBCB049D064C966057E1D7C8040F012995DD7D17F19A8BDD9CC5EEF6E76CB0DAEF40C28C5F27CCCEF39062E2E323F16DAFDE67495A9616074B06BC966078C490A33C5F98C2425E2F890A659D796FFC950436C962E91A642F719EDD786F12CE20C16C206E14B5D610DB7A5E06A03661320C9AE7769EF3354EBE15EB59CE9F86FE6E717D1145C4451D70A96153ECFA2B7457ACFE6A33F924B9F86967F96D363DA5366AC08B83BD47033D4F8BE1C084D912C603D7176E3F7969E043FC98048B8B77719EA25BC4C730A13E5B5F0D3C09C3F84F4FB8F82EA65A5CCB099ED02A4AF34464CB255958CA385E4B44E5926C2E6B65F89717441B41A35CD26C2CC2054AB3E0D50B8B9040DD8D80928D41BFD7E41FA6ABE33E83D6D791D091CA8F368E7658E81B9490B01BC251B990B85F5B8A20A49E27ED0E4139131300AA722F61A4B9F1F8081EE5972539654F073FF6D1D73E1854723203A286C512D0D8DF74AD80DBAA2BFBABA979065C7AAD3FEEF0D6F2E7380CD65E8F4D1E090303044B730E03DC1F6694A45F18EEDCAC2D930E2DABCABC04ABD4BC85CBB363122C1C245EBD20F41E4324EC6FB029F38250F918703F0475DCAB57E49D7A7B5E6E5EDE78EB6235135AA5E7EB202350E0C78DF6FBBCF0D13E8DD20F23DD0F05D4D97F444BE3F692E3DC7689FBF6B1532F4F79C5949F7EBC8B9FAE2639727B76879018B0AEF8361F3B063F016D6ABD80DC26560BCCB6086B8525874259D3DFA7B25544865BE4A5FC8527FABB1DB7F631489E9FEC9948653B3AE99D6E2D864B5BF6C3D98D53E5B3DD162644C1C8D09A28390C635848B1C29CA5FAB61BE6A9BB099807287BBBEF98F1B387E8C813F6E15D6DB8FA41DD824B5E560F9908CF5728CA5719DA7447B08C830166E559A50317BFE16B792C9360314889223AE99431B734C8B335491C86281179F169169B6185EFA5C891FE6E73F728BCC8A3222B7FBD8B4931E7F84780FE247F71ECA8CFE34F8A081AAF13E14108EAB3C504CB8F4E434F087CD07C9D4FFF2FDF737771DEA665656211F43C96B086EB7F0CE2722533C77D94C59DE155C82CA730772F08F5882FA9E765DE55944C96D057869F3ACEDDEFEFE491F82389C756ED675B5E375E84C85B7200BF26C962425BF46F20BA778780DEF52B7702AF26C1C22245F1D353409EA710A4E392BAF0042505922DCF2160053029161E15C5B357204B2EC9E23C82F1FE630F25D8245B39018E4C8205CA9B277CC147FF84D4A9B637BE20724D835B2054DF6633B2DDA0E42926EFB1F86895A6798F19A08E93C1A8A66731F5D9F4F4AD74EB91B0044E9A096665D74E321EC33454551ABBD8ABBE8D3941773C09E2602F4C8180F4F94033893771BBE9D8CB5981E3D5652754CF6209D3E22546A368340FDCE26293ECF400CDB5E9EF36358778B55F67D3A76E5DBC5AA26062D08F94B90732EE459150B0A0F6BB0D37F1A4DAFE94FA2E7F7E26F35E9E13FD7DEC7D9D1F25CCBAABAD5DB7A67911A7E7E4511C3C6BD80469DACF8628F99898110D83618E7A48A93C87FA9BDD914079287489E712E2B9009D66E3211791830EDE47AEFA68C587B8AD0A7CCA8F567CCE50880463D27E9E15AAFB04F95DA38294B0E912EA57997D1820CF7E615A2B85849B72D43C35AB1E4D246731ACC5B00BC435554CE562B6DB6E93F588ABACE164D0627A164B58AEF5DF50007732AD771C5DCD98A6BF7F3ADFD9EAA4BDD64D9C1027F141A68B0BB28FBCB18FBCE1B66FE58FA99F04DB9EBD4AC1C5A43F29B30FD393FA3F40673EF24DD4B664FAD563527AB2DD8641F99019E1D4A15DB51C8659399CE20955BC8126A46C8ACDC607F11F7C152E4FD65FC75E9EBB88145F64388DA3A720D9F0AE487C9A8D534B9AFE1927EBCF5E2A0402A1532C360E919F27045199B7D9723D9E4DB2909284B28722A933099DF849340A5358ECDFFF195F783EEE5FE711B9ADCB1F5D08A93607FFFEB738CFCEA322D8DCEF99CF9FFF0BC91D780332F369363EBE3E4AD30B0C51B43E05D60E62B2DD3256DC7968BFCEC6B8D3EBFDD3D00B368EB61F0A5E3DF71F243C86B1F68E228013918B9800EC80D17EB6E4F58717E610B3EAFB2C7174193F0791231C15BC7AE248C26358241485E26FAF81E011CF25599D0F1779FE0371CB3626613698F88AD03714ADBB0341C2C0A0F9A539075A4F836F3C7579E1C9347AC0D04D7A92A6B11F14736E695F2713CE8772756072605093DA6E3963BCAC0553CFB37D289F9803A0623856500CA11623FA6AA4E824E0BD973C838EEA0E26F8FD0D99B27E78AAB90E08140E56E9973C0C3F1E3E79211FA442A3C20FC720A4CC51C7AD021FAAD98AE1B2B321B79E7000AD0BF3EE09419EA903144AE4B443A2C9B4AA3F7AF4B5770EA06A9A620AA09ADC7AA661D03025EFF903A892D30E40E39AA10980549EDA9AE2A8A276300A829CE70FA252CC5D1F0E5DE0B09EA4917BCC5E10A18427696681D597E6775A7FA8C2005FC56B14A66DBE3BFF056DBC4231E9D6F3AB6DEB8B2049B3332FF31EBD1495248707F5DA02CF89DFD20C6D8E08C1D1DD7F86A761501C42D404575E143CA134BB8FF1D4FBE3E1CFEFDEFF7C787012065E4AB6D8C2A7C383EF9B304A7FF58BDD532F8AE2ACA8FAC7C3972CDBFE7A7C9C1625A6479BC04FE2347ECA8EFC7873ECADE363CCEB97E3F7EF8FD17A73CC67AFD81A7179F7AF3597345D8734F8A845493DA727976A4E300AAA2B406CDBE195160F8B1A2EB7E8895A54F06DCE67FC002C4488141F0F03A2DCC24CD40F42AC6FBC8C3C53D95A8BC3030240B2F5D580F058C99E3E9DA78AE195FAEB2A5AA3EF1F0FFFABC8F6EBC1EA7F3DB4397F3A28EE73FF7AF0EEE07F5B0B501E6E94459343E2AC085E6BC9A4DC4A299944AF5EE2BF78C9FFD878DFFFA735A7DA43A0E4F514C65E66CDA3710F702110E52160D73E4DC65ECD43FB14F46C24CAA3C0AE264D46594DB244DCFB824BD75544E4442F9C95F6A1EE0F3B6218CA634B16C1AAD65AA5BF47C17FE6B8C5EEB10E49FB5C79DF2F51F49CBD7C3C7CFF57EBF2CB7D71BE7C8AE7CF7F7D67CDF4C22BEE470EC2FB2A1E8E376ED7626BD031DB9B04912BCCCD9BD2AED92372C36AC002EA9799156CFFD2816D75E6692EAC89FDF9B23A8B934F4192BD9CE27F82273217740F94B32478C5D3BDCBC047859389DB3A90B3D66D9C64F549A65BEEC4365F3F152AEA3DD694BCFE1E07517191B127B7720975FDF439483A4C94D8DCFD264BEC4D323B3998CCBDC4A887BA3AD46FB7896399BB9720ED8EBA9D0875BE5E8533D12EFACC17879BE29974BBC16778E3CCD3444EABB45CA05E277739FE7EB2DE0451CDF331B06FA6FA5A59770EF8E763B9BAEFCD0A576E8B48DC8C66DAD9472EAF1AA85DA8FD7C8392673CFABCD98FCE26EC6F51199170E0623E8571BCC63D3CDF6A977126EC2A37D5FA06B50B969F51F0FC923961F5D51DABF3EF59E29DE6491290905349E1C9166401D2AF867B2CBD4A73BE230BAF411610921978B72D89EB2D22D3AAF299A19E43C679B476C56A9592B84774B0B3EE16B1E0144729D9C72CCC0D2DA130CA9B740C81637190DF93E1195AE745B048BCD4C233FEF20DBE1E3C57E9F52BFE89EBEA468D0D37A7AAACB9DE786FC4D3C151DD6DC76463EB7426BBF7BF581305ED0D751A2D06B1756768EB25C5D5B72E6BA4366FAFC501398C826A37B74DE059AC10E6BE073CAAB1A801B8B715A3DA8A62AEDFC760540C1C9CB6ECFBF98FD4CFCB45E6BEB30FDFD9F7BDEB47E95DE77859B44B13EE5594E27C79064E291D1C03C6CF89B771CEF753EC256B1BAE66FB7F691E0A7B55FD4F869BF701FBBAA2C44ED88CEC9763EDE55078DCEC46E71AA44F0D67DABEE36202243CE5B058F50F6BDB1AEE43F9207C458F696077926F74D44E2EF6BA3ED4A077A2F606741A03BADA78CF7C4CAFE5F6DD0DB5EB8A91F418445EF2D66D8F6BAE0D46BB6FEC48B32D6BC42B4FF7EEF1BFE9131FED63B14D30B9DF35B1D75DDD67E8BCBD84B88FBB8AD0E6EC274085AA0E438E317E4FE3CDD68B76E5C2C0FC8FA70771DF5CD2346F188F98939020C723918986F2EC7926A1EF8A1EFF2576BF6FD07277334F5D7D19C60F160FA409797F63489BD46E3FEFC755F7E36A2F375D8141CF11B697305CF6D98FB567E835F091ECB9CBC5A31ABE9423BD6863B49B5FDD78A5A4B73D567941FE37471E5C5DDC8ECCC111A47EB00D8B356AF970EF8EC163BA4B9C826689181D4ED3257CA6BF616A7AA7B387172B5CF91D81A8836583F51945F10A6411AA0F7CB96AB1BA9CBCBBC30FC8DA8902F1E87729C94BA2A2C5CFBCB7A69F76BB90F335C85EE29C3C2BD989A13150451DEC0842E77618F0354EBE152BAA9E37B56A1539118AB4BE9B7B4E84D310478D95B5D0C5A55F2C4C2737A4CEAE103271FAFB30A242F4F7612386E5EFC38D8BC6DF87151784BF0FAB7336F6FE00E303FB3EF46EF4398C54B4BE16CE9FBB6DF5168AB94149AAB9FF31BF75422F580421AAE238ED08282637C4B80A2F6E20B9D4783D36E8C3ADBDB35B6CCB0E84B58A9C6CC45DE7D91236F43EC761B0F6766559BF98F1A7DD57D911CD4FDEF70B8D76DB32A5B2F612E1E4D50B0A7ACB45B21D6AF63B9A3A1BD073FDB64AC96BB524C300F6B668C2FD5EEAAE757D873793E6B6F147F5A74E479B5E9EBAEFE4BB797DABD77DBFEEC76B90092C9F8E776FFE626FBFF3E9CAEC615DBA88B5EBD07611916E91978A9723BAED04107ECDB94E0F2364D8CD2C4C8F8BCE3FAD115B8CBDA8D7BFBB7368426AD561AE52E472127572F136AB9B61E80ABBDD9AAFCFD2F580D378B7DEC130F8113B8A31C0AF5094AF32B4E90269E8D90D1DA4393DBA877442E24DE10A49FD309D84CCF8A543C48C38CA92380CEB674A5D4C414AD73687FCC2F0228F0AA66EE6037F04E84FF2D7FC563604F845B7EC339D5CF9D169E8195CB6E9E1CA788571F312BEED0F89667948E47E41B1FCBD8C814FAAAA0E51CE3AEE5E10FFD2D5BE47EC942FEAFCFCBA4E1ED322BEB71336375E84C81358FD564145CF75C0A47E90AB17A3DFA3F8E9292001D57BCBD4B2722259B1CBEF84D375F1408D1356AC679A03A91C303A59FF037775E28CE76A91EE82CF1744FCC887711F47C9534C22F7FB6895A6F9AE4CB21C1CAC7651E1AD472EDDEE880EABCA4CEF0F39FDFC80EB241D6607028721C2CC60B137F1EEEDD74DDEFEFBFBD8D29E5123CED9C9BF8B89EA7D6CCFC5B893DDEE5C28FEB2468E5C07DD9D7DDEE5CFCF648A68C06F644F837D18DE41B72827B7F7839DE692171DF0546013A46947F331C3B387FACD747EB66DD05E6556B6ADE848143FFFADCBF676790C7289E7406E7ABB1791ED7C7338802C884B654F166728446D4F7508CBBAE949738C0F4A380809DBF49D177D2E4F246A359170275D55557796633D691D55C556B3B2FED853BB6C9017676C69EC3D7434231C0F97F6E464BB0D83320E3C514057094536DD85348F3A5A4C82DB2DAD1D99AE4E3E3D7032F173B12FD8E3A28E6C52BBBC4B87BB32A7B6ECD63B184A611F80C0749F64490108EEF2C7D44F82ED0E01D5D9134543DEB1E6A61C0B9ED79F1696BB7F90C1555A3C256CB182329D5C77DCE600620C77E7741A474F41B2E9F7D8EA8D97A67FC6C9FAB397EA631918EDBA213F4F3030EF326FE3E619EC22DC311C35B72F3F272ABCFF33BEF07C6C71CE2392ABDF13C2B1FF2DCEB3F3A808F2F47BE6F7DDA66B18F616EDC4F7519A5E60E0A1F5293D95EDD23349DF1E73F57E1A7AC1AEB8FEC2EBF26E3B5D442D848F3B6E7F7861EE86DD9257E234F62EE3E7A0D344A8EB7ED0401B37453DF0B7D760EDCA1257CC70857E78C47C45E81B8AD63B3261EEFA428793F7392867BC9E73EC93348DFDA01096DB8BAA24E59A0B8FDB0764C2582BA016E30E854F47F5A7AB3CCC48C85E1F17881174C837F075B5EF7F505E7A202701A9EFADC56A63A1D7B2F26B391909DA8FAC0CFF24B0C6E042E48249E0916926790201B7A088C420F283AD17B275E6C80C214BEAD330E453CED016F70D2C0F5F3B93B25A2C8A25368C39CDEAEAFFE19882862162CA0D2F58F4AE2DF7EEE8E8BDD078222FC2634C2458B58F232C147534298FDAF79C04109433CE149800DEF32BD831DF178F8A3399D3CE6C4151BFAD320D26EAD23948B49F7700114D65960588E2ADF76951513E370F43A34ADB217C94355A2248CAA3A83D48660512EA7472EAA14529BE9B461D683D33D508D5A5C1C71DA83809275BE750309BD40CED6730B3B13BED3A47267A9F161CCCD24CB53EB26CDF915748AD74139A98165193D898FDCA797E66A6D95F930ADEAF09776883D6B669C7DCA5E56EB84D03A5755E1EFB3CB8B52B4341A8169715A2FDBA785BD4D6C5A430DA4F785A5354FF511CADEB20449FBF33302A1376D61AC9953410986C804B0B371D9EBE63A90214F9682106A991979582FABC7C93D45666113669B5F19E51BA0CFC94B23212D49F168F9BAA228BC04C338EDDC579E2A3EBA7CF4122470E4344B71D9BB0B3E3985C49C320C90EB5947093E1A988D37412ADAB2852839C650D04284E74D6A1874F5BBC91E26B6452E4D467603CB8F6B0DA09584D3E08F2B89A60C3718F125D91536F3E96BE85F7F8DF14F35C88ED6164065C54DBA4E5638AADD0320C0F8B2912236BE6CEC653236A3CC7E30E786ADB6F2E88BA8FF7785A2E9EEAD69B81A3C7B2C63D516E89A3C10E8D7F40A51631060218E3426A2EE088772E801BF9D8B723E68098AB73811E1340750FBC1D039E105D771AD805A94F1A202211BCFCD2694EF8A43C4F9690332D2F2119079542E11AD98642A5815A0702A8584593526181E784D465CCFF6682BF51E77FDD1037F9FCAF16A078C7A488365245B85F06D4A4E2834201548B079EBC6E0BC79FF8EC9314890029DDFC50F20F8C4E9D62E78853F81DB149112B068B5C96C904825D425241643B633475013F25A54F6E352F8210914832DEF330D12E06821E25362306F37DF1E0A26B6352DCD47E1D3498F6305A2C8CE660943294B4AF0A2F064C8CD43CA2B8C45D80155BA54560AB9D352E0355ADBC8C14F4E7C52389AACCD23054FCA9DCE36D2984F61B7127772A1849F4330B1C35B24D0BA4456D93D1128B48DA9DCD30A63ACB314A35961665962682D3F8A6C9064F33334E13B852EF4D8DA4BCA93DA8C993E90B19ADB0A42C748A0FCB870CA9C63246258295298CC78FDEF2733012A75EE8E7E182C27370420B0062D276024B748D166350685C891DC2D60AEC2C968C6DC504382279E682A1652DC041D955E8DAA199325CB125DAAD66A1AEEA3B6EACC90F02C42E56683A3C7259E706CB1D70999A1B3EA77196EA01D119794A5DC551F612BE2DED4458109B1106485DFC082DD6691183B388AF09B62CF668312B74EA9D8D4AEAD234DEBD20B4104F02516E08604CF2AE208CAED4920C128DB11D9892CD0581D34CC53A627146D3B01B943CC5C98658EA5BFC3D7A5E86E113C4667B8398BA78B327D66911564FC417F56595A6B91C6702A1A489ABC41F08776A15CE097FBCA4D3E1308937F1E2768679A959B40989CB37727C959661E3786C2D2F48CA1C8036729C8A4E589B4B781401724B0B8EB2079C19E0E61116E5967AF2691103E7AD04E8B73BF548D4AD2580271F29691C4DB01DBB47C51C375F57843D563691FEE1F71425A9140C3429FB40059360058A3A2B2959CAB74D1C041E8CF8266D06F760F36730F86A999549B5D3742604978E57959B204D891511A51F182E6CF9AC19E19276022A5CA54C4A245926C347B9AF7AB2FE479E96CFE02E2804022F3BFB8E8B90B8F8914AA89249995347441000B687D6AE406BF2F9B180AD0926C97BA42C60CE7CB2252A2FBE9039DCC369E8051BF9BC9923679A5448EB3C7B2E84904E9FABD441F0C2D761E4297459359342F9769BCCD2F0825CC6CF413435800A21A400AA5277114065D5160D2052A9A9F133D5F27D62F4182F0567039EAF087DC3653CCCFC05944A4C4680E6DBC2DF3CA9EB6152D4A84F9D9C17B8C679329C0325F5AA2F5EA38B2049B3332FF31EBD549C0B935C7728AB45E61F662B09544FB7DDF92F68E37D3C5C3FC6B8C5BD47A2A5EC317C1008055BC416DDCEBB8532DB2459612D85A694BA2388F5AA12A4D5A9D235FC992341A1102655561243A42DAE7E35002CAD4D9417D6D21897F55B12E75B65811585BED48A50878D75EECB744AA549D1D1921882B00CFC22056299AC036349A52B917A5F5C2C8F4A949646D168CAAADFA316CAA9136465D4E91AFEEC9BC942296CB2AC2C96CAA83BB72F8148BA754BA0EEDE2D9DA6DCD378B3F522C83E3629B2921A02E3EEA6A81E44A4EF74C6D53C43AF818FE89B1680043C89BC7C9E52573AF0A881583E40249500A0B59641621A6484E6B298990B796073A9B5026875A60BC8A2110C72A51724828864A240B486CA81E26B4BB50311EBD403E5D1C87681D22C78F5C2625A0488C3A5CB24E0C87485D2D17BC522E95469813491BE382EB42B542647A22898A3D494FE390E83B50775842645565643A029820E1229944227CA0AA2694CCA92181B2A4D59929149618388C165690D074BA52BB1883D2296547C969650A41A7066020080853014AAF21842BBA2150A95101A0A62AAE42B14E5AB0C6D0009DA24A8CC3A556F5E809B986259228DACA200A99900CC3D2999040C914604865637F009572AC4614F20910E7A02A579E9F5DD0955F1358D41F935A94E00C1E9542C5F2091162F506A4ABF552EB76F4D96DBB716CB6DDE55452C9123800A6569F4BD8CF5BA11D76E4C3254204D615E5CBBF92B2DB22551155B5319AC1F856352710D299048D79102A551E99A59A3CD6CB1C32CF12E7F4CFD24D8CA4A6792A5E532549A12856304A15081022A9723B2835975C6A9C45945A3035A75A26B537A7540A62CBDA2D1955E1D07EA4A6F36C385329B1459DB36045C11D4AEB0B81E6A362F292A6E15049E2980A70A4D65DA4FC2F636E46241E593EED61EB3D5B0A922E7AFA6AAAACAB5ADBBF0604E52F05055A7F6890D6AAFA01E4201C03E789159B5B9DD4505F59696910664C4C32880DF99AFEA2FDD6FEF53FD621BDD4A07608E6115C11C1B70DA80CF01FAAB84F304335089CA776CE12A11CF5D94BD84A71EA44213761A2B708C8B8B21ABDF8E033463F58841513A147F9211437A954AA38251DA7F0C1534F32086AD62BEA4B8F0D94FF871678AF521F083AAC501AA41E687FC817699557A4EDDA395D9D36845333384FA4AD35BD34CC5A14DE7715BBA39115737354036485B0B27FC655EF9B9BD7D95CB037A6575799221AACA3A1814F924AE033D00CD3A082800CD10CA8586BC160AD1559E08A3029A73A53298E76B720CD1F612D7B172A340E30BD65F2536CAD86135E8A7349A1C3BA012C693470D0C09E9204A003D95A86D34A9534E6F1590E82BF21D4039B1EBBDC00955701F1B2BA0215D72F5455F2F654750910FBBD005D561E0A9E644257CC02933CD28C354395D12CD444D6C9024332529022B2D5F4582EB9ED49B0FD095716645A5954E8965C58DDC0B355C351C8751A5D24E29C807B15313A844EA82A95ECCEA730D79CE2575476558E9BD4A5DAA0B7220B5519C3E36321D984DEAFE5A6840EFCC3A53B58A3E0C463054651BF4BC55EAC3C1F032F0CCB05719E5296BB04257500FA120C013B8C8AC72EFEDA702D3CAEF46B51957655DDD25C403290074C9AEB5A076B1B657456B8B944A80C886A8BEE8B05DE455B861F7AA32E574ADACB3F4C16D5E74615E2775EB9EA6DA2653139870B0D686464AA5777ACFAAEBDB5CF314BDDB561FBBFAFA1D47E3D7D3978602E6F16FA8E2F2D7C15D5595BA48515611BA24D1B16A062DAB7BD27A01D5045F6696D455FF8AB3C30A03D7589ABAABEEA5F45743A95DBD0AA057756D5B6D3ED5361ACA6C9ECB1D060BA06133BB21E44C45425B186B4AF334A91B2CCC5D7D661B131DDF231D7057624A552ADEBD047467FA4AA6A32E2ABD0757B0D05F6B73A10EFD686DF7B4E3D255033E4A28D78AFE0D43B70A01AE2CD21A515D4074A212331BD4E1F1BD01EDCF14AA533CF406E8CBF459384758925EFE2CB5ABBDCBE9421DE28D5513B5E89EE2822BC9DCAFE5EB08DE979D5A5DD2E7B3202D99BDB5E50A3B927BBBA52E74F7701DA8C2E0F0DDEE8528A7E7CA93AB477BE86EF396D1A25503BEA2032844FFDA8EA3AE035C3A2F32AB6E92F7ABB67E7267FA44CCA254003C710239366B1E426104872ED31792B3092A1F67C9FD78864B9BD81F07B1F872070400806C4835B0E5B120E0927AAB40F73805A00EABF72C1C750A59F8828283361C8103B5582964B755A1B798568F062C542D9278F780364C22E3335591C48D286A22A4199A53260E84604FAB54E74AA98336E89502457B1F5C294C780A412955AA73A510398C7452128EAB9231865D2ED636A00955346E46F46EEEE25C9090229B2CFA87BC7A245037C9DE04866ED23E1C973146AA0FF867E90F7315AF5198165F3F1CDFE638F70695BFCA0542C3E203E619A1C2EDB1655AD3ACA2A7B88E88CD495493D4C94DBCB2CCC306D83B49B2E0C9F3339CEC233C83200BF13FBC302F0CEF235AAFA2EB3CDBE619AE32DA3C86CC0A83C4D55695FFE15890F9C3751127277551052C6640C690EBE8531E84EB46EE0B2F4C394CCA589080DDBF21FCBD6CCB0CFF1F3DBF359CBEC49121A34A7D4D9CF17B840728CC2CBD8EEEBC57D44536DCBB2ED1B3E7BFE1EFAFC19A745819137D43B06AFF701678CF89B7492B1E6D7EFC136378BDF9FE6FFF1F04C4A40AB40C0400, N'6.1.3-40302')
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201711091111025_Change Filterattendance', N'FTL_HRMS.Migrations.Configuration', 0x1F8B0800000000000400ED7DD96E24B992E5FB00F30F829E661AD55266D5BD8DDB85CC6E28B554C66D292548AACA79135C1E94E4373DDCA37D51A5D0982F9B87F9A4F985217DE562DCDCE95B64A0802C85D368341A0F8D9BD1F8FFFECFFFFDF0EFDF37E1C12B4AD2208E3E1EBE3F7A777880223F5E07D1F3C7C33C7BFAE7BF1DFEFBBFFDF7FFF6E17CBDF97EF0474DF70BA1C339A3F4E3E14B966D7F3D3E4EFD17B4F1D2A34DE027711A3F65477EBC39F6D6F1F1CFEFDEFDEBF1FBF7C708B338C4BC0E0E3EDCE651166C50F103FF3C8D231F6DB3DC0BAFE2350AD3EA3B4EB92BB81E7CF13628DD7A3EFA7878717FF9F0F9F6EAEEE8ECE4F2F0E0240C3C2CC31D0A9F0E0FBC288A332FC312FEFA7B8AEEB2248E9EEFB6F88317DEBF6D11A67BF2C2145592FFDA929B56E2DDCFA412C76DC69A959FA759BCB164F8FE974A2BC77CF64EBA3D6CB486F5768EF59BBD915A17BAFB78F8298EF2F4245ADFA0C80BB3B7C303BECC5F4FC384D0531A2E1BE3E8C67B4BE2303CE258FC745013FED40003E387FCF7D3C1691E6679823E4628CF122FFCE9E0267F0C03FF3FD0DB7DFC0D451FA33C0C6981B1C8388DF9803FDD24F11625D9DB2D7AAAAA71171E1E1CB3F98EF98C4D362A4F59B35594FDF2F3E1C1175CB8F718A2060F9416EEB23841BFA108255E86D6375E96A10437E76A8D0A8D0AA573659D6FB661FC86D06AAD2B53CDE70C975E73207FDFE3DE62CD8464AF99E0DE80BBF4E1C195F7FD1245CFD9CBC743FCE7E1C145F01DADEB2F15D7DFA3005B009C294B72FB524F3631EEDD8DF0316E787B26B718EFC9B7747CE94F13441AFED35BBFF62BD93869C5DFB76B5022935CB0005CCE2FDE6BF05CA01FAC440DE9C3835B141664E94BB02D6D2E6F131EF82C1749BCB98D43D10071940F77719EF844D6D888FCDE4B9E51665E15AB4A98886F2C786791CB16B4129CCF22179FA5D456822387AAF2E1B81D739423512B9EF910F43939AAB3ED871DC8DC6035296CE5FBBF0E612AC9BF8A427FFEEBBB214ABDF0B217DCECD3147E154F583886C71A25E3977B93A014C3F064BDC67FA886E4A1CA47093612534A70153F06A1AAC5FF324CB9E71B2F081D5457D38F576771F22948B29753FC4FF014F8D4AC613C259F25C12B2EEA32F0F102D445F91A547969BA8D93EC4BBE7974D2ABF413FAEBA742CBBD678425AFBFC74154C8DA935B39F65F3F7D0E92FECB169406CF5131A4F565558FF784475F5E9F122FF25FFA72F90DAFBDD33B2FF492B79EAB1BD9226377D6145CCE557AF7966668739DDCE5F8FBC97A1344358F4F319E727A913D6EF1D4294FFB72C13F1F0BBCBA61872BBA457EE085ED2CB7A77C5E35F87551FBF906E1897AE4BF391BC074ABF672893276B99FC2385EE3EE996F1D6C1868A602688DC7C7F07380972EAD1D18ACB8CF28787EC9062FE6EB38C59C7FC7ABB2D33C49023FC766F4C4CFF0909F05C8C53E8FE9B2BA1C0BC0E574B3BEAD69DAF53397242C98F974DBC5BEC1564B53847C8F45422297B6E7AE0A35DCAB4566080179A974B9B03491F5FECF3AF71572D6A9E0D68F98DA14DFC82892D4D5B0DDA12A376D55CA6429016DD20472753254D6FAFC8E7F05E5441D92B54986352A268B2A05686C75BADA78CFC4B800129649A0745C9220199F6E2B153DE156B7344B09B4344D206F6986AAD7DE5D6D18AD76EECA4CFB7D3BA0AC693692F4FB28031DAE5C6F1159B29653D79E4B98F368ED8AD52ABDC4594FBD90CC4A7033F69DB317DC708F0ED6041618268C94D592D192C599272C3AF51CCE101E970A0B84B00188326CB7EC98ACD2EB57FC138BEF4E3B0DC77E1AAAD9DC786F1B5CB5AE35ECB2EC333697CCB4C7CA665239F78613284B73E0E166B9328D753E435B2FC908A6FB6E58916980A60E239FA2EFEC0657C7DDA33EEB417A39245F12CAA984F9A282D47E615863582B3A4D0A8BDD522845A6C806386AA74B921FB3CBA994A2BB3C5EA7F56939E4D419F7230E649C7E8411A7D843ED3BECEC8DFF18C6BF31762ADB2F21028C918CB2BBE5AFF6E3D5C20BE4A0F41C954A7C9ED4F59666E7A18BDF45D28C704E87AE86BB6AE49210A9943DCCB85521A1E3E055E4DE8F609085DDDD116C3FE48C39E49496D564DC81281506052477BEF6E839F888865C3B4E0D62CCCB224C2C3A44A96D05A72EBFEDE99B9DCF6F9D6F6FCF81B256518AC9F24CB7C733985B68FC9C789BF10BFE147BC9DA41B13A5F9614E36A02FF66DC81DDDCFE899DB0E97CA3A9DB5D0CEBA378E1D4537E5ADFEBBA4279DADEE9CA0221D85B30A0AC690CD7A0473EB463841D589A8C7BA800654D3CD835C54F7607E12B7A4C03277EF99A61FD258E54A5B8F1D864B659F643ECB843ACBD739630C82A1CB83A99CDDA53CBCA649699F6E612B2571BCA13E253101577164C367BE60D5D0BAF3D1EB232AFBE4E70651DF3AC404B67DD4317286B072785A507E43DFE377D2277CD3AB84FD699F7907169858089819BBB62F7B1233E55B3779B67740C87C0000E8E8600938871042474B67BBA6DC398484C534B656E8974525394B672D7303091BAA595CA5C93E8246EE87A0D72A7F166EB459D42055559F7C60A28EB07F3F91EE70EFB6EAD8E47BA36791212E46250BFA2D12E883E076956BA7C7F8947D8C36F8B73B3E05F7D19295E009E8F261966DD41EC0E2E171DA7A62283BDC5074DA0BBE9A9C3080BF7B14B66134C5445F88193550599E214DCCDA455EF4E2694276453D684A336A9109FC57E52DBA1565C26659D185A931AB1197ABAA1BD063E3AC13D3C5A7B96A738F5EC97E7B1378A0AA3E8267499C6CF2A45496FAFE617E47F7374DDB0EB653A731407A91F6CC362DFF7C4EF72FF4C60B007B102C4BDE32609EA26DC661144D65D245663073E5019F04023903EC87253238E692671E831CEE9343E29502C38CF919399D4C4911BB744FD3DADCFDE854652D618BB49D6BE5297C87BC5037B1E654D9CA44E8E53029F3D0480B25C0D42375EB02EC3DB9DE5899345E1B99744451B9E796FCD08D23166DED7207B89F30CCFB13B73ECE82E210323EC3DA1A596C6695164B11D4EC4A6B4AC1AC4C0A492623E8BEA02997B8D455025EC57732297BD1502CA9A9B53D5D738F956EC9FF60DD659ABC48954044CDD0268929C43F9AC563DB24239D9320F1EF3AEFD45CE6DDF6F80B25C8DDEEEA2D37EF2D2C077C2E9739CA7E816F57E0DA40ABA791286F19F1E151CBA23BBCBE009ADA2344F1CF0BA88E3B52BB970C7C4BFBC20DA745059BFC98DD85995B31B05B974BC57E5E935CC5FA0340B5EBDB078B4A28BC56218EC8D146C12D0FA5AE5AA3D90F740D12437B819E0D85ECBDE1073337A5F042122CD5BA8C76A994DE5DCA31E28CBDDC2BA0DFA3F7D0CAE4E6C1C1FEB1A5C37A7C0A9B86A2EA712C62105A9D3DD5ABA1C9DC846C23A1A2503E2EDD3EF6093E7B1B71A035A0D2783D52A727264799D674B39FAFC1C87C1DAEBE4B55A65DDA37A2834CE6DEAD46E415ACE9CDA8C7BB00065B93281859A5D9C849FBC7A4141DF613BB0DB2CA045083809009285390044633B536934A813922204A56CD25562B644BDA62A942CF6BD727F522C296B9A7B07FDF7DF57E9F93AC8BCC7D63DDDF18CA1804DB7836A3AEB1E744059B31B091CC67C9BDB8917D5D53AEAC6CBD3094CC4B4B1F3068B843898E39C601FEF10B27F4DAFC7B446E5FF0012C07386DE5E0E06939BBA0CF9F446A0500BAB9EE2A8A43508EEC794248FECA720530BEF32A6DF65EC753A1E26F9F643255096B3A1122BF864532E6A7B4DBC1C0E9544A65BE4A5CA683F03ED8393B25BFF9F3E9A753670741A20BB0C75DD86D6DD1CB9300CE0118B4E10ED27933A84CD2705286CBD980CCBE8DAB6D71BB51DBD8038167B8B2FB14CAE9EC99ECDA8D1C3DC76EFD814D2A47D1CA201BB1248683D372DA64706E2968472518BCEAD13B32072D9E53B6C87487AFE7E77445E967B8F7B4EF9CBB72E4E6C8272C1AA26D575BDFE8B587EAC35AF8E90555B2BDE6E18568ECF36D0750449E9EABB08C6994C2BEBFA16C2158AF255863636B6B4CE3389D5249DDDD66AEA0D844BAB996042A21EA1D45E2720BF0CF334491C65645C4489A6F881168FE5FDC989CA0EC38B3C2A04187E35F94780FE247FEDCECE2EB102D7C9BA8D36D46DF05DF9D169E83989FDD5CD00E21EF012BEF5733D1398EC27944059B3F23D9B76BB6CBCF3A4EE4E6D9DE7B7426F00A7B6722A612EA4201D60CF4B2C4DBE01A6A335A88ACBADB18A7B3941BC7B41C8CA758CB3661497BD391BD09CB95F67CFF1B6DDC923B925D3DFE1A6E473E34528CCFAF2BA2C6CAA0B2E3728F2FACBF37B143F3D057EE085FDA56A79B991AD38AA75C3EA3A7BC136C40D2FEE7A9D03B95C703A59FF035B5D7251D2D9AEB513465F10095131CE2E97388AA8A60110996CF0046907DAF5018A54EFF8186530A999F3781328798A930D9973ACD234B75AE77C4E8EF8ECFB2901D4BF46F062EDD2E0B75E56C8D2B5C5CBFCFB2607CAAA55DBCB2E4FFB50136F19B99E3ED2EB3902DCC0E1424E25D85405A9F56021D84EA30A88D95415E1A90D2A2464E9374260E1E28EE7ABC45E70D9F7E662C04E3AE768DB0D109C39B23B5981DEC7DDD87434675C6F80AD998C48ECFB52CA21027F0BA529C27EEB68F555193EE4B750A434E0B79A525F1787C1BE6F6909AD2C3195736F8481B24AFD38BA343C8DDFF05DFEFC4CB63E9C943D8B1B290E1DBAC73E6319EBC8D5D5E46154D766CA18816320942ED859906880B31FBA1CF9A98F9C4A29B8CB931EC21DCFFE37419A5A8E0F6CCEBD0F0FD49FB18ADAF2A0771F7EFE9B230F8AD251E812CF1E8637B05E44BC50ACBBBDC8865C7976C0E60C85A835B6FDCD106936B85733907F28E9A8EE2C268BFD18A0E9F7A67485C5521879F7BD0AFC244EE3A7ECE824DD7E41D9519DF1A864799160767FC6C9B7239AE34F07C6F9DA7EFFB369BFFFE5FDE3D32F7FFBEBBF78EB5FFEE52FE897BF8E6F037AF449DB231337BE69C663548A1B037E5D9A6ADE878A8C7A605A4815DF9816499C2098B0728FE29AEBFC915C8F16C77AD2FAA91F5BE03B1B906C27D2CCD34443956BFEB079B17FD31EAE76716EE179EC17C84059B3F2D47372F23D52702FD9B276D2409763AF8EFB04D6E4BBA722BAA6865418FF74F44EE36C0A8519D5C05CF62117C14261F295B086545F0F976B62372F04EC5F06300DBABD8FC36FCA6B1E71F8CD3B52FE98FA49B0ED70F44067DDF71BA02CCD8BA26EB67A867C97FB64BBC5ED54E8822C0F6CF0C1655DE0DEE3784BAFD342AB2EDE865DA5E496DDABFD469BED22D4D521871784E394721A474F41B2E970314670264DD33FE364FDD94B5DC4D2D79CC7213F4F709FBCCBBCCD76F0D26E5EE2087DC9378FED45BF31CA72D634F77FC6179E8F6DF6794472F5E67719FBDFE23C3B8F8A97B27ECF7CDBC3C2868113714E7C1FA5E90506335A9FD2EBE46EB682D89B91773C4F432FD8C05B9EDC78F15093B6EB0F9842587648C8ECA3213C079199A835A95CD492422B6A45662B2A61662669452917B420D0CA595239DB502E5AC8FD8E72C176FE5BCA733F20D5EE0B3B3AB124AD45CA18A7A43FBC30775D5427F0177DDE3DF80BB6F3077FD74392710E2BA0011DAB157F7B0DD663CC91AA82B036A682EA5784705BAFBB6CAE5559F73B0340599FC84ED34BFF239737CB95BF6A1A51CA04CE23AAA67CA849DA09049B22CC1CB8649B29C3499AC67E5048C26DC2D762B0F5C2B3F403F69D4741A8B207D1AF4DE28E84111890E90D16E1E3E13F09EA92B3AD2B43B1ADF5C3327D7FC803F6BAF287392863E2100F99D4F7D6626361DDACD92F18E388441D0A3CB2A44D71AF09A24CEC1041E4075B2F548BCE6533EC4944ACA6003EE50C6D717B63F9D46D615272DB47C4F29B623875E9B4F3E198829521DAB853292D3C6447538ED02739C93260FFEEE8E8BD50423F3CC1C28C892B58DB261250A7B593008CBE3C60883179160866CCB50773A4290A99046C7A7946C09B5EF3CB80DCD64BC0B3763918243960C0D5C4767893153111DC34E28C82368DD69705B6DF9238DFDA230ECAA6865D91A31BF6C0C22606A04AA65151A86A892542917372314587CCE16510284ABC652685222CD30450845BC24410CA236EEA2198078BC100C9EB618841982F03409E06E6D32F78F5D519771497B49B89105CD6C9D6C3546DACCDA795E5EC08DB79D9CB3998CA455BC976B94521C364C94AD57F9835315580D234CED12AAA6A31F22A5A6C273B6B38A12194DE1B36818F8929EC8DD17918439D3C23236EB9E6B09199868676A7180C66E268171A8A7C6206E1E9CDA0AA12636E5D03ED63660299E84DD300729DFB5C980729640452108C3595151A45D616966E064094CA3F060CA5ED6252387D6B705A9358FF51F80E69ED164DEDDC2832CC15482CDD9C6687465535C6B48B501BD96092E49B0E97DFB15401625E42900247A40531D99059A11260BE2CEB28AFC0186894B7CD22ECE36AE33DA3548F418E0EC25F4962833D9EE9A27027117E04CC49DA621178E384BE7EFA1C2406E3314DED7C3C6698031864449D1D0E95D518733C86DAC8A87C2ADF64B82C9E783889D6D51314A6A7D0EA6C1052B91C3680D514B628EB69569711E06BD68226824C7D7ACDD7C41ABC23C17637003B03A82E7202C057C2706B5C9D6D60C4CE619BDC4CA60950B8DCEDF2D273FC1EFF9B629E06F612A607C1C7905A614F52C8B28CA5BA1263A054DD56CB30956C1DC86D1ACD9516698E41314A15B3A0CB2EDA4A8C8F52B1BD4C646873CD05A9F7B11D4E6BFA4151DA14B25C8CF255181FA17C4B994850E799814B9AF958AFC8A3764BEB825455618B1AF70D2A3202660DDA6E11E33F500FFED11D0B4449DFE01910C5B2677C96E202625EA569602D6953D359C31C5C44805AB12F3159C04DF230D3800087DF765A30BCC10A4D036EB035CD261CB3007690FAA4B12312DDD62F7D59854F4A6F14630E20D4054A2BA81B170D611EAEE6FCE06F5BC931FA816D9B9BC804739853CFD0CFC7E5794641FF42E7E3FA8A4C83E945CEC76B012E91F78A8AC88AC2439E3244E9B3AABC1A845C5DDC1C14452F0AD3E6F51901DAE6EDBA7084DF78C1BA0A839F2766170B4C984C807A400800FF407D17D413E4759CB44FC8DBDF442C31F7E4FD447C18C27C2850E455F50AE8450BFB6EA12A7C91A38141854684BE41DB2E623CB808424442187ACFC6F1CDE459205453D436305614B228F4EAEB310268F52D6622C4D4DE6F742DAC403A303C970FCC8921B948C3D9CE7DF4600468212CB664365084982F0A898A0A8C004445DB2C0D87C59FCAFD6688781024B6DC0128B672CE198B4215C605A3D03E26C53799A685A3F1A619482D0564874D02B880E599C70937C0946DB41C1359D7C0D0480AE483A172D1A6525A89B181B90BE6D2F0A28422CF60209DC3FD080381C646DD726F465CC69EC14E264305822BF6AC76275986CB1A8721D1C7001CD406CB187789E4A6464DA47586B759182FB920636168D9C6EAD40BFD3C340C3525CB208314456B8B2EB098C51936552D46C2A7AABD1663EEE84A14B6CA143A450F1D169D4511D01A03B0A9F343252DFD0488A4DBC7686981E9E78244F3ED18753E037C765967680A5DB22D9D72DBC6AC2D9768599B353F671B2D11C677F131D1CD972D31CB0ACB3F5BAC4BAA361DE425ED6C6AC5A9AC73EB00162E7BC61C46ED06BBE3A9675DC1E9FAC3CEF8E85DC551F612BE9D64192EC9330AB429CF02E15EA0B681BCA2A8454D69F4F51801CAFA765BC44446AC86E1F6982EE308E89DC32E9AA954932072B9FB6B555D4AB37EF78290814F93228F028C1479073482852DD198AA2A321E76556DB724734AD7C3624A6C947B142CEFCE34D8AA72D3207D67A6BF3728798A930D197B6EF1F7E8596FB4E559209C0BD436305714B5288BADAFC70830D6B7DB22ECB5580DEACB2A4D731BE4F2594740B0502464A5F91A2D00D1B27A4D826C59BB1A59672EEF74484FE24D6C75F622CD01E29A23B682B5B4A065D9655D35C600AFAECD966195F95A988620D3651C1CB94B0F3E665AA129A0BC0381C7843A99851D53671B1CD4CB0E3866569D2900BDF06063B794F0DAC904440C01F7560E25156641F68B9A3DA86A30023855EDB38839035D01C3230C799681903987030BBD3C23A36DB987142BC21E370FA9D3C3EF294A5229D44452F0353B8ACA0801CA020078D55484002A8403B24DF7C3DCF05A7713A42969D482B9B44B88B4608763C8ACFA1C5080421BC6EAEED6E1E4C298801C36A8E63D4DAE6B93D209FD6426BDDCD53E59FF234F8B28ADA6E16034F920ACF1596CD0A62B6E51D310C3CA8C304618B6A2892453C78911AA620FE1D1C0BB23B09D036017398F166A613899D6E41B1CB773985B1B0A350516973BCB3ED992962ABE9069EBC369E8051BF94C1B2687E0C7515A4EB825E56826DD0595BB59372FC465FC1C44E6BA29C987D74D558E463705D570BA21A299ABA6A01E5E336531A3AED4BE22F40DF7D907CD73592C19A4898AC2C66A734C17F42A162CF908961C6E079382477D01EBBC402BCE93E11C28A944F87C7B7577F648BEA2EFB89D7D3C1AC51B2F8AE2AC60F02B46F7699890E64F3F1E6609E06D41B8DEA1ACAE12FFBE6A49A07A8155C022CBB19D1408ACDA240D8F52D1D8B800E25470D670600E5004264CAA9653FDA609C8A84D34E6F35B12E75B25B38A42A7E975EECB6A48A519B657198148DA6665B28ED777DC630254F8A18B9CA8440D9FD5C67B065BBF4ED0E4A71FA307B8B0C946586C5F1A9220B225D0F03B8D375B2F827A5A93628C248554109196EF6BE023FA2E01C09527D1F1049EF010B90244D67C25F895111AF60A207CBDB48B00B49A5220775F813D4464283D14665C2A3E44AC29E702A559F0EA85C51801B0E6D275DCE838BD222F3A55CF29438912C9228986E7E7380CD61ED4FE4D8A86438B0F4857743C4E1346C5182FE36384712ACC552067658AE532C484C8A3F86C9097B9070DB26128EC38CA3BAF8C50C3FF0A45F92A431B80639BA4E3215EDE12998934665C999B0B32B60C91CE56094EAFA2A51248CC79D6BEBB2AA6358D8E2BEB1602635BF4E7D170BD55CE286F2D6694FC319CC88C2330E0074E9698D338DD948B5B1EAB18B64B68DD3C4CD88F14E7620289114FCD98D6612CBBCB1F533F09B6328E4CB2865779742D3211B63A2C1AA5DAA553B74AB51B66C1B6DAE052B3AD3692346C9BED0B815793C271A096BEE2E4A5DE4F39A0A8B8290BB8E5C2EC8F7094CDAE4B233BBD601556F5723EF5460BC5A75DB4F21B266C356D54C09DCFAA54A13ACA85AB2239C4EDAC1AC929AD01BF0E2AA21DFDF45A5250CB2B26CF04E94AB1A760CA75688DD52B422385C988553593E481D525DB3931E43996B28ABD182B8D81394CAA086554EB0EDC28B2E13EAE16B9934F032DAACE4A95F5941C963AD2A2E46C74F08E2BC8ACECB93CB55137E3ABEAA4EFF24C0185E9763F7BE9CD06785D3067053753A58D0CB27658A245558FA514A5D9884755CFD1184A7154A2CAB18E4C1065E63F2DAD9909A6ECF5352AAA9A62184915B358E9E51D78CA095DDAE93C7F85EEE49829BB8B66EA039A071586002A454504625027B293211DAF9190C21E3829A0C2101A342F4DDF1F2C0C37856AC09DD80EEA690ECFD47801C8149511A941C5480FEEB4DC86C54C791AA854084F22179FA38414019F3E2AB98CD469D8134D45A761080D604ED3F7EF340C374035CA83597BF570AE0B068B384D0E7915D51921CD69FC2A6CB80F0B32BE701BFD75D0DC503A9B485BFA79A22687790DF5F3C53EFA1B75DEC8FA54A821272155D40CCE012A4CE9DB61C4755445916BF9F26D7339B171B5DA3C6ED545F11D766B9D2BF73E365657436A5CA93A875B55355C875594E840A4EC862A7293DD0EA3EEA8776A32E63ED6961D8370CD2AD82097556565B15A9C6A5416A065B0953320031BF4C34CB38A4021BA2AC3C1429C6A158E10329C4E052FB90799E31CA05CE3CC0A1598F200D5ADF51FEC5816A477337F42274DA0B4B70A72ABAA2BED6D6FC58EBC10165C30D5BB25FA5CFA45AD3CB36ABDACF515B52D6B2AFD42DEA9369AD605F335D48322AAEF80DA5704F5A54A3570E0EDDE22A2A79311E455D9F49A50E456A95DEF95655DDAB0B0A77C8E0D368F14D4F24ACA33419A5438419B721D4F63A6BAB2D4926BFD8CA499D67A28150391C9EB0150436A613CCF156A81D88DA615CA715DA916C9EBEEB28A88AFBBF7538CF8963BCFAFF2CC77A31B93790C4CA8A98DC96C85BF21A0D3D0C8B312F87578AD868C01247D78DE858E26C2917E3FDAF8BD7A79DDF43BD15DB536EA2E34FB803AA42AF90BEB6C15C037D6697500B764141C46A8B6014E744F7F8B1530C0859122464701F832B54427FA57AC850A29DFB1E6B4A3B80565C6775C6D950DAAD794F892B2B236CC5BCA8E34C43C9EACC1647FCD180DEC368FFCAAEA6634D81BDD84B32A6554A8C99E8C35D7ABF2B159938ACB9E9B1D44CBB2F765CD7B833B9D9BED02757CE2D4441966FB3FEE943FE1CE8FE2494C40E7A60F68329536784293AAA5F68EAA29EF61CD85F6614623ED194F788C9F7374A8C951E745AA1705E5AAD43F4008D551F904A1A840C5BD6663EEA32BCFCC8676780F4F576533DBD95FB513DA4CC53B6A808E4D5F5D632A6CF0EE1A5D4BDDAD7653DEC3C2D4E0352F23FDA9DF00D3D455FA0A98437D4A9FFD82CB80C31B74D0AFF40D2948AD660F4EB135D53E39455710089CA0D2A2F695A98140A97BCCC84477E68E35C64F2039D4E4E84E359A77744C346AEA5063F8F68E436D8EEC4C033EE3026850FFDC0B532DE5832F546514A148F4FC86EDB98A274534FA319E761B3C41D25357A34EB2811731A05B2E9A773398BA285ECEA03553C680515D75913F9041DF9911E3CAF487512C3E8D01E107205334B1482DD38B3C368F9623A021575AD13DEE0068C8EA3D08A66EA62F42D0977B34917EACF80FDBEBE4CF0B9828B18BFA8653DC542AD3DB77ABC0F8EA4AEA2D7D3F258E6AF325F1D9010D9A4472672AA689E54ED5A71A6B144AD2C46B978C0175659C6BA90E64A5D71214D35D59332EAA7B2F2D7191DB255AAA2BE35C4BD560A3575249685EAF82DE8D8A4A5623CD25B8E0ED806654E1DD997A4802BC531590C44153717173C987046D27F99B70E24DDA87E33BCC6BE3551F3E1C63121F6DB3DC0BAFE2350AD33AE1CADB6E83E8396D73565F0EEEB69E8F053FFDE7BBC383EF9B304A3F1EBE64D9F6D7E3E3B4609D1E6D023F89D3F8293BF2E3CDB1B78E8F7F7EF7EE5F8FDFBF3FDE943C8E7D066B1F38699B924A973D2E95C4545EA38B2049B3332FF31EBD146BFD74BD11C8D8E0E9ACEE1AFDD665C1F1D1C57623D988A74E9D8FFC5DF942DE5F3E90228FCE4E2E8530EA1CA3569D17B88664CC292A8BE866D771C03CEE7C3272D591EBA978F9A771986F2279FC7C79EE7A602361EF692EF477736EB88110CBA7FC62CEA174F1A239401730541C4E3665945D9A47FDCD9CCB2D4676F22D65D9341FCDF9504F69D19C142F6CE978893AA6BF9B73A3DEA9A199299EAFD1F11225A3BF8BDC3E1C739D82EF7EC742FFE32C22DFAD8D3ABD62066AD8DB651C0CBAB93CEB30FD9BD84E0E2DB1F8EA878A03F997E5507E31E770E1652F789620326212CCF95DC5127E4C8239BFDFF0804CEEF0D1ACEA6FE65C6E1294E2163F59AFF11F9CE1E0D32CB822722421E52BA4DA68F13108050596DF6CC60F2F08F9A1A3F86481B0D5599C7C0A92ECE514FF133C91D9220F3998C462644A8257DC2F2F03BF8C74C38C515C9A45EB7869BA8D93EC4BBE79E411C4A7D98DA2D74F456DC5C1B449B0E5F7F738888A933391639364CE938EE5C2CF1DF8340B49D967DA1949D52FB8AB105A5A5C62CB65B39C3ACD9C6BFB5810CD4FFE84909CD36F78EE9C965B129C09A213F6330E17330E19B7557AF7966668739DDCE5F8FBC97A430262D37C610A8BFE927959CE59EFFA9BCD18133F96EB3F809D906855FF2DC24BCBB0DDE7E22ACF27DBD8C86A8C121B8B4BB2E9D308AF9B23FF0D1AC484449B597FE907A8E42F25B2B01E611CAFAB40A88CFDA0BE5B8CE4688DC7C3B0F1776446742ECD9CEB67143CBF70EBA8FA9B3997AF0097AFD65CCEBF6789779A2749403C351372FBFC35C88A9362A6E5E564B3597D34DB3BDDF71AC09D2A932D0649C661561EFDD70DE094B7C34CF77A8BC8F4E63EE00562122CD018AD4176F4771BDB7B89AD5FED848CD6BCE9E553CD391739E3280DF04AA6B058A2C812921E659C796F9A22CEC4C79474259CA1755E449CC00B1E3C49CF8A0BC07C2120914D3B5CBFE29FB8FEF2B680282C7058E756B78982CCBEAC1BEF8DD80299DE146443CD6D26B2BB6AEF2543D3AB6062607F95B97777FBA78D7F24AEE9E8148B695A1C2251AEF6EB7EB134E4626921DD5D11BADEB8B7CB7818757679E61FA1AF17AB1879876F92F77D75DF57B50F99587758909155AF9570D8DDAEBBEF5E3BDABDDA2721BA1F78CA58989C78CAF30ED39956518A0BCF33608AC825596DBC3E27DE46D8702D3F5A6CFCC55EC29F19949F6C3629D33CE4B6D4EA6F1687B2E40E8AD00DDAAF162E1BB1C8A7FE36B423CAC42E04923043966E0410170B570238FB5C37F5966132DB3751BAB7AE948749DB2A322FC76A3619C19D5C31D5E658E1310D78A3D37CB4B0EB2F71C41F4A959F3A1D5A4B8FACF79679A49E5B3DE2D3B9D7C2F90D7AAC2CE340BD75236CE5569F76B45D352F1B19B6AE8A8B411BABB3EF47DC1EEDCB3D98D1F37054C6C7F89054CE60985676EB86DD3EB7C2F363536C4612981FFDDD825BA55D607462526683CED378B3F5A21EF703240C0CF028CD395773E3EAD4DE8567E99CA6896EFC6D4FC20C25644EF98A208640B2CDC6C23389C25E4C58BFC4FC06039BD68DABD8E1C5540B33B2FA0239DE529FAD06C984848A112564536663928047681CEC9BF718384D982C65F054F82103C9560B32396721718707D433F41AF8888A84D703BB6A4E46C8D5B11816B7E2E90F9B6271BA91A2848755FDCDE2F4E605F9DF44BF24EAB38D5795DC9B4AED45351532C54791BA6353CBCB049D064C966057E1A7B5040F012995DD7D17F19A8BDD9CC5EEF6E76CB0DAEF40C28C5F27CCCEF39062E2E323F1A1AADE67495A9616074B06BC966078C490A33C5F98C2425E2F890A659D796FFC950436C962E91A642F719EDD786F12CE20C16C206E14B5D610DB7A5E06A03661320C9AE7769EF3354EBE15EB59CE9F86FE6E717D1145C4451D70A96153ECFA2B7457ACFE6A33F924B9F86967F96D363DA5366AC0F379BD47033D4F8BE1C084D912C603D7176E3F7969E043FC98048B8B77719EA25BC4C730A13E5B5F0D3C09C3F84F4FB8F82EA65A5CCB099ED02A4AF34464CB255958CA385E4B44E5926C2E6B65F89717441B41A35CD26C2CC2054AB3E0D50B8B9040DD8D80928D41BFD7E41FA6ABE33E83D6D791D091CA8F368E7658E81B9490B01BC251B990B85F5B8A20A4DEDAEC0E4139131300AA722F61A4B9F1F8081EE5972539654F073F6C9C5D6C10EB38990151C3620968EC6FBA56C06DD595FDD5D43C032EBDD61F77786BF9731C066BAFC7268F84810182A5398701EE0F334AD22F0C776ED69649879655655E82556ADEC2E5D93109160E12AF5E107A8F2112F637D8947941A87C0CB81F823AEED52BF24EBD3D2F372F6FBC75B19A09ADD2F375901128F0E346FB7D5EF8689F46E98791EE8702EAEC3FA2A5717BC9716EBBC47DFBD8A997A7BC62CA4F3FDEC54F57931CB93DBB43480C58577C9B8F1D839F8036B55E406E13AB05665B84B5C29243A1ACE9EF53D92A22C32DF252FEC213FDDD8E5BFB1824CF4FF64CA4B21D9DF44EB716C3A52DFBE1ECC6A9F2D96E0B13A26064684D941C86312CA45861CE527DDB0DF3D4DD04CC0394BDDD77CCF8D94374E409FBF0AE365CFDA06EC1252FAB874C84E72B14E5AB0C6DBA2358C6C100B3F2ACD2818BDFF0B53C9649B018A444119D74CA985B1AE4D99A240E439488BCF8348BCDB0C2F752E4487FB7B97B145EE4519195BFDEC5A49873FC23407F92BF3876D4E7F12745048DD789F02004F5D96282E547A7A127043E68BECEA7FF97EFB9BB386FD3B232B1087A1E4B58C3F53F0671B99299E33ECAE2CEF02A64965398BB17847AC497D4F332EF2A4A264BE82BC34F1DE7EEF777F248FC91C463ABF6B32DAF1B2F42E42D39805F936431A12DFA3710DDBB4340EFFA953B815793606191A2F8E92920CF5308D271495D78829202C996E710B00298140B8F8AE2D92B90259764711EC178FFB187126C92AD9C004726C102E5CD13BEE0A37F42EA54DB1B5F10B9A6C12D10AA6FB319D96E50F21493F7587CB44AD3BCC70C50C7C96054D3B398FA6C7AFA56BAF548580227CD04B3B26B27198F611AAA2A8D5DEC55DFC69CA03B9E0471B017A64040FA7CA099C49BB8DD74ECE5ACC0F1EAB213AA67B18469F112A351349A076E71B149767A80E6DAF4779B9A43BCDAAFB3E953B72E5E2D513031E847CADC0319F7A248285850FBDD869B78526D7F4A7D973F3F93792FCF89FE3EF6BECE8F1266DDD5D6AE5BD3BC88D373F2280E9E356C8234ED6743947C4CCC8886C130473DA4549E43FDCDEE48A03C14BAC47309F15C804EB3F1908BC84107EF23577DB4E243DC56053EE5472B3E6728448231693FCF0AD57D82FCAE51414AD87409F5ABCC3E0C9067BF30AD9542C24D396A9E9A558F2692B318D662D805E29A2AA67231DB6DB7C97AC455D6703268313D8B252CD7FA6F28803B99D63B8EAE664CD3DF3F9DEF6C75D25EEB264E8893F820D3C505D947DED847DE70DBB7F2C7D44F826DCF5EA5E062D29F94D987E949FD1FA0331FF9266A5B32FDEA31293DD96EC3A07CC88C70EAD0AE5A0EC3AC1C4EF1842ADE40135236C566E383F80FBE0A9727EBAF632FCF5D448A2F329CC6D153906C7857243ECDC6A9254DFF8C93F5672F150281D029161B87C8CF1382A8CCDB6CB91ECF2659484942D94391D499844EFC241A85292CF6EFFF8C2F3C1FF7AFF388DCD6E58F2E84549B837FFF5B9C67E751116CEEF7CCE7CFFF85E40EBC0199F9341B1F5F1FA5E90586285A9F026B0731D96E192BEE3CB45F6763DCE9F5FE69E8051B47DB0F05AF9EFB0F121EC3587B4711C089C8454C0076C0683F5BF2FAC30B738859F57D9638BA8C9F83C8118E0A5E3D7124E1312C128A42F1B7D740F088E792ACCE878B3CFF81B8651B93301B4C7C45E81B8AD6DD81206160D0FCD29C03ADA7C1379EBABCF0641A3D60E8263D49D3D80F8A39B7B4AF9309E743B93A303930A8496DB79C315ED682A9E7D93E944FCC0150311C2B2886508B117D35527412F0DE4B9E4147750713FCFE864C593F3CD55C07040A07ABF44B1E861F0F9FBC900F52A151E187631052E6A8E356810FD56CC570D9D9905B4F3880D68579F78420CFD4010A2572DA21D1645AD51F3DFADA3B0750354D3105504D6E3DD330689892F7FC0154C96907A071CDD004402A4F6D4D7154513B180541CEF3075129E6AE0F872E70584FD2C83D662F8850C29334B3C0EA4BF33BAD3F546180AFE2350AD336DF9DFF82365EA19874EBF9D5B6F54590A4D99997798F5E8A4A92C3837A6D81E7C46F6986364784E0E8EE3FC3D330280E216A822B2F0A9E509ADDC778EAFDF1F0E777EF7F3E3C3809032F255B6CE1D3E1C1F74D18A5BFFAC5EEA91745715654FDE3E14B966D7F3D3E4E8B12D3A34DE027711A3F65477EBC39F6D6F131E6F5CBF1FBF7C768BD39E6B3576C8DB8BCFBD79A4B9AAE431A7CD4A2A49ED3934B35271805D51520B6EDF04A8B87450D975BF4442D2AF836E7337E001622448A8F8701516E6126EA0721D6375E469EA96CADC5E1010120D9FA6A4078AC644F9FCE53C5F04AFD7515ADD1F78F87FF5564FBF560F5BF1EDA9C3F1D14F7B97F3D7877F0BFAD05280F37CAA2C921715604AFB564526EA5944CA2572FF15FBCE47F6CBCEFFFD39A53ED2150F27A0A632FB3E6D1B807B81088F210B06B9F2663AFE6A17D0A7A3612E55160579326A3AC265922EE7DC1A5EB2A2272A217CE4AFB50F7871D310CE5B1258B60556BADD2DFA3E03F73DC62F75887A47DAEBCEF97287ACE5E3E1EBEFFAB75F9E5BE385F3EC5F3E7BFBEB3667AE115F72307E17D150FC71BB76BB135E898ED4D82C815E6E64D69D7EC11B961356001F5CBCC0AB67FE9C0B63AF33417D6C4FE7C599DC5C9A720C95E4EF13FC113990BBA07CA5912BCE2E9DE65E0A3C2C9C46D1DC859EB364EB2FA24D32D77629BAF9F0A15F51E6B4A5E7F8F83A8B8C8D8935BB984BA7EFA1C241D264A6CEE7E9325F626999D1C4CE65E62D4435D1DEAB7DBC4B1CCDD4B907647DD4E843A5FAFC29968177DE68BC34DF14CBADDE033BC71E66922A7555A2E50AF93BB1C7F3F596F82A8E6F918D837537DADAC3B07FCF3B15CDDF766852BB745246E4633EDEC2397570DD42ED47EBE41C9331E7DDEEC476713F6B7A88C483870319FC2385EE31E9E6FB5CB381376959B6A7D83DA05CBCF28787EC99CB0FAEA8ED5F9F72CF14EF3240948C8A9A4F0640BB200E957C33D965EA539DF9185D7200B08C90CBCDB96C4F516916955F9CC50CF21E33C5ABB62B54A49DC233AD859778B58708AA394EC6316E686965018E54D3A86C0B138C8EFC9F00CADF32258245E6AE1197FF9065F0F9EABF4FA15FFC47575A3C6869B5355D65C6FBC37E2E9E0A8EEB663B2B1753A93DDFB5FAC8982F6863A8D1683D8BA33B4F592E2EA5B9735529BB7D7E2801C4641B59BDB26F02C560873DF031ED558D400DCDB8A516D4531D7EF63302A060E4E5BF6FDFC47EAE7E52273DFD987EFECFBDEF5A3F4AE73BC2CDAA509F72A4A71BE3C03A7940E8E01E3E7C4DB38E7FB29F692B50D57B3FDBF340F85BDAAFE27C3CDFB807D5D5162276C46F6CBB1F672283C6E76A3730DD2A786336DDF713101129E7258ACFA87B56D0DF7A17C10BEA2C734B03BC9373A6A27177B5D1F6AD03B517B033A8D015D6DBC673EA6D772FBEE86DA75C5487A0C222F79EBB6C735D706A3DD3776A4D99635E295A77BF7F8DFF4898FF6B1D82698DCEF9AD8EBAEEE3374DE5E42DCC75D456873F613A042558721C718BFA7F166EB45BB726160FEC7D383B86F2E699A378C47CC494890E391C8444379F63C93D077458FFF12BBDF3768B9BB99A7AEBE0CE3078B07D284BCBF31A44D6AB79FF7E3AAFB71B5979BAEC0A0E708DB4B182EFBECC7DA33F41AF848F6DCE5E2510D5FCA915EB431DACDAF6EBC52D2DB1EABBC20FF9B230FAE2E6E47E6E008523FD886C51AB57CB877C7E031DD254E41B3448C0EA7E9123ED3DF3035BDD3D9C38B15AEFC8E40D4C1B2C1FA8CA27805B208D507BE5CB5585D4EDEDDE10764ED448178F4BB94E42551D1E267DE5BD34FBB5DC8F91A642F714E9E95ECC4D018A8A20E7604A1733B0CF81A27DF8A1555CF9B5AB58A9C08455ADFCD3D27C26988A3C6CA5AE8E2D22F16A6931B526757089938FD7D185121FAFBB011C3F2F7E1C645E3EFC38A0BC2DF87D5391B7B7F80F1817D1F7A37FA1C462A5A5F0BE7CFDDB67A0BC5DCA024D5DCFF98DF3AA1172C821055719C760414931B625C851737905C6ABC1E1BF4E1D6DED92DB67E7B614E6CC82A72B29B769D674BD895FB1C87C1DADB95B5F962069176736447343FF9185268B4DBBE2795B5970827AF5E50D05BAE74ED50B3DF96D4D9809E8BB0554A9E9C251906B0B74513EE374477ADEB3BBC5E34B7DD3BAA3F753A9FF4F2D47D27DFCD3B58BD2EED753F23834C60F9FEBB7BF3177BFBED4B57660FEBD245C05C87B68B88748BBC54BCE1D06D394FF83587333D8C906137B3303D2E3AFFB4466C31F6A25EFFEECEC907A95587B94A91CB49E8C8C5DBAC6E86A12BEC766BBE3E4BFF014EE3DD7A07C3E047EC28C600BF4251BECAD0A60BA4A1B7337490E6F4E81ED209091A852B24DD407612F7E2970E612FE2284BE230ACDF1A75310529FDD31CF20BC38B3C2A98BA990FFC11A03FC95FF35BD910E017DDB2CF7472E547A7A1677063A6873FE215C6CD4BF8F6A39CF42CECC913F70B8AE5EF650C7C5255758872D671F782F8E7AAF63D62A71C4AE7E79C75F2981641BA9DB0B9F12244DEB1EAB70A2A7AAE0326F5AB5ABD18FD1EC54F4F01898ADE5BA6969513C98A5D7E279CAE8B57669CB062DDCB1C48E580D1C9FA1FB8AB138F3A578B74177CBE20E20C3E8C0F384A9E62127EDF47AB34CD776592E5E060B58B0A6F3D72737647745855667AA7C6E9E7075C27E9303B10380C112B068BBD89776FBF6EF2F6DF5FAA96F68C1A71CE4EFE5D4C54EF637B2EC69DEC76E7E2E9973572E43AE8EEECF32E7F7E265344037E237B1AEC63E90EBA4539B9BD1FEC34973CCB80A7029B204D3B9A8F199E3DD40F9FF3B36D83F62AB3B26D458793F8F96F5DB6B7CB63904B3C0772D3DBBD886CE79BC30164415C2A7BB23843216A7BAA4358D64D4F9A637C50C29144D8A6EFBCE873792251AB89C42CE9AAAABAB31CEB49EBD028B69A95F5C79EDA6523B538634B63EFA1A319E178B8B42727DB6D1894C1DC8902BA4A28B2E92EA479E8D06212DC6E69EDC87475F2E98193899F8B7DC11E17756493DAE5DD1CDC9539B565B7DEC17808FB2802A6FB244B8A2270973FA67E126C7708A8CEDE191AF2A23437E558F0BCFEB4B0DCFD2305AED2E23D608B1594E9E4BAE336071028B83BA7D3387A0A924DBF17536FBC34FD334ED69FBD541F90C068D70DF97982817997791B376F5917318BE1D0B77DF93951E1FD9FF185E7638B731E915CFDDE018EFD6F719E9D4745A4A6DF33BFEF365DC3B0B76827BE8FD2F402030FAD4FE9A96C979E49FAF698ABF7D3D00B76C5F5175E9777DBE9226A217CDC71FBC30B7337EC96BC12A7B177193F079D26425DF78306DAB829EA81BFBD066B5796B862862BF4C323E62B42DF50B4DE910973D767369C3CB24139E3F59C639FA469EC0785B0DC5E542529D75C78DC3E2013C65A01B51877287C3AAA3F5DE56146E2EEFAB8408CA043BE81AFAB7DFF83F2D2033909487D6F2D561B0BBD96955FCBC948D07E6465F8278135061722174C028F4C33C93B06B805452406911F6CBD90AD3347660859529F86219F7286B6B86F6079F8DA9994D562512CB161CC695657FF0FC714340C11536E78C1A2776DB9774747EF85C61379111E6322C1AA7D1C61A1A8A34979D4BEE72480A09C71A6C004F0285FC18EF9BE78549CC99C76660B8AFA819469305197CE41A2FDBC0388682AB32C40140FB64F8B8AF2CD78181A55DA0EE1A3ACD11241521E45ED41322B9050A793530F2D4AF1DD34EA40EB99A946A82E0D3EEE40C54938D93A8782D9A466683F83998DDD69D73932D1FBB4E0609666AAF59165FB8EBC426AA59BD0C4B4889AC4C6EC57CEF33333CDFE9A54F07E4DB8431BB4B64D3BE62E2D77C36D1A28ADF3F2D8E7C1AD5D190A42B5B8AC10EDD7C5DBA2B62E2685D17EC2D39AA2FA8FE2685D0721FAFC9D815199B0B3D648AEA481C064035C5AB8E9F0F41D4B15A0C8470B31488DBCAC14D4E7E59BA4B6328BB049AB8DF78CD265E0A7949591A0FEB478DC54155904669A71EC2ECE131F5D3F7D0E1239721822BAEDD8849D1DC7E44A1A064976A8A5849B0C4F459CA693685D45911AE42C6B204071A2B30E3D7CDAE28D145F239322A73E03E3C1B587D54EC06AF24190C7D5041B8E7B94E88A9C7AF3B1F42DBCC7FFA698E7426C0F2333E0A2DA262D1F536C85966178584C9118593377369E1A51E3391E77C053DB7E7341D47DBCC7D372F154B7DE0C1C3D9635EE89724B1C0D7668FC032AB5883110C0181752730147BC7301DCC8C7BE1D3107C45C9D0BF49800AA7BE0ED18F084E8BAD3C02E487DD2001189E0E5974E73C227E579B2849C697909C938A8140AD7C836142A0DD43A1040C52A9A940A0B3C27A42E63FE3713FC8D3AFFEB86B8C9E77FB500C53B2645B4912AC2FD32A026151F140AA05A3CF0E4755B38FEC4679FA4480448E9E687927F6074EA143B479CC2EF884D8A583158E4B24C2610EC12920A22DB19A3A90BF829297D72AB791184884492F19E8789763110F428B1193198EF8B07175D1B93E2A6F6EBA0C1B487D1626134B9516A47F665C0A895979182FEBC78105195591A868A3F95FB702D85D07E23EEB64D0523897E6681A346B66981B4A8AD0C5A621149BBB361C154673946A9C6D2A2CCD244701ADF34D9E06966C6690277D7BDA9919437B5972B79D67A21A3159694854EF161F99021D558C6A844B03285F1F8D15B7E0E46E2D40BFD3C5C5008054E6801404CDA4E6089AED1620C0A8D2BB143D85A819DC592B1AD98004724CF5C30B4AC053828BB0A5D3B3453862BB644BBD52CD4557DC78D35F94180D8C50A4D87472EEBDC60B9036E2D73C3E7340E2D3D203A236F96AB38CA5EC2B7932CC325798B09802488CD0803A42E7E8416EBB488C159C4D7045B167BB498153AF5CE462575691AEF5E105A882781283704302679571046576A490689C6D80E4CC9E682C069A6621DB138A369D80D4A9EE264432CF52DFE1E3D2FC3F00962B3BD414C5DBCD913EBB408AB27E28BFAB24AD35C8E338150D2C455E20F843BB50AE7843F5ED2E97098C49B78713BC3BCD42CDA84C4E51B39BE4ACBB0713CB69617C8620E401B39964027ACCD25848500B9A505B0D803CE0C70F3085D714B3DCBB38881F35602F4DB9D7AC8E7D612C0938F94348E26D88EDDA3628E9BAF2BC21E2B9B48FFF07B8A92540A069A947D448049B002459D95942CE5DB260E020F467C9336837BB0F953057CB5CCCAA4DA693A13824BC7ABCA4D90A6C48A88D20F0C17B67CD68C70493B0115AE522625922C93E1A3DC573D59FF234F877B6C7DA0590B2F3BFBD68690B8F8914AA892499953DF5A1700B687D6AE406BF2F9B180AD0926C97BA42C60CE7CB2252A2FBE9039DCC369E8051BF9BC9923679A5448EB3C7B2E84904E9FABD441F0C2D761E4297459359342F9769BCCD2F0825CC6CF413435800A21A400AA5277114065D5160D2052A9A9F133D5F27D62F4182F0567039EAF087DC3653CCCFC958A4A4C4680E6DBC2DFA5A8EB6152D4A8CF519C17B8C679329C0325F5AA2F5EA38B2049B3332FF31EBD549C0B935C7728AB45E61FCF2A0954CF6BDDF92F68E37D3C5C3FC6B8C5BD47A2A5EC317C1008055BC416DDCEBB8532DB2459612D85A694BA2388F5AA12A4D5A9D235FC992341A1102655561243A42DAE8EEC0E96D626CA0B6B698CCBFA2D89F3ADB2C08A425F6A45A8C346FB74BB088E364D8A8E96C4108465E0172910CB641D184B2A5D89D41BD0627954A2B4348A465356FD66B0504E9D202BA34ED7F067DFB5154A61936565B15446DDB97DAD41D2AD5B0275F76EE934E59EC69BAD1741F6B1499195D41018773745F520227DA733AEE6197A0D7C44DFB40024E049E4E5F394BAD281C0F362F90091540280D65A06896990119ACB62662EE4C1A7A5D60AA0D5992E208B4630C8955E900822928902D11A2A078A812CD50E44AC530F944723DB054AB3E0D50B8B6911200E972E938023D3154A4758158BA453A505D244FAE2329428CD8548A22898A3D494FE390E83B50775842645565643A029820E1229944227CA0AA2694CCA92181B2A4D59929149618388C165690D074BA52BB1883D2296547C969650A41A7066020080853014AAF21842BBA2150A95101A0A62AAE42B14E5AB0C6D0009DA24A8CC3A556F5E809B986259228DACA200A99900CC3D2999040C914604865637F009572AC4614F20910E7A02A579E9F5DD0955F1358D41F935A94E00C1E9542C5F2091162F506A4ABF552EB76F4D96DBB716CB6DDE55452C9123800A6569F4BD8CF5BA11D76E4C3254204D615E5CBBF92B2DB22551155B5319AC1F856352710D299048D79102A551E99A59A3CD6CB1C32CF12E7F4CFD24D8CA4A6792A5E532549A12856304A15081022A9723B2835975C6A9C45945A3035A75A26B537A7540A62CBDA2D1955E1D07EA4A6F36C385329B1459DB36045C11D4AEB0B81E6A362F292A6E15049E2980A70A4D65DA4FC2F636E46241E593EED61EB3D5B0A922E7AFA6AAAACAB5ADBBF0604E52F05055A7F6890D6AAFA01E4201C03E789159B5B9DD4505F59696910664C4C32880DF99AFEA2FDD6FEF53FD621BDD4A07608E6115C11C1B70DA80CF01FAAB84F304335089CA776CE12A11CF5D94BD84A71EA44213761A2B708C8B8B21ABDF8E033463F58841513A147F9211437A954AA38251DA7F0C1534F32086AD62BEA4B8F0D94FF871678AF521F083AAC501AA41E687FC817699557A4EDDA395D9D36845333384FA4AD35BD34CC5A14DE7715BBA39115737354036485B0B27FC655EF9B9BD7D95CB037A6575799221AACA3A1814F924AE033D00CD3A082800CD10CA8586BC160AD1559E08A3029A73A53298E76B720CD1F612D7B172A340E30BD65F2536CAD86135E8A7349A1C3BA012C693470D0C09E9204A003D95A86D34A9534E6F1590E82BF21D4039B1EBBDC00955701F1B2BA0215D72F5455F2F654750910FBBD005D561E0A9E644257CC02933CD28C354395D12CD444D6C9024332529022B2D5F4582EB9ED49B0FD095716645A5954E8965C58DDC0B355C351C8751A5D24E29C807B15313A844EA82A95ECCEA730D79CE2575476558E9BD4A5DAA0B7220B5519C3E36321D984DEAFE5A6840EFCC3A53B58A3E0C463054651BF4BC55EAC3C1F032F0CCB05719E5296BB04257500FA120C013B8C8AC72EFEDA702D3CAEF40B5DB0EA8AC35443644A5452FE522AFC2F7B85795294F63659DA5AF4CF3A20B9319A92FF334D536198F0DDEB377D9DAD0F0A074C9EE59757D9B6BDE5F77DBEA63575FBFCD66FC64F8D250C0BC780D555CFE24B6ABAA52B707CA2A4237033A56CDA06575EF382FA09AE073C492BAEA9F2E765861E0EE465377D5658CFE6A28B5AB5701F494AC6DABCDA7DA464399CD1BB1C36001346C66D7629CA948680B634D69DEE3748385B9ABCF6C35DEF111CE0197E253AA52F1D823A03BD3A7211D7551E9E5AF8285FE2E970B75E8476BBBF70C97AE1AF0253EB956F40FF7B95508704F8FD688EAD69D139598D9A00E2FCE0D687FA6509DE27533405FA66FA139C292F4C663A95DED054617EA10AF699AA845F7FE145C49E652295F47F092E8D4EA92BE190569C9EC812957D8915C562D75A1BB7CEA40150627CE76CF22393D4C9D5C3DDA93669B077C16AD1AF0E9184021FA27661C751DE0A6759159757DBA5FB5F5933BD3775116A502E05D0FC89B57F3FA0723387483BC909C4D5039F64A2E85335CDAC4FE3888C5E72A2000006443AA812D8F050197D45B05BA17190075583DE2E0A853C8EEEC171CB477F01DA8C54A21BBAD0ABDC5B48A94BF50B54882BC03DA300907CF5445122CA1A8899066684E99E007823DAD529D2BA58E54A0570A14E27C70A530311904A554A9CE9542E430D2494938AE4AC61876B900D380265421A819D1BBF9487391318A6CB29017F2EA91E8D4247B130DB949FB705C06D6A83EE09FA513C855BC46615A7CFD707C9BE3DC1B54FE2A17080D8B0F9867840A5FBF96694DB38A9EE23A0C3427514D52273741BA320F1B60EF24C98227CFCF70B28FF00C822CC4FFF0C2BC30BC8F68BD8AAEF36C9B67B8CA68F318322B0C124C5A55FE876341E60FD7457098D44515B098011943AEA34F7910AE1BB92FBC30E530296341A254FF86F0F7B22D33FC7FF4FCD670FA1247868C2AF535C1B5EF111EA030B3F43ABAF35E5117D970EFBA44CF9EFF86BFBF066BD261654CF40DC1AAFDC359E03D27DE26AD78B4F9F14F8CE1F5E6FBBFFD7F2D9F663C7D040400, N'6.1.3-40302')
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201711200712510_update_tbl_salary_adjustment_201117', N'FTL_HRMS.Migrations.Configuration', 0x1F8B0800000000000400ED7DD96E24B992E5FB00F30F829E661AD55266D5BD8DDB85CC6E28B554C66D292548AACA79135C1E94E4373DDCA37D51A5D0982F9B87F9A4F985217DE562DCDCE95B64A0802C85D368341A0F8D9BD1F8FFFECFFFFDF0EFDF37E1C12B4AD2208E3E1EBE3F7A777880223F5E07D1F3C7C33C7BFAE7BF1DFEFBBFFDF7FFF6E17CBDF97EF0474DF70BA1C339A3F4E3E14B966D7F3D3E4EFD17B4F1D2A34DE027711A3F65477EBC39F6D6F1F1CFEFDEFDEBF1FBF7C708B338C4BC0E0E3EDCE651166C50F103FF3C8D231F6DB3DC0BAFE2350AD3EA3B4EB92BB81E7CF13628DD7A3EFA7878717FF9F0F9F6EAEEE8ECE4F2F0E0240C3C2CC31D0A9F0E0FBC288A332FC312FEFA7B8AEEB2248E9EEFB6F88317DEBF6D11A67BF2C2145592FFDA929B56E2DDCFA412C76DC69A959FA759BCB164F8FE974A2BC77CF64EBA3D6CB486F5768EF59BBD915A17BAFB78F8298EF2F4245ADFA0C80BB3B7C303BECC5F4FC384D0531A2E1BE3E8C67B4BE2303CE258FC745013FED40003E387FCF7D3C1691E6679823E4628CF122FFCE9E0267F0C03FF3FD0DB7DFC0D451FA33C0C6981B1C8388DF9803FDD24F11625D9DB2D7AAAAA71171E1E1CB3F98EF98C4D362A4F59B35594FDF2F3E1C1175CB8F718A2060F9416EEB23841BFA108255E86D6375E96A10437E76A8D0A8D0AA573659D6FB661FC86D06AAD2B53CDE70C975E73207FDFE3DE62CD8464AF99E0DE80BBF4E1C195F7FD1245CFD9CBC743FCE7E1C145F01DADEB2F15D7DFA3005B009C294B72FB524F3631EEDD8DF0316E787B26B718EFC9B7747CE94F13441AFED35BBFF62BD93869C5DFB76B5022935CB0005CCE2FDE6BF05CA01FAC440DE9C3835B141664E94BB02D6D2E6F131EF82C1749BCB98D43D10071940F77719EF844D6D888FCDE4B9E51665E15AB4A98886F2C786791CB16B4129CCF22179FA5D456822387AAF2E1B81D739423512B9EF910F43939AAB3ED871DC8DC6035296CE5FBBF0E612AC9BF8A427FFEEBBB214ABDF0B217DCECD3147E154F583886C71A25E3977B93A014C3F064BDC67FA886E4A1CA47093612534A70153F06A1AAC5FF324CB9E71B2F081D5457D38F576771F22948B29753FC4FF014F8D4AC613C259F25C12B2EEA32F0F102D445F91A547969BA8D93EC4BBE7974D2ABF413FAEBA742CBBD678425AFBFC74154C8DA935B39F65F3F7D0E92FECB169406CF5131A4F565558FF784475F5E9F122FF25FFA72F90DAFBDD33B2FF492B79EAB1BD9226377D6145CCE557AF7966668739DDCE5F8FBC97A1344358F4F319E727A913D6EF1D4294FFB72C13F1F0BBCBA61872BBA457EE085ED2CB7A77C5E35F87551FBF906E1897AE4BF391BC074ABF672893276B99FC2385EE3EE996F1D6C1868A602688DC7C7F07380972EAD1D18ACB8CF28787EC9062FE6EB38C59C7FC7ABB2D33C49023FC766F4C4CFF0909F05C8C53E8FE9B2BA1C0BC0E574B3BEAD69DAF53397242C98F974DBC5BEC1564B53847C8F45422297B6E7AE0A35DCAB4566080179A974B9B03491F5FECF3AF71572D6A9E0D68F98DA14DFC82892D4D5B0DDA12A376D55CA6429016DD20472753254D6FAFC8E7F05E5441D92B54986352A268B2A05686C75BADA78CFC4B800129649A0745C9220199F6E2B153DE156B7344B09B4344D206F6986AAD7DE5D6D18AD76EECA4CFB7D3BA0AC693692F4FB28031DAE5C6F1159B29653D79E4B98F368ED8AD52ABDC4594FBD90CC4A7033F69DB317DC708F0ED6041618268C94D592D192C599272C3AF51CCE101E970A0B84B00188326CB7EC98ACD2EB57FC138BEF4E3B0DC77E1AAAD9DC786F1B5CB5AE35ECB2EC333697CCB4C7CA665239F78613284B73E0E166B9328D753E435B2FC908A6FB6E58916980A60E239FA2EFEC0657C7DDA33EEB417A39245F12CAA984F9A282D47E615863582B3A4D0A8BDD522845A6C806386AA74B921FB3CBA994A2BB3C5EA7F56939E4D419F7230E649C7E8411A7D843ED3BECEC8DFF18C6BF31762ADB2F21028C918CB2BBE5AFF6E3D5C20BE4A0F41C954A7C9ED4F59666E7A18BDF45D28C704E87AE86BB6AE49210A9943DCCB85521A1E3E055E4DE8F609085DDDD116C3FE48C39E49496D564DC81281506052477BEF6E839F888865C3B4E0D62CCCB224C2C3A44A96D05A72EBFEDE99B9DCF6F9D6F6FCF81B256518AC9F24CB7C733985B68FC9C789BF10BFE147BC9DA41B13A5F9614E36A02FF66DC81DDDCFE899DB0E97CA3A9DB5D0CEBA378E1D4537E5ADFEBBA4279DADEE9CA0221D85B30A0AC690CD7A0473EB463841D589A8C7BA800654D3CD835C54F7607E12B7A4C03277EF99A61FD258E54A5B8F1D864B659F643ECB843ACBD739630C82A1CB83A99CDDA53CBCA649699F6E612B2571BCA13E253101577164C367BE60D5D0BAF3D1EB232AFBE4E70651DF3AC404B67DD4317286B072785A507E43DFE377D2277CD3AB84FD699F7907169858089819BBB62F7B1233E55B3779B67740C87C0000E8E8600938871042474B67BBA6DC398484C534B656E8974525394B672D7303091BAA595CA5C93E8246EE87A0D72A7F166EB459D42055559F7C60A28EB07F3F91EE70EFB6EAD8E47BA36791212E46250BFA2D12E883E076956BA7C7F8947D8C36F8B73B3E05F7D19295E009E8F261966DD41EC0E2E171DA7A62283BDC5074DA0BBE9A9C3080BF7B14B66134C5445F88193550599E214DCCDA455EF4E2694276453D684A336A9109FC57E52DBA1565C26659D185A931AB1197ABAA1BD063E3AC13D3C5A7B96A738F5EC97E7B1378A0AA3E8267499C6CF2A45496FAFE617E47F7374DDB0EB653A731407A91F6CC362DFF7C4EF72FF4C60B007B102C4BDE32609EA26DC661144D65D245663073E5019F04023903EC87253238E692671E831CEE9343E29502C38CF919399D4C4911BB744FD3DADCFDE854652D618BB49D6BE5297C87BC5037B1E654D9CA44E8E53029F3D0480B25C0D42375EB02EC3DB9DE5899345E1B99744451B9E796FCD08D23166DED7207B89F30CCFB13B73ECE82E210323EC3DA1A596C6695164B11D4EC4A6B4AC1AC4C0A492623E8BEA02997B8D455025EC57732297BD1502CA9A9B53D5D738F956EC9FF60DD659ABC48954044CDD0268929C43F9AC563DB24239D9320F1EF3AEFD45CE6DDF6F80B25C8DDEEEA2D37EF2D2C077C2E9739CA7E816F57E0DA40ABA791286F19F1E151CBA23BBCBE009ADA2344F1CF0BA88E3B52BB970C7C4BFBC20DA745059BFC98DD85995B31B05B974BC57E5E935CC5FA0340B5EBDB078B4A28BC56218EC8D146C12D0FA5AE5AA3D90F740D12437B819E0D85ECBDE1073337A5F042122CD5BA8C76A994DE5DCA31E28CBDDC2BA0DFA3F7D0CAE4E6C1C1FEB1A5C37A7C0A9B86A2EA712C62105A9D3DD5ABA1C9DC846C23A1A2503E2EDD3EF6093E7B1B71A035A0D2783D52A727264799D674B39FAFC1C87C1DAEBE4B55A65DDA37A2834CE6DEAD46E415ACE9CDA8C7BB00065B93281859A5D9C849FBC7A4141DF613BB0DB2CA045083809009285390044633B536934A813922204A56CD25562B644BDA62A942CF6BD727F522C296B9A7B07FDF7DF57E9F93AC8BCC7D63DDDF18CA1804DB7836A3AEB1E744059B31B091CC67C9BDB8917D5D53AEAC6CBD3094CC4B4B1F3068B843898E39C601FEF10B27F4DAFC7B446E5FF0012C07386DE5E0E06939BBA0CF9F446A0500BAB9EE2A8A43508EEC794248FECA720530BEF32A6DF65EC753A1E26F9F643255096B3A1122BF864532E6A7B4DBC1C0E9544A65BE4A5CA683F03ED8393B25BFF9F3E9A753670741A20BB0C75DD86D6DD1CB9300CE0118B4E10ED27933A84CD2705286CBD980CCBE8DAB6D71BB51DBD8038167B8B2FB14CAE9EC99ECDA8D1C3DC76EFD814D2A47D1CA201BB1248683D372DA64706E2968472518BCEAD13B32072D9E53B6C87487AFE7E77445E967B8F7B4EF9CBB72E4E6C8272C1AA26D575BDFE8B587EAC35AF8E90555B2BDE6E18568ECF36D0750449E9EABB08C6994C2BEBFA16C2158AF255863636B6B4CE3389D5249DDDD66AEA0D844BAB996042A21EA1D45E2720BF0CF334491C65645C4489A6F881168FE5FDC989CA0EC38B3C2A04187E35F94780FE247FEDCECE2EB102D7C9BA8D36D46DF05DF9D169E83989FDD5CD00E21EF012BEF5733D1398EC27944059B3F23D9B76BB6CBCF3A4EE4E6D9DE7B7426F00A7B6722A612EA4201D60CF4B2C4DBE01A6A335A88ACBADB18A7B3941BC7B41C8CA758CB3661497BD391BD09CB95F67CFF1B6DDC923B925D3DFE1A6E473E34528CCFAF2BA2C6CAA0B2E3728F2FACBF37B143F3D057EE085FDA56A79B991AD38AA75C3EA3A7BC136C40D2FEE7A9D03B95C703A59FF035B5D7251D2D9AEB513465F10095131CE2E97388AA8A60110996CF0046907DAF5018A54EFF8186530A999F3781328798A930D9973ACD234B75AE77C4E8EF8ECFB2901D4BF46F062EDD2E0B75E56C8D2B5C5CBFCFB2607CAAA55DBCB2E4FFB50136F19B99E3ED2EB3902DCC0E1424E25D85405A9F56021D84EA30A88D95415E1A90D2A2464E9374260E1E28EE7ABC45E70D9F7E662C04E3AE768DB0D109C39B23B5981DEC7DDD87434675C6F80AD998C48ECFB52CA21027F0BA529C27EEB68F555193EE4B750A434E0B79A525F1787C1BE6F6909AD2C3195736F8481B24AFD38BA343C8DDFF05DFEFC4CB63E9C943D8B1B290E1DBAC73E6319EBC8D5D5E46154D766CA18816320942ED859906880B31FBA1CF9A98F9C4A29B8CB931EC21DCFFE37419A5A8E0F6CCEBD0F0FD49FB18ADAF2A0771F7EFE9B230F8AD251E812CF1E8637B05E44BC50ACBBBDC8865C7976C0E60C85A835B6FDCD106936B85733907F28E9A8EE2C268BFD18A0E9F7A67485C5521879F7BD0AFC244EE3A7ECE824DD7E41D9519DF1A864799160767FC6C9B7239AE34F07C6F9DA7EFFB369BFFFE5FDE3D32F7FFBEBBF78EB5FFEE52FE897BF8E6F037AF449DB231337BE69C663548A1B037E5D9A6ADE878A8C7A605A4815DF9816499C2098B0728FE29AEBFC915C8F16C77AD2FAA91F5BE03B1B906C27D2CCD34443956BFEB079B17FD31EAE76716EE179EC17C84059B3F2D47372F23DD6126D06712DC75E0CF789A3C9F74645304D0DA930DCE9E89D86D5140A33AA81B9EC43AE7985C2E40B5F0DA9BE1E2E97C06E1E04D83F04601A637B1F76DF94D73CC2EE9B77A4FC31F59360DBE1A481CEBAEF3740599A0744DD4C3A867C86FB64BBC5ED54E882AC066CF0C1655DE056E3782BADD342AB2E9E825DA5E452DDABFDBE9AED9AD3D599861784E394721A474F41B2E9700F46F01D4DD33FE364FDD94B5D84CED71CBF213F4F709FBCCBBCCD76F0D26E5EE2087DC9378FEDBDBE31CA72D634F77FC6179E8F6DF6794472F5E67719FBDFE23C3B8F8A87B17ECF7CDBB3C1868113714E7C1FA5E90506335A9FD2CBE26EB682D89B9137384F432FD8C03B9CDC78F15093B6EB0F9842587648C8EC831F3C079199A835A95CD492422B6A45662B2A61662669452917B420D0CA595239DB3F2E5AC8FD0672C176FE3BC8733F0FD56E033BDA3B23AD45CA18A7A43FBC30775D5427F0177DDE3DF80BB6F3077FD7339171CE26A0011DAB157F7B0DD663CC91AA82B036A682EA5784705BAFBB6CAE5559F73B0340599FC84ED34BFF139637CB95BF6A1A51CA04CE23AAA67CA849DA09049B22CC1CB8649B29C3499AC67E5048C26DC2D762B0F5C2B3F403F6594741A8B207D18F4BE28E84111890E90D16E1E3E13F09EA92B3AD2B43B1ADF5C3327D7FC803F6BA727F392843E0108798D4F7D6626361DDACD92F18E38804190A3CB2A44D71AF09A24CEC1041E4075B2F548BCE6533EC4944ACA6003EE50C6D717B63F9D46D615272DB47C4F29B623875E9B4F3E198829521DAB853292D3C6447538ED02739C93260FFEEE8E8BD50423F3CC1C28C892B58DB261250A7B593008CBE2B60883179160866CC2D0773A4290A99046C7A7946C09B5EF3CB80DCD64BC0B3763918243960C0D5C4767893153111DC34E28C82368DD69705B6DF9238DFDA230ECAA6865D91A31BF6C0C22606A04AA65151A86A892542917372314587CCE16510284ABC652685222CD30450845BC24410CA236EEA2198078BC100C9EB618841982F03409E06E6D32F78F5D519771497B49B89105CD6C9D6C3546DACCDA795E5EC08DB79D9CB3998CA455BC976B94521C364C94AD57F9835315580D234CED12AAA6A31F22A5A6C273B6B38A121945E1336818F8929EC8DD17918439D3C23236EB9E6B09199868676A7188C5DE268171A0A746206E1E9CDA0AA12636E5D03ED63660299604DD300729DFB5C540729640452108C3595151A45D616966E064094CA3F060CA5ED6252387D49705A9358FF51F80E69ED164DEDDC2832CC15482CDD9C6687465535C6B48B501BD96092E49B0E97DFB15401621E3E900247A40531D99059A11260BE2CEB28AFC0186894B7CD22ECE36AE33DA3548F418E0EC25F4962833D9EE9A27027117E04CC49DA621178E384BE7EFA1C2406E3314DED7C3C6698031864449D1D0E95D518733C86DAC8A87C2ADF64B82C5E743889D6D58B13A6A7D0EA6C1052B91C3680D514B628EB69569711E06BD68226824C7D7ACDD7C41ABC23C17637003B03A82E7202C057C2706B5C9D6D60C4CE619BDC4CA60950B8DCEDF2D273FC1EFF9B629E06F612A607C1C7905A614F52C8B28CA5BA1263A054DD56CB30956C1DC86D1ACD9516698E41314A15B3A0CB2EDA4A8C8F52B1BD4C646873CD05A9F7B11D4E6BFA4151DA14B25C8CF255181FA17C4B994850E799814B9AF958AFC8A3764BEB825455618B1AF70D2A3202660DDA6E11E33F500FFE8D1D0B44499FDC1910C5B2577B96E202625EA569602D6953D359C31C5C44805AB10F2F59C04DF20ED38000879F725A30BCC10A4D036EB035CD261CB3007690FAA4B12312DDD62F7D59854F4A6F14630E20D4054A2BA81B170D611EAEE6FCE06F5BC931FA816D9B9BC804739853CFD0CFC7E5794641FF42E7E3FA8A4C83E945CEC76B012E91F78A8AC88AC2BB9D3244E9B3AABC1A845C5DDC1C14452F0AD3E6F51901DAE6EDBA7084DF78C1BA0A839F2766170B4C984C807A400800FF407D17D413E4759CB44FC8DBDF442C31F7E4FD447C18C27C2850E455F50AE8450BFB6EA12A7C91A38141854684BE41DB2E623CB808424442187ACFC6F1CDE459205453D436305614B228F4EAEB310268F52D6622C4D4DE6F742DAC403A303C970FCC8921B948C3D9CE7DF4600468212CB664365084982F0A898A0A8C004445DB2C0D87C59FCAFD6688781024B6DC0128B672CE198B4215C605A3D03E26C53799A685A3F1A619482D0564874D02B880E599C70937C0946DB41C1359D7C0D0480AE483A172D1A6525A89B181B90BE6D2F0A28422CF60209DC3FD080381C646DD726F465CC69EC14E264305822BF6AC76275986CB1A8721D1C7001CD406CB187789E4A6464DA47586B759182FB920636168D9C6EAD40BFD3C340C3525CB208314456B8B2EB098C51936552D46C2A7AABD1663EEE84A14B6CA143A450F1D169D4511D01A03B0A9F343252DFD0488A4DBC7686981E9E78244F3ED18753E037C765967680A5DB22D9D72DBC6AC2D9768599B353F671B2D11C677F131D1CD972D31CB0ACB3F5BAC4BAA361DE425ED6C6AC5A9AC73EB00162E7BC61C46ED06BBE3A9675DC1E9FAC3CEF8E85DC551F612BE9D64192EC9330AB429CF02E15EA0B681BCA2A8454D69F4F51801CAFA765BC44446AC86E1F6982EE308E89DC32E9AA954932072B9FB6B555D4AB37EF78290814F93228F028C1479073482852DD198AA2A321E76556DB724734AD7C3624A6C947B142CEFCE34D8AA72D3207D67A6BF3728798A930D197B6EF1F7E8596FB4E559209C0BD436305714B5288BADAFC70830D6B7DB22ECB5580DEACB2A4D731BE4F2594740B0502464A5F91A2D00D1B27A4D826C59BB1A59672EEF74484FE24D6C75F622CD01E29A23B682B5B4A065D9655D35C600AFAECD966195F95A988620D3651C1CB94B0F3E665AA129A0BC0381C7843A99851D53671B1CD4CB0E3866569D2900BDF06063B794F0DAC904440C01F7560E25156641F68B9A3DA86A30023855EDB38839035D01C3230C799681903987030BBD3C23A36DB987142BC21E370FA9D3C3EF294A5229D44452F0353B8ACA0801CA020078D55484002A8403B24DF7C3DCF05A7713A42969D482B9B44B88B4608763C8ACFA1C5080421BC6EAEED6E1E4C298801C36A8E63D4DAE6B93D209FD6426BDDCD53E59FF234F8B28ADA6E16034F920ACF1596CD0A62B6E51D310C3CA8C304618B6A2892453C78911AA620FE1D1C0BB23B09D036017398F166A613899D6E41B1CB773985B1B0A350516973BCB3ED992962ABE9069EBC369E8051BF94C1B2687E0C7515A4EB825E56826DD0595BB59372FC465FC1C44E6BA29C987D74D558E463705D570BA21A299ABA6A01E5E336531A3AED4BE22F40DF7D907CD73592C19A4898AC2C66A734C17F42A162CF908961C6E079382477D01EBBC402BCE93E11C28A944F87C7B7577F648BEA2EFB89D7D3C1AC51B2F8AE2AC60F02B46F7699890E64F3F1E6609E06D41B8DEA1ACAE12FFBE6A49A07A8155C022CBB19D1408ACDA240D8F52D1D8B800E25470D670600E5004264CAA9653FDA609C8A84D34E6F35B12E75B25B38A42A7E975EECB6A48A519B657198148DA6665B28ED777DC630254F8A18B9CA8440D9FD5C67B065BBF4ED0E4A71FA307B8B0C946586C5F1A9220B225D0F03B8D375B2F827A5A93628C248554109196EF6BE023FA2E01C09527D1F1049EF010B90244D67C25F895111AF60A207CBDB48B00B49A5220775F813D4464283D14665C2A3E44AC29E702A559F0EA85C51801B0E6D275DCE838BD222F3A55CF29438912C9228986E7E7380CD61ED4FE4D8A86438B0F4857743C4E1346C5182FE36384712ACC552067658AE532C484C8A3F86C9097B9070DB26128EC38CA3BAF8C50C3FF0A45F92A431B80639BA4E3215EDE12998934665C999B0B32B60C91CE56094EAFA2A51248CC79D6BEBB2AA6358D8E2BEB1602635BF4E7D170BD55CE286F2D6694FC319CC88C2330E0074E9698D338DD948B5B1EAB18B64B68DD3C4CD88F14E7620289114FCD98D6612CBBCB1F533F09B6328E4CB2865779742D3211B63A2C1AA5DAA553B74AB51B66C1B6DAE052B3AD3692346C9BED0B815793C271A096BEE2E4A5DE4F39A0A8B8290BB8E5C2EC8F7094CDAE4B233BBD601556F5723EF5460BC5A75DB4F21B266C356D54C09DCFAA54A13ACA85AB2239C4EDAC1AC929AD01BF0E2AA21DFDF45A5250CB2B26CF04E94AB1A760CA75688DD52B422385C988553593E481D525DB3931E43996B28ABD182B8D81394CAA086554EB0EDC28B2E13EAE16B9934F032DAACE4A95F5941C963AD2A2E46C74F08E2BC8ACECB93CB55137E3ABEAA4EFF24C0185E9763F7BE9CD06785D3067053753A58D0CB27658A245558FA514A5D9884755CFD1184A7154A2CAB18E4C1065E63F2DAD9909A6ECF5352AAA9A62184915B358E9E51D78CA095DDAE93C7F85EEE49829BB8B66EA039A071586002A454504625027B293211DAF9190C21E3829A0C2101A342F4DDF1F2C0C37856AC09DD80EEA690ECFD47801C8149511A941C5480FEEB4DC86C54C791AA854084F22179FA38414019F3E2AB98CD469D8134D45A761080D604ED3F7EF340C374035CA83597BF570AE0B068B384D0E7915D51921CD69FC2A6CB80F0B32BE701BFD75D0DC503A9B485BFA79A22687790DF5F3C53EFA1B75DEC8FA54A821272155D40CCE012A4CE9DB61C4755445916BF9F26D7339B171B5DA3C6ED545F11D766B9D2BF73E365657436A5CA93A875B55355C875594E840A4EC862A7293DD0EA3EEA8776A32E63ED6961D8370CD2AD82097556565B15A9C6A5416A065B0953320031BF4C34CB38A4021BA2AC3C1429C6A158E10329C4E052FB90799E31CA05CE3CC0A1598F200D5ADF51FEC5816A477337F42274DA0B4B70A72ABAA2BED6D6FC58EBC10165C30D5BB25FA5CFA45AD3CB36ABDACF515B52D6B2AFD42DEA9369AD605F335D48322AAEF80DA5704F5A54A3570E0EDDE22A2A79311E455D9F49A50E456A95DEF95655DDAB0B0A77C8E0D368F14D4F24ACA33419A5438419B721D4F63A6BAB2D4926BFD8CA499D67A28150391C9EB0150436A613CCF156A81D88DA615CA715DA916C9EBEEB28A88AFBBF7538CF8963BCFAFF2CC77A31B93790C4CA8A98DC96C85BF21A0D3D0C8B312F87578AD868C01247D78DE858E26C2917E3FDAF8BD7A79DDF43BD15DB536EA2E34FB803AA42AF90BEB6C15C037D6697500B764141C46A8B6014E744F7F8B1530C0859122464701F832B54427FA57AC850A29DFB1E6B4A3B80565C6775C6D950DAAD794F892B2B236CC5BCA8E34C43C9EACC1647FCD180DEC368FFCAAEA6634D81BDD84B32A6554A8C99E8C35D7ABF2B159938ACB9E9B1D44CBB2F765CD7B833B9D9BED02757CE2D4441966FB3FEE943FE1CE8FE2494C40E7A60F68329536784293AAA5F68EAA29EF61CD85F6614623ED194F788C9F7374A8C951E745AA1705E5AAD43F4008D551F904A1A840C5BD6663EEA32BCFCC8676780F4F576533DBD95FB513DA4CC53B6A808E4D5F5D632A6CF0EE1A5D4BDDAD7653DEC3C2D4E0352F23FDA9DF00D3D455FA0A98437D4A9FFD82CB80C31B74D0AFF40D2948AD660F4EB135D53E39455710089CA0D2A2F695A98140A97BCCC84477E68E35C64F2039D4E4E84E359A77744C346AEA5063F8F68E436D8EEC4C033EE3026850FFDC0B532DE5832F546514A148F4FC86EDB98A274534FA319E761B3C41D25357A34EB2811731A05B2E9A773398BA285ECEA03553C680515D75913F9041DF9911E3CAF487512C3E8D01E107205334B1482DD38B3C368F9623A021575AD13DEE0068C8EA3D08A66EA62F42D0977B34917EACF80FDBEBE4CF0B9828B18BFA8653DC542AD3DB77ABC0F8EA4AEA2D7D3F258E6AF325F1D9010D9A4472672AA689E54ED5A71A6B144AD2C46B978C0175659C6BA90E64A5D71214D35D59332EAA7B2F2D7191DB255AAA2BE35C4BD560A3575249685EAF82DE8D8A4A5623CD25B8E0ED806654E1DD997A4802BC531590C44153717173C987046D27F99B70E24DDA87E33BCC6BE3551F3E1C63121F6DB3DC0BAFE2350AD33AE1CADB6E83E8396D73565F0EEEB69E8F053FFDE7BBC383EF9B304A3F1EBE64D9F6D7E3E3B4609D1E6D023F89D3F8293BF2E3CDB1B78E8F7F7EF7EE5F8FDFBF3FDE943C8E7D066B1F38699B924A973D2E95C4545EA38B2049B3332FF31EBD146BFD74BD11C8D8E0E9ACEE1AFDD665C1F1D1C57623D988A74E9D8FFC5DF942DE5F3E90228FCE4E2E8530EA1CA3569D17B88664CC292A8BE866D771C03CEE7C3272D591EBA978F9A771986F2279FC7C79EE7A602361EF692EF477736EB88110CBA7FC62CEA174F1A239401730541C4E3665945D9A47FDCD9CCB2D4676F22D65D9341FCDF9504F69D19C142F6CE978893AA6BF9B73A3DEA9A199299EAFD1F11225A3BF8BDC3E1C739D82EF7EC742FFE32C22DFAD8D3ABD62066AD8DB651C0CBAB93CEB30FD9BD84E0E2DB1F8EA878A03F997E5507E31E770E1652F789620326212CCF95DC5127E4C8239BFDFF0804CEEF0D1ACEA6FE65C6E1294E2163F59AFF11F9CE1E0D32CB822722421E52BA4DA68F13108050596DF6CC60F2F08F9A1A3F86481B0D5599C7C0A92ECE514FF133C91D9220F3998C462644A8257DC2F2F03BF8C74C38C515C9A45EB7869BA8D93EC4BBE79E411C4A7D98DA2D74F456DC5C1B449B0E5F7F738888A933391639364CE938EE5C2CF1DF8340B49D967DA1949D52FB8AB105A5A5C62CB65B39C3ACD9C6BFB5810CD4FFE84909CD36F78EE9C965B129C09A213F6330E17330E19B7557AF7966668739DDCE5F8FBC97A430262D37C610A8BFE927959CE59EFFA9BCD18133F96EB3F809D906855FF2DC24BCBB0DDE7E22ACF27DBD8C86A8C121B8B4BB2E9D308AF9B23FF0D1AC484449B597FE907A8E42F25B2B01E611CAFAB40A88CFDA0BE5B8CE4688DC7C3B0F1776446742ECD9CEB67143CBF70EBA8FA9B3997AF0097AFD65CCEBF6789779A2749403C351372FBFC35C88A9362A6E5E564B3597D34DB3BDDF71AC09D2A932D0649C661561EFDD70DE094B7C34CF77A8BC8F4E63EE00562122CD018AD4176F4771BDB7B89AD5FED848CD6BCE9E553CD391739E3280DF04AA6B058A2C812921E659C796F9A22CEC4C79474259CA1755E449CC00B1E3C49CF8A0BC07C2120914D3B5CBFE29FB8FEF2B680282C7058E756B78982CCBEAC1BEF8DD80299DE146443CD6D26B2BB6AEF2543D3AB6062607F95B97777FBA78D7F24AEE9E8148B695A1C2251AEF6EB7EB134E4626921DD5D11BADEB8B7CB7818757679E61FA1AF17AB1879876F92F77D75DF57B50F99587758909155AF9570D8DDAEBBEF5E3BDABDDA2721BA1F78CA58989C78CAF30ED39956518A0BCF33608AC825596DBC3E27DE46D8702D3F5A6CFCC55EC29F19949F6C3629D33CE4B6D4EA6F1687B2E40E8AD00DDAAF162E1BB1C8A7FE36B423CAC42E04923043966E0410170B570238FB5C37F5966132DB3751BAB7AE948749DB2A322FC76A3619C19D5C31D5E658E1310D78A3D37CB4B0EB2F71C41F4A959F3A1D5A4B8FACF79679A49E5B3DE2D3B9D7C2F90D7AAC2CE340BD75236CE5569F76B45D352F1B19B6AE8A8B411BABB3EF47DC1EEDCB3D98D1F37054C6C7F89054CE60985676EB86DD3EB7C2F363536C4612981FFDDD825BA55D607462526683CED378B3F5A21EF703240C0CF028CD395773E3EAD4DE8567E99CA6896EFC6D4FC20C25644EF98A208640B2CDC6C23389C25E4C58BFC4FC06039BD68DABD8E1C5540B33B2FA0239DE529FAD06C984848A112564536663928047681CEC9BF718384D982C65F054F82103C9560B32396721718707D433F41AF8888A84D703BB6A4E46C8D5B11816B7E2E90F9B6271BA91A2848755FDCDE2F4E605F9DF44BF24EAB38D5795DC9B4AED45351532C54791BA6353CBCB049D064C966057E1A7B5040F012995DD7D17F19A8BDD9CC5EEF6E76CB0DAEF40C28C5F27CCCEF39062E2E323F1A1AADE67495A9616074B06BC966078C490A33C5F98C2425E2F890A659D796FFC950436C962E91A642F719EDD786F12CE20C16C206E14B5D610DB7A5E06A03661320C9AE7769EF3354EBE15EB59CE9F86FE6E717D1145C4451D70A96153ECFA2B7457ACFE6A33F924B9F86967F96D363DA5366AC0F379BD47033D4F8BE1C084D912C603D7176E3F7969E043FC98048B8B77719EA25BC4C730A13E5B5F0D3C09C3F84F4FB8F82EA65A5CCB099ED02A4AF34464CB255958CA385E4B44E5926C2E6B65F89717441B41A35CD26C2CC2054AB3E0D50B8B9040DD8D80928D41BFD7E41FA6ABE33E83D6D791D091CA8F368E7658E81B9490B01BC251B990B85F5B8A20A4DEDAEC0E4139131300AA722F61A4B9F1F8081EE5972539654F073F6C9C5D6C10EB38990151C3620968EC6FBA56C06DD595FDD5D43C032EBDD61F77786BF9731C066BAFC7268F84810182A5398701EE0F334AD22F0C776ED69649879655655E82556ADEC2E5D93109160E12AF5E107A8F2112F637D8947941A87C0CB81F823AEED52BF24EBD3D2F372F6FBC75B19A09ADD2F375901128F0E346FB7D5EF8689F46E98791EE8702EAEC3FA2A5717BC9716EBBC47DFBD8A997A7BC62CA4F3FDEC54F57931CB93DBB43480C58577C9B8F1D839F8036B55E406E13AB05665B84B5C29243A1ACE9EF53D92A22C32DF252FEC213FDDD8E5BFB1824CF4FF64CA4B21D9DF44EB716C3A52DFBE1ECC6A9F2D96E0B13A26064684D941C86312CA45861CE527DDB0DF3D4DD04CC0394BDDD77CCF8D94374E409FBF0AE365CFDA06EC1252FAB874C84E72B14E5AB0C6DBA2358C6C100B3F2ACD2818BDFF0B53C9649B018A444119D74CA985B1AE4D99A240E439488BCF8348BCDB0C2F752E4487FB7B97B145EE4519195BFDEC5A49873FC23407F92BF3876D4E7F12745048DD789F02004F5D96282E547A7A127043E68BECEA7FF97EFB9BB386FD3B232B1087A1E4B58C3F53F0671B99299E33ECAE2CEF02A64965398BB17847AC497D4F332EF2A4A264BE82BC34F1DE7EEF777F248FC91C463ABF6B32DAF1B2F42E42D39805F936431A12DFA3710DDBB4340EFFA953B815793606191A2F8E92920CF5308D271495D78829202C996E710B00298140B8F8AE2D92B90259764711EC178FFB187126C92AD9C004726C102E5CD13BEE0A37F42EA54DB1B5F10B9A6C12D10AA6FB319D96E50F21493F7587CB44AD3BCC70C50C7C96054D3B398FA6C7AFA56BAF548580227CD04B3B26B27198F611AAA2A8D5DEC55DFC69CA03B9E0471B017A64040FA7CA099C49BB8DD74ECE5ACC0F1EAB213AA67B18469F112A351349A076E71B149767A80E6DAF4779B9A43BCDAAFB3E953B72E5E2D513031E847CADC0319F7A248285850FBDD869B78526D7F4A7D973F3F93792FCF89FE3EF6BECE8F1266DDD5D6AE5BD3BC88D373F2280E9E356C8234ED6743947C4CCC8886C130473DA4549E43FDCDEE48A03C14BAC47309F15C804EB3F1908BC84107EF23577DB4E243DC56053EE5472B3E6728448231693FCF0AD57D82FCAE51414AD87409F5ABCC3E0C9067BF30AD9542C24D396A9E9A558F2692B318D662D805E29A2AA67231DB6DB7C97AC455D6703268313D8B252CD7FA6F28803B99D63B8EAE664CD3DF3F9DEF6C75D25EEB264E8893F820D3C505D947DED847DE70DBB7F2C7D44F826DCF5EA5E062D29F94D987E949FD1FA0331FF9266A5B32FDEA31293DD96EC3A07CC88C70EAD0AE5A0EC3AC1C4EF1842ADE40135236C566E383F80FBE0A9727EBAF632FCF5D448A2F329CC6D153906C7857243ECDC6A9254DFF8C93F5672F150281D029161B87C8CF1382A8CCDB6CB91ECF2659484942D94391D499844EFC241A85292CF6EFFF8C2F3C1FF7AFF388DCD6E58F2E84549B837FFF5B9C67E751116CEEF7CCE7CFFF85E40EBC0199F9341B1F5F1FA5E90586285A9F026B0731D96E192BEE3CB45F6763DCE9F5FE69E8051B47DB0F05AF9EFB0F121EC3587B4711C089C8454C0076C0683F5BF2FAC30B738859F57D9638BA8C9F83C8118E0A5E3D7124E1312C128A42F1B7D740F088E792ACCE878B3CFF81B8651B93301B4C7C45E81B8AD6DD81206160D0FCD29C03ADA7C1379EBABCF0641A3D60E8263D49D3D80F8A39B7B4AF9309E743B93A303930A8496DB79C315ED682A9E7D93E944FCC0150311C2B2886508B117D35527412F0DE4B9E4147750713FCFE864C593F3CD55C07040A07ABF44B1E861F0F9FBC900F52A151E187631052E6A8E356810FD56CC570D9D9905B4F3880D68579F78420CFD4010A2572DA21D1645AD51F3DFADA3B0750354D3105504D6E3DD330689892F7FC0154C96907A071CDD004402A4F6D4D7154513B180541CEF3075129E6AE0F872E70584FD2C83D662F8850C29334B3C0EA4BF33BAD3F546180AFE2350AD336DF9DFF82365EA19874EBF9D5B6F54590A4D99997798F5E8A4A92C3837A6D81E7C46F6986364784E0E8EE3FC3D330280E216A822B2F0A9E509ADDC778EAFDF1F0E777EF7F3E3C3809032F255B6CE1D3E1C1F74D18A5BFFAC5EEA91745715654FDE3E14B966D7F3D3E4E8B12D3A34DE027711A3F65477EBC39F6D6F131E6F5CBF1FBF7C768BD39E6B3576C8DB8BCFBD79A4B9AAE431A7CD4A2A49ED3934B35271805D51520B6EDF04A8B87450D975BF4442D2AF836E7337E001622448A8F8701516E6126EA0721D6375E469EA96CADC5E1010120D9FA6A4078AC644F9FCE53C5F04AFD7515ADD1F78F87FF5564FBF560F5BF1EDA9C3F1D14F7B97F3D7877F0BFAD05280F37CAA2C921715604AFB564526EA5944CA2572FF15FBCE47F6CBCEFFFD39A53ED2150F27A0A632FB3E6D1B807B81088F210B06B9F2663AFE6A17D0A7A3612E55160579326A3AC265922EE7DC1A5EB2A2272A217CE4AFB50F7871D310CE5B1258B60556BADD2DFA3E03F73DC62F75887A47DAEBCEF97287ACE5E3E1EBEFFAB75F9E5BE385F3EC5F3E7BFBEB3667AE115F72307E17D150FC71BB76BB135E898ED4D82C815E6E64D69D7EC11B961356001F5CBCC0AB67FE9C0B63AF33417D6C4FE7C599DC5C9A720C95E4EF13FC113990BBA07CA5912BCE2E9DE65E0A3C2C9C46D1DC859EB364EB2FA24D32D77629BAF9F0A15F51E6B4A5E7F8F83A8B8C8D8935BB984BA7EFA1C241D264A6CEE7E9325F626999D1C4CE65E62D4435D1DEAB7DBC4B1CCDD4B907647DD4E843A5FAFC29968177DE68BC34DF14CBADDE033BC71E66922A7555A2E50AF93BB1C7F3F596F82A8E6F918D837537DADAC3B07FCF3B15CDDF766852BB745246E4633EDEC2397570DD42ED47EBE41C9331E7DDEEC476713F6B7A88C483870319FC2385EE31E9E6FB5CB381376959B6A7D83DA05CBCF28787EC99CB0FAEA8ED5F9F72CF14EF3240948C8A9A4F0640BB200E957C33D965EA539DF9185D7200B08C90CBCDB96C4F516916955F9CC50CF21E33C5ABB62B54A49DC233AD859778B58708AA394EC6316E686965018E54D3A86C0B138C8EFC9F00CADF32258245E6AE1197FF9065F0F9EABF4FA15FFC47575A3C6869B5355D65C6FBC37E2E9E0A8EEB663B2B1753A93DDFB5FAC8982F6863A8D1683D8BA33B4F592E2EA5B9735529BB7D7E2801C4641B59BDB26F02C560873DF031ED558D400DCDB8A516D4531D7EF63302A060E4E5BF6FDFC47EAE7E52273DFD987EFECFBDEF5A3F4AE73BC2CDAA509F72A4A71BE3C03A7940E8E01E3E7C4DB38E7FB29F692B50D57B3FDBF340F85BDAAFE27C3CDFB807D5D5162276C46F6CBB1F672283C6E76A3730DD2A786336DDF713101129E7258ACFA87B56D0DF7A17C10BEA2C734B03BC9373A6A27177B5D1F6AD03B517B033A8D015D6DBC673EA6D772FBEE86DA75C5487A0C222F79EBB6C735D706A3DD3776A4D99635E295A77BF7F8DFF4898FF6B1D82698DCEF9AD8EBAEEE3374DE5E42DCC75D456873F613A042558721C718BFA7F166EB45BB726160FEC7D383B86F2E699A378C47CC494890E391C8444379F63C93D077458FFF12BBDF3768B9BB99A7AEBE0CE3078B07D284BCBF31A44D6AB79FF7E3AAFB71B5979BAEC0A0E708DB4B182EFBECC7DA33F41AF848F6DCE5E2510D5FCA915EB431DACDAF6EBC52D2DB1EABBC20FF9B230FAE2E6E47E6E008523FD886C51AB57CB877C7E031DD254E41B3448C0EA7E9123ED3DF3035BDD3D9C38B15AEFC8E40D4C1B2C1FA8CA27805B208D507BE5CB5585D4EDEDDE10764ED448178F4BB94E42551D1E267DE5BD34FBB5DC8F91A642F714E9E95ECC4D018A8A20E7604A1733B0CF81A27DF8A1555CF9B5AB58A9C08455ADFCD3D27C26988A3C6CA5AE8E2D22F16A6931B526757089938FD7D185121FAFBB011C3F2F7E1C645E3EFC38A0BC2DF87D5391B7B7F80F1817D1F7A37FA1C462A5A5F0BE7CFDDB67A0BC5DCA024D5DCFF98DF3AA1172C821055719C760414931B625C851737905C6ABC1E1BF4E1D6DED92DB67E7B614E6CC82A72B29B769D674BD895FB1C87C1DADB95B5F962069176736447343FF9185268B4DBBE2795B5970827AF5E50D05BAE74ED50B3DF96D4D9809E8BB0554A9E9C251906B0B74513EE374477ADEB3BBC5E34B7DD3BAA3F753A9FF4F2D47D27DFCD3B58BD2EED753F23834C60F9FEBB7BF3177BFBED4B57660FEBD245C05C87B68B88748BBC54BCE1D06D394FF83587333D8C906137B3303D2E3AFFB4466C31F6A25EFFEECEC907A95587B94A91CB49E8C8C5DBAC6E86A12BEC766BBE3E4BFF014EE3DD7A07C3E047EC28C600BF4251BECAD0A60BA4A1B7337490E6F4E81ED209091A852B24DD407612F7E2970E612FE2284BE230ACDF1A75310529FDD31CF20BC38B3C2A98BA990FFC11A03FC95FF35BD910E017DDB2CF7472E547A7A1677063A6873FE215C6CD4BF8F6A39CF42CECC913F70B8AE5EF650C7C5255758872D671F782F8E7AAF63D62A71C4AE7E79C75F2981641BA9DB0B9F12244DEB1EAB70A2A7AAE0326F5AB5ABD18FD1EC54F4F01898ADE5BA6969513C98A5D7E279CAE8B57669CB062DDCB1C48E580D1C9FA1FB8AB138F3A578B74177CBE20E20C3E8C0F384A9E62127EDF47AB34CD776592E5E060B58B0A6F3D72737647745855667AA7C6E9E7075C27E9303B10380C112B068BBD89776FBF6EF2F6DF5FAA96F68C1A71CE4EFE5D4C54EF637B2EC69DEC76E7E2E9973572E43AE8EEECF32E7F7E265344037E237B1AEC63E90EBA4539B9BD1FEC34973CCB80A7029B204D3B9A8F199E3DD40F9FF3B36D83F62AB3B26D458793F8F96F5DB6B7CB63904B3C0772D3DBBD886CE79BC30164415C2A7BB23843216A7BAA4358D64D4F9A637C50C29144D8A6EFBCE873792251AB89C42CE9AAAABAB31CEB49EBD028B69A95F5C79EDA6523B538634B63EFA1A319E178B8B42727DB6D1894C1DC8902BA4A28B2E92EA479E8D06212DC6E69EDC87475F2E98193899F8B7D419793AEA55E1CDC9529B565AFDEC17008FB2002A6DB244B0A2270973FA67E126C7708A8CE9E191AF29E3437E358F0B4FEB4B0DCFD0305AED2E239608B0594E9DCBAE32E071027B83BA7D3387A0A924DBF07536FBC34FD334ED69FBD541F8FC068D30DF97982817997791B374F5917218BE1C8B77DF93951E1FD9FF185E7638B731E915CFD9E018EFD6F719E9D4745A0A6DF33BFEF2E5DC3B0B76827BE8FD2F402030FAD4FE9996C979E49FAF6988BF7D3D00B76C5F3175E96775B0210B5103EEEB8FDE185B91B764B5E88D3D8BB8C9F834E13A1AEDB4103EDDB14F5C0DF5E83B52B4B5C31C315FAE111F315A16F285AEFC884B9EB2B1B4EDED8A07CF17ACEB14FD234F68342586E2BAA92946B2E3C6E1F900963AD805A8C3B143E1DD59FAEF2302361777D5C2046D021DFC0D7D5B6FF4179E7811C04A4BEB716AB8D855ECBCAAFE56424683FB232FC93C01A830B91FB258147A699E41903DC82221283C80FB65EC8D6992333842CA94FC3904F39435BDC37B03C7CED4CCA6AB12896D830E634ABABFF87630A1A86882937BC60D1BBB6DCBBA3A3F742E389BC088F319160D53E8EB050D4D1A43C6ADF73124050BE385360027893AF60C77C5F3C2ACE643E3BB30545FD3ECA3498A84BE720D17EDE014434955916208AF7DAA74545F9643C0C8D2A6D87F051D6688920298FA2F620991548A8D3C9A98716A5F86E1A75A0F5CC54235497061F77A0E2249C6C9D43C16C5233B49FC1CCC6EEB4EB1C99E87D5A70304B33D5FAC8B27D475E21B5D24D68625A444D6263F62BE7F99999667F4D2A78BF26DCA10D5ADBA61D739796BBE0360D94D67979ECF3E0D6AE0C05A15A5C5688F6EBE26D515B1793C26837E1694D51FD4771B4AE83107DFECEC0A84CD8596B2457D24060B2012E2DDC7478FA8EA50A50E4A38518A4465E560AEAF3F24D525B9945D8A4D5C67B46E932F053CACA48507F5A3C6EAA8A2C0233CD387617E7898FAE9F3E07891C390C11DD766CC2CE8E6372250D83243BD452C24D86A7224CD349B4AE82480D72963510A038D159871E3E6DF1468AAF914991539F81F1E0DAC36A276035F920C8E36A820DC73D4A74454EBDF958FA16DEE37F53CC7321B687911970516D93968F29B642CB303C2CA64888AC993B1B4F8DA8F11C8F3BE0A96DBFB920EA3EDEE369B978AA5B6F068E1ECB1AF744B9258E063B34FE01955AC41808608C8BA8B98023DEB9006EE463DF8E980342AECE057A4CFCD43DF0760C784270DD696017A43E69808804F0F24BA739E193F23C5942CEB4BC84641C540A856B641B0A95066A1D08A062154D4A85059E13529731FF9B09FE469DFF7543DCE4F3BF5A80E2199322DA4815E07E1950938A0F0A05502D1E78F2BA2D1C7FE2AB4F522402A474F343C93F303A758A9D234EE167C42645AC182C72592613087609490591ED8CD1D405FC94943EB9D5BC08424422C978CFC344BB18087A94D88C18CCF7C5838BAE8D497153FB75D060DAC368B1309ADC28B523FB3260D4CACB48417F5E3C88A8CA2C0D43C59FCA7DB8964268BF1177DBA68291443FB3C05123DBB4405AD456062DB188A4DDD9B060AAB31CA354636951666922388D6F9A6CF03433E33481BBEBDED448CA9BDACB95BC6ABD90D10A4BCA42A7F8B07CC8906A2C6354225899C278FCE82D3F072371EA857E1E2E28840227B40020266D27B044D768310685C695D8216CADC0CE62C9D8564C802392672E185AD6021C945D85AE1D9A29C3155BA2DD6A16EAAABEE3C69AFC2040EC6285A6C32397756EB0DC01B796B9E1731A87961E109D9137CB551C652FE1DB4996E192BCC5044012C466840152173F428B755AC4E02CE26B822D8B3D5ACC0A9D7A67A392BA348D772F082DC49340941B021893BC2B08A32BB5248344636C07A6647341E03453B18E589CD134EC06254F71B22196FA167F8F9E9761F804B1D9DE20A62EDEEC89755A84D513F1457D59A5692EC799402869E22AF107C29D5A8573C21F2FE974384CE24DBCB89D615E6A166D42E2F28D1C5FA565D8381E5BCB0B643107A08D1C4BA013D6E612C24280DCD20258EC016706B87984AEB8A59EE559C4C0792B01FAED4E3DE4736B09E0C9474A1A47136CC7EE5131C7CDD715618F954DA47FF83D45492A05034DCA3E22C0245881A2CE4A4A96F26D1307810723BE499BC13DD8FCA902BE5A666552ED349D09C1A5E355E5264853624544E907860B5B3E6B46B8A49D800A572993124996C9F051EEAB9EACFF91A7C33DB63ED0AC85979D7D6B43485CFC482554C9A4CCA96FAD0B00DB436B57A035F9FC58C0D60493E43D521630673ED91295175FC81CEEE134F4828D7CDECC91334D2AA4759E3D174248A7CF55EA2078E1EB30F214BAAC9A49A17CBB4D666978412EE3E7209A1A408510520055A9BB08A0B26A8B0610A9D4D4F8996AF93E317A8C9782B301CF5784BEE1321E66FE4A4525262340F36DE1EF52D4F530296AD4E728CE0B5CE33C19CE81927AD517AFD14590A4D99997798F5E2ACE8549AE3B94D522F38F679504AAE7B5EEFC17B4F13E1EAE1F63DCE2DE23D152F6183E0884822D628B6EE7DD42996D92ACB09642534ADD11C47A5509D2EA54E91AFECC91A05008932A2B8921D216574776074B6B13E585B534C665FD96C4F956596045A12FB522D461A37DBA5D04479B2645474B6208C232F08B148865B20E8C2595AE44EA0D68B13C2A515A1A45A329AB7E335828A74E909551A76BF8B3EFDA0AA5B0C9B2B2582AA3EEDCBED620E9D62D81BA7BB7749A724FE3CDD68B20FBD8A4C84A6A088CBB9BA27A1091BED31957F30CBD063EA26F5A0012F024F2F2794A5DE940E079B17C80482A01406B2D83C434C808CD65313317F2E0D3526B05D0EA4C1790452318E44A2F480411C94481680D9503C540966A0722D6A907CAA391ED02A559F0EA85C5B40810874B9749C091E90AA523AC8A45D2A9D20269227D71194A94E642245114CC516A4AFF1C87C1DA833A4293222BAB21D014410789144AA1136505D1342665498C0D95A62CC9C8A4B041C4E0B2B48683A5D29558C41E114B2A3E4B4B28520D38330100C042180A55790CA15DD10A854A080D053155F2158AF255863680046D1254669DAA372FC04D4CB12C9146565180D44C00E69E944C028648230243AB1BF8842B15E2B0279048073D81D2BCF4FAEE84AAF89AC6A0FC9A542780E0742A962F90488B172835A5DF2A97DBB726CBED5B8BE536EFAA2296C8114085B234FA5EC67ADD886B3726192A90A6302FAEDDFC9516D992A88AADA90CD68FC231A9B8861448A4EB4881D2A874CDACD166B6D8619678973FA67E126C65A533C9D272192A4D89C2318250A8400195CB11D9C1AC3AE354E2ACA2D101AD3AD1B529BD3A2053965ED1E84AAF8E0375A5379BE142994D8AAC6D1B02AE086A57585C0F359B971415B70A02CF14C05385A632ED27617B1B72B1A0F249776B8FD96AD85491F357535555E5DAD65D7830272978A8AA53FBC406B557500FA100601FBCC8ACDADCEEA2827A4BCB480332E26114C0EFCC57F597EEB7F7A97EB18D6EA50330C7B08A608E0D386DC0E700FD55C2798219A844E53BB6709588E72ECA5EC2530F52A1093B8D1538C6C5C590D56FC7019AB17AC4A0281D8A3FC98821BD4AA551C128ED3F860A9A7910C356315F525CF8EC27FCB833C5FA10F841D5E200D520F343FE40BBCC2A3DA7EED1CAEC69B4A29919427DA5E9AD69A6E2D0A6F3B82DDD9C88AB9B1A201BA4AD8513FE32AFFCDCDEBECAE501BDB2BA3CC91055651D0C8A7C12D7811E80661D0414806608E542435E0B85E82A4F845101CDB95219CCF3353986687B89EB58B951A0F105EBAF121B65ECB01AF4531A4D8E1D5009E3C9A3068684741025809E4AD4369AD429A7B70A48F415F90EA09CD8F55EE0842AB88F8D15D0902EB9FAA2AF97B223A8C8875DE882EA30F05473A2123EE09499669461AA9C2E8966A226364892999214819596AF22C1754FEACD07E8CA38B3A2D24AA7C4B2E246EE851AAE1A8EC3A85269A714E483D8A909542275C1542F66F5B9863CE792BAA332ACF45EA52ED5053990DA284E1F1B990ECC26757F2D34A077669DA95A451F062318AAB20D7ADE2AF5E16078197866D8AB8CF2943558A12BA8875010E0095C6456B9F7F6538169E577A0DA6D0754D61A221BA2D2A297729157E17BDCABCA94A7B1B2CED257A679D185C98CD497799A6A9B8CC706EFD9BB6C6D687850BA64F7ACBABECD35EFAFBB6DF5B1ABAFDF66337E327C6928605EBC862A2E7F12DB5555A9DB036515A19B011DAB66D0B2BA779C17504DF03962495DF54F173BAC307077A3A9BBEA32467F3594DAD5AB007A4AD6B6D5E6536DA3A1CCE68DD861B0001A36B36B31CE5424B485B1A634EF71BAC1C2DCD567B61AEFF808E7804BF12955A978EC11D09DE9D3908EBAA8F4F257C1427F97CB853AF4A3B5DD7B864B570DF8129F5C2BFA87FBDC2A04B8A7476B4475EBCE894ACC6C508717E706B43F53A84EF1BA19A02FD3B7D01C61497AE3B1D4AEF602A30B7588D7344DD4A27B7F0AAE2473A994AF237849746A7549DF8C82B464F6C0942BEC482EAB96BAD05D3E75A00A831367BB67919C1EA64EAE1EED49B3CD033E8B560DF8740CA010FD13338EBA0E70D3BAC8ACBA3EDDAFDAFAC99DE9BB288B5201F0AE07E4CDAB79FD83111CBA415E48CE26A81C7B2597C2192E6D627F1CC4E27315100000B221D5C096C782804BEAAD02DD8B0C803AAC1E7170D4296477F60B0EDA3BF80ED462A590DD5685DE625A45CA5FA85A2441DE016D98848367AA22099650D444483334A74CF003C19E56A9CE9552472AD02B050A713EB85298980C8252AA54E74A217218E9A4241C5725630CBB5C80694013AA10D48CE8DD7CA4B9C818453659C80B79F548746A92BD8986DCA47D382E036B541FF0CFD209E42A5EA3302DBE7E38BECD71EE0D2A7F950B8486C507CC334285AF5FCBB4A659454F711D069A93A826A9939B205D99870DB0779264C193E76738D94778064116E27F78615E18DE47B45E45D779B6CD335C65B4790C9915060926AD2AFFC3B120F387EB22384CEAA20A58CC808C21D7D1A73C08D78DDC175E98729894B12051AA7F43F87BD99619FE3F7A7E6B387D8923434695FA9AE0DAF7080F5098597A1DDD79AFA88B6CB8775DA267CF7FC3DF5F8335E9B03226FA8660D5FEE12CF09E136F93563CDAFCF827C6F07AF3FDDFFE3FA7350D386B040400, N'6.1.3-40302')
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201711230834596_FilterAttendanceview', N'FTL_HRMS.Migrations.Configuration', 0x1F8B0800000000000400ED7D5B6F1CB992E6FB02FB1F043DED0ECE5876F79CC199863D0359B2DA35235B82A46EEF9B91AAA2A43CCECAACC98B6C61B0BF6C1FF627ED5F5832B3B292972019CC64DECA8506DCAA6430180C7E0CDE82C1FFF77FFEEFDB7FFBB18E8E9E499A8549FCEEF8CDABD7C747245E26AB307E7C775CE40FFFF8B7E37FFBD7FFFEDFDE7E58AD7F1CFD59D3FDCAE868CE387B77FC94E79BDF4E4EB2E5135907D9AB75B84C932C79C85F2D93F549B04A4E7E79FDFA5F4EDEBC392194C531E57574F4F6A688F3704DCA1FF4E759122FC9262F82E853B22251B6FD4E536E4BAE479F8335C936C192BC3BBEB8BBFCFAF1E6D3EDABF3D3CBE3A3D3280CA80CB7247A383E0AE238C9839C4AF8DB1F19B9CDD3247EBCDDD00F4174F7B22194EE218832B295FCB7861C5B89D7BFB04A9C34196B56CB22CB93B523C337BF6EB57222676FA5DBE39DD6A8DE3E50FDE62FACD6A5EEDE1DBF4FE2223B8D57D7240EA2FCE5F8482EF3B7B32865F49C86ABC678751DBCA44914BD9258FCE5A826FCCB0E18143FECBFBF1C9D15515EA4E45D4C8A3C0DA2BF1C5D17F751B8FC0FF272977C23F1BBB888225E602A324D133ED04FD769B22169FE72431EB6D5B88D8E8F4EC47C2772C65D362E4F55B3459CFFFACBF1D1675A78701F911D1E382DDCE6494A7E273149839CACAE833C27296DCEC58A941A554A97CAFAB0DE44C90B218B95AD4C339F735A7ACD81FD7D477B8B331396BD66427B03EDD2C7479F821F97247ECC9FDE1DD33F8F8F2EC21F64557FD972FD230EA905A099F2B4702FF5749DD0DEBD133EA10DEFCEE486E23DFD960D2FFD594A58C3BF7FE9D67E151B2FADF8C766054A84C9050B20E5FC1C3C878F25FAC14AD4903E3EBA215149963D859BCAE6CA36E1AB9CE5224DD63749A41A2089F2EB6D52A44B266B8222BF0BD24792E3ABE254098CF868C15B8B5CB5A093E07216BDF822A5B51212395495B727CD98631C891AF1F043D0C7F4559DED30EC40E686AAC9602BDFFCB50F53C9FE3514FACB5F5FF751EA45903FD1661FA7F04FC988855378AC483A7CB9D729C9280C4F572BFA876948EEAB7C92522331A6049F92FB3032B5F83FF553EE877510461EAA6BE9C78BF3247D1FA6F9D319FD277C0897DCAC6138259FA7E1332DEA325CD205A88FF22DA80AB26C93A4F9E7627DEFA557D927F4570FA5963BCF082B5EFF9E8471296B476ED5D87FF5F0314CBB2F5B48163EC6E590D695553DDE331E5D79BD4F8378F9D495CBEF74ED9DDD065190BE745CDDE81619FBB3A690722EB2DB972C27EBABF4B6A0DF4F57EB30AE79BC4FE8943388DD714BA74E45D6950BFD795FE2D50F3B5AD10D598641D4CC723BCA176C07BF366AFFB02674A21E2F5FBC0D60B6557BB54419BADCF75192AC68F72C361E360C2C5301B2A2E363F431A44B97C60EF456DC47123E3EE5BD17F36598623EFCA0ABB2B3224DC36541CDE8E932A7437E1E121FFB3CD86575351680CBE9DDFAB6A669D6CF5292B26096D35D17FB88AD965D11FA3D160D895EDA8EBB2ADC706F16592004E4E5D2F5C2F244CEFB3FAB626990B34E05B77ED4D45DF13B195592BA1AAE3B54D5A6AD49992225A04D9E40AF4E81CA599F3FE8AFB09AA843B2EE92618DAAC9AA4A011A579D2ED6C123332E80845512289D94A44826A7BB4AC54FB8CD2D2D52022DCD13E85B5AA0EAB477571B46A79DBB2AD361DF0E286B9C8D24FB3E4A4F872B571BC296ACD5D4B5E312E643BCF2C56A915DD2AC6741C46625B419BBCED94B6EB447872B060B0A1341CAED92D191C579A02C3AED1CCE091D974A0B44A80188736AB7DC982CB2AB67FA938AEF4F3B3B8EDD3454B3B90E5ED6B46A6D6BD866D9873697C2B4C7C96672390F861328CB72E0E167B9328E753E279B20CD19A6BB6E58B16980A50E039FA2EFED0657CBDDA32EEB417E39A45F12EAA994F9A281D47D615863D82A3A4F0A8BDD501845E6C87A386AE74BD21FB3EBA98CA2FB3C5EE7F5E938E4D4190F230E649C7E8611A7DC43ED3AEC1C8CFF10C67F67EC4CB65F430418231D657BCBBFDD8F370BAF9083D24B5426F16552DF5B9AAD872E7917C932C2791DBA76DC4D239786C8A4EC7EC6AD2D125A0E5E65EEC3080659D8FD1DC10E43CE90434E655931E30E4469302820B9F7B547C7C14735E4D671AA17635E1581B1E810A5B515BCBAFC36A76F6E3EBF75BE833D07CA5AC419252B72DB1E4F6F6EA1C9631AAC872FF87D12A42B0FC5DA7C59328AAB11FC9B6907F673FB27F1C2A6F58DA67677319C8FE295534FFD697DA7EB0AD5697BAB2B0B8CE060C180B2C6315CBD1EF9F08E116E60D9653C4005286BE4C16E57FC687710BE90FB2CF4E2976F19D69F92D8548A1F8F4D619BE530C40E3BC4BA3B672983ACC181AB95D9AC3DB59C4C6695E9602E217BB5E63C21DE8771796701B3D9336DE83A78EDC990D579F5B582ABE898E7045A3EEB01BA40597B3829AC3C20EFE8BFD903BB6BD6C27DB2CE7C808C4F2B044C0CFCDC15BB4B3CF1D9367BBB7946CB700802E0E0680830891A474043E7BAA7DB340C46629E5A2B734364939AA37495BB860146EA86562B734D62937847D769903B4BD69B206E152A689BF560AC80B27E329FEF61EEB0EFD7EA78A06B93A711432E05F53319EC82E86398E595CBF7E764803DFCA6383F0BFEC5E781E205D0F9689A53D62DC46EE172D1726AAA3238587CD004FA9B9E7A8CB07097F86436C24455851F38593590194EC1FD4C5AEDEE644A794A36634D246A4C85E42CEE93DA16B5923219EB24D0626A2466E8E886F61C2EC929EDE1F12A703CC5A967BF328F83513418453FA1CB2C7E5619493B7B353F91E5374FD70DDB5EA6C3A338CC96E1262AF77D4F976DEE9F290C0E203680B873DC2445DD8CDB2482C8FA8BC48A76E00395010F340AE9575D6E6EC4C16652871E744EAFF1498162C1798E9E0C53134F6EDC1AF577B43E07171A4D5943EC2639FB4A5D92E0990EEC459CEFE224B5729C52F81C200094E56B10BA0EC25515DEEEBC48BD2C0A3F04695CB6E179F0B21B415AC6CCFB12E64F4991D339766B8E2DDD25746084BD27ACD4DA382D862CAEC389DA948E558318602AA9E673A82E90B9D3580455C27D35A772395821A0ACA939557D49D26FE5FE69D7609DB54ABC48C5C0D42E8026CBD997CFEAB6476E51CEB6CCC3FBA26D7FD1733BF41BA02C5FA3B7BFE8B4EF832C5C7AE1F431293272433ABF06B20DBA791A45C9F7800B0EDD92DD65F840167156A41E785D24C9CA975CB463D25F4118AF5BA8ACDBE446EDACC6D98D815C3BDE9BF2741AE62F489687CF41543E5AD1C662090C0E460A360964756572D5EEC97BA06C926BDA0C706CAF796F88F919BD2FC288B0E62DD5E3B4CCE6721E500F94E56F61DD04FD1F3F06572B369E8F7511D7CD39701AAE9AEBA99471C840EA75B7962FC726324A584FA364C8BC7DBA1D6CCA3C0E56A347ABE165B05AC45E8E2CAF8A7C2E479F3246FF0CC9771F58677C46C13B8F2757DCE3B138D889849F303383C4B2E9B507B6EC702EB7583C2906DDF53E2651B80A5A398C6FB31E0694BE6038B5554BB3FBEFB86869321EC00294E56BF651AAD98713CAE9731096F42D76E2DB4DC01B8480F36F2059997E4334AE8B849D066D427284A094BB7493980D51A75502278B7BAF3C386968CA1AE7CA4FF7A3AF45F66115E6C17D7333C4F364BD844D3B1F113EEB01744059931B093C865B9CDA6133D7D55AEA2628B2114CC4B8612B7B5BB8F5E6B3AAD8C75B42DC1FB2EC30AD31B91E8104F09CA1B3831162725397A19FDE28146661CD531C93B488B89A4249FAA09A0632B3F03EC3695E26412BCF0C96EF30540265791B2AA9824FD7D5A2B6D3C4CBE350C964BA2141660CB4D5D311142BBB71BDEBA2596F0347AB01B2CD50D76E68DDCF918BC2001EB1F804D57E0AA97DD87C5680C1D6ABC9B08CBE6D7B7D46D2D2014F6271B0F81ACBE4EB85FAC98C1A1DCC6DFB8ECD214DDBC7211AB02B8184CE73D3727A8410B722D48B5A766E9B982591CF2EDF623B44D3F30FBB23FAB2FC5F7691943F7FEBE2C5261817AC66525BD7EBBE8895C75A7C7594ACD65AC976035939395B4F378134A59BAF01A133612BEBFB02D02712178B9CAC5D6C699D6714ABD9C6C76348DF8EEB20A5844C3D4AA99D4E407EEDE755A024CED9B848524F3E29AE479FE5D5E591CA8EA28B222E05E87F35C97CA3D85FFBB3B3CBACC055BA6A027DB51B7C17CBF82C0ABC84DD6B6700690F788A5EBA797D2A4C0E134AA0AC49B97D8EBB5D36DC79527B7FD2D6F35BA53780535B3D9532173290F6B0E7A596A6DF00B3D122AAE2736B6CCBBD9A20DE3E11E2E43A2659338ECBC19CF568CEFCAFB3A778D1F5F49E5D50EBEE7053F1B90E6212E55D795D9636D507976B1207DDE5F9234E1E1EC2651844DDA56A78F991AD3CAAF5C3EA2A7FA236C40F2FE966AB07B97C703A5DFD9D5A5D7647D9DBAEB517469F098B0E33CC2E973A8A98A60110996EF004697BDAF5018A34EFF8A032606AE63DD40B491F9274CDE61C8B2C2B9CD6391FD35772F6C39400EA5F0378B1B669F09B202F6569DBE255FE43930365D5AAED6497C77D234DB68C524F1FE8E12A056EE070A1A7526CAA81D479B0506C27AA026A365345646A4485942CDD46082A5CD2F27C95D90B29FBC15CF4D849A71CE87E07046F8EEC5E56A077493B362DCD99D41B606BA62352FBBE96B28F98FB4A698688FB365A7B55FA8FB6AF14A98DB56FA6B4D7C5639CFD1B5E42274BCCE53C1861A0AC4A3F9E2E0D8FE3377C5B3C3EB2AD0F2F654FE2468A4787EEA1CF58863A72F5357918D4B5993346E01808A52B761624EAE1EC872F477FEAA3A7320AEEF3A48771A7B3FF7598658EE38398F3E0C303F567AAA2A63CE8C9955FFEE6C983A27214BAA4B387FE0D6C1057917E1CBBBDCA865D79F6C0E69C44A431B6DDCD106B36B8570B90FF5AD171DD594D56FB3140D3ED39F72D162B61F4DDF753B84C932C79C85F9D669BCF247F55677C55B1BC4829BBEF49FAED15CFF12F47E87C4DBFFF05DBEF7F7D73FFF0EBDFFEFACFC1EAD77FFE27F2EB5F87B7011DFAA4EB91891FDF34F41895D1C6801F76E79AF7EB968C7BDB5D49559F775749BC2098B1F28FE29AEBF4915C8F162776D2FA952D57E07B1B905C27D2C2AB607D958B465CB57FD31CAEB6716E91791C16C8405993F2D4F372F23DD4126D022165875E0C7709612BF746431C5B0BA932DCD9E8BD46B4550A43D5002F7B9F6B5EA530FDC2D7426AAF87CF25B09FB7380E6F7060C3DB1F5EBCC0F29AC68B17F88E54DC67CB34DCB43869E0B31EFA0D50D654A3F0A2C171BAD9D0762A75C156032EF890B2CE70AB71B895D659A9551FAF302F3276A9EED97D5FCD75CDE9EB4C2308A3614A394BE287305DB7B807A3F88E66D9F7245D7D0C321FAF56588EDFC8B248699FBCCD83F5A6F7D2AE9F92987C2ED6F7CDBDBE21CAF2D63477DF938B60496DF68798E5EACCEF32597E4B8AFC435CBE49F747BE743D1BDC31F022CEE97249B2EC828299ACCEF865713B5BC1ECCDC01B9C675110AEE11D4E69BCF85A9336EB0F9842597668C8DC831F3C86314ED49A542F6A456115754BE62A2A638693744BA917B424B0CA595179DB3F2E5BC8FF0672C976FA3BC8533F0FB56E037BDA3B63ADC5CA18A6A43F83A8F05D542BF0977DDE3FF84BB6D3077FDB339161CE26A0019DAA957E7B0E5743CC91B605516D8C05D52F84D0B65EB5D95CDB663DEC0C0065BD673B4D4FDD4F585E1C57FEA669442513388FD836E5D79AA499408829CACC414A7699329C6659B20C4B49A44DF85A0CB15E74967E24BEA8AA0855F520FE5D57DA9128024336BDA122BC3BFE07455D7AB6756538B6B57E44A66F8E65C05E6DDD5F8EAA1038CC21265B062BB5B1A86E56E2178A71C2820C85015BD266B4D78471AE7688305E869B20328B2E6543F62426D6AE0039E59C6C687B53F9CC6D8129B9E9236AF9BB622475D9B4F3F6848315126DD2A994151EBAA3294FE8D39C6421D8BF7EF5EA8D5242373CC1C20C892B58DB1809B8D3DA5100C6DF1540624C9F05829970CB018F344321A380CD2ECF0078B36B7E1E90DB042978D6AE078326070CB89AD80D6FBA2246829B459C41D066D1FABCC0F67B9A141B77C441D9CCB02B73B4C31E58D8C80034C934280A4D2D3147284A4E2E5874E81C5E7A81A2C65B665428C2328D0045B8253082701E71630FC132581003A4AC873E0661B90C007916988FBFE0B55767D8515CD36E1821A4ACA3AD87B9DA389B4F27CBD912B6D3B297533095B3B692CD728B430666C9CAD5BF9F35315780D1344ED12A9A6A31F02A5A6D27376B38A221D45E13C6C007630A3B63741AC6D026CFC0889BAF39DCC9CC43C3BA530CC62EF1B40B0D053AC141787C3368AAC4905BD740FBE04CA010AC691C40AE8AA514D5410B19851404634DE5844695B583A59B0010B5F20F01436DBB600AE72F098E6B12EB3F4ADF21ABDDE2A9BD1B4581B90189959BD3E4D068AAC69076116A23174CB27CE3E1F207952A24C2C3075AE0A8B4202677644EA80498CFCB3AEA2B30041AF56D330BFBB858078F24B36350A283F05791B8604F663A2BDC69841F00739AB69805DE24A1AF1E3E8629623CE6A9BD8FC70273008382A893C3A1B11A438EC7501BA1CAE7F28D86CBF24587D378B57D71027B0A6DCE062155CAE102584B61B3B29EB8BA0C005F5C0B620419FBF45AAE8933780782ED7E007602509DE50440AE04726BDC9CAD67C44E619B1C27D308289CEF7679E5397E47FFCD284F84BD84E941F009A44ED8D314322F6369AEC4102835B7D53C4CA55807769BC672A5459BA3578C72C5CCE8B28BB512C3A3546D2F8C0C4DAEA920F52E71C3694DDF2B4A7785CC17A372158647A8DC521809EA3C137049C38FF5863C66B7B43648351536AB711F519101308B68BB598CFF403DE437761C10A57D72A74714EB5EED998B0B08BE4AE3C05AD3A6D859C3145C44805A890F2F39C04DF30E538F00879F729A31BCC10A8D036EB03571138E49003BCC96ACB16316DD7659F9B22A9F8CDE28680E20D4154A27A8A38B86300F57737AF077ADE410FDC0B5CD3132C11CA6D433ECF3717D9E41D03FD3F9B8BD22E3607A96F3F15A804B123C9332B2A2F26EA70E51F6AC26AF0625571B370743D1B3C234BE3E03401BDFAE3347F87510AEB661F08B1477B100C36404D4034200F807EA3BA39EA0AFE3A87D42DFFE18B1D4DCA3F713F56108FC5060C86BEA15D08B16EEDDC254F82C4703448506843EA26D67311E5C841161210C8347747C337D1608D51CB50B8C0D85CC0ABDF67A0C005A7B8B618418DBFB8DAF8513487B86E7FC813932246769389BB98F1D8C002D84C586CC058A10F35921D15081018068689BB9E1B0FCD3B8DF0C11F782C4863B00C546CE296351A9C2B06054DA0753FC2ED3B870446F9A81D45A40B6D824800B989F791C7103CCD846F33191750D90465221EF0D95B33695DA4A0C0DCC7D3097C88B12863CBD81740AF72310020D8DBAF9DE8CB84C02C44EA64005822B099C76274586F31A8721D187001CD406F3187799E458A3A6D27AC3DB248C975E90A130346F63751644CB2242869AD265D0418AA375451758CCEC0C9BA91603E1D3D45EB331777C254A5B85854ED943FB45675904B4C6006CEAF450C94B3F0222F9F6412D2D28FD549088DF8E31E743E0B3CD3AC352E89C6DE998DB36B8B69CA365DDADF925DBE88830B98B0F896EB96C8D593658FEC9625D53B5F120AF6967AC15E7B24EAD0338B8ECA1390CDA0DF6C753CFB982E3F587BDF1D1FB94C4F953F4729AE7B4A4001568539F05C2BD42ED02794351B39AD2D8EB310094EDED368B898C5A0DE4F6982DE300E89DC22E1A56AA511039DFFDB56D5D2AB37EFB4408C2A7C990C700468EBC051AC1C2E6684C4D15190EBBA6B69B9339E5EBE1302546E51E04CBFB330D76AADC3848DF9BE9EF35491F9274CDC69E1BFA3D7EB41B6D7D1608E70AB50BCC0D45CDCA62DBEB31008CEDED360B7BAD5683FBB2C8B2C205B972D60110AC14095969B9463340B4AE5EA3205BD7AE28EB2CE51D0FE969B24E9CCE5EB439405C4BC44EB0D616342FBB6CABC610E0B5B5D93CACB25C0B6C08325BC6DE913BF7E063D80A8D01E53D083CA6D4091776CC9CAD7750CF3BE018AE3A63007AE6C1C66E38E1AD9309881802EE8D1E4A26CC82EC67357B30D56000709ADA67167306BE02C8230C7D969E903985030BBB3C03A36DBE87140BC69E360FABD3D73F3292665AA8A9A4E06B761C150A01C6020078D5548C002A4402B24BF7A3DCE85A771D66196BD492B9B64BA8B4608713C89CFA1C5080411B6875B7EB707A613020870D2ABEA7E9758D299DD18F66D2AB5DEDD3D5DF8BAC8CD28A0D0763C907614DCEE282365B71B39A86202B33C018816C458C2463C78951AAE20EE1C1C0BB27B09D026067398F566A819C4C5BF2F58EDB29CCAD91428D81C5F9CEB24F37ACA5CA2F6CDAFAF52C0AC2B57EA60D9343F093281D27DC9A722C93EE92CADFAC5B16E232790C63BC6E2AF2FE75B32DC7A29B92AA3FDD30D1F0AA29A9FBD74C55CCA02BB52F847CA37DF6ABE5B92C910CD2C496C2C56A4B4C67F42A162CF900961C6E074CC183BE80F5A1442BCD93D31C24DD8AF0F1E6D3EDF93DFB4A7ED0765ED2D1285907719CE42583DF28BACFA294357FF6EE384F016F0BC6F596E47595E4F7552B02D30BAC0A16458ECDA44061D5245978548AA6C60510670B670B07E100456122A45A39D56F9A808C9A44349FDFD3A4D818996D296C9A5E154B5D0DB934647B551188B46D5625DB78FDA03D2624A51FBACA894BB4F059AC8347B0F5EB044B7EFE317A808B988CC262F3D29006910D8185DF59B2DE0431D4D37629682419A48288AC7C9FC325E1EF12005C65121B4FE0090F952B40E4CC57835F1D21B25700E1EBB55D04A0B59402B9FB2AEC2122A4F4509871ADF810B1A59C0B92E5E17310956304C05A4AB771E3E3F4AABCF8543BA79CA44624AB248E3CFF0CC97704DF8ACCC2FB631285AB00C2D62EC5C2A1C11ED40E7CAC4F0CA372FEA0E383EA3F5C08AD50CF0ADB4FAAF0152A8FF23322AF70C71A642350B871D41B061DA185FF2712178B9CAC018E4D928D877A314C65A6D2E0B80AB722746C05229B1D541C6A552BA890E079D67EC126A6358D8DABE87202635BF515B270BD31CE566F1C66ABF2119FCA4C2240F0032762C2499F6D3A272DBD4D0C9BE5B96D8EA7EC75AAF33C8504C5D3325EB618276F8BFB6C99861B1D4721D9C2AB3A16579928DB280E8DB2DD0134B7CA76A7CD81ED76F3CCCC76BB496561BBDB1A5178ED52240EDCB25A9D18D57B35471C95341D02B77384BD178972B7A3B3939D5F0C2B3B067A3EF5260EC7A75910CB9B3162355D54209DFD9A54613A2686ABA239206EAD1ACD0930825F0B15F14E84762D19A8F515D367827465D8AFC072ED5B63F56A13A5301DB1A9669A3CB0BA74BB32489E4329ABDCE771D2189803534528A35977E026940BF761B5289DAA22B4683A8735D6537310EB498B9A73D7DE3BAE22B3B1E7CAD4A86E2657D54BDF9599020AB3EDAC76D29B0BF0DA60CE096E58A50D0CB26658E245358FA51C256EC4E3AAE7690CE5381A51E559471844E17CB3B535C360CA5D5F83A26A578C20A96116ABBD18044F39A10B41ADE7AFD07D1F9CB2DB68A63EFCF96AC2104065A888420CEA4477EA64E3351052C4C32C0354044244F3F2F4DDC1227033A806DC896DA19EDDC19C192F0099A1322A35A818EDA1A0955BBF98A94E1A8D0A9149F4E24B949022E0934D2397813A8D785A6AE834022102E63C7DF74E23700354633CF475578FE4168158C45972E8AB68CE0869CEE2B3E1C2BD5F90C985BBE8AF85E6FAD2D948DAB2CF132D39F035B4CF17BBE86FD079A3E8AF61869C86D450333807A830A3DF088AEBA08A6257FEF5DBE67A6274B59A3C7ED5C5F1ED776B5D2AF72E41AB6B478AAE549DC3AFAA765CFB5594EA9C64EC862672CC6E07AA3BDA1DA6D0DC87DAB213106E590523723955561707C6AB4675C15F7A5B390332880145709A350421B155190E44E255AB70F491FE74AA78E07DD539E501CA456736A800CB0354B7D537B1655990DE71BE8A5E9AC0686F0DE44E5537DADBCE8A1D7821ACB8779A774BECB9EC8B5A7D66D37AD9EA87EA5AD658FA853C5F5D346D0B148CD4832162708FDA37040CE64A453807B76F11D5D30905795336BB260CB94D6AB77B653997D62FEC397F66C4E691815A5F497D26489306076B2CD7E13486D595A3967CEB6720CD34D6C3A818884C5F0F801A528BE0796E500BC46E30AD708EEB46B5685E8ED755447D39BE9B62D477E2657E5BCF7C3FBAC1CC6360424B6D30B315F986804D4303CF4AE097E7AD1A420348FBA8BD0F1D8D8423FB7EB4891C5937FB4E745BAD0DBA0B2D3ECE0EA94AFF7ABB5805F0FD765E1DC02D19038701AA8DC089ED5971B502085CA01431380AC057AF353AB1BF90AD54C8F846B6A41DC32D281CDF61B55535A85D53EA2BCDC6DA08EF347BD290F030B30593DD35831AD85D1E1036D50D35D8A36EC239953228D474CFD1E2F56A7CC8165371DD53B6BD6859F7762DBE37F8D3396E17A8E5F3A91865E0F67FFC297FC49D1FC3739B80CEB18F730A95463CCFC9D5D27A4715CBBB5F73617DF411A53DF48407FD54A4474D0E3A2F32BD56A857A5FD7143A88EC6E70D55051AEE35A3B90FAE3C9C0D6DF1D69EADCA38DBD95DB523DA4CC31B6D808EB12FBA091546BCE9C6D7D276AB1DCBBB5F98225E0A43E9CFFCBE98A5AEDA17C63CEA53FBA4185C061CDEA0857EB5EF53416AC53D6625D6D4FA9C155F41207082498BD617AC7A02A5EDA1248CEEF08E35E8E7953C6A7270A71ACB1B3D188D621D6A90EFFA78D4E6C0CE34E013318006ED4FC908D5323E26C355C6108AC4CEAFDF9E6B78AEC4A21FF4B41BF1BC49475D0D3AC9065EDB806EB958DEE410EA62789583D74C1503C674D545FFF8067F67468D2BD31D4689FAEC06841F80CCD0C42AB54E2FFAD83C568E80867C69C5F67004A021A7B72684BA615F9BE02FF75822FD38F1EFB7D7E99F2EC028B18DFAFA53DC582AB3DB77A7A0FBE64ADA2D7D37250E6AF335B1DF010D62A2C40B15B3C489E7EAB31D6B0C4AB2C482D78C017565BC6BA90E6465D712142FDE583329627C272D4951E1355AAA2BE35D4BDBC1C6AEA48A105FAF92DE8F8A2A5603CD25A4C0F080664CA1E3857A6882C77315D0C4413371F173C987058467F977A1CA77696F4F6E29AF75B0FDF0F684922CC9262F82E853B2225156277C0A369B307ECC9A9CDB2F47B79B6049053FFBC7DBE3A31FEB28CEDE1D3FE5F9E6B79393AC649DBD5A87CB34C99287FCD532599F04ABE4E497D7AFFFE5E4CD9B9375C5E3642960EDAD24EDAEA4CA654F4A65F19A57E4224CB3FC3CC883FB20A35A3F5BAD15323130BBA8BB9D7EEBB2E0D8EB6ABBB16CCC53A7CEC7FEDEFA42DE5D7E6545BE3A3FBD5442B44B8C1A755ED01AB231A7AC2CE19BDDC681F2B85DB291AB8E8ACFC5E23F4BA2621DEB63F3EB73D7031B0BA9CF73E1BFE3B9D10622229FEA0B9E43E5E2C573802E6098389CAEAB28BB3C8FFA1B9ECB0D4576FA2D13D9EC3EE2F970CF74F19C0CAF77D978A93AE6BFE3B9716FE0F0CC0C4FE3D878A992F1DF556E6F4FA44E2177BF13A5FF491651EED6A84E6F9881227BBB8E03A29BEBB3F6D3BF99ED94D092A82F8A9838B07F450ED5173C878B207FA2B30495919080E7F729D1F01312F0FC7EA70332BBC3C7B3AABFE1B95CA724A32D7EBA5AD13F24C321A7397025EC4842CB574975D1E27D18290AACBEB98C1F4118C94347F9C901618BF3247D1FA6F9D319FD277C60B34519723089C3C89486CFB45F5E86CB2AD28D304649690EAD1364D92649F3CFC5FA5E46909CE6368A5E3D94B55507D35D822BBF7F4FC2B83C395339EE92F03CF9582EF2DC414E7390547C025E90D4FC3ABC09A195C565B65C37CBA9D3F05C9B8788787EFAE789F49C7EA773E7ACDA92904C109F709871F89871E8B82DB2DB972C27EBABF4B6A0DF4F576B16109BE70B5338F4973CC80BC97AD7DF5CC698E4BE5AFF01EC9444A7FA6F085D5A46CD3E97547939D9C5466EC728B5B1A424973E4DE8BA395EBE40839892E832EBAFFC008DFCB5440ED6234A92D53610AA603FB8EF0E233959D1F130DAF93B0A23BA9486E7FA91848F4FD23AAAFE86E7F205E0F2C599CB871F791A9C15691A324FCD94DD3E7F0EF3F2A45868793DD964561FBBED9DF67B0DE04E15668B4193B19F9547F7750338E56D31D3BDDA1036BDB90B658184040734C62B901DFFDDC5F65E52EB573B2193956C7AE5543CE732671267215DC994164B155943D2A18CF3E0C552C4B9FA9892AD8473B22ACA881374C14327E9797901582E0424726987AB67FA93D65FDF161085030EEBDCE6363190B997751DBC305BA0D39B81ACAFB9CD4876D7ECBD8434BD062608FB6BCCBDBFDB3F4DFC23754DC7A7384CD39288A872355F0F8BA53E174B33E9EE86D0F5E8DEAEE381EAECFACC3F435F2F5731FA0EBF4B3EF4D5435FB53E64E2DC6141464EBD56C3617FBBEEA17BED69F76A9E84687FE0A9638139F1D4E7EDA7332DE28C165EE4C014514A72DA787D4C83B5B2E15A7D74D8F84B82543E33A83EB96C526645246DA9D5DF1C0E65D91D14A51B345F1D5C3612954FFDAD6F4794915D083461861CDD08202E0EAE0470F6A96EEACDC364366FA2B46F5D2D0F4CDB1A32CFC76AEE32823BB96AAACBB1C27D16CA4667F7D1C1AE3F25B17C28557D6A7568AD3DB23E58E6817AEEF6119FD6BD16CE8FE8B1BA8C3DF5D6B5B295BBFDB4A7ED6A79D908D9BA262E883636673F8CB81DDA577A30A3E3E1A88E0FFA9054CFA09F56F6EB86DD3CB722F313535C4612981FFFDD81DB56BBC0E824A44C069D67C97A13C41DEE07681820F0A8CD395573E3EBD4DE8767E994A6897EFC6D4FA39CA46C4EF94C208640B2CBC6C2238BC25E4E583F27F2068398D68EABDAE1D5540733B2F80C39DE729F9D06C994858A512514532663928047683CEC9B771838314CE632781AFC908164A705999EB392B8C703EA39790E97848B84D701BB664E28E4DA58F48B5BF5F4474C7138DDC8482AC3AAFEE6707AF34496DF54BF24EEB38B5795DE9BCAEC45351632D54791DA63D3CA0B834E049339D855F8692DC543404BE576DF45BDE6E2366771BBFD3919AC763B90C0F16B85D9691E528C7C7CA43E54D5F92CC9CAD2E16009C16B0E86470D392AF385291CE40DD2B854D679F0225F4910931C96AE61FE9414F975F0A2E10C124C06E2A8A8B5486CDB7921408D61D20F9AA7769EF32549BF95EB59C99F86FFEE707D91C4CC451D70A91153DCFA2B7457ACFEEA32F964B9E46967F56D323DA5366AC0F3799D47033B4F87E100C36C0EE381EF0BB7EF832C5C42FC8404878B774991911B22C730E13E3B5F0D3C8DA2E47BA05C7C57531DAEE5840F64116745AAB295921C2C6592AC34A24A492E97B572FA2B08E3B5A25129693216E1826479F81C446548A0F646C0C806D1EF2DF9FBE9EAB4CF90D555AC74A4EAA38BA31D15FA9AA42CEC867254AE241ED6962A08B9B736DB4350CF04034053EE398C34D7811CC1A3FA3227A7ECF1E0478DB38F0D621B271C102D2CE680C6EEA66B01DC565DB85F4D2D72E0D26BFD718FB7966518FD1992EFFE700D716B816D98CD3008C56FDC696D5CE78B34FBD64B66E1FDF53189C255D061FB53C300817F6DCE7E4CFA4F337FE4DFDE6EDDAC0D93162D6BCA3C87F17AF74AB4CC4E4870701D7A0EC228B88F88B2F327A64C0B42D533D9DD10D4F214CB9077EC832BBD797991AD8BD31A61917D58853983823CA36ABE4F0B1FCDA341DD30D2FEB8CC9CFD67B4347EAFFF4EEDFCA46B1F3B0B8A4C564CF5E9E7BB12ED6B92A3B767B784A8A11CCB6FD3B163F0E3E858EB05E4C6582D30DB2CAC15951C0AF2CE7F1FCB5631196E4890C95701F9EF6EDC9A6752657EBA07548DEDE8A577FAB5183E6DD94F6737CE8C0FDA3B98100323A4353172E8C7B0B0629539CBF6DB7E98A7F626601AA0ECECD886E3E70ED18127ECFD3BA149F583BA85943CAF1E32129E3F91B858E464DD1EC13A0E08CCEAB36A072EF928C4F1C032A562B0125574F229C36EE9C7799A441149555E729AC36658E995AC72E4BFBBDCCA8B2E8AB8CC2A5F7C1452F01CD9310CFB4B62C77D1E7E52C4D078952A4FA5709F1D2658CBF82C0A949020BBAFD3E9FF14624FD18B8F93682B2B8C45B0F398C31AAEFB3188CF95CC14F7516677BABD45663585B97D22A443E4553B2F7C57313299435FE97FEA38758FD8D37BE6A9A71E5B359F5D795D073161AF2C02FC76490E13DAB27F0371EF5B84BAAFDF7F5478ED121C2C529C3C3C84ECE116453A29A90D4F505220D9F11C02568090E2E045513E0807B294921CCE2304BF58F150424C729513E0282438A07CF7B835F81CA6923AD6F6C667C22E30490B84EDB7C98C6CD7247D48D84B454BB2C8B2A2C30CD0C60931AAD9598C7D363D7E2BDD042C608797668259B9B5938E473F0DB52D4D5CEC6DBF0D3941F73C099260AF4C8180F4E940334DD649B3E9D8C95941E2D56627D4CE620ED3E239C669D9691EB8DF2826B9E9019A6BF3DF5D6A0EF16ABE4EA64FDDF878CFC7C004D18F8CB97B32EE65915018ADE6BB0B37F5A4DAFD94FAB6787C64F35E9913FF7DE87D9D9FE501025F5BBB7E4DF32C4ECFD9735174D6B00EB3AC9B0D31F2C198110B837E8E7A58A93287FA9BDB914075287449E712EAB9009FE6E2211797D750241FB9ED47273ECC6D55E1537D74E2734E22A21893E6F3A450DD25FCF58A94A48C4D9B20D8C6ECFD0079F20BD35A292C109BA7E6A9597568223D8B7E2D865B88BAB1A28D97B3DD669BAC43C4710B27448BD959CC61B9D67D4301DCC974DE71F435631AFF66F67467ABA3F65A3F1174BC44CE192F62CE2126CD21268DDFBE55DC67CB34DC74EC55062E98FE64CCDE4F4F1AF246F9486DCBA65F1D26A5A79B4D14564FFC314E2DDAD5CAA19F95C3199D50256B68422AA6B86C7C30FFC167E5F264FD75E8E5B98F3714CA0C6749FC10A66BD915494E73716AC9B2EF49BAFA18644A881C3EC561E3902C8B94212A0FD61BA9C78B490E52B2471EA03706848456FC341A85291CF6EFBF2717C192F6AF0F31BBAD2B1F5D28A92E07FFCB6F49917F88CB308C7FE44BF9FC5F496EC11B90594E73F1F15D922CBBA01025AB3360EDA026BB2D63D59D87E6EB648C3BBFDE3F8B8270ED69FBA1E4D571FF41C3A31F6BEF29363E13B98C09200E18CD67475E7F06510131DB7E9F248E2E93C730F684A39257471C6978F48B84B250FAED39543CE2A524A7F3E132CF7F1069D926244C06135F08F946E2557B206818209A5F9BB3A7F534F8FA599BB7CFB0D103FA6ED2D32C4B966139E7D6F67536E1FC5AAD0E30070635A9EB9633C5CB4A31F532DBAFD5E38B0054906305C7106A31A6AF9D14AD04BC0BD247D051DDC304BFBB2133D68F4E35572183C2D122FB5C44D1BBE387209283545854F8F60484141E75D22AF0EB76B6825C76EEC89D271C40EBC2BC3B425066EA01851A39DD9088995675478FBDF6DE01B49DA6600154933BCF34100D53F19E3E80B672BA01685833340290AA535B2C8EB6D41E464190F3F4415489B9EFC3A10F1CD69334768F39086392CA24BB59E0F6CBEE77567FD806C8FE94AC489435F96E974F641D948AC936C172BB6D7D11A6597E1EE4C17D90918AE4F8A85E5BD039F14B9693F52B46F0EAF63FA3B3282C0F216A824F411C3E902CBF4BE8D4FBDDF12FAFDFFC727C741A8541C6B6D8A287E3A31FEB28CE7E5B96BBA7411C277959F577C74F79BEF9EDE4242B4BCC5EADC3659A64C943FE6A99AC4F8255724279FD7AF2E6CD0959AD4FE4EC5BB6282EAFFFA5E69265AB88071FB728A9E7F4EC52CD2945C1F60A90D87674A525C3A286CB0D79E01615729BCB19DF020B1126C5BBE39029B73413F55329ABEB20670FB836D6E2F88801906D7DED40786264CF9FCE73C5C84AFD6D11AFC88F77C7FF5566FBED68F1BFBE3639FF7254DEE7FEEDE8F5D1FF7616A03ADCA88A6687C47919B0D69149B5955231899F8374F914A4FF631DFCF89FCE9C6A0F818AD7439404B9338F9D7B800F81380F01B7F6D965ECD43CBC4F41C746E23C0ADC6AB2CBA8AB499EAA7B5F70E9B68AA89CF885B3D13ED4FD614F0C43756C2922D8D45A8BEC8F38FCCF82B6D81DD5216B9F4FC18F4B123FE64FEF8EDFFCD5B9FC6A5F5C2E9FE3F9CB5F5F3B33BD08CAFB91BDF0FE94F4C79BB66BB935E899ED754AD815E6DD6BEBBED91376C3AAC702EA37CB0D6CFFA905DBED99275E588CFDF9BC384FD2F7619A3F9DD17FC2073617F40F94F3347CA6D3BDCB70494A2713BF756067AD9B24CDEB934CBFDC996DBE7A2855D479ACA978FD7B12C6E545C68EDCAA25D4D5C3C7306D31511273779B2C8937C9DCE410327712A31EEAEA50BFED268E55EE4E82343BEA6E22D4F93A152E44BBE8325FEC6F8A87E976BDCFF08699A7A99C1659B540BD4A6F0BFAFD74B50EE39AE77DE8DE4CF5B5B2F61CE8CFFB6A75DF9915ADDC86B0B819BB696717B982ED40ED43ED1FD6247DA4A3CF8BFBE88C617F43AA88843D17F33E4A9215EDE1C5C6BA8CC3B0DBBAA9D637A87DB0FC48C2C7A7DC0BAB2FFE587DF891A7C15991A6210B3995969E6C611E12FB6AB8C3D2AB32E77BB2F0EA6501A19981B7DB92B8DA1036ADAA9E16EA38647C8857BE582D3216F7880F76D6DE22969C923863FB98A5B9E1255446794CC750389607F91D199E935551068BA44B2D3AE3AF5EA7ECC073915D3DD39FB4AE7ED4B8E3E6559535D7EBE085793A78AABBEB988CB64EE7BA7BFFB33551D0DE50ABD1A2175B774E36415A5E7D6BB3466AF2765A1CB0C328A87653DB049EC40A61EA7BC0831A8B1A80075B31A8AD28E7FA5D0CC6968187D396433FFF99FA79B5C83C74F6FE3BFBA177FD2CBDEB035D16EDD3847B1167345F9183534A0FC780C9631AACBDF37D9F04E9CA852B6EFF2F2B2265AFAAFBC9F0EE7DC0AEAE2889173603FBE5387B39941E37FBD1B97AE953FD99B61FB49890284F39CC56FDFDDAB61DF7BE7C10BE90FB2C743BC9471DB5B38BBDBE0F35F89DA883011DC7802ED6C1A31CD36BBE7D77CDEDBA5224DD877190BEB4DBE39A6A83F1EE1B7BD26CF31AF1AAD3BD3BFA6FF62047FB986D138CEE77CDEC755BF7193E6F2721EE92B6223439BB09B045558B21078DDFB364BD09E27DB93030FDE3E95EDC37E734CDEBC723E63462C8095864A2BE3C7B1E59E8BBB2C77F4EFCEF1B34DCFDCC53179FFBF183A50369CADEDFE8D32635DBCF8771D5FFB8DAC94D5761D07184ED248C947DF263ED39790E9744F7DCE5EC510D5FCAD15EB441EDE66F6FBC72D2BB1EAB3C91E5374F1E5C6DDC8EF0E008B365B889CA356AF570EF9EC163BC4B9C8A6699182D4ED3357CC6BF618ABDD3D9C18B15AEFC9E40D4C3B2C1F98CA27C05B20CD507BE5C355B5D8EDEDDE10764DD44817874BB9414A471D9E2E7C1CBAE9FB6BB90F325CC9F92823D2BD98A211AA8AA0EF604A1533B0CF892A4DFCA1555C79B5AB58ABC08C55ADFCF3D27C6A98FA3C6ADB5B0C5A59F2D4C4737A4DEAE100A71FABB30E242F47761A386E5EFC24D8AC6DF85951484BF0BAB0F62ECFD1EC607F17DE8FDE87314A96475A59C3FB7DBEA2D15734DD2CC72FF637AEB844EB00823B28DE3B427A018DD10D32A3CF981E45CE3F5B8A08FB6F6DE6EB175DB0BF3624316B197DDB4AB229FC3AE9C8CA7F2D5C61698D2BF3366C796A6F187D80469E51EEFCDCF7E20BCA26E75DAE1EAEE53DEC73EDDC7240A57C1BE6C26CD66D6D3ECE6ED89E6479FF4941A6DB751CF65ED24C2E9731096F48E5B336EA839ECA3DB6C40C75D8345C6DE4866197A9820944D78D8C1DFB7AEEFF13EDCD4B69BB9FED4EA403D2832FF9D7C3F2F0D769AFDB63FD4854CE02D210EF1C5F0E62F090EFBEDBECC1ED5A58F08CF1E6D1713E98604997A25A7DDFE13E3B73B4DEC608490DDCCC1F4F8E8FCE31AB1D9D88B7AC3667F8EEA58AD5ACC55CA5C5E629DCEDE66B5330C6D61B75FF3F5493ABC481A6FD73B04063F63474103FC13898B454ED66D20DD664FBAE7BDE8EB206551CE6885B4271E5E02B5FCDA224E4B12E7691245F5E3B83EA6209543A5477E517451C425533FF30176ECC1FE9ADECA8601BFEC965DA6938B657C1605882B5E1D36E63F51DC3C452F3FCBD1E4CCDEE8F1BFA098FF5E46CF47ABDB0E51CD3A6E9F88FCBEDAA147EC9507F4F4BC094FEFB332AABC1736D7414CD8C36BDD564165CFF5C0A47E06AE13A33FE2E4E1216461FC3BCBD4B0F22259B9CBEF85D355F92C921756A23FA407A93C303A5DFD9D7675E602EA6B91EE83CF67C26E2FF4736981A40F097B2F6249165956ECCB24CBC3C16A1B15DE04ECAAF79EE8705B99F1BD70C79F1F489DA4C5EC40E1D04770232AF63AD9BFFDBAD1DBFF100540DB336AC4793BF9F73151BD4BDCB9A03BD9CDDE3D0051D5C893EBA0BFB3CFDBE2F1914D1111FC06F63438047FEE758B72747BDFDB692E7B47844E05D66196B5341F133C7B60956A8AC0BC2EBC6BAF2AABD8567CFC935FFED6667BBB3A06B9A473203FBD3D88CB5B0C6838802C984B654716E724224D4FF508CBBAE959730C0F4A38F48DD8F47D5EC87056130BB2D35655756739B193D6B17C5C35ABEB8F1DB52B8616F2C696C7DED7966644E2E1D39E9C6E365158BD3EC014D05642954D7B21F1B16ECB4970B3A5B527D3D5D1A7075E267E3EF6057D4EBAE67AD3755FA6D48EBD7A0FE3771CA25E60B749E614F5E2B6B8CF9669B8D923A08E795F17AD7769C631E369FD5969B9BB47B65C64E5FBD50E0B28ECDCBAE52E0710D8BA3DA7B3247E08D375B7177EAF832CFB9EA4AB8F41660FA081DA7423CB22A5C0BCCD83B59FB7D7CB18DB70A8E6AEFCBCA8F0EE7B72112CA9C5F910B35CDDDEAD4E96DF9222FF109791C5FEC8975D77E9760C3B8B76BA5C922CBBA0C023AB337E26DBA667B2BE3DE4E2FD2C0AC27DF1FC8597E5ED96004C2D8C8F3F6E7F0651E187DD9C17E23CF62E93C7B0D544A8ED76504FFB36653DE8B7E770E5CB126F99D10AFDF488F942C83712AFF664C2DCF659182F8FC270BE781DE7D8A759962CC35258692B6A2BA9D45C74DC3E6213C65A01B518B7247A78557FFA5444398B13BDA40552041DCB0D7CB5DDF63FAAEE3CB083806C19ACD46A53A157BAF26B3905099A8FA20CFFA0B0A6E022EC7E4918B069267B7783B6A08AC4305E869B2012EB2C912121CBEAB36328A79C930DED1B541EB97698B21A2CAA25EE184B9AB5D5FFED09070D2462AA0D2F58F4B62DF7FAD5AB374AE3A9BC188F2191E0D43E9EB050D611531EB7EF390A20385F9C3130013C2259B213BECF1E15E73A9F9DC982A27ED0671C4CD4A54B90683EEF012276959917207E4F9362332E2A4A1134D0D8A6ED113EAA1ACD1124D551D40124930209773A39F6D06214DF4FA3F6B49E196B846AD3E0C30E549284A3AD7338988D6A860E3398C9D89D669DA313BD4B0BF66669C65A1F39B6EFC02BA446BA114D4C83A8516CCC61E53C3D33B3DB5FD30ADEAD09F76883D6B56987DCA5952EB88D03A555511DFB7CF56B57FA82502DAE2844F375F6B6A8A90BA630FD0B12439BA2FA8FF268DD0621FEFC5D805195B0B7D648AFA49EC0E4025C5EB8F1F0F4834A1592784966629076F28A52709FE76F929ACACCC2262DD6C123C9E6819F4A564182FAD3EC71B3ADC82C30B31BC76E93225D92AB878F61AA478E40C4B79D98B0B7E3985E49FD20C90DB59C70A3E1A90CD3741AAFB641A47A39CBEA095092E8A2438F9C367B2325D70853E4D8676032B80EB0DA0B588D3E08CAB81A61C3F180125B91636F3E56BE8577F4DF8CF29C89ED1164065C549BA4F9634AACD03C0C8F882916226BE2CEC663236A38C7E316786ADA6F2A88BA4B0E789A2F9EEAD69B80A3C7BCC63D556E8DA3C11E8D7F40A5663106021893226ACEE088772A801BF8D8B725E68090AB53819E103FF500BC3D039E125C771CD885D9923540CC02782D2BA739E593F13C59432EB4BC866418542A855B64EB0B9508B5F60450B58A98526181A784D479CCFF2682BF41E77FED1037FAFCAF16A07CC6A48C36B20D703F0FA869C5078502A8660F3C7DDD668E3FF5D5272D120152BEF9A1E49F189D36C54E11A7F03362A322560D16392F930904BB84A482C8F6C668DA027E6A4A1FDD6A5E841161916482C77EA25DF4043D4E6C410CE1FBECC1C5D70653DCD87E1D3C980E309A2D8C46374ACDC83E0F1835F20A52F09F670F22AE3273C350F9A7711FAEA150DA6FC0DDB6B160A4D1CF2470B4936D5C20CD6A2B83975845D2FE6C5808D5998F51AAB1342BB334129C86374D2E789A98711AC1DDF5606A34E58DEDE5CA5EB59EC968452515A1537E983F645835E6312A31AC8C613C7EF6969F8291380BA26511CD28848224B40220216D2FB0C4D768360685C795DA215CADC0DE62096D2B46C011CB33150CCD6B010ECA6E42D71ECD94E18ACDD16EED16EAA6BEE3C79AFC24406C6385C6C3A394756AB0DC03B796A9E1731C87960E109D9037CBA724CE9FA297D33CA72505B30980A4882D0803A4CE7E8456EB348BC159C5D7085B1607B4E00A1D7B67632B75651A6F9F08998927812A37043021795F10C6576A4E0689C7D81E4CC9A682C071A6622DB138A169D835491F9274CD2CF50DFD1E3FCEC3F029628BBD414D9DBDD953EB340BABA7E28BFBB2C8B2428F338550D3C4DBC49F087766154E097FB2A4E3E1304DD6C9EC768665A945B42989F337727295E661E3646CCD2F90C5148036702C8156589B4A080B0572730B6071001C0E70D3085D71C33DCB338B81F34603F49BBD7AC8E7C611C0A38F943C8E46D88E3DA0628A9BAF0BC69E2A9B49FFF58F8CA499160C3CA9F8888090E0048A3A2B2B59CBB749EC051E82F89836837B30FEA902B95AB832B9761ACF84D0D2E9AA721D6619B322AAF43DC3452C5F342352D25E4045AA14A6449665347C54FBAAA7ABBF17597F8FADF7346B916517DFDA5012673F522955C29439F6AD7505600768ED0BB4469F1F2BD81A61927C40CA0CE6CCA71BA6F2F20B9BC37D3D8B8270AD9F374BE442932A69AD67CFA510DAE9F336B517BCC87518780A5D550D53A8DC6EA3591A5990CBE4318CC7065029841640DBD47D045055B5590388556A6CFC8CB57C1F193DE8A5E064C0F385906FB48CAF137FA5622BA620C0EEDBCCDFA5A8EB81296AD0E7283E94B8A679729A83A4F5AA2F59918B30CDF2F3200FEE834C9D0BB35CB724AF45961FCFAA084CCF6BDD2E9FC83A7877BCBA4F688B07F74C4BF97DF45521546C91587433EF56CA6C92748535149652EA8EA0D66B9BA0ADCE36DDC25F3812540A115275250944D6E2EAC8EE60694DA2BEB086065DD6EF69526C8C056E29ECA56E096DD8689E6E57C1D1A469D1D190204158057ED102B14AB681B1A2B295C8BD01AD96C7256A4BE3682C65D56F062BE5D409BA32EA740B7FF15D5BA514315957964885EACECD6B0D9A6EDD1098BB77436729F72C596F8218B28FBB145D493B02747733540F22B2773A7435CFC973B824FC4D0B400299445FBE4C692B1D083CAF960F10692500689D65D098061D215E169CB9D0079FD65A2B80D666BA802C16C120577A45228848270A448B540E140359AB1D88D8A61E288F45B60B92E5E1731095D322401C295D278144662B948FB0AA16C9A76A0BE489ECC5E524359A0B95C450B044E958FA9F21F98E90A02283A480292D527C4CA2701540DD7197A2ABF18EC052041FAA5229854FD415C4D360CAD2983C2ECD5812CAB089A1CCE0B2ACE64BA4B295584640514B2A3F6B4B2853119C85300460210285A93C81D0AD68834235844841B04AFE44E262919335204193049559A7DA8D1C701F542D4BA5D1551420C50920DCD6D2492010594410686DC3AF72B1431D7C1512EDD0AB50E24BAF6F70988AAF6910E5D7A4360114D757B57C85445BBC426929FDC6B8E8BFC12CFA6F1C16FDB2C38C5AA24400152AD2D87B99E8FBA3AE208564A8409E025F5CB305AD2DB22131155B532156B1CA61ADBA925548B4AB59851255BA65EEEA32676D3157BD2DEEB3651A6E74A50BC9DA72052A4B89CA618652A84201952B11B9C16C7BD26AC4D996C606B4EDB9B24BE9DB633A63E95B1A5BE9DB43495BE9BB2D79A5CC5D8AAE6D77045211DCDEB4BA2ADB6DA17254D25A0C3CD900CF367695693E299BEC90A307974FBB677C2256C3A58A92D79CA9AA2607BBF6C2833959C17D559DDBAD46D4DE40DD870280DDF832B3698BBD8D0AEA8D35940674C4FD28403E1FD8D65FBBEBDFA5FAE566BE930EC01CFD2A4238BC90B4019F46745789E48F865089C9836DE62A514F7F8CBD44A6EEA54223761A27700C8B8B3EABDF8C033C63F388C1517A147F9411437BA1CBA28241DA7F0815ECE641025BC37CC970EDB49BF0C3CE14EBA3E8AFA61607A87A991FCAC7EA5556ED6979875616CFC40DCD2C10DA2BCD6F4D0B1587369D876DE9DDB9BCB9A901B25EDA5AF133A8F2EABD07DCAB5CB90918AB2B93F45155D1CDA1CCA77160E80068D14DC1006881502F34E43B518A6EF2871814D0924317629E6FC9D147DB6B1CD8AA8D028B475A7795B828638FD5609FD25872EC814A047F22333034A4BD2801F497E2B6D1B4AE419D55C062C0E87700F5C4BEF7024754C15D8256C08E74CED5573DCE8C1DC144DEEF42175407C25FCE8B4AE4B05738CD188365795D124D444D62A8269C920CE19DE6AF22C58150EB5308E80A9DD95069A36B6455719493A385AB85633FAA34DA2903792F766A0495681D41CD8B597BAE3ECFB9B44EB1022BBB6FAB4F75416EAC2E8AB34768E6C3C3699D704B0DD85D6A27AA56D58701054353B65ECF5BB53E1C022F846786BBCA387F5DC40ADD40DD8782007FE432B3C9C9B89B0AB095DF836A371DD0586B88AC8F4AAB5ECA655E83EF71A72A739EC6C63A6BDFBA96455726335A5FE671AA8D198F61C2DE5A1B1A1E8C2ED91DAB6E6F73CB2BF07E5B7DE8EADBB7D9D00F97CF0D05C2BBDB50C5F50F73FBAA2A777BA0AA227433A065D5102D6B7B4D7A06D5041F45D6D4D5FE80B2C70A0377377675375DC6E8AE864ABB7615400FDABAB6DA74AA8D1ACA5C5EAAED070BA061C35D8BF1A622A52DD09AB2BC0AEA070B53571F6E35DEF229D01E97E263AAD2F0E424A03BEC03959EBAA8F6F257C9C27E97CB873AECA3B5DBAB8A73570DF81EA05E2BF6E703FD2A04B8A7C76BC474EBCE8B4A7036A8C5BB773DDA9F31546778630DD017F645364F58D2DE78ACB46BBDC0E8431DEA354D8C5A6CAF60C195142E95CA75042F898EAD2EEDCB55909670CF5CF9C28EE6B26AA50BDBE5530FAA409C38BB3DCEE4F5307574F5584F9A5D9E119AB56AC0076C0085D81FBAF1D475809BD66566D3F5E96ED5B64FEEB0AFB3CC4A05C0EB229037AFE50D124170E8067929B9986072ECD55C0A17B83489DD7190A88F66400000C8FA5483589E080229A9B30A6CEF4200EA707A4AC253A7D0DDD92F3958EFE07B508B9342F65B15768BE914AF7FA66AD1849A07B481094A2F5445132CA1AC89928634A742F003C59E6E53BD2BA58E5460570A1468BD77A508311914A56C53BD2B85C981D2494538AC4A861876A530D780264C81B005D1DBF9484B9131CA6CBA9017FAEAB118D92CFB2E26F32EEDED49155863FB81FEAC9C403E252B1265E5D7B7273705CDBD26D5AF6A81B063F196F28C49E9EBD730AD6916F1435207A39624AA49EAE45D90AE3CA00638384DF3F02158E6347949E80C822DC4FF0CA2A234BCF764B588AF8A7C53E4B4CA647D1F092B0C16D2DA54FEDB1345E6B757657098CC4715A898211B43AEE2F74518AD76725F0451266152C782C5CAFE9DD0EF555BE6F4FFE4F165C7E9731223196DD5B70BF17D47E80045996557F16DF04CDAC8467BD725790C962FF4FB73B8621D56C7C4DE10A2DADF9E87C1631AACB32D8F263FFD4931BC5AFFF8D7FF0F0EEE7C9A930E0400, N'6.1.3-40302')
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
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (69, 67, N'Attendance By Department', N'MonthlyAttendances', N'EmployeeAttendenceReport', N'EmployeeAttendenceReport', N'EmployeeAttendenceReport', N'#', 4, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (70, 67, N'Attendance By Date Range', N'MonthlyAttendances', N'EmployeewiseAttendenceReport', N'EmployeewiseAttendenceReport', N'EmployeewiseAttendenceReport', N'#', 3, NULL)
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
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (82, NULL, N'', N'#', N'#', N'#', N'#', N'#', 17, N'fa fa-adjust')
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (83, 82, N'Add Salary Adjustment', N'SalaryAdjustments', N'Create', N'Create', N'Create', N'#', 1, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (84, 82, N'Salary Adjustment List', N'SalaryAdjustments', N'Index', N'Index', N'Index', N'#', 2, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (85, NULL, N'Loan', N'#', N'#', N'#', N'#', N'#', 16, N'fa fa-adjust')
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (86, 85, N'Loan Application', N'Loans', N'Create', N'Create', N'Create', N'#', 1, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (87, 85, N'Application List', N'Loans', N'Index', N'Index', N'Index', N'#', 2, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (88, 85, N'Loan Approval', N'Loans', N'LoanApproval', N'LoanApproval', N'LoanApproval', N'#', 3, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (89, NULL, N'Subscription', N'#', N'#', N'#', N'#', N'#', 20, N'fa fa-calendar')
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (90, 89, N'Subscription', N'Subscriptions', N'Edit', N'Edit', N'Edit', N'#', 1, NULL)
INSERT [dbo].[MenuItems] ([Id], [ParentItemId], [Name], [ControllerName], [ActionName], [AllFunctions], [ViewNames], [Remarks], [MenuOrder], [IcnClass]) VALUES (91, 67, N'Filter Attendance', N'MonthlyAttendances', N'EmployeewiseFilterAttendenceReport', N'EmployeewiseFilterAttendenceReport', N'EmployeewiseFilterAttendenceReport', N'#', 2, NULL)
SET IDENTITY_INSERT [dbo].[MenuItems] OFF
SET IDENTITY_INSERT [dbo].[RolePermissions] ON 

INSERT [dbo].[RolePermissions] ([Id], [RoleId], [MenuItemIdList], [CanView], [CanEdit], [CanDelete]) VALUES (1, N'04e3d876-31ca-4e05-b6dd-193ea918dec0', N'1,2,3,4,5,6,7,8,9,10,56,57,58,59,60,61,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,45,32,33,34,35,36,37,43,44,46,47,48,55,38,39,40,41,42,49,50,51,52,53,54,62,63,64,65,66,67,68,69,70,91,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90', 1, 1, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [MenuItemIdList], [CanView], [CanEdit], [CanDelete]) VALUES (2, N'994d2a55-1e9f-4e40-bb40-149b9e4a77c6', N'8,9,10,56,57,58,59,60,61,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,45,32,33,34,35,36,37,43,44,48,55,38,39,40,41,42,49,50,51,52,53,54,62,63,64,65,66,67,68,69,70,91,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,88', 1, 1, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [MenuItemIdList], [CanView], [CanEdit], [CanDelete]) VALUES (3, N'b4f77c9b-2aa7-40e5-b563-b583a8828580', N'29,30,31,37,46,47,85,86,87', 1, 1, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [MenuItemIdList], [CanView], [CanEdit], [CanDelete]) VALUES (1003, N'23826810-48ff-476b-b2d1-1ea893153048', N'8,9,10,56,57,58,59,60,61,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,45,32,33,34,35,36,37,43,44,46,47,48,55,38,39,40,41,42,49,50,51,52,53,54,62,63,64,65,66,67,68,69,70,91,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88', 1, 1, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [MenuItemIdList], [CanView], [CanEdit], [CanDelete]) VALUES (1004, N'48b64668-cea9-4606-89b3-0e2853df7014', N'29,30,31,37,46,47,55,85,86,87', 1, 1, 1)
SET IDENTITY_INSERT [dbo].[RolePermissions] OFF
SET IDENTITY_INSERT [dbo].[tbl_Branch] ON 

INSERT [dbo].[tbl_Branch] ([Sl], [Name], [Address], [OpeningTime], [EndingTime], [IsLateCalculated], [LateConsiderationTime], [LateConsiderationDay], [LateDeductionPercentage], [IsOvertimeCalculated], [OvertimeConsiderationTime], [OvertimePaymentPercentage], [Status]) VALUES (1, N'Super Admin', N'Dhaka', CAST(N'2017-01-01 00:00:00.000' AS DateTime), CAST(N'2017-01-01 00:00:00.000' AS DateTime), 0, 0, 0, 0, 0, NULL, NULL, 0)
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
INSERT [dbo].[tbl_SourceOfHire] ([Sl], [Name], [Status]) VALUES (2, N'Bd Jobs', 0)
INSERT [dbo].[tbl_SourceOfHire] ([Sl], [Name], [Status]) VALUES (3, N'Bd Jobs', 1)
SET IDENTITY_INSERT [dbo].[tbl_SourceOfHire] OFF
SET IDENTITY_INSERT [dbo].[tbl_Subscription] ON 

INSERT [dbo].[tbl_Subscription] ([Sl], [Code], [Date]) VALUES (2, N'OEQC5Y1xKabPpKS25RoEJ8k4TgeHOZsP', CAST(N'2018-01-31 00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[tbl_Subscription] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ApplicationUser_Id]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_ApplicationUser_Id] ON [dbo].[IdentityUserClaims]
(
	[ApplicationUser_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ApplicationUser_Id]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_ApplicationUser_Id] ON [dbo].[IdentityUserLogins]
(
	[ApplicationUser_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ApplicationUser_Id]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_ApplicationUser_Id] ON [dbo].[IdentityUserRoles]
(
	[ApplicationUser_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_IdentityRole_Id]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_IdentityRole_Id] ON [dbo].[IdentityUserRoles]
(
	[IdentityRole_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[RolePermissions]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CreatedBy]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_CreatedBy] ON [dbo].[tbl_BonusAndPenalty]
(
	[CreatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_BonusAndPenalty]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UpdatedBy]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_UpdatedBy] ON [dbo].[tbl_BonusAndPenalty]
(
	[UpdatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_BranchTransfer]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FromBranchId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_FromBranchId] ON [dbo].[tbl_BranchTransfer]
(
	[FromBranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ToBranchId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_ToBranchId] ON [dbo].[tbl_BranchTransfer]
(
	[ToBranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CreatedBy]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_CreatedBy] ON [dbo].[tbl_Department]
(
	[CreatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DepartmentGroupId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_DepartmentGroupId] ON [dbo].[tbl_Department]
(
	[DepartmentGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UpdatedBy]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_UpdatedBy] ON [dbo].[tbl_Department]
(
	[UpdatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CreatedBy]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_CreatedBy] ON [dbo].[tbl_DepartmentGroup]
(
	[CreatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UpdatedBy]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_UpdatedBy] ON [dbo].[tbl_DepartmentGroup]
(
	[UpdatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_DepartmentTransfer]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FromDesignationId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_FromDesignationId] ON [dbo].[tbl_DepartmentTransfer]
(
	[FromDesignationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ToDesignationId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_ToDesignationId] ON [dbo].[tbl_DepartmentTransfer]
(
	[ToDesignationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CreatedBy]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_CreatedBy] ON [dbo].[tbl_Designation]
(
	[CreatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DepartmentId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_DepartmentId] ON [dbo].[tbl_Designation]
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UpdatedBy]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_UpdatedBy] ON [dbo].[tbl_Designation]
(
	[UpdatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DisciplinaryActionTypeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_DisciplinaryActionTypeId] ON [dbo].[tbl_DisciplinaryAction]
(
	[DisciplinaryActionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_DisciplinaryAction]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_Education]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BranchId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_BranchId] ON [dbo].[tbl_Employee]
(
	[BranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Code]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Code] ON [dbo].[tbl_Employee]
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CreatedBy]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_CreatedBy] ON [dbo].[tbl_Employee]
(
	[CreatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DesignationId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_DesignationId] ON [dbo].[tbl_Employee]
(
	[DesignationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeTypeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeTypeId] ON [dbo].[tbl_Employee]
(
	[EmployeeTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SourceOfHireId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_SourceOfHireId] ON [dbo].[tbl_Employee]
(
	[SourceOfHireId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_EmployeeLeaveCountHistory]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PaidSalaryDurationId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_PaidSalaryDurationId] ON [dbo].[tbl_EmployeeLeaveCountHistory]
(
	[PaidSalaryDurationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_EmployeeSalaryDistribution]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_Experience]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CreatedBy]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_CreatedBy] ON [dbo].[tbl_FileStorage]
(
	[CreatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_FileStorage]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_Images]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_LeaveCount]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LeaveTypeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_LeaveTypeId] ON [dbo].[tbl_LeaveCount]
(
	[LeaveTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_LeaveHistory]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LeaveTypeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_LeaveTypeId] ON [dbo].[tbl_LeaveHistory]
(
	[LeaveTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UpdatedBy]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_UpdatedBy] ON [dbo].[tbl_LeaveHistory]
(
	[UpdatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_Loan]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UpdatedBy]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_UpdatedBy] ON [dbo].[tbl_Loan]
(
	[UpdatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_LoanCalculation]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LoanId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_LoanId] ON [dbo].[tbl_LoanCalculation]
(
	[LoanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_LoanCalculationHistory]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LoanCalculationId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_LoanCalculationId] ON [dbo].[tbl_LoanCalculationHistory]
(
	[LoanCalculationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PaidSalaryDurationId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_PaidSalaryDurationId] ON [dbo].[tbl_LoanCalculationHistory]
(
	[PaidSalaryDurationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_MonthlyAttendance]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UpdatedBy]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_UpdatedBy] ON [dbo].[tbl_MonthlyAttendance]
(
	[UpdatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_MonthlySalarySheet]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PaidSalaryDurationId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_PaidSalaryDurationId] ON [dbo].[tbl_MonthlySalarySheet]
(
	[PaidSalaryDurationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_PerformanceRating]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PerformanceIssueId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_PerformanceIssueId] ON [dbo].[tbl_PerformanceRating]
(
	[PerformanceIssueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_PromotionHistory]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FromDesignationId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_FromDesignationId] ON [dbo].[tbl_PromotionHistory]
(
	[FromDesignationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ToDesignationId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_ToDesignationId] ON [dbo].[tbl_PromotionHistory]
(
	[ToDesignationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_Resignation]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UpdatedBy]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_UpdatedBy] ON [dbo].[tbl_Resignation]
(
	[UpdatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CreatedBy]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_CreatedBy] ON [dbo].[tbl_SalaryAdjustment]
(
	[CreatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeId]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeId] ON [dbo].[tbl_SalaryAdjustment]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UpdatedBy]    Script Date: 1/28/2018 2:50:56 PM ******/
CREATE NONCLUSTERED INDEX [IX_UpdatedBy] ON [dbo].[tbl_SalaryAdjustment]
(
	[UpdatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BranchId]    Script Date: 1/28/2018 2:50:56 PM ******/
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
