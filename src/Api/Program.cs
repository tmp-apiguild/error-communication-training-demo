using Common;
using FluentValidation;
using static Common.ValidationError;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.EnableTryItOutByDefault());
}

app.UseHttpsRedirection();
app.UseCors(c =>
{
    c.AllowAnyOrigin();
    c.AllowAnyHeader();
    c.AllowAnyMethod();
});

app.MapPost("/form", (FormRequest request, IValidator<FormRequest> validator) =>
{
    var validation = validator.Validate(request);
    if (!validation.IsValid)
    {
        return Results.BadRequest(new ValidationError
        {
            invalidParameters = validation.Errors.Select(e => new InvalidParameter
            {
                name = e.PropertyName,
                reason = e.ErrorMessage
            })
        });
    }

    return Results.Ok();
});

app.Run();

public class FormRequestValidator : AbstractValidator<FormRequest>
{
    public FormRequestValidator()
    {
        RuleFor(m => m.email)
            .NotEmpty().WithMessage("Can not be empty")
            .EmailAddress().WithMessage("Not a valid email address");

        RuleFor(m => m.height)
            .NotEmpty().WithMessage("Can not be empty")
            .Must(m => int.TryParse(m, out int num)).WithMessage("Must be a number");
    }
}