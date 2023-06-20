using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesLibrary.ActivityLogging;
using WebApi.Dto_s;
using WebApi.Models.Response;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityLogController : ControllerBase
    {
        private readonly IActivityLogService activityLogService;
        private readonly IMapper mapper;

        public ActivityLogController(IActivityLogService activityLogService,IMapper mapper)
        {
            this.activityLogService = activityLogService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("getactivitylogs")]
        public async Task<IActionResult> GetActivityLogs(int pageNumber =1, int pageSize =10)
        {
            var result = activityLogService.GetAll(pageNumber,pageSize);
            
            var response = new ResponseModel();
            response.Data = result;
            return Ok(response);
        }
    }
}
