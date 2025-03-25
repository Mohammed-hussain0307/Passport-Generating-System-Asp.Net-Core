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

        public List<UserDetail> Applications()
        {
            List<UserDetail> user = new List<UserDetail>();
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "SPA_Officer";

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

        public List<UserDetail> GetApplication(int id)
        {
            List<UserDetail> user = new List<UserDetail>();
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "SPG_User";
                    sqlCommand.Parameters.AddWithValue("@ID", id);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        user.Add(new UserDetail
                        {
                            GivenName = sqlDataReader["givenName"].ToString(),
                            SureName = sqlDataReader["sureName"].ToString(),
                            Gender = sqlDataReader["gender"].ToString(),
                            DateOfBirth = DateOnly.FromDateTime(Convert.ToDateTime(sqlDataReader["dateOfBirth"])),
                            PlaceOfBirth = sqlDataReader["placeOfBirth"].ToString(),
                            MaritalStatus = sqlDataReader["maritalStatus"].ToString(),
                            EmploymentType = sqlDataReader["employmentType"].ToString(),
                            EducationQualification = sqlDataReader["educationQualification"].ToString(),
                            AadhaarNumber = Convert.ToInt64(sqlDataReader["aadhaarNumber"]),
                            FatherGivenName = sqlDataReader["fatherGivenName"].ToString(),
                            FatherSureName = sqlDataReader["fatherSureName"].ToString(),
                            MotherGivenName = sqlDataReader["motherGivenName"].ToString(),
                            MotherSureName = sqlDataReader["motherSureName"].ToString(),
                            SpousesGivenName = sqlDataReader["spousesGivenName"].ToString(),
                            SpousesSureName = sqlDataReader["spousesSureName"].ToString(),
                            HouseStreet = sqlDataReader["houseStreet"].ToString(),
                            VillageTownCity = sqlDataReader["villageTownCity"].ToString(),
                            AddressState = sqlDataReader["addressState"].ToString(),
                            AddressDistrict = sqlDataReader["addressDistrict"].ToString(),
                            PoliceStation = sqlDataReader["policeStation"].ToString(),
                            Pincode = sqlDataReader["pincode"].ToString(),
                            MobileNumber = Convert.ToInt64(sqlDataReader["mobileNumber"].ToString()),
                            EmailID = sqlDataReader["emailID"].ToString(),
                            ContactName = sqlDataReader["contactName"].ToString(),
                            ContactMobileNumber = Convert.ToInt64(sqlDataReader["contactMobileNumber"]),
                            MessageInfo = sqlDataReader["message_info"].ToString()
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

        public bool Verified(UserDetail userDetail)
        {
            int check = 0;
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "SPV_Officer";
                    sqlCommand.Parameters.AddWithValue("@ID", userDetail.ID);
                    sqlCommand.Parameters.AddWithValue("@AdminStatus", "NULL");
                    sqlCommand.Parameters.AddWithValue("@MessageInfo", userDetail.MessageInfo);

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

        public bool Rejected(UserDetail user)
        {
            int check = 0;
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "SPR_Officer";
                    sqlCommand.Parameters.AddWithValue("ID", user.ID);
                    sqlCommand.Parameters.AddWithValue("@MessageInfo", user.MessageInfo);

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
