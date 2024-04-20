using FUNFOX.NET5.Application.Features.Products.Queries.GetEnrolledUsers;
using FUNFOX.NET5.Application.Interfaces.Repositories;
using FUNFOX.NET5.Domain.Entities.Catalog;
using FUNFOX.NET5.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using FUNFOX.NET5.Shared.Wrapper;
using System;
using System.Threading;
using FUNFOX.NET5.Infrastructure.Contexts;
using FUNFOX.NET5.Application.Features.Classes.Queries.GetAllPaged;
using System.Linq.Expressions;
using MediatR;
using FUNFOX.NET5.Application.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using AutoMapper;
using FUNFOX.NET5.Application.Specifications.Catalog;
using FUNFOX.NET5.Application.Interfaces.Services;
using Microsoft.Extensions.Localization;
namespace FUNFOX.NET5.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IRepositoryAsync<Class, int> _repository;
        private readonly IRepositoryAsync<UserClassEnrollment,int> _UserEnrollrepository;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly BlazorHeroContext blazorHeroContext;
        private readonly IExcelService excelService;
        IMapper Mapper;
    //    private readonly IStringLocalizer<ExportEnrolledUsersQueryHandler> _localizer;

        public ProductRepository(IRepositoryAsync<Class, int> repository,IExcelService excelService,IMapper mapper,BlazorHeroContext blazorHeroContext,IUnitOfWork<int> unitOfWork,IRepositoryAsync<UserClassEnrollment,int> enrollRepository)
        {
            _repository = repository;
            this.excelService = excelService;
            Mapper = mapper;
            _unitOfWork = unitOfWork;
            this.blazorHeroContext = blazorHeroContext;
           _UserEnrollrepository = enrollRepository;
        }

        public async Task<IResult<string>> DeletedEnrollment(int classId, string userId, CancellationToken cancellationToken)
        {

            try
            {
                var enrollment = await _unitOfWork.Repository<UserClassEnrollment>().Entities.SingleOrDefaultAsync(
    x => x.ClassId == classId && x.UserId == userId,
    cancellationToken: cancellationToken
);
                if (enrollment == null)
                {
                    return Result<string>.Fail("Enrollment record not found.");
                }

                // Remove the enrollment record from the database context
                _unitOfWork.Repository<UserClassEnrollment>().DeleteAsync(enrollment);

                // Save the changes to persist the deletion
                await _unitOfWork.Commit(cancellationToken);

                return Result<string>.Success("Enrollment record deleted successfully.");



            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
            //throw new NotImplementedException();
        }

        public async Task<IResult<int>> EnrollUser(int classId, string userId, CancellationToken cancellationToken)
        {
            try
            {
                // Create a new instance of UserClassEnrollment
                var userEnrollment = new UserClassEnrollment
                {
                    
                    ClassId = classId,
                    UserId = userId
                    // You can also set other properties of the UserClassEnrollment entity here if needed
                };
                var mappedobj = Mapper.Map<UserClassEnrollment>(userEnrollment);
                // Add the new enrollment record to the repository
                await _unitOfWork.Repository<UserClassEnrollment>().AddAsync(mappedobj);

               // blazorHeroContext.UserClassEnrollments.Add(userEnrollment);
               //var response= await blazorHeroContext.SaveChangesAsync(cancellationToken);
                // Save the changes to persist the new enrollment record
              var response=  await _unitOfWork.Commit(cancellationToken);

                if(response>0)
                {
                return Result<int>.Success(userEnrollment.UserId);
                }
                else
                {
                    return Result<int>.Fail("Enrollment Failed");

                }
                // Return a success result with the ID of the new enrollment record
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return a failure result
                return Result<int>.Fail(ex.Message);
            }
            //throw new System.NotImplementedException();
        }

        public async Task<string>ExportEnrolledUsers(CancellationToken cancellationToken, string searchString = "")
        {
            var productFilterSpec = new EnrolledUsersFilterSpecification(searchString);
            var products = await _unitOfWork.Repository<UserClassEnrollment>().Entities
                .Specify(productFilterSpec)
                .ToListAsync(cancellationToken);
            Dictionary<string , UserClassEnrollment> dictionary= new  Dictionary<string, UserClassEnrollment>();
           var mappers = new Dictionary<string, Func<UserClassEnrollment, object>>
    {
        // Add mappers for columns
        { "User Id", item => item.UserId+item.ClassId },
        { "UserName", item => item.BlazorHeroUser?.NormalizedUserName },
        { "Class Name", item => item.Class?.Name },
         { "Class ID", item => item.ClassId },
    };

            // Get the sheet name directly
            string sheetName = "EnrolledUsers"; // Replace "YourSheetName" with the desired sheet name

            var data = await excelService.ExportAsync(products, mappers: mappers, sheetName: sheetName);

            return data;

        }
        // new NotImplementedException();
    

        public async Task<PaginatedResult<GetEnrolledUsersResponse>> GeEnrolledUsersAsync( GetEnrolledUsersQuery request, int classId)
        {
            Expression<Func<UserClassEnrollment, GetEnrolledUsersResponse>> expression = e => new GetEnrolledUsersResponse
            {
               
                UserId = e.UserId,
                ClassId = e.ClassId,
                UserName = e.BlazorHeroUser.NormalizedUserName,
                UserEmail = e.BlazorHeroUser.Email,
                UserPhone = e.BlazorHeroUser.PhoneNumber,




                };
            var query = _unitOfWork.Repository<UserClassEnrollment>().Entities.AsQueryable();
            query = query.Where(p => p.ClassId == classId);
            if (!string.IsNullOrEmpty(request.SearchString))
            {
                query = query.Where(p => p.BlazorHeroUser.UserName.Contains(request.SearchString) || p.BlazorHeroUser.Email.Contains(request.SearchString));
            }

           
            var allClasses = await query.ToListAsync();
            // var query = _unitOfWork.Repository<Class>().Entities.AsQueryable();
            var data = await query
                .AsNoTracking()
                .Select(expression)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);


            return data;
            
                    }

       
    }
}