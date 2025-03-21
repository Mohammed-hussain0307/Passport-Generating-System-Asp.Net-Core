using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Server;
using PassportGeneratingSystem.Controllers;
using PassportGeneratingSystem.Models;

namespace PassportGeneratingSystem.DAL
{
    public class UserDetail_DAL
    {
        private SqlConnection sqlConnection = null;

        public static IConfiguration? configuration { get; set; }

        public UserDetail_DAL()
        {
            var connection = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            configuration = connection.Build();
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Connection"));
        }

        //create user
        public bool Register(UserDetail userDetail,int userId)
        {
            int check = 0;
            try {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "SPC_UserDetail";
                    sqlCommand.Parameters.AddWithValue("@GivenName", userDetail.GivenName);
                    sqlCommand.Parameters.AddWithValue("@SureName", userDetail.SureName);
                    sqlCommand.Parameters.AddWithValue("@Gender", userDetail.Gender);
                    sqlCommand.Parameters.AddWithValue("@DateOfBirth", userDetail.DateOfBirth);
                    sqlCommand.Parameters.AddWithValue("@PlaceOfBirth", userDetail.PlaceOfBirth);
                    sqlCommand.Parameters.AddWithValue("@MaritalStatus", userDetail.MaritalStatus);
                    sqlCommand.Parameters.AddWithValue("@EmploymentType", userDetail.EmploymentType);
                    sqlCommand.Parameters.AddWithValue("@EducationQualification", userDetail.EducationQualification);
                    sqlCommand.Parameters.AddWithValue("@AadhaarNumber", userDetail.AadhaarNumber);
                    sqlCommand.Parameters.AddWithValue("@FatherGivenName", userDetail.FatherGivenName);
                    sqlCommand.Parameters.AddWithValue("@FatherSureName", userDetail.FatherSureName);
                    sqlCommand.Parameters.AddWithValue("@MotherGivenName", userDetail.MotherGivenName);
                    sqlCommand.Parameters.AddWithValue("@MotherSureName", userDetail.MotherSureName);
                    sqlCommand.Parameters.AddWithValue("@SpousesGivenName", userDetail.SpousesGivenName);
                    sqlCommand.Parameters.AddWithValue("@SpousesSureName", userDetail.SpousesSureName);
                    sqlCommand.Parameters.AddWithValue("@HouseStreet", userDetail.HouseStreet);
                    sqlCommand.Parameters.AddWithValue("@VillageTownCity", userDetail.VillageTownCity);
                    sqlCommand.Parameters.AddWithValue("@AddressState", userDetail.AddressState);
                    sqlCommand.Parameters.AddWithValue("@AddressDistrict", userDetail.AddressDistrict);
                    sqlCommand.Parameters.AddWithValue("@PoliceStation", userDetail.PoliceStation);
                    sqlCommand.Parameters.AddWithValue("@Pincode", userDetail.Pincode);
                    sqlCommand.Parameters.AddWithValue("@MobileNumber", userDetail.MobileNumber);
                    sqlCommand.Parameters.AddWithValue("@EmailID", userDetail.EmailID);
                    sqlCommand.Parameters.AddWithValue("@ContactName", userDetail.ContactName);
                    sqlCommand.Parameters.AddWithValue("@ContactMobileNumber", userDetail.ContactMobileNumber);
                    sqlCommand.Parameters.AddWithValue("@UserID", userId);

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

        //view all user
        public List<UserDetail> GetAllUser(int userId)
        {
            List<UserDetail> user = new List<UserDetail>();
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "SPR_AllUser";
                    sqlCommand.Parameters.AddWithValue("@UserID", userId);

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

        //get user by id
        public List<UserDetail> GetUserById(int id)
        {
            List<UserDetail> userDetails = new List<UserDetail>();
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
                        userDetails.Add(new UserDetail
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
                        });
                    }
                }
            }
            finally
            {
                sqlConnection.Close();
            }
            return userDetails;
        }

        //update user
        public bool UpdateUser(UserDetail userDetail)
        {
            int check = 0;
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "SPU_User";
                    sqlCommand.Parameters.AddWithValue("@ID", userDetail.ID);
                    sqlCommand.Parameters.AddWithValue("@GivenName", userDetail.GivenName);
                    sqlCommand.Parameters.AddWithValue("@SureName", userDetail.SureName);
                    sqlCommand.Parameters.AddWithValue("@Gender", userDetail.Gender);
                    sqlCommand.Parameters.AddWithValue("@DateOfBirth", userDetail.DateOfBirth);
                    sqlCommand.Parameters.AddWithValue("@PlaceOfBirth", userDetail.PlaceOfBirth);
                    sqlCommand.Parameters.AddWithValue("@MaritalStatus", userDetail.MaritalStatus);
                    sqlCommand.Parameters.AddWithValue("@EmploymentType", userDetail.EmploymentType);
                    sqlCommand.Parameters.AddWithValue("@EducationQualification", userDetail.EducationQualification);
                    sqlCommand.Parameters.AddWithValue("@AadhaarNumber", userDetail.AadhaarNumber);
                    sqlCommand.Parameters.AddWithValue("@FatherGivenName", userDetail.FatherGivenName);
                    sqlCommand.Parameters.AddWithValue("@FatherSureName", userDetail.FatherSureName);
                    sqlCommand.Parameters.AddWithValue("@MotherGivenName", userDetail.MotherGivenName);
                    sqlCommand.Parameters.AddWithValue("@MotherSureName", userDetail.MotherSureName);
                    sqlCommand.Parameters.AddWithValue("@SpousesGivenName", userDetail.SpousesGivenName);
                    sqlCommand.Parameters.AddWithValue("@SpousesSureName", userDetail.SpousesSureName);
                    sqlCommand.Parameters.AddWithValue("@HouseStreet", userDetail.HouseStreet);
                    sqlCommand.Parameters.AddWithValue("@VillageTownCity", userDetail.VillageTownCity);
                    sqlCommand.Parameters.AddWithValue("@AddressState", userDetail.AddressState);
                    sqlCommand.Parameters.AddWithValue("@AddressDistrict", userDetail.AddressDistrict);
                    sqlCommand.Parameters.AddWithValue("@PoliceStation", userDetail.PoliceStation);
                    sqlCommand.Parameters.AddWithValue("@Pincode", userDetail.Pincode);
                    sqlCommand.Parameters.AddWithValue("@MobileNumber", userDetail.MobileNumber);
                    sqlCommand.Parameters.AddWithValue("@EmailID", userDetail.EmailID);
                    sqlCommand.Parameters.AddWithValue("@ContactName", userDetail.ContactName);
                    sqlCommand.Parameters.AddWithValue("@ContactMobileNumber", userDetail.ContactMobileNumber);

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

        public bool DeleteUser(int id)
        {
            int check = 0;
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "SPD_User";
                    sqlCommand.Parameters.AddWithValue("@ID", id);

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

        public bool Booking(UserDetail userDetail)
        {
            int check = 0;
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "SPB_User";
                    sqlCommand.Parameters.AddWithValue("ID", userDetail.ID);
                    sqlCommand.Parameters.AddWithValue("@Bookingplace", userDetail.BookingPlace);
                    sqlCommand.Parameters.AddWithValue("@BookingDate", userDetail.BookingDate);
                    sqlCommand.Parameters.AddWithValue("@Status", userDetail.status);

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
