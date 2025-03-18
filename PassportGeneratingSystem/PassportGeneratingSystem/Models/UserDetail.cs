using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PassportGeneratingSystem.Models
{
    public class UserDetail
    {
            [Key]
            public int ID { get; set; }

            [Required]
            [StringLength(30)]
            [DisplayName("Applicant Name")]
            public string? GivenName { get; set; }

            [Required]
            [StringLength(30)]
            public string? SureName { get; set; }

            [Required]
            public string? Gender { get; set; }

            [Required]
            public DateOnly DateOfBirth { get; set; }
            public string? PlaceOfBirth { get; set; }

            [Required]
            public string? MaritalStatus { get; set; }
            public string? EmploymentType { get; set; }
            public string? EducationQualification { get; set; }
            public long? AadhaarNumber { get; set; }

            [Required]
            [DisplayName("Father Name")]
            public string? FatherGivenName { get; set; }
            public string? FatherSureName { get; set; }

            [Required]
            public string? MotherGivenName { get; set; }
            public string? MotherSureName { get; set; }
            public string? SpousesGivenName { get; set; }
            public string? SpousesSureName { get; set; }

            [Required]
            public string? HouseStreet { get; set; }

            [Required]
            public string? VillageTownCity { get; set; }

            [Required]
            public string? AddressState { get; set; }

            [Required]
            public string? AddressDistrict { get; set; }

            [Required]
            public string? PoliceStation { get; set; }

            [Required]
            public string? Pincode { get; set; }

            [Required]
            [DisplayName("Mobile Number")]
            public long MobileNumber { get; set; }

            [DisplayName("Email")]
            public string? EmailID { get; set; }

            [Required]
            public string? ContactName { get; set; }

            [Required]
            public long ContactMobileNumber { get; set; }

            public int UserID { get; set; }

    }
}
