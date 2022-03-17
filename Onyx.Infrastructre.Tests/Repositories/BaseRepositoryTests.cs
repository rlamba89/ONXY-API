using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Onyx.Migrations;

namespace Onyx.Infrastructure.Tests.Repositories
{
    public class BaseRepositoryTests
    {
        protected DatabaseContext _databaseContext;

        public BaseRepositoryTests()
        {
            _databaseContext = new DatabaseContext(CreateNewContextOptions());

        }
        private DbContextOptions<DatabaseContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                                    .AddEntityFrameworkInMemoryDatabase()
                                    .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<DatabaseContext>();

            builder.UseInMemoryDatabase("Onyx")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}
