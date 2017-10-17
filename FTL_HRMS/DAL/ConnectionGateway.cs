using System.Data.SqlClient;
using System.Web.Configuration;

namespace FTL_HRMS.DAL
{
    public class ConnectionGateway
    {
        public string ConnectionString = WebConfigurationManager.ConnectionStrings["DeviceDbContext"].ConnectionString;

        public SqlConnection Connection { get; set; }
        public SqlCommand Command { get; set; }
        public SqlDataReader Reader { get; set; }
        public string Query { get; set; }

        public ConnectionGateway()
        {
            Connection = new SqlConnection(ConnectionString);
            Command = new SqlCommand {Connection = Connection};
        }

    }
}