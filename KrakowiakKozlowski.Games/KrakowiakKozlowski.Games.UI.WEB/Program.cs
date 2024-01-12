using KrakowiakKozlowski.Games.UI.WEB;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using KrakowiakKozlowski.Games.INTERFACES;
using KrakowiakKozlowski.Games.BL;
using KrakowiakKozlowski.Games.DAOSQL;
using Microsoft.EntityFrameworkCore;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddDbContext<DataContext>();

Singleton.SetDataAccess("KrakowiakKozlowski.Games.DAOMOCK.dll");

await builder.Build().RunAsync();
