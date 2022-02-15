namespace Oxygen.Survey.Domain.Factories
{
	using Oxygen.Domain.Common;
	using Oxygen.Survey.Domain.Models;

	public interface IQuestionItemFactory : IBuild<QuestionItem>
	{
		IQuestionItemFactory WithDescription(string description);
	}
}
