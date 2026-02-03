using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoscovsBackend.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string username { get; set; } = string.Empty;

        [Required]
        public string email { get; set; } = string.Empty;

        [Required]
        public string password { get; set; } = string.Empty;

        public string status { get; set; } = "Active";

        public string access { get; set; } = "user";

        public string created_at { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        public string updated_at { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}