using System;
using System.Collections.Generic;
using System.Data;
using FTL_HRMS.Models.Payroll;

namespace FTL_HRMS.DAL
{
    public class Device : ConnectionGateway
    {
        public List<DeviceAttendance> GetDailyAttendance()
        {
            List<DeviceAttendance> deviceAttendances = new List<DeviceAttendance>();
            Query = "select * from CHECKINOUTVIEW";
            Command.CommandText = Query;
            Command.CommandType = CommandType.Text;
            Command.Parameters.Clear();
            Connection.Open();
            try
            {
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    DeviceAttendance deviceAttendance = new DeviceAttendance();
                    deviceAttendance.EmployeeCode = Reader["EmployeeCode"].ToString();
                    deviceAttendance.UserId = Convert.ToInt32(Reader["USERID"].ToString());
                    deviceAttendance.CheckTime = (Reader["CHECKTIME"]) != DBNull.Value
                        ? DateTime.Parse(Reader["CHECKTIME"].ToString())
                        : DateTime.MinValue;
                    deviceAttendance.IsCalculated = (bool) ((Reader["IsCalculated"]) != DBNull.Value
                        ? Reader["IsCalculated"]
                        : 0);
                    deviceAttendances.Add(deviceAttendance);
                }
            }
            catch (Exception exception)
            {
                // ignored
            }
            finally
            {
                Reader?.Close();
                Connection?.Close();
            }

            return deviceAttendances;
        }
        public bool UpdateCheckInOutStatus(List<int> userIds)
        {
            Command.CommandType = CommandType.Text;
            Connection.Open();
            try
            {
                foreach (int id in userIds)
                {
                    Query = "Update CHECKINOUT set IsCalculated = 1 where USERID = " + id;
                    Command.CommandText = Query;
                    Command.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception exception)
            {
                // ignored
                return false;
            }
            finally
            {
                Reader?.Close();
                Connection?.Close();
            }
            
        }
    }
}