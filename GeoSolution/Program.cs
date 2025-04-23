using GeoSolution.Data;
using GeoSolution.Models;
using GeoSolution.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Prometheus;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = "/app"
});

// Add services to the container.
builder.Services.AddControllersWithViews();
//Add db based on Dbcontext
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddHttpClient();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOpenIdConnect(options =>
{
    options.Authority = "http://keycloak:8080/realms/TestRealm";
    options.ClientId = "aspnet-client";
    options.ClientSecret = "ROS8Svkgo9yYM4IYRwLktPVx2acxvFBg";
    options.ResponseType = OpenIdConnectResponseType.Code;
    options.RequireHttpsMetadata = false; // Для разработки
    options.SaveTokens = true;
});

var app = builder.Build();
//role initialization v1.0
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        //test building
        TestBuildingInitializer.InitializeAsync(services.GetRequiredService<ApplicationDbContext>()).Wait();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapMetrics();//prometheus
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
