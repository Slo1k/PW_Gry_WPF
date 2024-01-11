using KrakowiakKozlowski.Games.UI.WEB;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using KrakowiakKozlowski.Games.DAOMOCK;
using KrakowiakKozlowski.Games.INTERFACES;
using KrakowiakKozlowski.Games.BL;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

Singleton.SetDataAccess("DAOMOCK.dll");
// Add your service registrations here
builder.Services.AddSingleton<IDAO, DAO>();

await builder.Build().RunAsync();
