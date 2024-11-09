using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Maksab.Helpers.MessageHandler;
using Maksab.Helpers;

namespace Maksab.Controllers
{
    [ApiController]
    [Authorize]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        private readonly IMessageHandler _messageHandler;
        public BaseController(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        //[NonAction]
        //public RequestHeaderContent BindRequestHeader()
        //{
        //    var language = HttpContext?.Request?.Headers["Accept-Language"].FirstOrDefault() ??
        //                   HttpContext?.Request?.Headers["lang"].FirstOrDefault() ??
        //                   HttpContext?.Request?.Query["language"].ToString() ??
        //                   HttpContext?.Request?.Query["lang"].ToString() ??
        //                   "en_US";

        //    var isArabic = language.StartsWith("ar", StringComparison.InvariantCultureIgnoreCase);
        //    var isEnglish = !isArabic;

        //    var endpoint = HttpContext?.GetEndpoint();
        //    var endpointName = string.Empty;

        //    if (endpoint != null)
        //    {
        //        var controllerActionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();

        //        if (controllerActionDescriptor != null)
        //        {
        //            var controllerName = controllerActionDescriptor.ControllerTypeInfo.FullName;
        //            var actionName = controllerActionDescriptor.ActionName;

        //            endpointName = $"{controllerName}.{actionName}";
        //        }
        //    }

        //    return new()
        //    {
        //        UserId = int.Parse(HttpContext.User.FindFirstValue(Constants.UserId)),
        //        BusinessGroupId = int.Parse(HttpContext.User.FindFirstValue(Constants.BusinessGroupId)),
        //        IsOwner = bool.Parse(HttpContext.User.FindFirstValue(Constants.IsOwner)),
        //        UserEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email),
        //        Language = language,
        //        IsEn = isEnglish,
        //        IsAr = isArabic,
        //        CorrelationId = HttpContext.TraceIdentifier,
        //        ClientAgent = HttpContext.Request?.Headers?.UserAgent,
        //        ClientIp = HttpContext.Connection?.RemoteIpAddress?.ToString(),
        //        EndPointName = endpointName,
        //    };
        //}

        [NonAction]
        public ActionResult GetServiceResponse<T>(ServiceResponse<T> response)
        {
            if (!response.Succeed)
            {
                return BadRequest(new ApiResponse(response.Code, response.Description, response.Result));
            }

            return Ok(new ApiResponse(response.Code, response.Description, response.Result));
        }

        [NonAction]
        public ActionResult GetServiceResponse(ServiceResponse response)
        {
            var apiResponse = new ApiResponse(response.Code, response.Description);
            return response.Succeed ? Ok(apiResponse) : BadRequest(apiResponse);
        }

        //[NonAction]
        //public BadRequestObjectResult InvaidInput()
        //{
        //    return BadRequest(new ApiResponse((int)ErrorMessage.InvalidInput, _messageHandler.GetMessage(ErrorMessage.InvalidInput), ErrorExtractor.GetErrors(ModelState)));
        //}
    }
}