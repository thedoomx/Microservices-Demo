namespace Oxygen.Company.Domain.Exceptions
{
    using Oxygen.Domain.Common;

    public class InvalidOfficeException : BaseDomainException
    {
        public InvalidOfficeException()
        {
        }

        public InvalidOfficeException(string error) => this.Error = error;
    }
}
