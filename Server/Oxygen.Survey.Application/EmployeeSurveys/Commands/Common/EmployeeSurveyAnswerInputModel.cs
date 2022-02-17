namespace Oxygen.Survey.Application.EmployeeSurvey.Commands.Common
{
    public class EmployeeSurveyAnswerInputModel
    {
        public int QuestionId { get; set; }

        public int? QuestionAnswerId { get; set; }

		public string TextValue { get; set; }

		public bool? BoolValue { get; set; }
	}
}
