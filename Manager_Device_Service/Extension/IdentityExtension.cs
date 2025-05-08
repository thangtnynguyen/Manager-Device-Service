using Manager_Device_Service.Core.Constant;
using System.Security.Claims;

namespace  Manager_Device_Service.Extension
{
    public static class IdentityExtension
    {
        public static string GetSpecificClaim(this ClaimsIdentity claimsIdentity, string claimType)
        {
            var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == claimType);

            return (claim != null) ? claim.Value : string.Empty;
        }

        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = ((ClaimsIdentity)claimsPrincipal.Identity).Claims.SingleOrDefault(x => x.Type == ClaimTypeConstant.Id);

            if (claim == null)
            {
                return 0;
            }
            return int.Parse(claim.Value);
        }

        public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = ((ClaimsIdentity)claimsPrincipal.Identity).Claims.SingleOrDefault(x => x.Type == ClaimTypeConstant.UserName);

            if (claim == null)
            {
                return "Anonymous";
            }
            return claim.Value.ToString();
        }
        public static int GetOrganizationId(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = ((ClaimsIdentity)claimsPrincipal.Identity).Claims.SingleOrDefault(x => x.Type == ClaimTypeConstant.OrganizationId);

            if (claim == null)
            {
                return 0;
            }
            return int.Parse(claim.Value); 
        }

    }
}
