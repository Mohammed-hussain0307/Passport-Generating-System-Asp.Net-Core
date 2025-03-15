using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PassportGeneratingSystem.Models
{
    public class NewUser
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string? GivenName { get; set; }

        public string? SureName { get; set; }

        [Required]
        [EmailAddress]
        public string? EmailID { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 5)]
        public string? LoginID { get; set; }

        [Required]
        [StringLength(16,MinimumLength =8)]
        public string? Password { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8)]
        public string? ConfirmPassword { get; set; }
    }
}
