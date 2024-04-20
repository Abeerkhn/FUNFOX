using FUNFOX.NET5.Application.Interfaces.Common;
using FUNFOX.NET5.Application.Requests.Identity;
using FUNFOX.NET5.Application.Responses.Identity;
using FUNFOX.NET5.Shared.Wrapper;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Application.Interfaces.Services.Identity
{
    public interface ITokenService : IService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model);

        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}