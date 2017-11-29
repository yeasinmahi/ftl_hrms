using System.Data.SqlClient;
using System.Web.Configuration;

namespace FTL_HRMS.DAL
{
    public class ConnectionGateway
    {
        public string DeviceConnectionString = WebConfigurationManager.ConnectionStrings["DeviceDbContext"].ConnectionString;
        public string HrmsConnectionString = WebConfigurationManager.ConnectionStrings["HRMSDbContext"].ConnectionString;

        public SqlConnection DeviceConnection { get; set; }
        public SqlCommand DeviceCommand { get; set; }
        public SqlConnection HrmsConnection { get; set; }
        public SqlCommand HrmsCommand { get; set; }
        public SqlDataReader Reader { get; set; }
        public string Query { get; set; }

        public ConnectionGateway()
        {
            DeviceConnection = new SqlConnection(DeviceConnectionString);
            DeviceCommand = new SqlCommand {Connection = DeviceConnection};
            HrmsConnection = new SqlConnection(HrmsConnectionString);
            HrmsCommand = new SqlCommand { Connection = HrmsConnection };
        }

    }
}