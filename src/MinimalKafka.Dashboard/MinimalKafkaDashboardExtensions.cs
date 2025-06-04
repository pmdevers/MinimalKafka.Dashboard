using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MinimalKafka.Dashboard;

namespace MinimalKafka;

public static class MinimalKafkaDashboardExtensions
{
    public static IAddKafkaBuilder WithDashboard<T>(this T builder, Action<KafkaDashboardUIOptions>? options = null)
        where T : IAddKafkaBuilder
    {
        var o = new KafkaDashboardUIOptions();
        options?.Invoke(o);

        builder.Services.AddSingleton(o);

        return builder;
    }

    public static IApplicationBuilder UseKafkaDashboard(this IApplicationBuilder app)
    {
        app.UseMiddleware<KafkaDashboardApi>();
        app.UseMiddleware<KafkaDashboardUI>();
        return app;
    }
}
