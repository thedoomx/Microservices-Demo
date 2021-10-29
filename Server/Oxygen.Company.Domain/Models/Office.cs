namespace Oxygen.Company.Domain.Models
{
    using Oxygen.Company.Domain.Exceptions;
    using Oxygen.Domain.Common.Models;
    using static Oxygen.Company.Domain.Models.ModelConstants.Office;

    public class Office : Entity<int>
    {
        internal Office(string name, string address)
        {
            this.Validate(name, address);
        }

        public string Name { get; private set; }

        public string Address { get; private set; }

        public Office ChangeName(string name)
        {
            this.ValidateName(name);
            this.Name = name;

            return this;
        }

        public Office ChangeAddress(string address)
        {
            this.ValidateAddress(address);
            this.Address = address;

            return this;
        }

        private void ValidateAddress(string address)
            => Guard.ForStringLength<InvalidOfficeException>(
               address,
               MinAddressLength,
               MaxAddressLength,
               nameof(this.Address));

        private void ValidateName(string name)
            => Guard.ForStringLength<InvalidOfficeException>(
               name,
               MinNameLength,
               MaxNameLength,
               nameof(this.Name));

        private void Validate(string name, string address)
        {
            this.ValidateName(name);
            this.ValidateAddress(address);
        }

       
    }
}
