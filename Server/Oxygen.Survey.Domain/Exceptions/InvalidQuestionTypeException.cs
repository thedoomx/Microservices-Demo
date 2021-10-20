namespace Oxygen.Survey.Domain.Exceptions
{
    using Oxygen.Domain.Common;

    public class InvalidQuestionTypeException : BaseDomainException
    {
        public InvalidQuestionTypeException()
        {
        }

        public InvalidQuestionTypeException(string error) => this.Error = error;
    }
}
