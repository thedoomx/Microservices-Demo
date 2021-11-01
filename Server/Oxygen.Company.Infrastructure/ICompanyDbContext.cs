namespace Oxygen.Company.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Oxygen.Infrastructure.Common.Persistence;
    using Oxygen.Company.Domain.Models;

    internal interface ICompanyDbContext : IDbContext
    {
        DbSet<Department> Departments { get; }

        DbSet<Employee> Employees { get; }

        DbSet<JobTitle> JobTitles { get; }

        DbSet<Office> Offices { get; }
    }
}
