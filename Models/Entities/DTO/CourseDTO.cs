using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Entities.DTO
{
    public class CourseDTO
    {
        public class Course
        {
            [Required]
            public string CourseName { get; set; }
            public string? CourseDescription { get; set; }
            public string? CourseId { get; set; }

        }
    }
}
