namespace Ordering.Application.Orders.Queries.GetByCustomer
{
    public class GetOrdersByCustomerHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
    {
        private readonly IApplicationDbContext _dbContext = dbContext;

        public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
        {
            List<Order> orders = await _dbContext.Orders
             .Include(o => o.OrderItems)
             .AsNoTracking()
             .Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
             .OrderBy(o => o.OrderName)
             .ToListAsync(cancellationToken);

            return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
        }
    }
}
