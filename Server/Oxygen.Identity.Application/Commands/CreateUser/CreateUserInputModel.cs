namespace Oxygen.Identity.Application.Commands.CreateUser
{
    public class CreateUserInputModel : UserInputModel
    {
        public string PhoneNumber { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string SurName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public int Department { get; set; } = default!;

        public int JobTitle { get; set; } = default!;

        public int Office { get; set; } = default!;
    }
}
