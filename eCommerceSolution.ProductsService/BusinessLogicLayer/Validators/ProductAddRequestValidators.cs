using BusinessLogicLayer.DTO;
using FluentValidation;

namespace BusinessLogicLayer.Validators
{
    public class ProductAddRequestValidators : AbstractValidator<ProductAddRequest>
    {
        public ProductAddRequestValidators()
        {
            //Product Name
            RuleFor(temp => temp.ProductName).NotEmpty().WithMessage("Product Name cannot be empty");

            //Category
            RuleFor(temp => temp.Category).IsInEnum().WithMessage("Category cannot be blank");

            //UnitPrice
            RuleFor(temp => temp.UnitPrice).InclusiveBetween(0, double.MaxValue).WithMessage($"Unit price should be between 0 to {double.MaxValue}");

            //QuantityInStock
            RuleFor(temp => temp.QuantityInStock).InclusiveBetween(0, int.MaxValue).WithMessage($"Quantity in stock should be between 0 to {int.MaxValue}");
        }
    }
}
