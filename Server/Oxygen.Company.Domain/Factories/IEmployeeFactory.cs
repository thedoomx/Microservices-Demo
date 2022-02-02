namespace Oxygen.Company.Domain.Factories
{
    using Oxygen.Company.Domain.Models;
    using Oxygen.Domain.Common;

    public interface IEmployeeFactory : IFactory<Employee>
    {
        IEmployeeFactory WithFirstName(string firstName);

        IEmployeeFactory WithSurName(string surName);

        IEmployeeFactory WithLastName(string lastName);

        IEmployeeFactory WithDepartment(Department department);

        IEmployeeFactory WithOffice(Office office);

        IEmployeeFactory WithJobTitle(JobTitle office);

        IEmployeeFactory WithUserId(string userId);
    }
}
