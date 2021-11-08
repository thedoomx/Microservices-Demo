namespace Oxygen.Company.Application.Department.Queries.Common
{
    using AutoMapper;
    using Oxygen.Application.Common.Mapping;
    using Domain.Models;

    public class DepartmentOutputModel : IMapFrom<Department>
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = default!;

        public bool IsActive { get; private set; } = default!;

        public string Description { get; private set; } = default!;

        public virtual void Mapping(Profile mapper)
            => mapper
                .CreateMap<Department, DepartmentOutputModel>();
    }
}
