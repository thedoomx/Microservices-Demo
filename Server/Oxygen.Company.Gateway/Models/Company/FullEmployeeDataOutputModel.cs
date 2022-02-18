namespace Oxygen.Company.Gateway.Models.Company
{
	using Oxygen.Application.Common.Mapping;

	public class FullEmployeeDataOutputModel : EmployeeOutputModel, IMapFrom<EmployeeOutputModel>
	{
		public string Email { get; set; }

		public int TotalSurveys { get; set; }

		public int SubmittedSurveys { get; set; }
	}
}
