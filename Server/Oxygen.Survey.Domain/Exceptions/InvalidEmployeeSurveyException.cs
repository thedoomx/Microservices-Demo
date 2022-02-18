namespace Oxygen.Survey.Domain.Exceptions
{
    using Oxygen.Domain.Common;

    public class InvalidEmployeeSurveyException : BaseDomainException
    {
        public InvalidEmployeeSurveyException()
        {
        }

        public InvalidEmployeeSurveyException(string error) => this.Error = error;
    }
}
