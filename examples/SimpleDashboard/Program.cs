using MinimalKafka;
using MinimalKafka.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMinimalKafka(options =>
    {
        options.WithConfiguration(builder.Configuration.GetSection("Kafka"));
        options.WithDashboard(dashboardOptions =>
        {
            dashboardOptions.DashboardPath = "/kafka-dashboard"; // Set the path for the dashboard
        });
    });
    

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseKafkaDashboard();

app.Run();
