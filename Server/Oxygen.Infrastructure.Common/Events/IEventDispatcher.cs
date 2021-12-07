namespace Oxygen.Infrastructure.Common.Events
{
    using System.Threading.Tasks;
    using Oxygen.Domain.Common;

    public interface IEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent);
    }
}
