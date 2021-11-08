namespace Oxygen.Company.Application.JobTitle.Queries.Common
{
    using AutoMapper;
    using Oxygen.Application.Common.Mapping;
    using Domain.Models;

    public class JobTitleOutputModel : IMapFrom<JobTitle>
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = default!;

        public virtual void Mapping(Profile mapper)
            => mapper
                .CreateMap<JobTitle, JobTitleOutputModel>();
    }
}
