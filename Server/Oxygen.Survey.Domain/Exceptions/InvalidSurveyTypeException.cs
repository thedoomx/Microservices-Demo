namespace Oxygen.Survey.Domain.Exceptions
{
    using Oxygen.Domain.Common;

    public class InvalidSurveyTypeException : BaseDomainException
    {
        public InvalidSurveyTypeException()
        {
        }

        public InvalidSurveyTypeException(string error) => this.Error = error;
    }
}
