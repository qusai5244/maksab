using Maksab.Helpers.MessageHandler;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Maksab.Security.ACL
{
    internal class ClaimRequirementFilter : IAsyncAuthorizationFilter
    {
        private readonly Claim _claim;
        private readonly IMessageHandler _messageHandler;

        public ClaimRequirementFilter(Claim claim, IMessageHandler messageHandler)
        {
            _claim = claim;
            _messageHandler = messageHandler;
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
            //if (!hasClaim)
            //{
            //    context.Result = new ForbidResultError(new ApiResponse((int)ErrorMessage.Forbidden, _messageHandler.GetMessage(ErrorMessage.Forbidden)));
            //}

            return Task.CompletedTask;
        }
    }
}