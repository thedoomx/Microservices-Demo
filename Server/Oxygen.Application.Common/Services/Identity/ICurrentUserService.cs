namespace Oxygen.Application.Common.Services.Identity
{
    public interface ICurrentUserService
    {
        string UserId { get; }

        bool IsAdministrator { get; }
    }
}
