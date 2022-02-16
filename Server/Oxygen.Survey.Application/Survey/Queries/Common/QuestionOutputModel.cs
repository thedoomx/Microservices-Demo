namespace Oxygen.Survey.Application.Survey.Queries.Common
{
	using AutoMapper;
	using Oxygen.Survey.Domain.Models;
    using System.Collections.Generic;

    public class QuestionOutputModel
	{
        public QuestionOutputModel()
        {
            this.QuestionItems = new HashSet<QuestionItemOutputModel>();
        }

        public string Description { get; set; } = default!;

        public bool IsRequired { get; set; }

        public string QuestionType { get; set; }

        public IEnumerable<QuestionItemOutputModel> QuestionItems { get; set; }

        public virtual void Mapping(Profile mapper)
            => mapper
                .CreateMap<Question, QuestionOutputModel>()
                .ForMember(x => x.QuestionType, cfg => cfg
                    .MapFrom(x => x.QuestionType.Type));
    }
}
