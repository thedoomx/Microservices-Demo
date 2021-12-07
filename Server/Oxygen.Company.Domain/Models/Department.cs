namespace Oxygen.Company.Domain.Models
{
    using Oxygen.Company.Domain.Exceptions;
    using Oxygen.Domain.Common.Models;
    using static Oxygen.Company.Domain.Models.ModelConstants.Department;

    public class Department : Entity<int>
    {
        internal Department(string name, bool isActive, string description)
        {
            this.Validate(name, description);

            this.Name = name;
            this.IsActive = isActive;
            this.Description = description;
        }

        public string Name { get; private set; }

        public bool IsActive { get; private set; }

        public string Description { get; private set; }

        public Department ChangeName(string name)
        {
            this.ValidateName(name);
            this.Name = name;

            return this;
        }

        public Department ChangeIsActive()
        {
            this.IsActive = !this.IsActive;

            return this;
        }

        public Department ChangeIsActive(bool isActive)
        {
            this.IsActive = isActive;

            return this;
        }

        public Department ChangeDescription(string description)
        {
            this.ValidateDescription(description);
            this.Description = description;

            return this;
        }

        private void ValidateName(string name) 
            => Guard.ForStringLength<InvalidDepartmentException>(
               name,
               MinNameLength,
               MaxNameLength,
               nameof(this.Name));

        private void ValidateDescription(string description)
            => Guard.ForStringLength<InvalidDepartmentException>(
               description,
               MinDescriptionLength,
               MaxDescriptionLength,
               nameof(this.Name));

        private void Validate(string name, string description)
        {
            this.ValidateName(name);
            this.ValidateDescription(description);
        }
    }
}
