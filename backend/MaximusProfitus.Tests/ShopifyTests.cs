using NUnit.Framework;
using ShopifySharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MaximusProfitus.Tests;

public class ShopifyTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task CanListProducts()
    {
        var productService = BuildProductService();

        var products = await productService.ListAsync();

        Assert.That(products.Items, Is.Not.Empty);

        Console.WriteLine(string.Join(", ", products.Items.Select(p => p.Title)));
    }

    private static ProductService BuildProductService()
    {
        var myShopifyUrl = "https://quickstart-d97c14a1.myshopify.com";
        var accessToken = "REPLACE-ME";

        return new ProductService(myShopifyUrl, accessToken);
    }
}