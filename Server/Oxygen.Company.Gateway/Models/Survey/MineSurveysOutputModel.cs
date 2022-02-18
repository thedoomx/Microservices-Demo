namespace Oxygen.Company.Gateway.Models.Survey
{
	public class MineSurveysOutputModel
	{
		public int Id { get; set; }

		public bool IsSubmitted { get; set; }

		public SurveyOutputModel Survey { get; set; }
	}
}
