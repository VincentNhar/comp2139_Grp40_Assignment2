using System.ComponentModel.DataAnnotations;

namespace Grp40_Assignment2.Models
{
    public class UserReview
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string seller { get; set; }
        [Required]        
        public string reviewer { get; set; }
        [StringLength(350, ErrorMessage = "Review is too long (maximum {1} characters).")]
        public string feedback { get; set; }
        [Range(1,10)]
        public int rating { get; set; }
    }
}
