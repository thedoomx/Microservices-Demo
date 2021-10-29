namespace Oxygen.Company.Domain.Models
{
    using Oxygen.Company.Domain.Exceptions;
    using Oxygen.Domain.Common;
    using Oxygen.Domain.Common.Models;
    using static Oxygen.Company.Domain.Models.ModelConstants.JobTitle;

    public class JobTitle : ValueObject
    {
        internal JobTitle(string name)
        {
            this.Validate(name);

            this.Name = name;
        }

        public string Name { get; private set; }

        private void ValidateName(string name)
            => Guard.ForStringLength<InvalidJobTitleException>(
                name,
                MinNameLength,
                MaxNameLength,
                this.Name);

        private void Validate(string name)
        {
            this.ValidateName(name);
        }
    }
}
