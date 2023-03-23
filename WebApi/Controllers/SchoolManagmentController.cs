using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServicesLibrary.SchoolManagementServices.Enrollmentservices;
using ServicesLibrary.SchoolManagementServices.StudentServices;
using ServicesLibrary.SchoolManagementServices.TeacherServices;
using System.Resources;
using System.Web.Http;
using WebApi.Dto_s;
using WebApi.Models.Response;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace WebApi.Controllers
{

    [Route("schoolManagement")]
    [ApiController]
    public class SchoolManagmentController : Controller
    {
        private readonly IStudentService studentService;
        private readonly ITeacherService teacherService;
        private readonly IEnrollmentService enrollmentService;
        private readonly IMapper mapper;

        public SchoolManagmentController(IStudentService studentService, ITeacherService teacherService,IEnrollmentService enrollmentService, IMapper mapper)
        {
            this.studentService = studentService;
            this.teacherService = teacherService;
            this.enrollmentService = enrollmentService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("getstudents")]

        public async Task<IActionResult> GetStudents()
        {
            var result = studentService.GetAllStudents();
            if (!result.Result.Any())
                return BadRequest();
            var students = mapper.Map<List<StudentDto>>(result.Result.ToList());
            var response = new ResponseModel();
            response.Data = students;
            return Ok(response);
        }
    }
}
