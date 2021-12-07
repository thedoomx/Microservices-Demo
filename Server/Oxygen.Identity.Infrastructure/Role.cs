namespace Oxygen.Identity.Infrastructure
{
    using Microsoft.AspNetCore.Identity;

    internal class Role : IdentityRole
    {
        internal Role(string name)
            : base(name)
            => this.Name = name;
    }
}
