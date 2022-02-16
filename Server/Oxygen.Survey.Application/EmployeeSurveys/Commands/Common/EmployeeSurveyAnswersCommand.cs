namespace Oxygen.Survey.Application.EmployeeSurvey.Commands.Common
{
	using Oxygen.Application.Common;
	using System.Collections.Generic;

	public abstract class EmployeeSurveyAnswersCommand<TCommand> : EntityCommand<int>
		where TCommand : EntityCommand<int>
	{
		public EmployeeSurveyAnswersCommand()
		{
			this.QuestionAnswers = new HashSet<EmployeeSurveyAnswerInputModel>();
		}

		public string EmployeeId { get; set; } = default!;

		public int SurveyId { get; set; }

		public IEnumerable<EmployeeSurveyAnswerInputModel> QuestionAnswers { get; set; }
	}
}
