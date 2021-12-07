namespace Oxygen.Startup.Common.Infrastructure
{
    using Microsoft.AspNetCore.Authorization;
    using static Oxygen.Application.Common.Constants;

    public class AuthorizeAdministratorAttribute : AuthorizeAttribute
    {
        public AuthorizeAdministratorAttribute() => this.Roles = AdministratorRoleName;
    }
}
