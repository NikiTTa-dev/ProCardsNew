using Microsoft.AspNetCore.Mvc.Infrastructure;
using ProCardsNew.Api.Common.Errors;
using ProCardsNew.Api.Common.Mapping;

namespace ProCardsNew.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers()
            .AddNewtonsoftJson();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddSingleton<ProblemDetailsFactory, ProCardsProblemDetailsFactory>();

        services.AddMappings();

        return services;
    }
    
}