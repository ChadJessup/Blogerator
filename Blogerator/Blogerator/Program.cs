using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using MudBlazor;
using MudBlazor.Services;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// per machine configuration, so I can not check in stuff - works with .gitignore.
builder.Configuration.AddJsonFile($"appsettings.{Environment.MachineName}.json", optional: true, reloadOnChange: true);

builder.Services.AddOptions<BlogeratorOptions>();
builder.Services.Configure<BlogeratorOptions>(builder.Configuration);

builder.Services.AddSingleton<BlogeratorContainer>();
builder.Services.AddMudServices();
builder.Services.AddMudMarkdownServices();
builder.Services.AddHostedService<BlogeratorService>();
builder.Services.AddDbContext<BlogeratorDbContext>();

builder.Services
    .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

builder.Services
    .AddControllersWithViews()
    .AddMicrosoftIdentityUI();

builder.Services.AddAuthorization(options =>
{
    var required = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    options.AddPolicy("Required", required);

    // options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/counter");
});

builder.Services.AddServerSideBlazor()
    .AddMicrosoftIdentityConsentHandler();

builder.Services.AddScoped<Radzen.DialogService>();

builder.Services.AddScoped<MudBlazor.DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
