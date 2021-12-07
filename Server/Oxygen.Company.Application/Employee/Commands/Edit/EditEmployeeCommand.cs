
namespace Oxygen.Company.Application.Employee.Commands.Edit
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

    public class EditEmployeeCommand : EmployeeCommand<EditEmployeeCommand>, IRequest<Result>
    {
        public class EditEmployeeCommandHandler : IRequestHandler<EditEmployeeCommand, Result>
        {
            private readonly ICurrentUserService currentUser;
            private readonly IEmployeeDomainRepository _employeeRepository;
            private readonly IEmployeeQueryRepository _employeeQueryRepository;

            public EditEmployeeCommandHandler(
                ICurrentUserService currentUser,
                IEmployeeDomainRepository employeeRepository,
                IEmployeeQueryRepository employeeQueryRepository)
            {
                this.currentUser = currentUser;
                this._employeeRepository = employeeRepository;
                this._employeeQueryRepository = employeeQueryRepository;
            }

            public async Task<Result> Handle(
                EditEmployeeCommand request,
                CancellationToken cancellationToken)
            {
                var department = await this._employeeQueryRepository.FindDepartment(
                    request.Department,
                    cancellationToken);

                var jobTitle = await this._employeeQueryRepository.FindJobTitle(
                    request.Department,
                    cancellationToken);

                var office = await this._employeeQueryRepository.FindOffice(
                    request.Department,
                    cancellationToken);

                var employee = await this._employeeQueryRepository
                    .FindEmployee(request.Id, cancellationToken);

                employee
                    .ChangeFirstName(request.FirstName)
                    .ChangeSurName(request.SurName)
                    .ChangeLastName(request.LastName)
                    .ChangeDepartment(department)
                    .ChangeJobTitle(jobTitle)
                    .ChangeOffice(office);

                await this._employeeRepository.Save(employee, cancellationToken);

                return Result.Success;
            }
        }
    }
}
