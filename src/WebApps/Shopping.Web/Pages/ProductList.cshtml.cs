namespace Shopping.Web.Pages
{
    public class ProductListModel(ICatalogService catalogService, IBasketService basketService, ILogger<ProductListModel> logger) : PageModel
    {
        private readonly ICatalogService _catalogService = catalogService;
        private readonly IBasketService _basketService = basketService;
        private readonly ILogger<ProductListModel> _logger = logger;

        public IEnumerable<string> CategoryList { get; set; } = [];
        public IEnumerable<ProductModel> ProductList { get; set; } = [];

        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string categoryName)
        {
            GetProductsResponse response = await _catalogService.GetProducts();

            CategoryList = response.Products.SelectMany(c => c.Category).Distinct();

            if (!string.IsNullOrEmpty(categoryName))
            {
                ProductList = response.Products.Where(p => p.Category.Contains(categoryName));
                SelectedCategory = categoryName;
            }
            else
            {
                ProductList = response.Products;
            }

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
