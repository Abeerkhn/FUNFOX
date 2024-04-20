
using FUNFOX.NET5.Application.Interfaces.Serialization.Settings;
using Newtonsoft.Json;

namespace FUNFOX.NET5.Application.Serialization.Settings
{
    public class NewtonsoftJsonSettings : IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}