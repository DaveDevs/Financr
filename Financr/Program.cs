using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Financr;
using Financr.Utils;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<MortgageCalculator>();
builder.Services.AddScoped<MortgageGrapher>();
builder.Services.AddScoped<PlotlyGrapher>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();

