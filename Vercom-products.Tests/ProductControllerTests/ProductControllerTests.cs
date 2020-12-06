using AutoFixture;
using NSubstitute;
using System.Data;
using System.Web.Http.Results;
using Vercom_products.Controllers;
using Vercom_products.Validators;
using Xunit;

namespace Vercom_products.Tests.ProductControllerTests
{
    public class ProductControllerTests
    {
        private readonly IFixture _fixture;

        public ProductControllerTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void GetProductsByCategory_Returns_Ok_If_Category_Exists()
        {
            // Arrange
            string categoryNameMock = "ProductCategory1";
            var productsValidatorMock = Substitute.For<ProductControllerValidator>();
            var productsControllerMock = new ProductsController(productsValidatorMock);

            // Act
            var result = productsControllerMock.GetProductsByCategory(categoryNameMock);

            // Assert
            var okObjectResult = result as OkNegotiatedContentResult<DataTable>;
            Assert.NotNull(okObjectResult);
        }

        [Fact]
        public void GetProductsByCategory_Returns_BadRequest_If_Category_Doesnt_Exists()
        {
            // Arrange
            string categoryNameMock = _fixture.Create<string>();
            var productsValidatorMock = Substitute.For<ProductControllerValidator>();
            var productsControllerMock = new ProductsController(productsValidatorMock);

            // Act
            var result = productsControllerMock.GetProductsByCategory(categoryNameMock);

            // Assert
            var badRequestResult = result as BadRequestErrorMessageResult;
            Assert.NotNull(badRequestResult);
        }
    }
}
