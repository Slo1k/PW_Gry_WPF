using GamesCatalog;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using KrakowiakKozlowski.GamesCatalog.DAOMock;
using KrakowiakKozlowski.GamesCatalog.INTERFACES;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Add your service registrations here
builder.Services.AddSingleton<IDAO, DAOMock>();

await builder.Build().RunAsync();
