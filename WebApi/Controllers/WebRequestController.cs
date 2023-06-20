using Microsoft.AspNetCore.Mvc;
using ServicesLibrary.HttpClientServices;
using System.Net;
using WebApi.Models.Response;
using Microsoft.AspNetCore.Http;
using ServicesLibrary.ActivityLogging;
using Entities.Accounts;
using Entities.EmployeesEntities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]

    public class WebRequestController : ControllerBase
    {
        private readonly IWebRequestHelper webRequestHelper;
        private readonly IActivityLogService activityLogService;

        public WebRequestController(IWebRequestHelper webRequestHelper , IActivityLogService activityLogService)
        {
            this.webRequestHelper = webRequestHelper;
            this.activityLogService = activityLogService;
        }

        [HttpGet]
        [Route("testget")]

        public async Task<IActionResult> CheckGetMethod(string url, Dictionary<string, string> headers)
        {
            var result = await webRequestHelper.GetRequestAsync(url, headers);
            var response = new ResponseModel
            {
                Data = result,
                ResponseStatus = Enums.ResponseStatus.Success,
                Message = ""
            };

            var desc = $"A get request has been initiated to  {url}";

            activityLogService.Add(desc, 5, "", null, Request);
            return Ok(response);
        }
    }
}
