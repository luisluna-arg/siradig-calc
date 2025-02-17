using Microsoft.Extensions.DependencyInjection;
using SiradigCalc.ApiFramework.Common;
using SiradigCalc.Application.Converters;
using SiradigCalc.Application.Converters.Strategies;
using SiradigCalc.Application.Helpers.Reducers;
using SiradigCalc.Application.Mapping;
using Microsoft.AspNetCore.Cors;
using SiradigCalc.ApiFramework.Config;

namespace SiradigCalc.ApiFramework.Core.Config;

public static class ConfigExtensions
{
    public static void AddRecordConverters(this IServiceCollection services)
    {
        services.AddScoped<IRecordConverter, RecordConverter>();

        var mainStrategyInterfaceType = typeof(IRecordConverterStrategy);
        var converterAssembly = mainStrategyInterfaceType.Assembly;
        var strategyTypes = converterAssembly.GetTypes()
            .Where(t =>
                t.Namespace == mainStrategyInterfaceType.Namespace &&
                t.GetAllInterfaces().Any(i => i.Name.StartsWith(mainStrategyInterfaceType.Name)))
                .ToArray();

        foreach (var strategyInterfaceType in strategyTypes.Where(t => t.IsInterface))
        {
            var concreteStrategies = strategyTypes.Where(t => t.GetAllInterfaces().Any(i => i.Name.StartsWith(strategyInterfaceType.Name))).ToArray();

            if (concreteStrategies.Length > 1)
            {
                throw new TypeLoadException($"More than one service exists for interface {strategyInterfaceType.FullName}");
            }

            if (concreteStrategies.Length > 0)
            {
                services.AddScoped(strategyInterfaceType, concreteStrategies[0]);
                services.AddScoped(mainStrategyInterfaceType, concreteStrategies[0]);
            }
        }
    }

    public static void AddParsers(this IServiceCollection services)
    {
        var mainStrategyInterfaceType = typeof(IParserStrategy);
        var converterAssembly = mainStrategyInterfaceType.Assembly;
        var strategyTypes = converterAssembly.GetTypes()
            .Where(t =>
                t.Namespace == mainStrategyInterfaceType.Namespace &&
                t.GetAllInterfaces().Any(i => i.Name.StartsWith(mainStrategyInterfaceType.Name)))
                .ToArray();

        foreach (var strategyInterfaceType in strategyTypes.Where(t => t.IsInterface && !t.ContainsGenericParameters))
        {
            var concreteStrategies = strategyTypes.Where(t => t.GetAllInterfaces().Any(i => i.Name.StartsWith(strategyInterfaceType.Name))).ToArray();

            if (concreteStrategies.Length > 1)
            {
                throw new TypeLoadException($"More than one service exists for interface {strategyInterfaceType.FullName}");
            }

            if (concreteStrategies.Length > 0)
            {
                services.AddScoped(strategyInterfaceType, concreteStrategies[0]);
                services.AddScoped(mainStrategyInterfaceType, concreteStrategies[0]);
            }
        }
    }

    public static void AddDtoMappers(this IServiceCollection services)
    {
        services.AddScoped<IDtoMappingService, DtoMappingService>();
    }

    public static void EnableCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(Policies.AllowAll,
                policy => policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
    }
}