using System;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MaximusProfitus.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public async Task Test1()
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync("https://quickstart-d97c14a1.myshopify.com/admin/api/2023-07/products.json");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseString);
    }
}