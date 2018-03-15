using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public static DateTime GetCurrentDateTime()
        {
            DateTime timeUtc = DateTime.UtcNow;
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
        }
        public static List<string> GetErrorListFromModelState
                                              (ModelStateDictionary modelState)
        {
            var query = from state in modelState.Values
                        from error in state.Errors
                        select error.ErrorMessage;

            var errorList = query.ToList();
            return errorList;
        }
    }
}