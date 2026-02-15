using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

      

        [HttpGet("/myapi/getallstudents")]
        public async Task<ActionResult<List<Student>>> GetAllStudents()
        {
            return await _studentService.GetStudentsAsync();
        }

        [HttpGet]
        [Route("/myapi/getallstudentsbyid{id:int}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            try
            {
                return await _studentService.GetStudentByIdAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("/myapi/addstudent")]
        public ActionResult<Student> AddStudent(AddStudentDto addStudentDto)
        {

            if (!ModelState.IsValid)
            {
               
                return BadRequest(addStudentDto);
            }

            var student = _studentService.AddStudentAsync(addStudentDto);
            return Ok(student);
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id, AddStudentDto addStudentDto)
        {
            try
            {
                return  await _studentService.UpdateStudentAsync(id, addStudentDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
