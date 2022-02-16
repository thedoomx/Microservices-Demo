namespace Oxygen.Survey.Domain.Factories
{
	using Oxygen.Domain.Common;
	using Oxygen.Survey.Domain.Models;
	using System;
	using System.Collections.Generic;

	public interface IQuestionFactory : IBuild<Question>
	{
		IQuestionFactory WithDescription(string description);

		IQuestionFactory WithRequired(bool isRequired);

		IQuestionFactory WithQuestionType(string type);

		IQuestionFactory WithQuestionType(QuestionType questionType);

		IQuestionFactory WithQuestionItem(QuestionItem questionItem);

		IQuestionFactory WithQuestionItem(Action<IQuestionItemFactory> questionItem);

		IQuestionFactory WithQuestionItems(IEnumerable<QuestionItem> questionItems);

		IQuestionFactory WithQuestionItems(IEnumerable<Action<IQuestionItemFactory>> questionItems);
	}
}
