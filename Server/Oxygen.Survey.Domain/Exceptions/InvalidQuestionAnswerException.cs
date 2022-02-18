namespace Oxygen.Survey.Domain.Exceptions
{
    using Oxygen.Domain.Common;

    public class InvalidQuestionAnswerException : BaseDomainException
    {
        public InvalidQuestionAnswerException()
        {
        }

        public InvalidQuestionAnswerException(string error) => this.Error = error;
    }
}
