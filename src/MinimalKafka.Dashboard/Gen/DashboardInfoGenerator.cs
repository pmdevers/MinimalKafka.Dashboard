using MinimalKafka.Builders;
using System.Text.Json;
using Json.Schema;
using Json.More;
using Json.Schema.Generation;

namespace MinimalKafka.Dashboard.Gen;

internal class DashboardInfoGenerator(IKafkaBuilder builder) : IDashboardrInfoProvider
{
    public Task<DashboardInfo> GetDashboardInfoAsync()
    {
        var dashboard = new DashboardInfo
        {
            Consumers = builder.DataSource.Results
            .Select(c => new ConsumerInfo
            {
                TopicName = c.TopicName,
                GroupId = c.Metadata.GroupId(),
                ClientId = c.Metadata.ClientId(),
                KeyType = c.KeyType.Name,
                KeySchema = GetSchema(c.KeyType),
                ValueType = c.ValueType.Name,
                ValueSchema = GetSchema(c.ValueType)
            })
            .ToArray()
        };


        return Task.FromResult(dashboard);
    }

    private string GetSchema(Type type)
    {
        return new JsonSchemaBuilder()
            .FromType(type)
            .Build()
            .ToJsonDocument()
            .RootElement.GetRawText();
    }
}
