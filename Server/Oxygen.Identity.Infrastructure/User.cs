namespace Oxygen.Identity.Infrastructure
{
    using Microsoft.AspNetCore.Identity;
    using Oxygen.Identity.Application;

    public class User : IdentityUser, IUser
    {
        internal User(string email)
            : base(email)
            => this.Email = email;

        internal User(string email, string firstName, string surName, string lastName, int department, int jobTitle, int office) : base(email)
        {
            this.Email = email;
            this.FirstName = firstName;
            this.SurName = surName;
            this.LastName = lastName;
            this.Department = department;
            this.JobTitle = jobTitle;
            this.Office = office;
        }

        public string FirstName { get; set; }

        public string SurName { get; set; }

        public string LastName { get; set; }

        public int? Department { get; set; }

        public int? JobTitle { get; set; }

        public int? Office { get; set; }
    }
}
