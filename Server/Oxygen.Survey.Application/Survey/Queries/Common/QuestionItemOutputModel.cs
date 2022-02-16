namespace Oxygen.Survey.Application.Survey.Queries.Common
{
	using AutoMapper;
	using Oxygen.Survey.Domain.Models;

	public class QuestionItemOutputModel
	{
		public string Description { get; set; } = default!;

		public virtual void Mapping(Profile mapper)
			=> mapper
				.CreateMap<QuestionItem, QuestionItemOutputModel>();
	}
}
