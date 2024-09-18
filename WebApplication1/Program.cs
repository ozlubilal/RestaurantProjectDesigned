using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encyption;
using Core.Utilities.Security.Jwt;
using DataAccess.Concretes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantWepApp.Middlewares;

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
    });

//// Session kullan�m� eklenmi�se bu k�sm� ekleyin
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30); // �rne�in 30 dakika
//});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    //   pattern: "{controller=Account}/{action=Login}/{id?}");
    pattern: "{controller=Category}/{action=Index}/{id?}");

app.Run();
