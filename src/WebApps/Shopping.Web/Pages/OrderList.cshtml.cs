namespace Shopping.Web.Pages
{
    public class OrderListModel(IOrderingService orderingService, ILogger<OrderListModel> logger) : PageModel
    {
        private readonly IOrderingService _orderingService = orderingService;
        private readonly ILogger<OrderListModel> _logger = logger;

        public IEnumerable<OrderModel> Orders { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var customerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa");

            GetOrdersByCustomerResponse response = await _orderingService.GetOrdersByCustomer(customerId);
            Orders = response.Orders;

            return Page();
        }
    }
}
