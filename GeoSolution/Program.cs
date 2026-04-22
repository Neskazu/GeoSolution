using GeoSolution.Data;
using GeoSolution.Options;
using GeoSolution.Services;
using GeoSolution.Services.Email;
using GeoSolution.Services.Messaging;
using GeoSolution.Services.Messaging.Abstractions;
using GeoSolution.Services.Messaging.Handlers;
using GeoSolution.Services.Notification;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using NETCore.MailKit.Core;
using Prometheus;
using RabbitMQ.Client;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = "/app"
});

// Add services to the container.
builder.Services.AddControllersWithViews();
//add rabbitmq with settings
builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.AddSingleton<IConnectionFactory>(sp =>
{
    var opts = sp.GetRequiredService<IOptions<RabbitMqOptions>>().Value;
    return new ConnectionFactory
    {
        HostName = opts.Host,
        Port = opts.Port,
        UserName = opts.Username,
        Password = opts.Password,
        VirtualHost = opts.VirtualHost
    };
});

builder.Services.Configure<DeffaultQueueOptions>(builder.Configuration.GetSection("Queues:DeffaultQueue"));
//handlers
//builder.Services.AddSingleton<UserRegisteredEventHandler>();
//add Producer and Consumer
builder.Services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();
//notification
builder.Services.AddHostedService<KeycloakUserRegistrationListener>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.UseNetTopologySuite()
    )
);
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
    options.Scope.Add("roles");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = "preferred_username",
        RoleClaimType = ClaimTypes.Role
    };
    //fix bug. Info from stack "https://stackoverflow.com/questions/78727298/keycloak-with-asp-net-core-mvc-app-claims-never-contain-roles"
    options.Events = new OpenIdConnectEvents
    {
        OnTokenValidated = ctx =>
        {
            var token = ctx.TokenEndpointResponse?.AccessToken;
            if (string.IsNullOrEmpty(token))
                return Task.CompletedTask;

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

            var realmAccess = jwt.Claims.FirstOrDefault(c => c.Type == "realm_access")?.Value;
            if (realmAccess != null)
            {
                using var doc = JsonDocument.Parse(realmAccess);
                var roles = doc.RootElement.GetProperty("roles").EnumerateArray()
                              .Select(x => x.GetString());

                var id = (ClaimsIdentity)ctx.Principal.Identity;
                foreach (var r in roles)
                    id.AddClaim(new Claim(ClaimTypes.Role, r));
            }

            return Task.CompletedTask;
        }
    };
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
