using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.AutoFac;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Concrete.EntityFramework.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); //autofac ayarlamas�
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
#region cors a��klamas�
//CORS, web taray�c�s�n�n bir kayna��n (�rne�in bir web sitesi) belirli bir kayna�a (�rne�in bir API) eri�mesine izin vermek veya engellemek i�in taray�c� taraf�ndan uygulanan bir g�venlik �nlemidir.
//CORS politikalar�, API'lerin veya sunucular�n kaynaklar�na hangi kaynaklardan eri�ilebilece�ini kontrol etmek i�in kullan�l�r. Bu �ekilde, g�venlik �nlemleri al�n�r ve istemcilerin yetkisiz eri�imlerden kaynaklanan g�venlik a��klar�n� s�m�rmesi engellenir.
#endregion
var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>(); //getsection appsetting dosyalar�ndaki �zelliklere eri�im sa�lar.

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
