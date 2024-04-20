using FUNFOX.NET5.Application.Interfaces.Serialization.Options;
using System.Text.Json;

namespace FUNFOX.NET5.Application.Serialization.Options
{
    public class SystemTextJsonOptions : IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();
    }
}