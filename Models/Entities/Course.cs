using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Entities
{
    public class Course
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string? CourseId { get; set; }

    }
}
