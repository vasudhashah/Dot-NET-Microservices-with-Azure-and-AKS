using eCommerce.Core.DTO;
using FluentValidation;

namespace eCommerce.Core.Validators
{
    public class RegisterRequestValidator: AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            //Email
            RuleFor(temp => temp.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid Email address format");
            //Password
            RuleFor(temp => temp.Password)
                .NotEmpty().WithMessage("Password is required");
            //PersonName
            RuleFor(temp => temp.PersonName)
                .NotEmpty().WithMessage("Person Name is required")
                .Length(1, 50).WithMessage("Person Name length should be between 1 and 50");
            //Gender
            RuleFor(temp => temp.Gender)
                .IsInEnum().WithMessage("Invalid gender option");

        }
    }
}
