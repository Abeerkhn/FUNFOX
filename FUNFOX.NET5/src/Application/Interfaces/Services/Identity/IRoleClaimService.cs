using FUNFOX.NET5.Application.Interfaces.Common;
using FUNFOX.NET5.Application.Requests.Identity;
using FUNFOX.NET5.Application.Responses.Identity;
using FUNFOX.NET5.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Application.Interfaces.Services.Identity
{
    public interface IRoleClaimService : IService
    {
        Task<Result<List<RoleClaimResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleClaimResponse>> GetByIdAsync(int id);

        Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId);

        Task<Result<string>> SaveAsync(RoleClaimRequest request);

        Task<Result<string>> DeleteAsync(int id);
    }
}