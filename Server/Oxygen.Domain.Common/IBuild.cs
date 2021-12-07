namespace Oxygen.Domain.Common
{
    using Oxygen.Domain.Common.Models;

    public interface IBuild<out TEntity>
        where TEntity : IEntity
    {
        TEntity Build();
    }
}
