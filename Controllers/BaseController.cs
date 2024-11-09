using Maksab.Helpers;
using Maksab.Helpers.MessageHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maksab.Controllers
{
    [Authorize]
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
    }
}
