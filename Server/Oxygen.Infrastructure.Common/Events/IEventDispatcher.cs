namespace Oxygen.Infrastructure.Common.Events
{
    using System.Threading.Tasks;
    using Oxygen.Domain.Common;

    internal interface IEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent);
    }
}
