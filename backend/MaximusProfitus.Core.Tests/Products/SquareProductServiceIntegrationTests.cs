using MaximusProfitus.Core.Products;
using Microsoft.Extensions.Configuration;
using Square;

namespace MaximusProfitus.Core.Tests.Products;

public class SquareProductServiceIntegrationTests
{
    private SquareProductService _service;

    [SetUp]
    public void Setup()
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<SquareProductServiceIntegrationTests>()
            .Build();

        var accessToken = configuration["Square:AccessToken"];

        var squareClient = new SquareClient.Builder()
            .Environment(Square.Environment.Sandbox)
            .AccessToken(accessToken)
            .Build();

        _service = new SquareProductService(squareClient);
    }

    [Test]
    public async Task ReturnsProductsFromClient()
    {
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