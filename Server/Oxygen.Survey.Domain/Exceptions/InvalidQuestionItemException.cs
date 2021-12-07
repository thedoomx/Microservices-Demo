namespace Oxygen.Survey.Domain.Exceptions
{
    using Oxygen.Domain.Common;

    public class InvalidQuestionItemException : BaseDomainException
    {
        public InvalidQuestionItemException()
        {
        }

        public InvalidQuestionItemException(string error) => this.Error = error;
    }
}
