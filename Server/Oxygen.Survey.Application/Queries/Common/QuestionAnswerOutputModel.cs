namespace Oxygen.Survey.Application.Queries.Common
{
	using AutoMapper;
	using Oxygen.Application.Common.Mapping;
	using Oxygen.Survey.Domain.Models;

	public class QuestionAnswerOutputModel : IMapFrom<QuestionAnswer>
	{
		public int Id { get; private set; }

		public string Description { get; set; } = default!;

		public virtual void Mapping(Profile mapper)
			=> mapper
				.CreateMap<QuestionAnswer, QuestionAnswerOutputModel>();
	}
}
