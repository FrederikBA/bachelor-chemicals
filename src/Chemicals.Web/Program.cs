using System.Text;
using Chemicals.Core.Interfaces.Repositories;
using Chemicals.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

const string policyName = "AllowOrigin";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
        corsPolicyBuilder =>
        {
            corsPolicyBuilder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

//DBContext
builder.Services.AddDbContext<ChemicalContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"));
});

//Build services

//Build repositories
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

//JWT Key
var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireShippingCompanyAdminRole", policy => policy.RequireRole("ShippingCompanyAdmin"));
    options.AddPolicy("RequireKemiDbUserRole", policy => policy.RequireRole("KemiDbUser"));
    options.AddPolicy("RequireSuperAdminRole", policy => policy.RequireRole("SuperAdmin"));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(policyName);
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
