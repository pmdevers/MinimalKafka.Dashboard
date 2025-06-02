using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MinimalKafka.Dashboard.UI;

namespace MinimalKafka;

public static class MinimalKafkaDashboardExtensions
{
    public static IAddKafkaBuilder WithDashboard<T>(this T builder, Action<KafkaDashboardUIOptions> options)
        where T : IAddKafkaBuilder
    {
        builder.Services.AddOptions<KafkaDashboardUIOptions>()
            .BindConfiguration("KafkaDashboard")
            .PostConfigure(options);

        return builder;
    }

    public static IApplicationBuilder UseKafkaDashboard(this IApplicationBuilder app)
    {
        app.UseMiddleware<KafkaDashboardUI>();
        return app;
    }
}
