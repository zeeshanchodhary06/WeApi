using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using WebApi.Models.Entities;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    public class CourseController : ControllerBase
    {

        private readonly ICourseService _courseService;
        public CourseController (ICourseService courseService)
        {
            _courseService = courseService;

        }
        [HttpGet]
        [Route("/GetAllCourses")]
        public   async Task<List<Course>> GetAllCoursesAsyc()
        {
            return await _courseService.GetAllCourseAsync();
        }

    }
}
