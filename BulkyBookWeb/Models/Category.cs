using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public String Name { get; set; } = string.Empty;

        [Range(1, 100, ErrorMessage = "Display order must be between 1 and 100.")]
        public int DisplayOrder { get; set; }
    }
}
