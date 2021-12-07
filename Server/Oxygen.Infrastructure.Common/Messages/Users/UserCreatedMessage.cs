namespace Oxygen.Infrastructure.Common.Messages.Users
{
    public class UserCreatedMessage
    {
        public UserCreatedMessage(string userId, string firstName, string surName, string lastName,
            int department, int jobTitle, int office)
        {
            this.UserId = userId;
            this.FirstName = firstName;
            this.SurName = surName;
            this.LastName = lastName;
            this.Department = department;
            this.JobTitle = jobTitle;
            this.Office = office;
        }

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string SurName { get; set; }

        public string LastName { get; set; }

        public int Department { get; set; }

        public int JobTitle { get; set; }

        public int Office { get; set; }
    }
}
