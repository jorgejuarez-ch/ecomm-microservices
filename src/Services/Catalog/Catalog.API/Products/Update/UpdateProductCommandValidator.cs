namespace Catalog.API.Products.Update
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Product ID is required");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");

            RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}
