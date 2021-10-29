namespace Oxygen.Company.Domain.Models
{
    using Oxygen.Domain.Common.Models;
    using static Oxygen.Company.Domain.Models.ModelConstants.Employee;
    using Oxygen.Company.Domain.Exceptions;
    using Oxygen.Domain.Common;

    public class Employee : Entity<int>, IAggregateRoot
    {
        internal Employee(string firstName, string surName, string lastName, Department department, Office office, JobTitle jobTitle)
        {
            this.Validate(firstName, surName, lastName);

            this.FirstName = firstName;
            this.SurName = surName;
            this.LastName = lastName;
            this.Department = department;
            this.Office = office;
            this.JobTitle = jobTitle;
        }

        private Employee(string firstName, string surName, string lastName)
        {
            this.FirstName = firstName;
            this.SurName = surName;
            this.LastName = lastName;
        }

        public string FirstName { get; private set; }

        public string SurName { get; private set; }

        public string LastName { get; private set; }

        public Department Department { get; private set; }

        public Office Office { get; private set; }

        public JobTitle JobTitle { get; private set; }

        public Employee ChangeFirstName(string firstName)
        {
            this.ValidateFirstName(firstName);
            this.FirstName = firstName;

            return this;
        }

        public Employee ChangeSurName(string surName)
        {
            this.ValidateSurName(surName);
            this.SurName = surName;

            return this;
        }

        public Employee ChangeLastName(string lastName)
        {
            this.ValidateLastName(lastName);
            this.LastName = lastName;

            return this;
        }

        public Employee ChangeDepartment(Department department)
        {
            this.Department = department;

            return this;
        }

        public Employee ChangeOffice(Office office)
        {
            this.Office = office;

            return this;
        }

        public Employee ChangeJobTitle(JobTitle jobTitle)
        {
            this.JobTitle = jobTitle;

            return this;
        }

        private void ValidateFirstName(string firstName)
            => Guard.ForStringLength<InvalidEmployeeException>(
                firstName,
                MinFirstNameLength,
                MaxFirstNameLength,
                nameof(this.FirstName));

        private void ValidateSurName(string surName)
           => Guard.ForStringLength<InvalidEmployeeException>(
               surName,
               MinSurNameLength,
               MaxSurNameLength,
               nameof(this.SurName));

        private void ValidateLastName(string lastName)
           => Guard.ForStringLength<InvalidEmployeeException>(
               lastName,
               MinLastNameLength,
               MaxLastNameLength,
               nameof(this.LastName));

        private void Validate(string firstName, string surName, string lastName)
        {
            this.ValidateFirstName(firstName);
            this.ValidateSurName(surName);
            this.ValidateLastName(lastName);
        }
    }
}
