using AutoMapper;
using Entities;
using Entities.ActivityLogging;
using Entities.EmployeesEntities;
using Entities.Enums;
using Entities.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesLibrary.AccountServices;
using ServicesLibrary.ActivityLogging;
using ServicesLibrary.EmployeesServices;
using ServicesLibrary.SchoolManagementServices.StudentServices;
using System.Runtime.InteropServices;
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

        public EmployeesController(IEmployeeService employeeService, IMapper mapper, IActivityLogService activityLogService, IAccountService accountService)
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
        public async Task<ActionResult> AddEmployee(AddEmployeeDto employeeModel)
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
                Department = (Department)Enum.Parse(typeof(Department), employeeModel.Department),
                Gender = (Gender)Enum.Parse(typeof(Gender), employeeModel.Gender),
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };
            employeeService.Add(employee);
            employeeService.AddSallery(new Sallery { EmployeeId = employee.Id, SalleryAmount = employeeModel.SalleryAmount, CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now });
            var account = accountService.GetCachedAccount(Request.Headers[ApplicationConstants.ApiKeyHeaderName]);
            var desc = $"New Employee ({employee.FirstName + " " + employee.LastName})Added for {employeeModel.SalleryAmount} on {employee.CreatedOn}";

            activityLogService.Add(desc, 5, account.Username, account.AccountGuid.ToString(), Request);

            return CreatedAtAction(nameof(AddEmployee), new { id = employee.Id }, employee);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetEmployeeById(int id)
        {
            var response = new ResponseModel();

            if (id > 0)
            {
                var employee = employeeService.GetWithSallery(id);
                if (employee != null)
                {
                    var employeeDto = mapper.Map<EmployeeDto>(employee);
                    response.Data = employeeDto;
                    return Ok(response);

                }
            }
            return NotFound("No employee found with that id");

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteEmployeeById(int id)
        {
            var response = new ResponseModel();

            if (id > 0)
            {
                employeeService.Delete(id);
                
                    response.ResponseStatus =Enums.ResponseStatus.Success ;
                    response.Message ="Employee deleted successfully";
                    return Ok(response);
                
            }
            response.ResponseStatus =Enums.ResponseStatus.NotFound ;
            response.Message ="No employee found with that id";
            return NotFound(response);

        }
    }
}
