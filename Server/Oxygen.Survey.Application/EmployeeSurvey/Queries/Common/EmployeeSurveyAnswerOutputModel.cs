﻿namespace Oxygen.Survey.Application.EmployeeSurvey.Queries.Common
{
	using AutoMapper;
	using Oxygen.Application.Common.Mapping;
	using Domain.Models;

	public class EmployeeSurveyAnswerOutputModel : IMapFrom<EmployeeSurveyAnswer>
	{
		public EmployeeQuestionOutputModel Question { get; private set; }

		public QuestionAnswer QuestionAnswer { get; private set; }

		public string TextValue { get; private set; }

		public bool? BoolValue { get; private set; }

		public virtual void Mapping(Profile mapper)
		{
			mapper
				.CreateMap<EmployeeSurveyAnswer, EmployeeSurveyAnswerOutputModel>();

			mapper
				.CreateMap<Question, EmployeeQuestionOutputModel>()
				.ForMember(x => x.QuestionType, cfg => cfg
					.MapFrom(x => x.QuestionType.Type));
		}
	}
}
