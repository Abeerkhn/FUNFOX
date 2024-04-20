using FUNFOX.NET5.Application.Requests.Identity;
using FUNFOX.NET5.Shared.Wrapper;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Client.Infrastructure.Managers.Identity.Authentication
{
    public interface IAuthenticationManager : IManager
    {
        Task<IResult> Login(TokenRequest model);

        Task<IResult> Logout();

        Task<string> RefreshToken();

        Task<string> TryRefreshToken();

        Task<string> TryForceRefreshToken();

        Task<ClaimsPrincipal> CurrentUser();
    }
}