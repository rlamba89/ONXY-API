using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Onyx.Contracts.Data;
using Onyx.Contracts.Data.Entities;
using Onyx.Contracts.Data.Repositories;
using Onyx.Core.Handlers.Queries;
using Onyx.Core.Mapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Onyx.Core.Tests.Queries
{
    [TestClass]
    public class GetAllProductsQueryHandlerTests
    {
        private Mock<IUnitOfWork> _repository;
        private IMapper _mapper;
        private GetAllProductsQueryHandler _getAllProductsQueryHandler;
        private Mock<IProductRepository> _productRepository;

        [TestInitialize]
        public void Initialize()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            config.AssertConfigurationIsValid();

            _productRepository = new Mock<IProductRepository>();
            _repository = new Mock<IUnitOfWork>();
            _mapper = config.CreateMapper();

            _repository.Setup(a => a.Products).Returns(_productRepository.Object);
            

            _getAllProductsQueryHandler = new GetAllProductsQueryHandler(_repository.Object, _mapper);
        }

        [TestMethod]
        public async Task Handle_ForValidArgs_ShouldRetrunProducts()
        {
            var expectedProducts = GetProducts();
            _repository.Setup(a => a.Products.GetAll()).Returns(expectedProducts);

            var products = await _getAllProductsQueryHandler.Handle(new GetAllProductsQuery(), new CancellationToken());

            Assert.AreEqual(expectedProducts.Count(), products.Count());
        }

        private IEnumerable<Product> GetProducts()
        {
            return new List<Product>
            {
               new Product("Product 1", "Red", "Category 1"),
               new Product("Product 2", "Blue", "Category 2"),
               new Product("Product 2", "Black", "Category 2")
            };
        }
    }
}