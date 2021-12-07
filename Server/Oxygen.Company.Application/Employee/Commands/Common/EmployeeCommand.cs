namespace Oxygen.Company.Application.Employee.Commands.Common
{
    using Oxygen.Application.Common;

    public abstract class EmployeeCommand<TCommand> : EntityCommand<int>
        where TCommand : EntityCommand<int>
    {
        public string FirstName { get; set; } = default!;

        public string SurName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public int Department { get; set; }

        public int JobTitle { get; set; }

        public int Office { get; set; }
    }
}
