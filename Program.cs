using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using myApp;
using MyApp.Services; // Import the namespace where AnimeService is located
using System;
using System.Net.Http;
using System.Threading.Tasks;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register AnimeService as a scoped service
builder.Services.AddScoped<AnimeService>();

await builder.Build().RunAsync();
