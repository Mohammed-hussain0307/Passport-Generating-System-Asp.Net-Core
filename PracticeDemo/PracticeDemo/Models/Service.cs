using System.ComponentModel.DataAnnotations;

namespace PracticeDemo.Models
{
    public class Service
    {
        [Key]
        public int ID { get; set; }

        public string? ServiceType { get; set; }
    }
}
