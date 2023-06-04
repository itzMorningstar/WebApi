using AutoMapper;
using Entities;
using Entities.ActivityLogging;
using Entities.EmployeesEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesLibrary.AccountServices;
using ServicesLibrary.ActivityLogging;
using ServicesLibrary.EmployeesServices;
using ServicesLibrary.SchoolManagementServices.StudentServices;
using WebApi.Dto_s;
using WebApi.Models.Response;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly IMapper mapper;
        private readonly IActivityLogService activityLogService;
        private readonly IAccountService accountService;

        public EmployeesController(IEmployeeService employeeService,IMapper mapper,IActivityLogService activityLogService ,  IAccountService accountService)
        {
            this.employeeService = employeeService;
            this.mapper = mapper;
            this.activityLogService = activityLogService;
            this.accountService = accountService;
        }


        [HttpGet]
        [Route("getemployees")]
        public async Task<IActionResult> GetEmployees()
        {
            var result = employeeService.GetAll();
            if (result.Count() == 0)
                return NotFound("No employees were found.");

            var employees = mapper.Map<List<EmployeeDto>>(result);
            var response = new ResponseModel();
            response.Data = employees;
            return Ok(response);
        }

        [HttpPost]
        [Route("addemployee")]
        public async Task<ActionResult> AddEmployee([FromForm] AddEmployeeDto employeeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(employeeModel);
            }

            var employee = new Employee
            {
                Age = employeeModel.Age,
                FirstName = employeeModel.FirstName,
                LastName = employeeModel.LastName,
                Department = employeeModel.Department,
                Gender = employeeModel.Gender,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };
            employeeService.Add(employee);
            employeeService.AddSallery(new Sallery { EmployeeId = employee.Id, SalleryAmount = employeeModel.SalleryAmount, CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now });
            var account = accountService.GetCachedAccount(Request.Headers[ApplicationConstants.ApiKeyHeaderName]);

            activityLogService.Add(new ActivityLog { 
                Description = $"New Employee ({employee.FirstName +" " + employee.LastName})Added for {employeeModel.SalleryAmount} on {employee.CreatedOn}",
                Timestamp = DateTime.Now,
                IPAddress = HttpContext.Connection.RemoteIpAddress.ToString(),
                AccountGuid = account.AccountGuid,
                Username = account.Username,
                UserAgent = Request.Headers["User-Agent"],
                TypeId = 5
            });
            return CreatedAtAction(nameof(AddEmployee), new { id = employee.Id }, employee);
        }
    }
}
