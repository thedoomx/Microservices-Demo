namespace Oxygen.Company.Domain.Exceptions
{
    using Oxygen.Domain.Common;

    public class InvalidEmployeeException : BaseDomainException
    {
        public InvalidEmployeeException()
        {
        }

        public InvalidEmployeeException(string error) => this.Error = error;
    }
}
