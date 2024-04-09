using Space.Client;
using Space.Server.Database.Extensions;
using Space.Server.Extensions;
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

builder.Services.AddNewSpaceDatabaseContext(databaseConnectionString)
    .AddNewSpaceServices()
    .AddNewSpaceSync();

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
app.UseCors(u => u.WithOrigins("https://localhost:44351").AllowAnyHeader().AllowAnyMethod());
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor API V1");
});
app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapRazorComponents<App>().AddInteractiveWebAssemblyRenderMode();
app.MapFallbackToFile("index.html");

app.Run();
