namespace Oxygen.Survey.Domain.Exceptions
{
    using Oxygen.Domain.Common;

    public class InvalidUserSurveyException : BaseDomainException
    {
        public InvalidUserSurveyException()
        {
        }

        public InvalidUserSurveyException(string error) => this.Error = error;
    }
}
