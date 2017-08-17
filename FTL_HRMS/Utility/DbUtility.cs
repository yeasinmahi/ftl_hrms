using System;
using System.Linq;
using System.Reflection;

namespace FTL_HRMS.Utility
{
    class DbUtility
    {
        public enum Status
        {
            Success,
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
                case Status.Fail:
                    return "Failed";
                case Status.Error:
                    return "Error";
                case Status.Null:
                    return "Not Found";
                default:
                    return "cannot get the error";
            }
        }

        public static PropertyInfo GetPropertyInfo(object o, string property)
        {
            Type myObjOriginalType = o.GetType();
            PropertyInfo[] myProps = myObjOriginalType.GetProperties();
            return myProps.FirstOrDefault(propertyInfo => propertyInfo.Name == property);
        }

        public static bool SetValue(object o ,PropertyInfo propertyInfo, string value)
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
    }
}
