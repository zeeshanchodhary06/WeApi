using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Entities
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        // Personal Information
        
        [MaxLength(50)]
        public required string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        public DateTime? DateOfBirth { get; set; }

        [MaxLength(10)]
        public string? Gender { get; set; }

        // Contact Information
        [Required]
        [MaxLength(150)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(250)]
        public string? Address { get; set; }

        // Academic Information
        [Required]
        [MaxLength(30)]
        public string EnrollmentNumber { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Course { get; set; } = null!;

        [Required]
        public DateTime AdmissionDate { get; set; }

        public bool IsActive { get; set; } = true;

        // Audit Fields
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
