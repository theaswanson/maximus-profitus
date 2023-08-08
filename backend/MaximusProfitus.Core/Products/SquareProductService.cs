using Square;

namespace MaximusProfitus.Core.Products
{
    public class SquareProductService : IProductService
    {
        private readonly ISquareClient _squareClient;

        public SquareProductService(ISquareClient squareClient)
        {
            _squareClient = squareClient;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var response = await _squareClient.CatalogApi.ListCatalogAsync();

            if (response.Objects is null)
            {
                return Enumerable.Empty<Product>();
            }

            return response.Objects.Select(x => new Product { Name = x.Id });
        }
    }
}
