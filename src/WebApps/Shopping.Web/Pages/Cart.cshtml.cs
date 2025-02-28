namespace Shopping.Web.Pages
{
    public class CartModel(IBasketService basketService, ILogger<CartModel> logger) : PageModel
    {
        private readonly IBasketService _basketService = basketService;
        private readonly ILogger<CartModel> _logger = logger;

        public ShoppingCartModel Cart { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await _basketService.LoadUserBasket();

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(Guid productId)
        {
            _logger.LogInformation("Remove from cart clicked");
            Cart = await _basketService.LoadUserBasket();

            Cart.Items.RemoveAll(item => item.ProductId == productId);

            await _basketService.StoreBasket(new StoreBasketRequest(Cart));

            return RedirectToPage();
        }
    }
}
