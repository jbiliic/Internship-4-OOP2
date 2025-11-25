using System.Text.Json.Serialization;

namespace OOP2.Domain.Common.Validation
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ValidationType
    {
        Formal,
        Business,
        System,
        Security,
        Performance,
        Usability
    }
}
