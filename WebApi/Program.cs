using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.AutoFac;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Concrete.EntityFramework.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); //autofac ayarlamasý
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutoFacBusinessModule()));
//AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);//sonradan ekledim.
// Add services to the container.

IConfiguration configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins("https://localhost:7096"));
});
#region cors açýklamasý
//CORS, web tarayýcýsýnýn bir kaynaðýn (örneðin bir web sitesi) belirli bir kaynaða (örneðin bir API) eriþmesine izin vermek veya engellemek için tarayýcý tarafýndan uygulanan bir güvenlik önlemidir.
//CORS politikalarý, API'lerin veya sunucularýn kaynaklarýna hangi kaynaklardan eriþilebileceðini kontrol etmek için kullanýlýr. Bu þekilde, güvenlik önlemleri alýnýr ve istemcilerin yetkisiz eriþimlerden kaynaklanan güvenlik açýklarýný sömürmesi engellenir.
#endregion
var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>(); //getsection appsetting dosyalarýndaki özelliklere eriþim saðlar.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.WithOrigins("https://localhost:7096").AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
