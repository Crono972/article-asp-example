using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Api.Model;

[JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
public class LightBorough
{
    public string Name { get; set; }
    public int BoroughNumber { get; set; }
    public int ArinseeCode { get; set; }
    public decimal PerimeterSize { get; set; }
}