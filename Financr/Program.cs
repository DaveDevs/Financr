using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Financr;
using Financr.Utils;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<PlotlyGrapher>();
builder.Services.AddScoped<LoanCalculator>();
builder.Services.AddScoped<LoanGrapher>();
builder.Services.AddScoped<SavingsCalculator>();
builder.Services.AddScoped<SavingsGrapher>();
builder.Services.AddScoped<PlotlySavingsGrapher>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();

