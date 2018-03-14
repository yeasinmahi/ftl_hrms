﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using FTL_HRMS.DAL;
using Microsoft.AspNet.Identity;

namespace FTL_HRMS.Utility
{
    public class DbUtility
    {
        public enum Status
        {
            AddSuccess,
            UpdateSuccess,
            DeleteSuccess,
            AddFailed,
            UpdateFailed,
            DeleteFailed,
            Exist,
            NameExist,
            CodeExist,
            UnknownError,
            BlankError,
            Error,
            NotFound,
            NotAllowed,
            DateRangeExceed,
            PasswordMismatch,
            CodeFormat,
            RoleAddFailed,
            SyncSuccess,
            SyncFailed
        }

        public static string GetStatusMessage(Status status)
        {
            switch (status)
            {
                case Status.AddSuccess:
                    return "1Successfully Added";
                case Status.UpdateSuccess:
                    return "1Successfully Updated";
                case Status.DeleteSuccess:
                    return "1Successfully Deleted";
                case Status.AddFailed:
                    return "0Adding Failed";
                case Status.UpdateFailed:
                    return "0Update Failed";
                case Status.DeleteFailed:
                    return "0Delete Failed";
                case Status.Exist:
                    return "0Already Exist";
                case Status.NameExist:
                    return "0Name Already Exist";
                case Status.CodeExist:
                    return "0Code Already Exist";
                case Status.BlankError:
                    return "0Can not be blank";
                case Status.UnknownError:
                    return "0Unknown Error";
                case Status.Error:
                    return "0Error";
                case Status.NotFound:
                    return "0Not Found";
                case Status.NotAllowed:
                    return "0Not allowed to Delete this item";
                case Status.DateRangeExceed:
                    return "0Date Range Exceed";
                case Status.PasswordMismatch:
                    return "0Password doesn't match";
                case Status.CodeFormat:
                    return "0Enter only alphabets and digits as Code";
                case Status.RoleAddFailed:
                    return "0Role Add Failed";
                case Status.SyncSuccess:
                    return "1Sync Successfull";
                case Status.SyncFailed:
                    return "0Sync Failed";
                default:
                    return "0Cannot get the error";
            }
        }
        public enum ConnectionStringProperty
        {
            DataSource,
            DatabaseName,
            User,
            Password
        }

        public static string GetConectionStringProperty(string connectionString, ConnectionStringProperty connectionStringProperty)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            switch (connectionStringProperty)
            {
                case ConnectionStringProperty.DatabaseName:
                    return builder.InitialCatalog;
                case ConnectionStringProperty.DataSource:
                    return builder.DataSource;
                case ConnectionStringProperty.User:
                    return builder.UserID;
                case ConnectionStringProperty.Password:
                    return builder.Password;
                default:
                    return "something wrong";
            }
        }
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["HRMSDbContext"].ConnectionString;
        }
        public static PropertyInfo GetPropertyInfo(object o, string property)
        {
            Type myObjOriginalType = o.GetType();
            PropertyInfo[] myProps = myObjOriginalType.GetProperties();
            return myProps.FirstOrDefault(propertyInfo => propertyInfo.Name == property);
        }

        public static bool SetValue(object o, PropertyInfo propertyInfo, string value)
        {

            string proType = propertyInfo.PropertyType.Name;
            try
            {
                if (proType.Equals(typeof(Double).Name))
                {
                    propertyInfo.SetValue(o, Double.Parse(value), null);
                    return true;
                }
                if (proType.Equals(typeof(Int32).Name))
                {
                    propertyInfo.SetValue(o, Int32.Parse(value), null);
                    return true;
                }
                propertyInfo.SetValue(o, value, null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

        public static int GetUserId(HRMSDbContext db, string userName)
        {
            return db.Users.Where(i => i.UserName == userName).Select(s => s.CustomUserId).FirstOrDefault();
        }
        public static string GetRoll(HRMSDbContext db, string userId)
        {
            UserManager<Models.ApplicationUser> userManager = new UserManager<Models.ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<Models.ApplicationUser>(db));
            return userManager.GetRoles(userId).FirstOrDefault();

        }
        public static void ExecuteSeedOperation(HRMSDbContext context, List<string> scripts)
        {
            foreach (string script in scripts)
            {
                context.Database.ExecuteSqlCommand(script);
            }

        }

        public static string GetViewCheckQuery(string viewName)
        {
            string view = @"IF OBJECT_ID('dbo." + viewName + "', 'V') IS NOT NULL DROP VIEW dbo." + viewName;
            return view;
        }
    }
}
