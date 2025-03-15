using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Protocol.Core.Types;
using PassportGeneratingSystem.Models;

namespace PassportGeneratingSystem.DAL
{
    public class User_DAL
    {
        private SqlConnection sqlConnection = null;

        //For read the connection string
        public static IConfiguration? configuration { get; set; }

        public User_DAL()
        {
            var connection = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            configuration = connection.Build();
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Connection"));
        }

        //create user
        public bool CreateUser(NewUser newUser)
        {
            int check = 0;
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "SPC_User";
                    sqlCommand.Parameters.AddWithValue("@GivenName", newUser.GivenName);
                    sqlCommand.Parameters.AddWithValue("@SureName", newUser.SureName);
                    sqlCommand.Parameters.AddWithValue("@EmailID", newUser.EmailID);
                    sqlCommand.Parameters.AddWithValue("@LoginID", newUser.LoginID);
                    sqlCommand.Parameters.AddWithValue("@LoginPassword", newUser.Password);

                    sqlConnection.Open();
                    check = sqlCommand.ExecuteNonQuery();                    
                }
            }
            finally
            {
                sqlConnection.Close();
            }
            return check > 0;
        }

        //login user
        public List<Models.Login> UserLogin(Models.Login user)
        {
            List<Models.Login> login = new List<Models.Login>();
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "SPL_User";
                    sqlCommand.Parameters.AddWithValue("@LoginID", user.LoginID);
                    sqlCommand.Parameters.AddWithValue("@LoginPassword", user.Password);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        login.Add(new Models.Login
                        {
                            LoginID = sqlDataReader["login_id"].ToString(),
                            Password = sqlDataReader["login_password"].ToString()
                        });
                    }
                }
            }
            finally
            {
                sqlConnection.Close();
            }
            return login;
        }

    }
}
