using Microsoft.Data.SqlClient;
using PracticeDemo.Models;

namespace PracticeDemo.DAL
{
    public class Service_DAL
    {
        private SqlConnection sqlConnection = null;

       public static IConfiguration? configuration { get; set; }

        public Service_DAL()
        {
            var build = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            configuration = build.Build();
            sqlConnection = new SqlConnection(configuration.GetConnectionString("connection"));
        }

        public List<Service> Service()
        {
            List<Service> services = new List<Service>();
            try
            {

                using (sqlConnection)
                {
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "SPG_Service";

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        services.Add(new Service
                        {
                            ServiceType = sqlDataReader["service_type"].ToString()
                        });
                    }
                }
            }
            finally
            {
                sqlConnection.Close();
            }
            return services;
    }
}
