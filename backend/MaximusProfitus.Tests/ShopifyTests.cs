using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using ShopifySharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MaximusProfitus.Tests;

public class ShopifyTests
{
    private IConfigurationRoot _configuration;

    [SetUp]
    public void Setup()
    {
        _configuration = new ConfigurationBuilder()
            .AddUserSecrets<ShopifyTests>()
            .Build();
    }

    [Test]
    public async Task CanListProducts()
    {
        var productService = BuildProductService();

        var products = await productService.ListAsync();

        Assert.That(products.Items, Is.Not.Empty);

        Console.WriteLine(string.Join(", ", products.Items.Select(p => p.Title)));
    }

    private ProductService BuildProductService()
    {
        var myShopifyUrl = _configuration["Shopify:MyShopifyUrl"];
        var accessToken = _configuration["Shopify:AccessToken"];

        return new ProductService(myShopifyUrl, accessToken);
    }
}