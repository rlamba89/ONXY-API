using Onyx.Contracts.Data;
using Onyx.Contracts.Data.Repositories;
using Onyx.Infrastructure.Data.Repositories;
using Onyx.Migrations;

namespace Onyx.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }
        public IProductRepository Products => new ProductRepository(_context);

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}