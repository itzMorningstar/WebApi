using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesLibrary;
using WebApi.Models.Notifications;
using WebApi.Models.Response;
using WebApi.Services;
using Entities.DeviceRegistrationEntity;


namespace WebApi.Controllers
{
    [Route("firebase/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IDeviceManager deviceManager;

        public NotificationsController(IDeviceManager deviceManager)
        {
            this.deviceManager = deviceManager;
        }
        [HttpPost]
        [Route("sendnotification")]
        public async Task<IActionResult> SendNotification(Notification model)
        {
            try
            {
                var response = new ResponseModel();
                if (string.IsNullOrEmpty(model.ToUsername))
                {

                    response.Message = "Username cannot be null";
                    response.ResponseStatus = Enums.ResponseStatus.Forbidden;
                    return BadRequest(response);
                }

                var tokens = deviceManager.GetDeviceTokens(model.ToUsername);
                if (tokens.Any())
                {
                    await NotificationsManager.SendPushNotifications(model.Message, model.FromUsername, tokens);
                    response.Data = model;
                    response.Message = "Sussces";
                    response.ResponseStatus = Enums.ResponseStatus.Success;

                    return Ok(response);
                }

                response.Message = "No user found against this username";
                response.ResponseStatus = Enums.ResponseStatus.NotFound;
                return BadRequest(response);
            }
            catch (Exception e)
            {
                var response = new ResponseModel();
                response.Message = "an error occurred while processing your request" + e.Message;
                response.ResponseStatus = Enums.ResponseStatus.Error;
                return BadRequest(response); throw;
            }
        }
    }
}
