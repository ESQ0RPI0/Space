using Space.Registration.DataBase.Extentions;
using Space.Server.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorPages();
builder.Configuration.AddJsonFile("appsettings.json", false, true);
var conStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddRegistrationContext(conStr);
builder.Services.AddRegistrationServices();
builder.Services.AddGeneralServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy( "SpacePolicy",
        builder =>
        {
            builder.WithOrigins("https://localhost:44351")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
        });
});

var app = builder.Build();

app.UseCors("SpacePolicy");

app.MapGet("/", () => "Hello World!");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); 
});

app.Run();
