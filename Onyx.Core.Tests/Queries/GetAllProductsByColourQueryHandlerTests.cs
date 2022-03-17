using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Onyx.Contracts.Data;
using Onyx.Contracts.Data.Entities;
using Onyx.Contracts.Data.Repositories;
using Onyx.Core.Exceptions;
using Onyx.Core.Handlers.Queries;
using Onyx.Core.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Onyx.Core.Tests.Queries
{
    [TestClass]
    public class GetAllProductsByColourQueryHandlerTests
    {
        private Mock<IUnitOfWork> _repository;
        private IMapper _mapper;
        private GetProductByColourQueryHandler _getAllProductsByColourQueryHandler;
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
            

            _getAllProductsByColourQueryHandler = new GetProductByColourQueryHandler(_repository.Object, _mapper);
        }

        [TestMethod]
        public async Task Handle_ForValidArgs_ShouldRetrunProducts()
        {
            var expectedProducts = GetProducts();
            _repository.Setup(a => a.Products.Get(It.IsAny<Func<Product, bool>>())).Returns((Func<Product, bool> expr) => expectedProducts);

            var products = await _getAllProductsByColourQueryHandler.Handle(new GetProductByColourQuery("Red"), new CancellationToken());

            Assert.AreEqual(expectedProducts.Count(), products.Count());
        }


        [TestMethod]
        public async Task Handle_ForNonValidArgs_ShouldThrowEntityNotFoundException()
        {
            await Assert.ThrowsExceptionAsync<EntityNotFoundException>(() =>  _getAllProductsByColourQueryHandler.Handle(new GetProductByColourQuery("Red"), new CancellationToken()));
        }

        private IEnumerable<Product> GetProducts()
        {
            return new List<Product>
            {
               new Product("Product 1", "Red", "Category 1")
            };
        }
    }
}