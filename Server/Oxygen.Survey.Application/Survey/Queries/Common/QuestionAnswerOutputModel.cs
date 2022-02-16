namespace Oxygen.Survey.Application.Survey.Queries.Common
{
	using AutoMapper;
	using Oxygen.Survey.Domain.Models;

	public class QuestionAnswerOutputModel
	{
		public string Description { get; set; } = default!;

		public virtual void Mapping(Profile mapper)
			=> mapper
				.CreateMap<QuestionAnswer, QuestionAnswerOutputModel>();
	}
}
