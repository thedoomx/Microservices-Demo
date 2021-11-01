namespace Ogyxen.Common.Extensions
{
    using System.Security.Claims;
    using static Oxygen.Common.Constants.GlobalConstants;

    public static class ClaimsPrincipalExtensions
    {
        public static bool IsAdministrator(this ClaimsPrincipal user)
            => user.IsInRole(AdministratorRoleName);
    }
}
