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
        public async Task<IActionResult> GetActivityLogs()
        {
            var result = activityLogService.GetAll();
            if (result.Count() == 0)
                return NotFound("No activity logs were found.");
            var activityLogs = mapper.Map<List<ActivityLogDto>>(result);
            var response = new ResponseModel();
            response.Data = activityLogs;
            return Ok(response);
        }
    }
}
