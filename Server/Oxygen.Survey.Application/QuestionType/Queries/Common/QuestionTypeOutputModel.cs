namespace Oxygen.Survey.Application.QuestionType.Queries.Common
{
    using AutoMapper;
    using Oxygen.Application.Common.Mapping;
    using Domain.Models;

    public class QuestionTypeOutputModel : IMapFrom<QuestionType>
    {
        public int Id { get; private set; }

        public string Type { get; private set; } = default!;

        public virtual void Mapping(Profile mapper)
            => mapper
                .CreateMap<QuestionType, QuestionTypeOutputModel>();
    }
}
