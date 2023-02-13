using Common;
using FluentValidation;

namespace Function.Api;

public class FormRequestValidator : AbstractValidator<FormRequest>
{
    public FormRequestValidator()
    {
        RuleFor(m => m.email)
            .NotEmpty().WithMessage("Can not be empty")
            .EmailAddress().WithMessage("Not a valid email address");

        RuleFor(m => m.height)
            .NotEmpty().WithMessage("Can not be empty")
            .Must(m => int.TryParse(m, out int num)).WithMessage("Must be a whole number");
    }
}
