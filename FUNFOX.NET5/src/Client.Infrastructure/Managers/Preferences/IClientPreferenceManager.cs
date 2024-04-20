using FUNFOX.NET5.Shared.Managers;
using MudBlazor;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Client.Infrastructure.Managers.Preferences
{
    public interface IClientPreferenceManager : IPreferenceManager
    {
        Task<MudTheme> GetCurrentThemeAsync();

        Task<bool> ToggleDarkModeAsync();
    }
}