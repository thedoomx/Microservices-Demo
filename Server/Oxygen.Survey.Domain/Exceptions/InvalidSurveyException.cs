namespace Oxygen.Survey.Domain.Exceptions
{
    using Oxygen.Domain.Common;

    public class InvalidSurveyException : BaseDomainException
    {
        public InvalidSurveyException()
        {
        }

        public InvalidSurveyException(string error) => this.Error = error;
    }
}
