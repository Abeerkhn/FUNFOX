using FUNFOX.NET5.Application.Interfaces.Common;
using FUNFOX.NET5.Application.Requests.Identity;
using FUNFOX.NET5.Shared.Wrapper;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Application.Interfaces.Services.Account
{
    public interface IAccountService : IService
    {
        Task<IResult> UpdateProfileAsync(UpdateProfileRequest model, string userId);

        Task<IResult> ChangePasswordAsync(ChangePasswordRequest model, string userId);

        Task<IResult<string>> GetProfilePictureAsync(string userId);

        Task<IResult<string>> UpdateProfilePictureAsync(UpdateProfilePictureRequest request, string userId);
    }
}