using WebApi.Models.Entities;

namespace WebApi.Interfaces
{
    public interface IStudentService
    {


        Task<List<Student>> GetStudentsAsync();
        Task<Student> GetStudentByIdAsync(int id);
        Task<Student> AddStudentAsync(AddStudentDto addStudentDto);
        Task<Student> UpdateStudentAsync(int id, AddStudentDto addStudentDto);



    }
}
