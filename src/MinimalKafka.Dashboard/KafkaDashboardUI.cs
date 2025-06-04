using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Text.RegularExpressions;

namespace MinimalKafka.Dashboard;

public class KafkaDashboardUIOptions
{
    // Define properties for the options here
    public string Tilte { get; set; } = "MinimalKafka Dashboard";
    public string RoutePrefix { get; set; } = "kafka-dashboard";
    public JsonSerializerOptions JsonOptions { get; set; } = new()
    {
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
}

public class KafkaDashboardUI(
    RequestDelegate next,
    IWebHostEnvironment webHostEnvironment,
    ILoggerFactory loggerFactory,
    KafkaDashboardUIOptions options)
{
    private const string _embeddedFileNamespace = "MinimalKafka.Dashboard.Ui.dist";
    private readonly StaticFileMiddleware _staticFileMiddleware =
        new(next, webHostEnvironment, Options.Create(new StaticFileOptions
        {
            RequestPath = $"/{options.RoutePrefix}",
            FileProvider = new EmbeddedFileProvider(typeof(KafkaDashboardUI).Assembly, _embeddedFileNamespace),
        }), loggerFactory);
    public async Task InvokeAsync(HttpContext context)
    {
        var httpMethod = context.Request.Method;
        var path = context.Request.Path.Value ?? string.Empty;

        if (httpMethod == "GET" && IsMatch(path, "^/{0}/?$"))
        {
            var relativeIndexUrl = string.IsNullOrEmpty(path) || path.EndsWith('/') ?
                "index.html" :
                $"{path.Split("/")[^1]}/index.html";

            RespondWithRedirect(context.Response, relativeIndexUrl);
            return;

        }

        if (httpMethod == "GET" && IsMatch(path, "^/{0}/?index.html$"))
        {
            await RespondWithIndexHtml(context.Response);
            return;
        }

        await _staticFileMiddleware.Invoke(context);

        bool IsMatch(string path, string pattern) => Regex.IsMatch(
            input: path,
            pattern: string.Format(pattern, Regex.Escape(options.RoutePrefix)),
            options: RegexOptions.IgnoreCase,
            matchTimeout: TimeSpan.FromMilliseconds(50));
    }

    private static void RespondWithRedirect(HttpResponse response, string location)
    {
        response.StatusCode = 301;
        response.Headers["Location"] = location;
    }

    private async Task RespondWithIndexHtml(HttpResponse response)
    {
        response.StatusCode = 200;
        response.ContentType = "text/html;charset=utf-8";

        using var stream = typeof(KafkaDashboardUI).Assembly
            .GetManifestResourceStream($"{_embeddedFileNamespace}.index.html")!;

        using var reader = new StreamReader(stream);

        // Inject arguments before writing to response
        var htmlBuilder = new StringBuilder(await reader.ReadToEndAsync());
        foreach (var entry in GetIndexArguments())
        {
            htmlBuilder.Replace(entry.Key, entry.Value);
        }

        await response.WriteAsync(htmlBuilder.ToString(), Encoding.UTF8);
    }

    [Pure]
    private IDictionary<string, string> GetIndexArguments()
    {
        return new Dictionary<string, string>()
        {
            { "%(DocumentTitle)", options.Tilte },
            { "[RoutePrefix]", options.RoutePrefix },
        };
    }
}
