namespace Oxygen.Identity.Infrastructure
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
