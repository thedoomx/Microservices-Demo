namespace Oxygen.Identity.Application.Commands.CreateUser
{
    using FluentValidation;
    using static Oxygen.Identity.Domain.Models.ModelConstants.User;

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            this.RuleFor(u => u.Email)
                .MinimumLength(MinEmailLength)
                .MaximumLength(MaxEmailLength)
                .EmailAddress()
                .NotEmpty();

            this.RuleFor(u => u.Password)
                .MinimumLength(MinPasswordLength)
                .MaximumLength(MaxPasswordLength)
                .NotEmpty();

            this.RuleFor(u => u.Name)
                .MinimumLength(MinNameLength)
                .MaximumLength(MaxNameLength)
                .NotEmpty();
        }
    }
}
