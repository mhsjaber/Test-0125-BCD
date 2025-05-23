using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.OrderApi.Services;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nop.Plugin.Misc.OrderApi.Infrastructure;

public class NopStartup : INopStartup
{
    public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfig = Singleton<AppSettings>.Instance.Get<JwtConfig>();

        services.AddAuthentication()
            .AddJwtBearer("JWT", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey))
                };
            });

        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter your JWT token in the format: Bearer {your token}"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
            c.SwaggerDoc("orders", new OpenApiInfo
            {
                Title = "Public order API",
                Version = "v1"
            });

            c.DocInclusionPredicate((docName, apiDescription) =>
            {
                if (docName == "orders" && apiDescription.GroupName == "ORDERS")
                    return true;

                return false;
            });
            c.ResolveConflictingActions(apiDescriptions =>
            {
                return apiDescriptions.First();
            });
        });

        services.AddScoped<JwtTokenService>();
    }

    public void Configure(IApplicationBuilder application)
    {
        application.UseSwagger();
        application.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/orders/swagger.json", "Public order API V1");
            c.RoutePrefix = "swagger/orders";
        });
    }

    public int Order => 3000;
}
