using DemoOne.Models;
using Microsoft.Data.SqlClient;

namespace DemoOne.DAL
{
    public class StudentDetail_DAL
    {
        private SqlConnection sqlConnection;

        public static IConfiguration configuration { get; set; }

        public StudentDetail_DAL()
        {
            var build = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            configuration = build.Build();
            sqlConnection = new SqlConnection(configuration.GetConnectionString("connection"));
        }

        public bool CreateStudent(StudentDetail studentDetail)
        {
            int check = 0;
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "SPI_Student";
                    sqlCommand.Parameters.AddWithValue("@StudentName", studentDetail.StudentName);
                    sqlCommand.Parameters.AddWithValue("@StudentGender", studentDetail.Gender);
                    sqlCommand.Parameters.AddWithValue("@MobileNumber", studentDetail.MobileNumber);

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
    }
}
