using System.Data.SqlClient;

namespace AMS_Database_Project
{
    public class Database
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(
                @"Data Source=.;Initial Catalog=""Database project - AMS"";Integrated Security=True;Encrypt=True;TrustServerCertificate=True;"
            );
        }
    }
}