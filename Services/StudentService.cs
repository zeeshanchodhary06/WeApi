using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models.Entities;

namespace WebApi.Services
{
    public class StudentService : IStudentService
    {

        private readonly ApplicationDbContext _contex;
        public StudentService(ApplicationDbContext appContext)
        {
            _contex = appContext;
        }

        public async Task<Student> AddStudentAsync(AddStudentDto addStudentDto)
        {
            var student = new Student
            {
                FirstName = addStudentDto.FirstName,
                LastName = addStudentDto.LastName,
                DateOfBirth = addStudentDto.DateOfBirth,
                Gender = addStudentDto.Gender,
                Email = addStudentDto.Email,
                PhoneNumber = addStudentDto.PhoneNumber,
                Address = addStudentDto.Address,
                EnrollmentNumber = addStudentDto.EnrollmentNumber,
                Course = addStudentDto.Course,
                AdmissionDate = addStudentDto.AdmissionDate,
                IsActive = addStudentDto.IsActive,
                CreatedAt = addStudentDto.CreatedAt
            };

            await _contex.Students.AddAsync(student);
            await _contex.SaveChangesAsync();

            return student;
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Enter correct ID");

            var student = await _contex.Students.FindAsync(id);

            if (student == null)
                throw new KeyNotFoundException("No Student Found For This ID");

            return student;
        }
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _contex.Students
                                 .Where(x => x.StudentId > 5)
                                 .ToListAsync();
        }

        public  async Task<Student> UpdateStudentAsync(int id, AddStudentDto addStudentDto)
        {
            var student =  await _contex.Students
                                  .FirstOrDefaultAsync(x => x.StudentId == id);


            if (student == null)
                throw new KeyNotFoundException("Student Not Found");

            student.FirstName = addStudentDto.FirstName;
            student.LastName = addStudentDto.LastName;
            student.PhoneNumber = addStudentDto.PhoneNumber;
            _contex.SaveChanges();

            return student;
        }
    }
}
