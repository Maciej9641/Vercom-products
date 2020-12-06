using Xunit;
using AutoFixture;
using Vercom_products.Validators;

namespace Vercom_products.Tests.ProductValidatorTests
{
    public class ProductValidatorTests
    {
        private readonly IFixture _fixture;

        public ProductValidatorTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ValidateIfProductExists_Returns_False_If_Category_Doesnt_Exists()
        {
            // Arrange
            string categoryNameMock = _fixture.Create<string>();
            var productControllerValidatorMock = _fixture.Create<ProductControllerValidator>();

            // Act
            var result = productControllerValidatorMock.ValidateIfProductCategoryExists(categoryNameMock);

            // Assert
            Assert.False(result);
        }
    }
}
