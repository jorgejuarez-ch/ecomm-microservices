namespace Ordering.Application.Orders.Queries.Get
{
    public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        private readonly IApplicationDbContext _dbContext = dbContext;

        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            int pageIndex = query.PaginationRequest.PageIndex;
            int pageSize = query.PaginationRequest.PageSize;

            long totalCount = await _dbContext.Orders.LongCountAsync(cancellationToken);

            List<Order> orders = await _dbContext.Orders
                    .Include(o => o.OrderItems)
                    .OrderBy(o => o.OrderName.Value)
                    .Skip(pageSize * pageIndex)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);

            PaginatedResult<OrderDto> paginatedResult = new PaginatedResult<OrderDto>(pageIndex, pageSize, totalCount, orders.ToOrderDtoList());

            return new GetOrdersResult(paginatedResult);
        }
    }
}
