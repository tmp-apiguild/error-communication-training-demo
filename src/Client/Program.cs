using Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var uri = builder.Configuration["api"];

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(uri) });

await builder.Build().RunAsync();
