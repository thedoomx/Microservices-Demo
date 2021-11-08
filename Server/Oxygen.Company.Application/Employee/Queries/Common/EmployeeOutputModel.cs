namespace Oxygen.Company.Application.Employee.Queries.Common
{
    using AutoMapper;
    using Oxygen.Application.Common.Mapping;

    public class EmployeeOutputModel : IMapFrom<Domain.Models.Employee>
    {
        public int Id { get; private set; }

        public string FirstName { get; private set; } = default!;

        public string SurName { get; private set; } = default!;

        public string LastName { get; private set; } = default!;

        public string Department { get; private set; } = default!;

        public string Office { get; private set; } = default!;

        public string JobTitle { get; private set; } = default!;

        public virtual void Mapping(Profile mapper)
            => mapper
                .CreateMap<Domain.Models.Employee, EmployeeOutputModel>()
                .ForMember(x => x.Department, cfg => cfg
                    .MapFrom(x => x.Department.Name))
                .ForMember(x => x.Office, cfg => cfg
                    .MapFrom(x => x.Office.Name))
                .ForMember(x => x.JobTitle, cfg => cfg
                    .MapFrom(x => x.JobTitle.Name));
    }
}
