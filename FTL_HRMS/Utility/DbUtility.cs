using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using FTL_HRMS.DAL;
using FTL_HRMS.Models;

namespace FTL_HRMS.Utility
{
    public class DbUtility
    {
        public enum Status
        {
            Success,
            UpdateFailed,
            Fail,
            Error,
            Null
        }

        public static string GetStatusMessage(Status status)
        {
            switch (status)
            {
                case Status.Success:
                    return "Success";
                case Status.UpdateFailed:
                    return "Update Failed";
                case Status.Fail:
                    return "Failed";
                case Status.Error:
                    return "Error";
                case Status.Null:
                    return "Not Found";
                default:
                    return "Cannot get the error";
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
