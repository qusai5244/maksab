using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Maksab.Security.ACL
{
    public class HasPermissionAttribute : TypeFilterAttribute
    {
        public HasPermissionAttribute(string claimType, int claimValue) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim(claimType, claimValue.ToString()) };
        }
    }
}