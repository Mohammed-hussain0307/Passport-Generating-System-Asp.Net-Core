using System.ComponentModel.DataAnnotations;

namespace PassportGeneratingSystem.Models
{
    public class Login
    {
        public int ID { get; set; }

        [Required]
        public string? LoginID { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
