using Space.Server.AI.Logic;
using Space.Server.Database.Extensions;
using Space.Server.Extensions;
using Space.Server.Pages;
using Space.Server.Services.Extensions;
using Space.Server.Sync.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(NewSpaceMappingProfile));
builder.Services.AddSettings(builder.Configuration);
var databaseConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (databaseConnectionString is null)
    throw new ArgumentNullException($"{nameof(databaseConnectionString)}");

builder.Services.AddNewSpaceDatabaseContext(databaseConnectionString)
    .AddNewSpaceServices()
    .SetupSemanticKernelLogic(builder.Configuration)
    .AddNewSpaceSync()
    .AddMudServices();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(u => u.WithOrigins("https://localhost:44351", "https://localhost:7157").AllowAnyHeader().AllowAnyMethod());
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor API V1");
});
app.UseRouting();
app.UseAntiforgery();

app.MapRazorPages();
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Space.Client.Shared.NavMenu).Assembly);

app.MapFallbackToFile("index.html");

app.Run();
