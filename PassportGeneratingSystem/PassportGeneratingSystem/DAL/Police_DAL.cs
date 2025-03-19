using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using PassportGeneratingSystem.Models;

namespace PassportGeneratingSystem.DAL
{
    public class Police_DAL
    {
        private SqlConnection sqlConnection = null;
        public static IConfiguration? configuration { get; set; }

        public Police_DAL()
        {
            var connection = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            configuration = connection.Build();
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Connection"));
        }

        public List<Models.Login> Login(Models.Login login)
        {
            List<Models.Login> admin = new List<Models.Login>();
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "SPL_Officer";
                    sqlCommand.Parameters.AddWithValue("@OfficerID", login.LoginID);
                    sqlCommand.Parameters.AddWithValue("@Password", login.Password);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        admin.Add(new Models.Login
                        {
                            LoginID = sqlDataReader["officer_id"].ToString(),
                            Password = sqlDataReader["officer_password"].ToString()
                        });
                    }
                }
            }
            finally
            {
                sqlConnection.Close();
            }
            return admin;
        }

        public static implicit operator Police_DAL(Admin_DAL v)
        {
            throw new NotImplementedException();
        }
    }
}
