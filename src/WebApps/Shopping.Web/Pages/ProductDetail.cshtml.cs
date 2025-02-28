namespace Shopping.Web.Pages
{
    public class ProductDetailModel(ICatalogService catalogService, IBasketService basketService, ILogger<ProductDetailModel> logger) : PageModel
    {
        private readonly ICatalogService _catalogService = catalogService;
        private readonly IBasketService _basketService = basketService;
        private readonly ILogger<ProductDetailModel> _logger = logger;

        public ProductModel Product { get; set; } = default!;

        [BindProperty]
        public string Color { get; set; } = default!;

        [BindProperty]
        public int Quantity { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid productId)
        {
            GetProductByIdResponse response = await _catalogService.GetProduct(productId);
            Product = response.Product;

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            _logger.LogInformation("Add to cart button clicked");
            GetProductByIdResponse productResponse = await _catalogService.GetProduct(productId);

            ShoppingCartModel basket = await _basketService.LoadUserBasket();

            basket.Items.Add(new ShoppingCartItemModel
            {
                ProductId = productId,
                ProductName = productResponse.Product.Name,
                Price = productResponse.Product.Price,
                Quantity = 1,
                Color = "Black"
            });

            await _basketService.StoreBasket(new StoreBasketRequest(basket));

            return RedirectToPage("Cart");
        }
    }
}
