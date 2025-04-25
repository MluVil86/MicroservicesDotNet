using eCommerce.Core.DTO;
using FluentValidation;

namespace eCommerce.Core.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address format");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("Password cannot be empty");
            

        RuleFor(r => r.PersonName)
            .NotEmpty().WithMessage("Person Name cannot be empty")
            .Length(1,50).WithMessage("Length must be between 1 and 50 characters");

        RuleFor(r => r.Gender)
            .IsInEnum().WithMessage("Invalid gender option");
    }
}




