using FTL_HRMS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using FTL_HRMS.Models.ViewModels;

namespace FTL_HRMS.DAL
{
    public class FilterAttendanceViewGatway : ConnectionGateway
    {
        public List<FilterAttendanceView> GetFilterAttendanceView(string query)
        {
            List<FilterAttendanceView> filterAttendanceViews = new List<FilterAttendanceView>();
            
            HrmsCommand.CommandText = query;
            HrmsCommand.CommandType = CommandType.Text;
            HrmsCommand.Parameters.Clear();
            HrmsConnection.Open();
            try
            {
                Reader = HrmsCommand.ExecuteReader();
                while (Reader.Read())
                {
                    FilterAttendanceView filterAttendanceView = new FilterAttendanceView();
                    filterAttendanceView.Code = Reader["Code"].ToString();
                    filterAttendanceView.EmployeeId = Convert.ToInt32(Reader["EmployeeId"].ToString());
                    filterAttendanceView.Name = Reader["Name"].ToString();
                    filterAttendanceView.Date = (Reader["Date"]) != DBNull.Value
                        ? DateTime.Parse(Reader["Date"].ToString())
                        : DateTime.MinValue;
                    filterAttendanceView.InTime = (Reader["InTime"]) != DBNull.Value
                        ? DateTime.Parse(Reader["InTime"].ToString())
                        : DateTime.MinValue;
                    filterAttendanceView.OutTime = (Reader["OutTime"]) != DBNull.Value
                        ? DateTime.Parse(Reader["OutTime"].ToString())
                        : DateTime.MinValue;
                    filterAttendanceView.Status = Reader["Status"].ToString();
                    filterAttendanceViews.Add(filterAttendanceView);
                }
            }
            catch (Exception exception)
            {
                // ignored
            }
            finally
            {
                Reader?.Close();
                DeviceConnection?.Close();
            }

            return filterAttendanceViews;
        }
    }
}