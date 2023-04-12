using AutoMapper;
using Entities.SchoolManagement;
using Google.Apis.Upload;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using ServicesLibrary.SchoolManagementServices.Enrollmentservices;
using ServicesLibrary.SchoolManagementServices.StudentServices;
using ServicesLibrary.SchoolManagementServices.TeacherServices;
using System.Resources;
using System.Web.Http;
using WebApi.Dto_s;
using WebApi.Models.Response;
using WebApi.Models.SchoolManagment;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
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
        private readonly IWebHostEnvironment webHostEnvironment;

        public SchoolManagmentController(IStudentService studentService, ITeacherService teacherService,IEnrollmentService enrollmentService, IMapper mapper , IWebHostEnvironment webHostEnvironment)
        {
            this.studentService = studentService;
            this.teacherService = teacherService;
            this.enrollmentService = enrollmentService;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Route("getstudents")]
        public async Task<IActionResult> GetStudents()
        {
            var result = await studentService.GetAllStudents();
            if (result.Count()== 0)
                return NotFound("No students were found.");
            var students = mapper.Map<List<StudentDto>>(result);
            var response = new ResponseModel();
            response.Data = students;
            return Ok(response);
        }

        [HttpPost]
        [Route("addstudent")]

        // Have to add this FromForm otherWise the swagger doesnt show the input element and also swashbuck asp.net core package as well.
        public async Task<ActionResult> AddStudent([FromForm]AddStudentModel studentModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(studentModel);
            }

            var student = mapper.Map<Student>(studentModel);

            if (studentModel.ProfilePicture != null)
            {
                try
                {
                    var profilePicRelativePath = await SaveProfilePictureAsync(studentModel, "Student");
                    student.ProfilePicturePath = profilePicRelativePath;
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error saving profile picture: {ex.Message}");
                }
            }

            studentService.AddStudent(student);

            var response = new ResponseModel();
            response.Data = student;

            return Ok(response);
        }


        #region Private_Funtions
        private async Task<string?> SaveProfilePictureAsync <T>(T model, string folderName)
        {
            if (model == null)            
               return null;
            
            var profilePicProperty = model.GetType().GetProperty("ProfilePicture");
            if (profilePicProperty == null) return null;

            IFormFile? profilePic = profilePicProperty.GetValue(model) as IFormFile;
            if (profilePic == null) return null;

            if (!IsImageFile(profilePic))
            {
                throw new Exception("The uploaded file is not a valid image file.");
            }

            var usernameProperty = model.GetType().GetProperty("Name");
            if (usernameProperty == null) return null;

            string? username = usernameProperty.GetValue(model) as string;
            if (username == null) return null;

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(profilePic.FileName);

            var folderPath = Path.Combine(folderName, username);

            var completePath =  Path.Combine(webHostEnvironment.ContentRootPath +"/"+ folderPath);
            if (!Directory.Exists(completePath))
            {
                Directory.CreateDirectory(completePath);
            }

            var filePath = Path.Combine(completePath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
              await profilePic.CopyToAsync(stream);
            }

            return Path.Combine(folderPath, fileName);

        }

        private bool IsImageFile(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            return allowedExtensions.Contains(fileExtension);
        }

        #endregion
    }
}
