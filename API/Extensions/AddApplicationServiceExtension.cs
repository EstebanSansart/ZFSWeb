namespace API.Extensions;

using System.Text;
using Application.UnitOfWork;
/*  using API.Helpers;
using API.Services;
using Aplicacion.UnitOfWork; */
using AspNetCoreRateLimit;
using Domain.Entities;
using Domain.Interfaces;
//using Dominio.Interfaces;
 using Microsoft.AspNetCore.Authentication.JwtBearer;
 using Microsoft.AspNetCore.Identity;
 using Microsoft.AspNetCore.Mvc;
 using Microsoft.AspNetCore.Mvc.Versioning;
 using Microsoft.IdentityModel.Tokens;

     public static class AddApplicationServiceExtension
     {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyHeader()
                            .AllowAnyOrigin()
                            .AllowAnyMethod();
                });
            });
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
 
        public static void ConfigureRateLimiting(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddInMemoryRateLimiting();
            services.Configure<IpRateLimitOptions>(options =>
            {
                options.EnableEndpointRateLimiting = true;
                options.StackBlockedRequests = false;
                options.HttpStatusCode = 429; // El usuario ha hecho demasiadas solicitudes
                options.RealIpHeader = "X-Real-IP";
                options.GeneralRules = new List<RateLimitRule>
                {
                    new() {
                        Endpoint = "*",
                        Period = "1s",
                        Limit = 150
                    }
                };
            });
        }

        public static void ConfigureApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("v"),
                    new HeaderApiVersionReader("X-Version")
                );
            });
        }

    /*    public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuration from AppSettings
            services.Configure<JWT>(configuration.GetSection("JWT"));

            // Adding Authentication - JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                };
            });
        } */
     }
 