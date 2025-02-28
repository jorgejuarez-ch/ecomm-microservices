namespace Shopping.Web.Models.Catalog
{
    public record GetProductByCategoryResponse(IEnumerable<ProductModel> Products);
}
