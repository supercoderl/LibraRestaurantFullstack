﻿using System.Collections.Generic;
using System.Text;
using LibraRestaurant.Api.Swagger;
using LibraRestaurant.Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace LibraRestaurant.Api.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "LibraRestaurant",
                Version = "v1",
                Description = "Effective management system for branches and restaurant operations."
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. " +
                              "Use the /api/v1/employee/login endpoint to generate a token",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });

            c.ParameterFilter<SortableFieldsAttributeFilter>();

            c.SupportNonNullableReferenceTypes();

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });
        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        services.AddAuthentication(
                options => { options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; })
            .AddJwtBearer(
                jwtOptions =>
                {
                    jwtOptions.SaveToken = true;
                    jwtOptions.TokenValidationParameters = CreateTokenValidationParameters(configuration);
                });

        services
            .AddOptions<TokenSettings>()
            .Bind(configuration.GetSection("Auth"))
            .ValidateOnStart();

        services
            .AddOptions<GoogleSettings>()
            .Bind(configuration.GetSection("GoogleConfiguration"))
            .ValidateOnStart();

        return services;
    }

    public static TokenValidationParameters CreateTokenValidationParameters(IConfiguration configuration)
    {
        var result = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Auth:Issuer"],
            ValidAudience = configuration["Auth:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    configuration["Auth:Secret"]!)),
            RequireSignedTokens = false
        };

        return result;
    }
}