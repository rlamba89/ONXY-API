using Microsoft.EntityFrameworkCore;
using Onyx.Migrations;
using Onyx.Contracts.Data.Repositories;

namespace Onyx.Infrastructure.Data.Repositories.Generic
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DatabaseContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> Get(Func<T, bool> func)
        {
            return _dbSet.Where(func);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }
    }
}