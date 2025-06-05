using Json.More;
using Json.Schema;
using Json.Schema.Generation;
using System.Text.Json;

namespace MinimalKafka.Dashboard.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        JsonSerializerOptions options = JsonSerializerOptions.Default;

        

        var jsonSchema = new JsonSchemaBuilder().FromType(typeof(Person)).Build();
        var str = jsonSchema.ToJsonDocument().RootElement.GetRawText();


    }

    public class Person
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string? Address { get; set; }
    }
}