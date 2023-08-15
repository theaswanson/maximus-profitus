using MaximusProfitus.Core.Products;
using Square;
using Square.Apis;
using Square.Models;

namespace MaximusProfitus.Core.Tests.Products;

public class SquareProductServiceTests
{
    private Mock<ISquareClient> _squareClient;
    private SquareProductService _service;

    [SetUp]
    public void Setup()
    {
        _squareClient = new Mock<ISquareClient>();

        _service = new SquareProductService(_squareClient.Object);
    }

    [Test]
    public async Task ReturnsProductsFromClient()
    {
        // Arrange
        var catalogApi = new Mock<ICatalogApi>();

        catalogApi.Setup(c => c.ListCatalogAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ListCatalogResponse(objects: new List<CatalogObject>
            {
                new CatalogObject(default, default, itemData: new CatalogItem(name: "ABC123"))
            }));

        _squareClient.SetupGet(c => c.CatalogApi).Returns(catalogApi.Object);

        // Act
        var result = await _service.GetAllProductsAsync();

        // Assert
        result.Should().BeEquivalentTo(new[]
        {
            new Product
            {
                Name = "ABC123"
            }
        });
    }
}