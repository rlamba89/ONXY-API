using Onyx.Contracts.Data.Entities;
using Onyx.Contracts.Data.Repositories;
using Onyx.Infrastructure.Data.Repositories.Generic;
using Onyx.Migrations;

namespace Onyx.Infrastructure.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DatabaseContext context) : base(context)
        {
        }
    }
}