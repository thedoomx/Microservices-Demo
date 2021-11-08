namespace Oxygen.Company.Infrastructure.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Oxygen.Infrastructure.Common.Persistence;
    using Oxygen.Company.Domain.Repositories;
    using Domain.Models;
    using Oxygen.Company.Application;
    using Oxygen.Company.Application.Department.Queries.Common;
    using Oxygen.Company.Application.Employee.Queries.Common;
    using Oxygen.Company.Application.JobTitle.Queries.Common;
    using Oxygen.Company.Application.Office.Queries.Common;

    internal class EmployeeRepository : DataRepository<ICompanyDbContext, Employee>,
        IEmployeeDomainRepository,
        IEmployeeQueryRepository
    {
        private readonly IMapper mapper;

        public EmployeeRepository(ICompanyDbContext db, IMapper mapper)
            : base(db)
            => this.mapper = mapper;

        public async Task<EmployeeOutputModel> GetEmployeeDetails(int id, CancellationToken cancellationToken = default)
           => await this.mapper
               .ProjectTo<EmployeeOutputModel>(this
                   .All()
                   .Where(c => c.Id == id))
               .FirstOrDefaultAsync(cancellationToken);

        public async Task<DepartmentOutputModel> GetDepartmentDetails(int id, CancellationToken cancellationToken = default)
           => await this.mapper
               .ProjectTo<DepartmentOutputModel>(this
                    .Data
                    .Departments
                    .Where(c => c.Id == id))
               .FirstOrDefaultAsync(cancellationToken);

        public async Task<OfficeOutputModel> GetOfficeDetails(int id, CancellationToken cancellationToken = default)
           => await this.mapper
               .ProjectTo<OfficeOutputModel>(this
                    .Data
                    .Offices
                    .Where(c => c.Id == id))
               .FirstOrDefaultAsync(cancellationToken);

        public async Task<JobTitleOutputModel> GetJobTitleDetails(int id, CancellationToken cancellationToken = default)
           => await this.mapper
               .ProjectTo<JobTitleOutputModel>(this
                    .Data
                    .JobTitles
                    .Where(c => c.Id == id))
               .FirstOrDefaultAsync(cancellationToken);

        public async Task<IEnumerable<EmployeeOutputModel>> GetEmployees(
           CancellationToken cancellationToken = default)
           => (await this.mapper
               .ProjectTo<EmployeeOutputModel>(this
                   .All())
               .ToListAsync(cancellationToken));

        public async Task<IEnumerable<DepartmentOutputModel>> GetDepartments(
           CancellationToken cancellationToken = default)
           => (await this.mapper
               .ProjectTo<DepartmentOutputModel>(this
                    .Data
                    .Departments
                    .AsQueryable())
               .ToListAsync(cancellationToken));

        public async Task<IEnumerable<JobTitleOutputModel>> GetJobTitles(
           CancellationToken cancellationToken = default)
           => (await this.mapper
               .ProjectTo<JobTitleOutputModel>(this
                    .Data
                    .JobTitles
                    .AsQueryable())
               .ToListAsync(cancellationToken));

        public async Task<IEnumerable<OfficeOutputModel>> GetOffices(
           CancellationToken cancellationToken = default)
           => (await this.mapper
               .ProjectTo<OfficeOutputModel>(this
                    .Data
                    .Offices
                    .AsQueryable())
               .ToListAsync(cancellationToken));
    }
}
