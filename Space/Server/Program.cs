using Space.Server.Database.Extensions;
using Space.Server.Extensions;
using Space.Server.Services.Extensions;
using Space.Server.Sync.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();
builder.Services.AddSettings(builder.Configuration);
builder.Services.AddNewSpaceDatabaseContext()
    .AddNewSpaceServices()
    .AddNewSpaceSync();

builder.Configuration.AddJsonFile("appsettings.json");


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
app.MapFallbackToFile("index.html");

app.Run();
