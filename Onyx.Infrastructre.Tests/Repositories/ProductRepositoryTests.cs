using Microsoft.VisualStudio.TestTools.UnitTesting;
using Onyx.Contracts.Data.Entities;
using Onyx.Infrastructure.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onyx.Infrastructure.Tests.Repositories
{
    [TestClass]
    public class ProductRepositoryTests : BaseRepositoryTests
    {
        private ProductRepository _productRepository;
            
        [TestInitialize]
        public async Task Initialize()
        {
            _productRepository = new ProductRepository(_databaseContext);

            await AddProductsInDatabase();
        }

        
        [TestMethod]
        public void GetAll_ForValidProduct_ShouldReturnProductsList()
        {
            var actualProducts = _productRepository.GetAll();

            Assert.IsNotNull(actualProducts);
            Assert.AreEqual(Products().Count(), actualProducts.Count());
        }

        [TestMethod]
        public void GetWithPredicate_ForValidProduct_ShouldReturnProductList()
        {
            var expectedColour = "Red";
            var actualProducts = _productRepository.Get(p => p.ProductColour == expectedColour);

            Assert.IsNotNull(actualProducts);
            Assert.AreEqual(2, actualProducts.Count());
        }


        private async Task AddProductsInDatabase()
        {
            _databaseContext.Products.RemoveRange(_databaseContext.Products);

            await _databaseContext.Products.AddRangeAsync(Products());
            await _databaseContext.SaveChangesAsync();
        }

        private static IEnumerable<Product> Products()
        {
            return new List<Product>
            {
               new Product("Product 1", "Red", "Category 1"),
               new Product("Product 1.1", "Red", "Category 1.1"),
               new Product("Product 2", "Blue", "Category 2"),
               new Product("Product 2", "Black", "Category 2")
            };
        }

    }
}