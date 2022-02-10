namespace Oxygen.Survey.Domain.Exceptions
{
    using Oxygen.Domain.Common;

    public class InvalidEmployeeSurveyItemException : BaseDomainException
    {
        public InvalidEmployeeSurveyItemException()
        {
        }

        public InvalidEmployeeSurveyItemException(string error) => this.Error = error;
    }
}
