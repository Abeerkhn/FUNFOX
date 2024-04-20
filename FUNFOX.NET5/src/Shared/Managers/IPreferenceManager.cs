using FUNFOX.NET5.Shared.Settings;
using FUNFOX.NET5.Shared.Wrapper;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Shared.Managers
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();

        Task<IResult> ChangeLanguageAsync(string languageCode);
    }
}