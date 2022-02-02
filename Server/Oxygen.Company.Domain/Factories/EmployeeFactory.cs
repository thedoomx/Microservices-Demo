using Oxygen.Company.Domain.Exceptions;
using Oxygen.Company.Domain.Models;

namespace Oxygen.Company.Domain.Factories
{
    internal class EmployeeFactory : IEmployeeFactory
    {
        private Department department = default!;
        private bool departmentSet = false;

        private JobTitle jobTitle = default!;
        private bool jobTitleSet = false;

        private Office office = default!;
        private bool officeSet = false;

        private string employeeFirstName = default!;
        private string employeeSurName = default!;
        private string employeeLastName = default!;
        private string employeeUserId = default!;

        public IEmployeeFactory WithFirstName(string firstName)
        {
            this.employeeFirstName = firstName;

            return this;
        }

        public IEmployeeFactory WithSurName(string surName)
        {
            this.employeeSurName = surName;

            return this;
        }

        public IEmployeeFactory WithLastName(string lastName)
        {
            this.employeeLastName = lastName;

            return this;
        }

        public IEmployeeFactory WithDepartment(string name, bool isActive, string description) 
            => this.WithDepartment(new Department(name, isActive, description));

        public IEmployeeFactory WithDepartment(Department department)
        {
            this.department = department;
            this.departmentSet = true;

            return this;
        }

        public IEmployeeFactory WithOffice(string name, string address)
            => this.WithOffice(new Office(name, address));

        public IEmployeeFactory WithOffice(Office office)
        {
            this.office = office;
            this.officeSet = true;

            return this;
        }

        public IEmployeeFactory WithJobTitle(string name)
            => this.WithJobTitle(new JobTitle(name));

        public IEmployeeFactory WithJobTitle(JobTitle office)
        {
            this.jobTitle = office;
            this.jobTitleSet = true;

            return this;
        }

        public IEmployeeFactory WithUserId(string userId)
        {
            this.employeeUserId = userId;

            return this;
        }

        public Employee Build()
        {
            if (!this.departmentSet)
            {
                throw new InvalidEmployeeException("Department must have a value.");
            }

            if (!this.officeSet)
            {
                throw new InvalidEmployeeException("Office must have a value.");
            }

            if (!this.jobTitleSet)
            {
                throw new InvalidEmployeeException("Job title must have a value.");
            }

            return new Employee(
                this.employeeFirstName,
                this.employeeSurName,
                this.employeeLastName,
                this.department,
                this.office,
                this.jobTitle,
                this.employeeUserId);
        }
    }
}
