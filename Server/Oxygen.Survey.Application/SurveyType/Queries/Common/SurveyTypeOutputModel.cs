namespace Oxygen.Survey.Application.SurveyType.Queries.Common
{
    using AutoMapper;
    using Oxygen.Application.Common.Mapping;
    using Domain.Models;

    public class SurveyTypeOutputModel : IMapFrom<SurveyType>
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = default!;

        public virtual void Mapping(Profile mapper)
            => mapper
                .CreateMap<SurveyType, SurveyTypeOutputModel>();
    }
}
