using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalKafka.Dashboard.UI;

public class KafkaDashboardUIOptions
{
    // Define properties for the options here
    public string DashboardPath { get; set; } = "/kafka-dashboard";
}

public class KafkaDashboardUI(RequestDelegate next, IOptions<KafkaDashboardUIOptions> options)
{
   public async Task InvokeAsync(HttpContext context)
    {
        if(context.Request.Path.StartsWithSegments(options.Value.DashboardPath))
        {
            await HandleDashboardRequest(context);
        }
        else
        {
            await next(context);
        }
    }

    private static async Task HandleDashboardRequest(HttpContext context)
    {
        // Implement the middleware logic here
        // For example, you could serve a static HTML page or redirect to a dashboard
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync("<h1>Welcome to Kafka Dashboard</h1>");
    }
}
