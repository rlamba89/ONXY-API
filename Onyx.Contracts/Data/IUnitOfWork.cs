using Onyx.Contracts.Data.Repositories;

namespace Onyx.Contracts.Data
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        Task CommitAsync();
    }
}