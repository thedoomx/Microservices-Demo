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
	using Oxygen.Infrastructure.Common.Services;

	internal class EmployeeRepository : DataRepository<ICompanyDbContext, Employee>,
		IEmployeeDomainRepository,
		IEmployeeQueryRepository
	{
		private readonly IMapper mapper;

		public EmployeeRepository(ICompanyDbContext db, IPublisher publisher, IMapper mapper)
			: base(db, publisher)
			=> this.mapper = mapper;

		public async Task<int> GetEmployeeIdByUserId(string userId, CancellationToken cancellationToken = default)
		  => await this
				.All()
				.Where(c => c.UserId == userId)
				.Select(x => x.Id)
				.FirstOrDefaultAsync(cancellationToken);

		public async Task<Employee> FindEmployee(int id, CancellationToken cancellationToken = default)
		   => await this
				.All()
				.Include(x => x.Department)
				.Include(x => x.JobTitle)
				.Include(x => x.Office)
				.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

		public async Task<Department> FindDepartment(int id, CancellationToken cancellationToken = default)
		   => await this
				.Data
				.Departments
				.Where(c => c.Id == id)
				.FirstOrDefaultAsync(cancellationToken);

		public async Task<JobTitle> FindJobTitle(int id, CancellationToken cancellationToken = default)
		   => await this
				.Data
				.JobTitles
				.Where(c => c.Id == id)
				.FirstOrDefaultAsync(cancellationToken);

		public async Task<Office> FindOffice(int id, CancellationToken cancellationToken = default)
		   => await this
				.Data
				.Offices
				.Where(c => c.Id == id)
				.FirstOrDefaultAsync(cancellationToken);

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

		public async Task SaveDepartment(Department department, CancellationToken cancellationToken = default)
		{
			this.Data.Update(department);

			await this.Data.SaveChangesAsync(cancellationToken);
		}

		public async Task SaveJobTitle(JobTitle jobTitle, CancellationToken cancellationToken = default)
		{
			this.Data.Update(jobTitle);

			await this.Data.SaveChangesAsync(cancellationToken);
		}

		public async Task SaveOffice(Office office, CancellationToken cancellationToken = default)
		{
			this.Data.Update(office);

			await this.Data.SaveChangesAsync(cancellationToken);
		}
	}
}
