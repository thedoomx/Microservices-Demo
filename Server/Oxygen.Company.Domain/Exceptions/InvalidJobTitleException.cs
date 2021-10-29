namespace Oxygen.Company.Domain.Exceptions
{
    using Oxygen.Domain.Common;

    public class InvalidJobTitleException : BaseDomainException
    {
        public InvalidJobTitleException()
        {
        }

        public InvalidJobTitleException(string error) => this.Error = error;
    }
}
