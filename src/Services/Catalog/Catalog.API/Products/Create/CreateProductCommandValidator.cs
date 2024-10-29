namespace Catalog.API.Products.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(p => p.Categories).NotEmpty().WithMessage("Category is required");
            RuleFor(p => p.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}
