namespace Oxygen.Survey.Application.Queries.Common
{
    using AutoMapper;
    using Oxygen.Application.Common.Mapping;
    using Domain.Models;

    public class SurveyOutputModel : IMapFrom<Survey>
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = default!;

        public string Summary { get; private set; } = default!;

        public string SurveyType { get; private set; } = default!;

        public virtual void Mapping(Profile mapper)
            => mapper
                .CreateMap<Survey, SurveyOutputModel>()
                .ForMember(x => x.SurveyType, cfg => cfg
                    .MapFrom(x => x.SurveyType.Name));
    }
}
