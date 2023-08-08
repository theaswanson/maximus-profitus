using MaximusProfitus.API.Controllers;
using MaximusProfitus.Core.Products;

namespace MaximusProfitus.API.Tests.Controllers
{
    public class ProductControllerTests
    {
        private Mock<IProductService> _productService;
        private ProductController _controller;

        [SetUp]
        public void Setup()
        {
            _productService = new Mock<IProductService>();

            _controller = new ProductController(_productService.Object);
        }

        [Test]
        public async Task ReturnsProductsFromServiceAsync()
        {
            // Arrange
            var products = new[] { new Product() };

            _productService.Setup(s => s.GetAllProductsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(products);

            // Act
            var result = await _controller.GetProducts();

            // Assert
            result.Value.Should().BeEquivalentTo(products);
        }
    }
}