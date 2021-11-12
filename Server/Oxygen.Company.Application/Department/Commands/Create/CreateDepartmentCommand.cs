namespace Oxygen.Company.Application.Department.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using Oxygen.Application.Common;
    using Oxygen.Application.Common.Contracts;
    using Common;
    using Oxygen.Domain.Common.Models;
    using MediatR;
    using Oxygen.Company.Domain.Repositories;
    using Oxygen.Application.Common.Services.Identity;

    public class CreateDepartmentCommand : DepartmentCommand<CreateDepartmentCommand>, IRequest<Result>
    {
        public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Result>
        {
            private readonly ICurrentUserService currentUser;
            private readonly IEmployeeDomainRepository _employeeRepository;
            private readonly IEmployeeQueryRepository _employeeQueryRepository;
            //private readonly IDepartmentFactory

            public CreateDepartmentCommandHandler(
                ICurrentUserService currentUser,
                IEmployeeDomainRepository employeeRepository,
                IEmployeeQueryRepository employeeQueryRepository)
            {
                this.currentUser = currentUser;
                this._employeeRepository = employeeRepository;
                this._employeeQueryRepository = employeeQueryRepository;
            }

            public async Task<Result> Handle(
                CreateDepartmentCommand request,
                CancellationToken cancellationToken)
            {
                var department = await this._employeeQueryRepository.FindDepartment(
                    request.Id,
                    cancellationToken);

                department
                    .ChangeName(request.Name)
                    .ChangeIsActive(request.IsActive)
                    .ChangeDescription(request.Description);

                await this._employeeRepository.SaveDepartment(department, cancellationToken);

                return Result.Success;
            }
        }
    }
}
