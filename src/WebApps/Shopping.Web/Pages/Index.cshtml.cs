using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.Web.Pages
{
    public class IndexModel(ICatalogService catalogService, IBasketService basketService, ILogger<IndexModel> logger) : PageModel
    {
        private readonly ILogger<IndexModel> _logger = logger;
        private readonly ICatalogService _catalogService = catalogService;
        private readonly IBasketService _basketService = basketService;

        public IEnumerable<ProductModel> ProductList { get; set; } = new List<ProductModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            _logger.LogInformation("Index page visited");
            GetProductsResponse result = await _catalogService.GetProducts();
            //GetProductsResponse result = await _catalogService.GetProducts(2, 3);
            ProductList = result.Products;

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            _logger.LogInformation("Add to cart clicked");

            GetProductByIdResponse producttResponse = await _catalogService.GetProduct(productId);
            var basket = await _basketService.LoadUserBasket();

            basket.Items.Add(new ShoppingCartItemModel
            {
                ProductId = productId,
                ProductName = producttResponse.Product.Name,
                Price = producttResponse.Product.Price,
                Quantity = 1,
                Color = "Black"
            });

            await _basketService.StoreBasket(new StoreBasketRequest(basket));

            return RedirectToPage("Cart");
        }
    }
}
