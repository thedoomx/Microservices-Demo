namespace Oxygen.Survey.Domain.Exceptions
{
    using Oxygen.Domain.Common;

    public class InvalidQuestionException : BaseDomainException
    {
        public InvalidQuestionException()
        {
        }

        public InvalidQuestionException(string error) => this.Error = error;
    }
}
