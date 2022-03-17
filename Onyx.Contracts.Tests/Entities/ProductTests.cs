using Microsoft.VisualStudio.TestTools.UnitTesting;
using Onyx.Contracts.Data.Entities;
using System;

namespace Onyx.Contracts.Tests.Entities
{
    [TestClass]
    public class ProductTests
    {

        [TestMethod]
        public void Constructor_ForValidArgs_ShouldSetTheProductProperties()
        {
            var expectedProductName = "Product 1";
            var expectedProductColorName = "Red";
            var expectedProductCategory = "Category 1";

            var product = new Product(expectedProductName, expectedProductColorName, expectedProductCategory);

            Assert.AreEqual(expectedProductName, product.ProductName);
            Assert.AreEqual(expectedProductColorName, product.ProductColour);
            Assert.AreEqual(expectedProductCategory, product.ProductCategory);
        }

        [DataRow("", "Red", "Category")]
        [DataRow(null, "Red", "Category")]
        [DataRow(" ", "Red", "Category")]
        [DataRow("Product 1", "", "Category")]
        [DataRow("Product 1", null, "Category")]
        [DataRow("Product 1", " ", "Category")]
        [DataRow("Product 1", "Red", "")]
        [DataRow("Product 1", "Red", null)]
        [DataRow("Product 1", "Red", " ")]
        [DataTestMethod]
        public void Constructor_ForInvalidArgs_ShouldThrowArgumentException(string productName, string productColour, string productCategory)
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Product(productName, productColour, productCategory));
        }
    }
}