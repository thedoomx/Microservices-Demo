namespace Oxygen.Survey.Application.Survey.Queries.GetSubmit
{
	using AutoMapper;
	using Oxygen.Survey.Application.Queries.Common;
	using Oxygen.Survey.Application.Survey.Queries.Common;
	using Oxygen.Survey.Domain.Models;
	using System.Collections.Generic;

	public class GetSubmitSurveyOutputModel : SurveyOutputModel
	{
		public GetSubmitSurveyOutputModel()
		{
			this.Questions = new HashSet<QuestionOutputModel>();
		}

		public IEnumerable<QuestionOutputModel> Questions { get; set; }

		public new virtual void Mapping(Profile mapper)
		{
			mapper
				.CreateMap<Survey, GetSubmitSurveyOutputModel>()
				.ForMember(x => x.SurveyType, cfg => cfg
					.MapFrom(x => x.SurveyType.Name));

			mapper
				.CreateMap<Question, QuestionOutputModel>()
				.ForMember(x => x.QuestionType, cfg => cfg
					.MapFrom(x => x.QuestionType.Type));

			mapper
				.CreateMap<QuestionAnswer, QuestionAnswerOutputModel>();
		}
			
	}
}
