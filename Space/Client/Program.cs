using MudBlazor.Services;
using Space.Client.Http.Extensions;
using Space.Client.Logic.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Web;
using Space.Client.AI.Core.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.SetupClient(builder.Configuration);
builder.Services.SetupClientCopilot();
builder.Services
    .AddHttpClient("api_backend", (client) =>
    {
        client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    });

builder.Services.AddApiClients();

await builder.Build().RunAsync();
