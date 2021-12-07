namespace Oxygen.Company.Domain.Exceptions
{
    using Oxygen.Domain.Common;

    public class InvalidDepartmentException : BaseDomainException
    {
        public InvalidDepartmentException()
        {
        }

        public InvalidDepartmentException(string error) => this.Error = error;
    }
}
