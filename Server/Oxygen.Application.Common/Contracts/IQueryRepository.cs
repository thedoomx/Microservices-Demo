namespace Oxygen.Application.Common.Contracts
{
    using Oxygen.Domain.Common;

    public interface IQueryRepository<in TEntity>
        where TEntity : IAggregateRoot
    {
    }
}
