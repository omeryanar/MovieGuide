using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MovieGuide.Common;
using MovieGuide.WebApp;
using MovieGuide.WebApp.Shared;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();

builder.Services.AddHttpClient<TmdbService>(client => { client.BaseAddress = new Uri("https://api.themoviedb.org/"); });
builder.Services.AddHttpClient<CacheService>(client => { client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); });

WebAssemblyHost host = builder.Build();
await host.InitializeCulture();
await host.RunAsync();
