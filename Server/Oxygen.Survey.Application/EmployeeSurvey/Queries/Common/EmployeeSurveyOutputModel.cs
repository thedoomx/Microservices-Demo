namespace Oxygen.Survey.Application.EmployeeSurvey.Queries.Common
{
	using AutoMapper;
	using Oxygen.Application.Common.Mapping;
	using Domain.Models;
	using System.Collections.Generic;
	using Oxygen.Survey.Application.Queries.Common;

	public class EmployeeSurveyOutputModel : IMapFrom<EmployeeSurvey>
	{
		public int Id { get; private set; }

		public SurveyOutputModel Survey { get; set; }

		public IEnumerable<EmployeeSurveyAnswerOutputModel> EmployeeSurveyAnswers { get; set; }

		public virtual void Mapping(Profile mapper)
			=> mapper
				.CreateMap<EmployeeSurvey, EmployeeSurveyOutputModel>();
	}
}
