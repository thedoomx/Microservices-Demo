namespace Oxygen.Survey.Application.Survey.Queries.Mine
{
	using AutoMapper;
	using Oxygen.Application.Common.Mapping;
	using Domain.Models;
	using Oxygen.Survey.Application.Queries.Common;

	public class MineSurveysOutputModel : IMapFrom<EmployeeSurvey>
	{
		public int Id { get; private set; }

		public bool IsSubmitted { get; set; }

		public SurveyOutputModel Survey { get; set; }

		public virtual void Mapping(Profile mapper)
		{
			mapper
			   .CreateMap<EmployeeSurvey, MineSurveysOutputModel>();

			mapper
			   .CreateMap<Survey, SurveyOutputModel>();
		}

	}
}
