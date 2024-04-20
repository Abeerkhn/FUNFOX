using FUNFOX.NET5.Application.Features.Classes.Commands.AddEdit;
using FUNFOX.NET5.Application.Features.Classes.Queries.GetAllPaged;
using FUNFOX.NET5.Application.Features.Products.Commands.Delete_Enrollment;
using FUNFOX.NET5.Application.Features.Products.Commands.EnrollUser;
using FUNFOX.NET5.Application.Features.Products.Queries.GetEnrolledUsers;
using FUNFOX.NET5.Application.Requests.Catalog;
using FUNFOX.NET5.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Client.Infrastructure.Managers.Catalog.Product
{
    public interface IClassManager : IManager
    {
        Task<PaginatedResult<GetAllPagedClassesResponse>> GetClassesAsync(GetAllPagedClassesRequest request);

        Task<IResult<string>> GetProductImageAsync(int id);


        Task<PaginatedResult<GetEnrolledUsersResponse>> GetEnrolledUsers(int classId, GetEnrolledUsersRequest request);

        Task<IResult<int>> SaveAsync(AddEditClassCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
        Task<IResult<string>> EnrolledUsersExportToExcelAsync(string searchString = "");
        Task<IResult<int>> UserEnroll(EnrollUserCommand request);
        Task<IResult<int>> DeleteEnrollment(DeleteEnrollmentCommand command);
    }
}