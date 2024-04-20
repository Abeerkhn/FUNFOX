using FUNFOX.NET5.Application.Interfaces.Common;
using FUNFOX.NET5.Application.Requests.Identity;
using FUNFOX.NET5.Application.Responses.Identity;
using FUNFOX.NET5.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Application.Interfaces.Services.Identity
{
    public interface IRoleService : IService
    {
        Task<Result<List<RoleResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleResponse>> GetByIdAsync(string id);

        Task<Result<string>> SaveAsync(RoleRequest request);

        Task<Result<string>> DeleteAsync(string id);

        Task<Result<PermissionResponse>> GetAllPermissionsAsync(string roleId);

        Task<Result<string>> UpdatePermissionsAsync(PermissionRequest request);
    }
}