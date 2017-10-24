using System;
using System.Web;

namespace FTL_HRMS.Utility
{
    public class Utility
    {
        public static DateTime GetDefaultDate()
        {
            return new DateTime(1999, 1, 1);
        }

        public static byte[] GetBytesFromImagePath(string path)
        {
            if (HttpContext.Current != null)
                return System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(path));
            return null;
        }

        public enum DateTimeFormat
        {
            
            DisplayDateFormat,
            DisplayTimeFormat
                
        }
        public enum NotificationType
        {
            Leave,
            Resign,
            Loan
        }
        public enum NotificationStatus
        {
            Pending,
            Approve,
            Cancel,
            Recommendation,
            Consider
        }

        public static string GetDisplayDateFormat()
        {
            return "";
        }
        public static string GetDisplayTimeFormat()
        {
            return "{0:hh:mm tt}";
        }
    }
}