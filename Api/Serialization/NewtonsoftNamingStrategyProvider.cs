using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Api.Serialization;

public static class NewtonsoftNamingStrategyProvider
{
    private static readonly JsonSerializerSettings SnakeCaseSettings = new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() }
    };
    private static readonly JsonSerializerSettings KebabCaseSettings = new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        ContractResolver = new DefaultContractResolver { NamingStrategy = new KebabCaseNamingStrategy() }
    };
    private static readonly JsonSerializerSettings PascalCaseSettings = new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        ContractResolver = new DefaultContractResolver { NamingStrategy = new DefaultNamingStrategy() }
    };
    private static readonly JsonSerializerSettings CamelCaseSettings = new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
    };

    public static JsonSerializerSettings GetJsonSerializerSettings(string strategy)
    {
        return strategy switch
        {
            SerializationStrategy.SnakeCase => SnakeCaseSettings,
            SerializationStrategy.CamelCase => CamelCaseSettings,
            SerializationStrategy.KebabCase => KebabCaseSettings,
            SerializationStrategy.PascalCase => PascalCaseSettings,
            _ => CamelCaseSettings //default strategy
        };
    }
}