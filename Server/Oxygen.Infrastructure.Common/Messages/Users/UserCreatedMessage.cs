namespace Oxygen.Infrastructure.Common.Messages.Users
{
    public class UserCreatedMessage
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string SurName { get; set; }

        public string LastName { get; set; }

        public int Department { get; set; }

        public int JobTitle { get; set; }

        public int Office { get; set; }
    }
}
