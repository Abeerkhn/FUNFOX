using FUNFOX.NET5.Application.Features.Products.Queries.GetEnrolledUsers;
using FUNFOX.NET5.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<PaginatedResult<GetEnrolledUsersResponse>> GeEnrolledUsersAsync(GetEnrolledUsersQuery request, int classId);
        Task<IResult<int>> EnrollUser(int classId, string userId, CancellationToken cancellationToken);
        Task<IResult<string>> DeletedEnrollment(int classId, string userId, CancellationToken cancellationToken);
        Task<string> ExportEnrolledUsers(CancellationToken cancellationToken, string searchString = "");
    }
}