namespace Oxygen.Identity.Infrastructure
{
    using Microsoft.AspNetCore.Identity;
    using Oxygen.Identity.Application;

    public class User : IdentityUser, IUser
    {
        internal User(string email)
            : base(email)
            => this.Email = email;
    }
}
