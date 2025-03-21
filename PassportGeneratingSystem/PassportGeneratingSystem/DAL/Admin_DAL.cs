using Microsoft.Data.SqlClient;
using PassportGeneratingSystem.Models;

namespace PassportGeneratingSystem.DAL
{
    public class Admin_DAL
    {
        private SqlConnection sqlConnection = null;
        public static IConfiguration? configuration { get; set; }

        public Admin_DAL()
        {
            var connection = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            configuration = connection.Build();
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Connection"));
        }

        public List<Login> AdminLogin(Login login)
        {
            List<Login> admin = new List<Login>();
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "SPL_Admin";
                    sqlCommand.Parameters.AddWithValue("@AdminID", login.LoginID);
                    sqlCommand.Parameters.AddWithValue("@Password", login.Password);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        admin.Add(new Login
                        {
                            LoginID = sqlDataReader["admin_id"].ToString(),
                            Password = sqlDataReader["admin_password"].ToString()
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

        public List<UserDetail> Applications()
        {
            List<UserDetail> user = new List<UserDetail>();
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "SPA_User";

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        user.Add(new UserDetail
                        {
                            ID = Convert.ToInt32(sqlDataReader["id"]),
                            GivenName = sqlDataReader["givenName"].ToString(),
                            FatherGivenName = sqlDataReader["fatherGivenName"].ToString(),
                            MobileNumber = Convert.ToInt64(sqlDataReader["mobileNumber"]),
                            EmailID = sqlDataReader["emailID"].ToString()
                        });
                    }
                }
            }
            finally
            {
                sqlConnection.Close();
            }
            return user;
        }
    }
}
