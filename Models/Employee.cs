using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoscovsBackend.Models
{
    [Table("employees")]
    public class Employee
    {
        [Key]
        public int id { get; set; }
        public string employee_number { get; set; } = string.Empty;
        public string first_name { get; set; } = string.Empty;
        public string last_name { get; set; } = string.Empty;
        public string department { get; set; } = string.Empty;
        public string job_title { get; set; } = string.Empty;
        public string status { get; set; } = "Active";
        public DateTime created_at { get; set; } = DateTime.Now;
    }
}