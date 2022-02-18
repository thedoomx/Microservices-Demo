namespace Oxygen.Survey.Domain.Factories
{
	using Oxygen.Domain.Common;
	using Oxygen.Survey.Domain.Models;

	public interface IQuestionAnswerFactory : IBuild<QuestionAnswer>
	{
		IQuestionAnswerFactory WithDescription(string description);
	}
}
