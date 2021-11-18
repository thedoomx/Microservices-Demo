namespace Oxygen.Survey.Domain.Exceptions
{
    using Oxygen.Domain.Common;

    public class InvalidUserSurveyItemException : BaseDomainException
    {
        public InvalidUserSurveyItemException()
        {
        }

        public InvalidUserSurveyItemException(string error) => this.Error = error;
    }
}
