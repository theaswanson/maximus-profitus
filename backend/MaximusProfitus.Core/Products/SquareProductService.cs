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

        public async Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default)
        {
            var response = await _squareClient.CatalogApi.ListCatalogAsync(cancellationToken: cancellationToken);

            if (response.Objects is null)
            {
                return Enumerable.Empty<Product>();
            }

            return response.Objects.Select(x => new Product { Name = x.ItemData.Name });
        }
    }
}
