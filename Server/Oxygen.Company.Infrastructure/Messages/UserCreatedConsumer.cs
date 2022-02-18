namespace Oxygen.Company.Infrastructure.Messages
{
    using System.Threading.Tasks;
    using MassTransit;
    using Oxygen.Company.Application;
    using Oxygen.Company.Domain.Factories;
    using Oxygen.Company.Infrastructure.Persistence;
    using Oxygen.Infrastructure.Common.Messages.Users;
    using Oxygen.Infrastructure.Common.Persistence.Models;
    using Oxygen.Infrastructure.Common.Services;

    internal class UserCreatedConsumer : IConsumer<UserCreatedMessage>
    {
        private readonly CompanyDbContext _data;
        private readonly IMessageService _messages;
        private readonly IEmployeeFactory _employeeFactory;
        private readonly IEmployeeQueryRepository _employeeQueryRepository;

        public UserCreatedConsumer(
            CompanyDbContext data,
            IMessageService messages,
            IEmployeeFactory employeeFactory,
            IEmployeeQueryRepository employeeQueryRepository)
        {
            this._data = data;
            this._messages = messages;
            this._employeeFactory = employeeFactory;
            this._employeeQueryRepository = employeeQueryRepository;
        }

        public async Task Consume(ConsumeContext<UserCreatedMessage> context)
        {
            var message = context.Message;

            var isDuplicated = await this._messages.IsDuplicated(
                message,
                nameof(UserCreatedMessage.UserId),
                message.UserId);

            if (isDuplicated)
            {
                return;
            }

            var department = await this._employeeQueryRepository.FindDepartment(message.Department);
            var jobTitle = await this._employeeQueryRepository.FindJobTitle(message.JobTitle);
            var office = await this._employeeQueryRepository.FindOffice(message.Office);

            var employee = this._employeeFactory
                .WithFirstName(message.FirstName)
                .WithSurName(message.SurName)
                .WithLastName(message.LastName)
                .WithDepartment(department)
                .WithJobTitle(jobTitle)
                .WithOffice(office)
                .WithUserId(message.UserId)
                .Build();

            this._data.Employees.Add(employee);

            var dataMessage = new Message(message);

            dataMessage.MarkAsPublished();

            this._data.Messages.Add(dataMessage);

            await this._data.SaveChangesAsync();
        }
    }
}
