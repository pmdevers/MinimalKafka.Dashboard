using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MinimalKafka.Dashboard;
public sealed class KafkaDashboardApi(RequestDelegate next, KafkaDashboardUIOptions options)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var httpMethod = context.Request.Method;
        var path = context.Request.Path.Value ?? string.Empty;

        if (httpMethod == "GET" && IsMatch(path, "^/{0}/?dashboard.json$"))
        {
            await RespondWithDashboadJson(context.Response);
            return;
        }

        await next(context);

        bool IsMatch(string path, string pattern) => Regex.IsMatch(
            input: path,
            pattern: string.Format(pattern, Regex.Escape(options.RoutePrefix)),
            options: RegexOptions.IgnoreCase,
            matchTimeout: TimeSpan.FromMilliseconds(50));
    }

    private Task RespondWithDashboadJson(HttpResponse response)
    {
        response.StatusCode = 200;
        response.ContentType = "application/json";

        var dashboardInfo = new DashboardInfo();

        var result = JsonSerializer.Serialize(dashboardInfo, options.JsonOptions);

        return response.WriteAsync(result);
    }
}


public class DashboardInfo
{
    public string Name { get; set; } = "Dashboard Info";
}