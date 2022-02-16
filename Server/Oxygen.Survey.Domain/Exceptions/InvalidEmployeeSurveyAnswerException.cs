namespace Oxygen.Survey.Domain.Exceptions
{
    using Oxygen.Domain.Common;

    public class InvalidEmployeeSurveyAnswerException : BaseDomainException
    {
        public InvalidEmployeeSurveyAnswerException()
        {
        }

        public InvalidEmployeeSurveyAnswerException(string error) => this.Error = error;
    }
}
