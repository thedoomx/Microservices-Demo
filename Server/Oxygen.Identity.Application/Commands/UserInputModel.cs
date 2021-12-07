namespace Oxygen.Identity.Application.Commands
{
    public abstract class UserInputModel
    {
        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;
    }
}
