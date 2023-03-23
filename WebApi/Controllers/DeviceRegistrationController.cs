using Entities.DeviceRegistrationEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesLibrary;
using WebApi.Dto_s;
using WebApi.Models.Response;

namespace WebApi.Controllers
{
    [Route("Firebase/[controller]")]
    [ApiController]
    public class DeviceRegistrationController : ControllerBase
    {
        private readonly IDeviceManager deviceManager;

        public DeviceRegistrationController(IDeviceManager deviceManager)
        {
            this.deviceManager = deviceManager;
        }
        [HttpPost]
        [Route("registerdevice")]
        public async Task<IActionResult> RegisterDevice(DeviceRegisterationDto model)
        {
            try
            {
                var response = new ResponseModel();
                if (!ModelState.IsValid)
                {
                    response.Message = "an error occurred while processing your request";
                    response.ResponseStatus = Enums.ResponseStatus.Error;
                    return BadRequest(response);
                }
                var DeviceModel = new DeviceRegisterConnection
                {
                    UserName = model.UserName,
                    DeviceToken = model.DeviceToken,
                    CreatedOn = DateTime.Now,
                    Status = true
                };

                await deviceManager.AddDevice(DeviceModel);

                response.Data = model;
                response.Message = "Device has been registered";
                response.ResponseStatus = Enums.ResponseStatus.Success;
                return Ok(response);
            }
            catch (Exception e)
            {
                var response = new ResponseModel();
                response.Message = e.Message;
                response.ResponseStatus = Enums.ResponseStatus.Error;
                return BadRequest(response);
            }
        }
    }
}
