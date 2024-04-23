using System.Text;
using Chemicals.Core.Interfaces.DomainServices;
using Chemicals.Core.Interfaces.Integration;
using Chemicals.Core.Interfaces.Repositories;
using Chemicals.Core.Services.DomainServices;
using Chemicals.Core.Services.IntegrationServices;
using Chemicals.Infrastructure.Data;
using Chemicals.Web.Interfaces;
using Chemicals.Web.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.Integration.Configuration;

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
    options.UseSqlServer(Config.ConnectionStrings.ShwChemicals);
});

//Build repositories
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfReadRepository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));


//Build services
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IProductWsIntegrationService, ProductWsIntegrationService>();

builder.Services.AddScoped<IProductViewModelService, ProductViewModelService>();

//JWT Key
var key = Encoding.UTF8.GetBytes(Config.Authorization.JwtKey);

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
    options.AddPolicy(Config.Authorization.Policies.RequireShippingCompanyAdminRole, policy => policy.RequireRole(Config.Authorization.Roles.ShippingCompanyAdmin));
    options.AddPolicy(Config.Authorization.Policies.RequireKemiDbUserRole, policy => policy.RequireRole(Config.Authorization.Roles.KemiDbUser));
    options.AddPolicy(Config.Authorization.Policies.RequireSuperAdminRole, policy => policy.RequireRole(Config.Authorization.Roles.SuperAdmin));
    options.AddPolicy(Config.Authorization.Policies.RequireIntegrationPolicy, policy => policy.RequireRole(Config.Authorization.Roles.IntegrationPolicy));
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
