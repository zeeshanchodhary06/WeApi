using WebApi.Models.Entities;

namespace WebApi.Interfaces
{
    public interface ICourseService
    {

        Task<List<Course>> GetAllCourseAsync();

    }
}
