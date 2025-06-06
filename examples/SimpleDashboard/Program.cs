using MinimalKafka;
using MinimalKafka.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMinimalKafka(options =>
    {
        options.WithConfiguration(builder.Configuration.GetSection("Kafka"));
        options.WithDashboard();
    });
    

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapTopic("test", (string key, string value) => Task.CompletedTask);

app.UseKafkaDashboard();

app.Run();
