namespace Shopping.Web.Pages
{
    public class CheckoutModel(IBasketService basketService, ILogger<ProductModel> logger) : PageModel
    {
        private readonly IBasketService _basketService = basketService;
        private readonly ILogger<ProductModel> _logger = logger;

        [BindProperty]
        public BasketCheckoutModel Order { get; set; } = default!;
        public ShoppingCartModel Cart { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await _basketService.LoadUserBasket();

            return Page();
        }

        public async Task<IActionResult> OnPostCheckoutAsync()
        {
            _logger.LogInformation("Checkout button clicked");

            Cart = await _basketService.LoadUserBasket();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Order.CustomerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa");
            Order.UserName = Cart.UserName;
            Order.TotalPrice = Cart.TotalPrice;

            await _basketService.CheckoutBasket(new CheckoutBasketRequest(Order));

            return RedirectToPage("Confirmation", "OrderSubmitted");
        }
    }
}
