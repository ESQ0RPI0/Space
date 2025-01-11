using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Space.FrontHttpClient.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services
    .AddHttpClient("api_backend", (client) =>
    {
        client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    });

builder.Services.AddApiClients();

await builder.Build().RunAsync();
