﻿using Microsoft.Data.SqlClient;
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
                            EmailID = sqlDataReader["emailID"].ToString(),
                            AdminStatus = sqlDataReader["admin_status"].ToString(),
                            OfficerStatus = sqlDataReader["officer_status"].ToString(),
                            Document = sqlDataReader["document"] as byte[]
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
                            ID = Convert.ToInt32(sqlDataReader["id"]),
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
                            Document = sqlDataReader["document"] as byte[],
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

        public bool Approved(UserDetail user)
        {
            int check = 0;
            string status = "";
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandText = "SPAP_admin";
                        sqlCommand.Parameters.AddWithValue("@ID", user.ID);
                        sqlCommand.Parameters.AddWithValue("@MessageInfo", user.MessageInfo);

                        check = sqlCommand.ExecuteNonQuery();
                    }

                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandText = "SPG_User";
                        sqlCommand.Parameters.AddWithValue("@ID", user.ID);
                        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                        while (sqlDataReader.Read())
                        {
                            status = sqlDataReader["officer_status"].ToString();
                        }
                        sqlDataReader.Close();
                    }
                    if (status == "verified")
                    {
                         Random random = new Random();
                         string passportNumber = "IP" + random.Next(1000000, 9999999);
                         using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                         {
                            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                            sqlCommand.CommandText = "SPP_Admin";
                            sqlCommand.Parameters.AddWithValue("@ID", user.ID);
                            sqlCommand.Parameters.AddWithValue("@PassportNumber", passportNumber);

                            check = sqlCommand.ExecuteNonQuery();
                         }
                    }                    
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
                    sqlCommand.CommandText = "SPRE_Admin";
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

        public List<NewUser> ViewAdmin()
        {
            List<NewUser> allAdmin = new List<NewUser>();
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "SPA_Admin";

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        allAdmin.Add(new NewUser
                        {
                            ID = Convert.ToInt32(sqlDataReader["id"]),
                            GivenName = sqlDataReader["admin_name"].ToString(),
                            EmailID = sqlDataReader["email_id"].ToString(),
                            LoginID = sqlDataReader["admin_id"].ToString()
                        });
                    }
                }
            }
            finally
            {
                sqlConnection.Close();
            }
            return allAdmin;
        }

        public bool CreateAdmin(NewUser newAdmin)
        {
            int check = 0;
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "SPC_Admin";
                    sqlCommand.Parameters.AddWithValue("@AdminName", newAdmin.GivenName);
                    sqlCommand.Parameters.AddWithValue("@EmailID", newAdmin.EmailID);
                    sqlCommand.Parameters.AddWithValue("@AdminID", newAdmin.LoginID);
                    sqlCommand.Parameters.AddWithValue("@AdminPassword", newAdmin.Password);

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
