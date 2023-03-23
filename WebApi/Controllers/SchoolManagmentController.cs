using Microsoft.AspNetCore.Mvc;
using ServicesLibrary.SchoolManagementServices.Enrollmentservices;
using ServicesLibrary.SchoolManagementServices.StudentServices;
using ServicesLibrary.SchoolManagementServices.TeacherServices;
using System.Resources;
using System.Web.Http;
using WebApi.Models.Response;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace WebApi.Controllers
{

    [Route("schoolManagement")]
    [ApiController]
    public class SchoolManagmentController : ApiController
    {
        private readonly IStudentService studentService;
        private readonly ITeacherService teacherService;
        private readonly IEnrollmentService enrollmentService;

        public SchoolManagmentController(IStudentService studentService, ITeacherService teacherService,IEnrollmentService enrollmentService)
        {
            this.studentService = studentService;
            this.teacherService = teacherService;
            this.enrollmentService = enrollmentService;
        }
        public IActionResult GetStudents()
        {
            var result = studentService.GetAllStudents();
            var response = new ResponseModel();
            response.Data = result;
            return (IActionResult)Ok(response);
        }
    }
}
