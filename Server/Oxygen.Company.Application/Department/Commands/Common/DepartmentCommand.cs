namespace Oxygen.Company.Application.Department.Commands.Common
{
    using Oxygen.Application.Common;

    public abstract class DepartmentCommand<TCommand> : EntityCommand<int>
        where TCommand : EntityCommand<int>
    {
        public string Name { get; set; } = default!;

        public bool IsActive { get; set; } = default!;

        public string Description { get; set; } = default!;
    }
}
