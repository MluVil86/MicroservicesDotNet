using eCommerce.Core.DTO;
using FluentValidation;

namespace eCommerce.Core.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{

    public LoginRequestValidator()
    {
        //Email
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid Email address format");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}
