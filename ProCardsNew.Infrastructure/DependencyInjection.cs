﻿using System.Text;
using Amazon.S3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProCardsNew.Application.Common.Interfaces.Authentication;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Application.Common.Interfaces.Services;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Domain.UserAggregate;
using ProCardsNew.Infrastructure.Authentication;
using ProCardsNew.Infrastructure.Persistence;
using ProCardsNew.Infrastructure.Persistence.Interceptors;
using ProCardsNew.Infrastructure.Persistence.Repositories;
using ProCardsNew.Infrastructure.Services;
using ProCardsNew.Infrastructure.Settings;

namespace ProCardsNew.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services
            .AddAuth(configuration)
            .AddSingleton<IRandomNumberGeneratorService, RandomNumberGeneratorServiceService>()
            .AddSingleton<IDateTimeProvider, DateTimeProvider>()
            .AddSettings(configuration)
            .AddPersistence(configuration)
            .AddEmailSender(configuration)
            .AddPasswordRecoveryCodeSettings(configuration)
            .AddLockoutSettings(configuration)
            .AddAWS(configuration);
        
        return services;
    }

    private static IServiceCollection AddAWS(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        
        services.AddDefaultAWSOptions(configuration.GetAWSOptions())
            .AddAWSService<IAmazonS3>();

        return services;
    }

    private static IServiceCollection AddPersistence(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDeckRepository, DeckRepository>();
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IStatisticRepository, StatisticRepository>();

        services.AddDbContext<ProCardsDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("default"));
        });

        return services;
    }

    private static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton<IPasswordHasherService, PasswordHasherService>();
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        
        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });

        return services;
    }

    private static IServiceCollection AddSettings(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var validationSettings = new ValidationSettings();
        configuration.Bind(ValidationSettings.SectionName, validationSettings);
        services.AddSingleton(Options.Create(validationSettings));
        
        return services;
    }

    private static IServiceCollection AddEmailSender(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var emailSettings = new EmailSettings();
        configuration.Bind(EmailSettings.SectionName, emailSettings);
        services.AddSingleton(Options.Create(emailSettings));

        services.AddScoped<IEmailSender, EmailSender>();

        return services;
    }

    private static IServiceCollection AddPasswordRecoveryCodeSettings(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var settings = new PasswordRecoveryCodeSettings();
        configuration.Bind(PasswordRecoveryCodeSettings.SectionName, settings);
        services.AddSingleton(Options.Create(settings));

        return services;
    }

    private static IServiceCollection AddLockoutSettings(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var lockoutSettings = new LockoutSettings();
        configuration.Bind(LockoutSettings.SectionName, lockoutSettings);
        services.AddSingleton(Options.Create(lockoutSettings));

        return services;
    }
}