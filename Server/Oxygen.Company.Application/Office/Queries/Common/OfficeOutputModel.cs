namespace Oxygen.Company.Application.Office.Queries.Common
{
    using AutoMapper;
    using Oxygen.Application.Common.Mapping;
    using Domain.Models;

    public class OfficeOutputModel : IMapFrom<Office>
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = default!;

        public string Address { get; private set; } = default!;

        public virtual void Mapping(Profile mapper)
            => mapper
                .CreateMap<Office, OfficeOutputModel>();
    }
}
