using AutoMapper;
using FUNFOX.NET5.Application.Extensions;
using FUNFOX.NET5.Application.Interfaces.Repositories;
using FUNFOX.NET5.Application.Specifications.Catalog;
using FUNFOX.NET5.Domain.Entities.Catalog;
using FUNFOX.NET5.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Application.Features.Classes.Queries.GetAllPaged
{
    public class GetAllClassesQuery : IRequest<PaginatedResult<GetAllPagedClassesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetAllClassesQuery(int pageNumber, int pageSize, string searchString, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                OrderBy = orderBy.Split(',');
            }
        }
    }
    internal class GetAllClassesQueryHandler : IRequestHandler<GetAllClassesQuery, PaginatedResult<GetAllPagedClassesResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
       
        public GetAllClassesQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }

        public async Task<PaginatedResult<GetAllPagedClassesResponse>> Handle(GetAllClassesQuery request, CancellationToken cancellationToken)
        {


            Expression<Func<Class, GetAllPagedClassesResponse>> expression = e => new GetAllPagedClassesResponse
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Level = e.Level,
                Maxclasssize = e.MaxClassSize,
                Frequency=e.Frequency,
                StartTime = e.StartTime,
                EndTime=e.EndTime,
                ImageUrl = e.ImageDataURL
            };

            var query = _unitOfWork.Repository<Class>().Entities.AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                query = query.Where(p => p.Name.Contains(request.SearchString) || p.Description.Contains(request.SearchString));
            }

            if (request.OrderBy?.Any() == true)
            {
                var ordering = string.Join(",", request.OrderBy);
                query = query.OrderBy(ordering);
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