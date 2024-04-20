using FUNFOX.NET5.Shared.Constants.Localization;
using FUNFOX.NET5.Shared.Settings;
using System.Linq;

namespace FUNFOX.NET5.Server.Settings
{
    public record ServerPreference : IPreference
    {
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";

        //TODO - add server preferences
    }
}