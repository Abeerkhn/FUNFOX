using FUNFOX.NET5.Application.Features.Classes.Commands.AddEdit;
using FUNFOX.NET5.Application.Features.Classes.Queries.GetAllPaged;
using FUNFOX.NET5.Application.Features.Products.Commands.Delete_Enrollment;
using FUNFOX.NET5.Application.Features.Products.Commands.EnrollUser;
using FUNFOX.NET5.Application.Features.Products.Queries.GetEnrolledUsers;
using FUNFOX.NET5.Application.Requests.Catalog;
using FUNFOX.NET5.Client.Infrastructure.Extensions;
using FUNFOX.NET5.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Client.Infrastructure.Managers.Catalog.Product
{
    public class ClassManager : IClassManager
    {
        private readonly HttpClient _httpClient;

        public ClassManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.ClassesEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.ClassesEndpoints.Export
                : Routes.ClassesEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }
        public async Task<IResult<string>> EnrolledUsersExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.ClassesEndpoints.EnrolledUsersExport
                : Routes.ClassesEndpoints.EnrolledUsersExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<IResult<string>> GetProductImageAsync(int id)
        {
            var response = await _httpClient.GetAsync(Routes.ClassesEndpoints.GetProductImage(id));
            return await response.ToResult<string>();
        }

        public async Task<PaginatedResult<GetAllPagedClassesResponse>> GetClassesAsync(GetAllPagedClassesRequest request)
        {
            var response = await _httpClient.GetAsync(Routes.ClassesEndpoints.GetAllPaged(request.PageNumber, request.PageSize, request.SearchString, request.Orderby));
            return await response.ToPaginatedResult<GetAllPagedClassesResponse>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditClassCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.ClassesEndpoints.Save, request);
            return await response.ToResult<int>();
        }

        public async Task<PaginatedResult<GetEnrolledUsersResponse>> GetEnrolledUsers(int classId, GetEnrolledUsersRequest request)
        {
            var response = await _httpClient.GetAsync(Routes.ClassesEndpoints.GetEnrolledUsers(request.PageNumber, request.PageSize, request.SearchString, request.Orderby, classId));
          
                return await response.ToPaginatedResult<GetEnrolledUsersResponse>();
           
        }

        public async Task<IResult<int>> UserEnroll(EnrollUserCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.ClassesEndpoints.EnrollUsers,request);
            //throw new NotImplementedException();
            return await response.ToResult<int>();
        }

        public async Task<IResult<int>> DeleteEnrollment(DeleteEnrollmentCommand command)
        {
          
                var response = await _httpClient.DeleteAsync($"{Routes.ClassesEndpoints.DeleteEnrollment}/{command.ClassId}/{command.UserId}");
                return await response.ToResult<int>();
           
        }
    }
}