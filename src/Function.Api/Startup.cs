using FluentValidation;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Function.Api.Startup))]

namespace Function.Api;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblyContaining<Startup>();
    }
}
