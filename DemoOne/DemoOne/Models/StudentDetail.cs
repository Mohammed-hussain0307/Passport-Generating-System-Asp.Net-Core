using System.ComponentModel.DataAnnotations;

namespace DemoOne.Models
{
    public class StudentDetail
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string? StudentName { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        public long? MobileNumber { get; set; }
    }
}
