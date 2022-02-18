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

		IQuestionFactory WithQuestionAnswer(QuestionAnswer questionAnswer);

		IQuestionFactory WithQuestionAnswer(Action<IQuestionAnswerFactory> questionAnswer);

		IQuestionFactory WithQuestionAnswers(IEnumerable<QuestionAnswer> questionAnswers);

		IQuestionFactory WithQuestionAnswers(IEnumerable<Action<IQuestionAnswerFactory>> questionAnswers);
	}
}
