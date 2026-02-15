using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models.Entities;

namespace WebApi.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;
        public CourseService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
       public async  Task<List<Course>> GetAllCourseAsync()
        {
            return await _context.Courses.ToListAsync();
        }
    }
}
