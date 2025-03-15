using System.ComponentModel.DataAnnotations;

namespace PassportGeneratingSystem.Models
{
    public class Login
    {
        [Required]
        public string? LoginID { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
