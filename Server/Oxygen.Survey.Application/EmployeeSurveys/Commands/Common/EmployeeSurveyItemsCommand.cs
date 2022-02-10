namespace Oxygen.Survey.Application.EmployeeSurvey.Commands.Common
{
	using Oxygen.Application.Common;
	using System.Collections.Generic;

	public abstract class EmployeeSurveyItemsCommand<TCommand> : EntityCommand<int>
		where TCommand : EntityCommand<int>
	{
		public EmployeeSurveyItemsCommand()
		{
			this.QuestionAnswers = new HashSet<EmployeeSurveyItemInputModel>();
		}

		public string EmployeeId { get; set; } = default!;

		public int SurveyId { get; set; }

		public IEnumerable<EmployeeSurveyItemInputModel> QuestionAnswers { get; set; }
	}
}
