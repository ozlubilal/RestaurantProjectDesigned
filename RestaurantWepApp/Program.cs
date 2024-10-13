using Autofac.Extensions.DependencyInjection;
using Autofac;
using Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encyption;
using DataAccess.Concretes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantWepApp.Middlewares;
using Core.Utilities.Security.Jwt;

var builder = WebApplication.CreateBuilder(args);

// MVC ve Endpoint API gezgini
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();

// Token ayarlar�
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

// JWT Bearer Authentication yap�land�rmas�
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };

        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                // Yetkilendirme yap�lmam��sa Login sayfas�na y�nlendirme
                if (!context.Response.HasStarted)
                {
                    context.Response.Redirect("/Account/Login");
                }
                context.HandleResponse(); // Varsay�lan yan�t� engelle
                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                // Hata durumunda yap�lacak i�lemler
                context.NoResult();
                context.Response.StatusCode = 500;
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync(context.Exception.ToString());
            },
        };
    });

// DbContext yap�land�rmas�
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Autofac yap�land�rmas�
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutofacBusinessModule());
});
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Core mod�lleri
builder.Services.AddDependencyResolvers(new ICoreModule[] {
    new CoreModule()
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseMiddleware<JwtMiddleware>();

// Hata durumlar�nda sayfa y�nlendirmesi
app.UseStatusCodePages(async context =>
{
    if (context.HttpContext.Response.StatusCode == 403)
    {
        context.HttpContext.Response.Redirect("/Account/Login");
    }
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
