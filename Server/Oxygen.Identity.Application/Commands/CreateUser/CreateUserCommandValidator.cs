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

            this.RuleFor(u => u.FirstName)
                .MinimumLength(MinNameLength)
                .MaximumLength(MaxNameLength)
                .NotEmpty();

            this.RuleFor(u => u.SurName)
                .MinimumLength(MinNameLength)
                .MaximumLength(MaxNameLength)
                .NotEmpty();

            this.RuleFor(u => u.LastName)
                .MinimumLength(MinNameLength)
                .MaximumLength(MaxNameLength)
                .NotEmpty();

            this.RuleFor(u => u.Department)
                .NotEmpty();

            this.RuleFor(u => u.JobTitle)
                .NotEmpty();

            this.RuleFor(u => u.Office)
                .NotEmpty();
        }
    }
}
