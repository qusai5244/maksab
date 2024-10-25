using Maksab.Helpers;
using Maksab.Helpers.MessageHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maksab.Controllers
{
    //[Authorize]
    public class BaseController : ControllerBase
    {
        private readonly IMessageHandler _messageHandler;
        public BaseController(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }
        public ActionResult GetServiceResponse<T>(ServiceResponse<T> response)
        {
            if (!response.Succeed)
            {
                return BadRequest(new ApiResponse(response.Code, response.Description, response.Result));
            }

            return Ok(new ApiResponse(response.Code, response.Description, response.Result));
        }
        public ActionResult GetServiceResponse<T, R>(ServiceResponse<T, R> response)
        {
            if (!response.Succeed)
            {
                return BadRequest(new ApiResponse(response.Code, response.Description));
            }

            return Ok(new ApiResponse(response.Code, response.Description, response.Result, response.Metadata));
        }
        public ActionResult GetServiceResponse(ServiceResponse response)
        {
            var apiResponse = new ApiResponse(response.Code, response.Description);
            return response.Succeed ? Ok(apiResponse) : BadRequest(apiResponse);
        }
        public BadRequestObjectResult InvaidInput()
        {
            return BadRequest(new ApiResponse((int)ErrorMessage.BadRequest, _messageHandler.GetMessage(ErrorMessage.BadRequest), ErrorExtractor.GetErrors(ModelState)));
        }
    }
}
