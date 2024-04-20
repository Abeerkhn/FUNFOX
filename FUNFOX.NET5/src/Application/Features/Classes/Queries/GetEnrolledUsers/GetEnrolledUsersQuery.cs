using FUNFOX.NET5.Application.Features.Classes.Queries.GetAllPaged;
using FUNFOX.NET5.Application.Interfaces.Repositories;
using FUNFOX.NET5.Domain.Entities.Catalog;
using FUNFOX.NET5.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Application.Features.Products.Queries.GetEnrolledUsers
{
    public class GetEnrolledUsersQuery: IRequest<PaginatedResult<GetEnrolledUsersResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...
        public int ClassId { get; set; }
        public GetEnrolledUsersQuery(int pageNumber, int pageSize, string searchString,  int Classid, string orderBy)
        {
            ClassId = Classid;
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                OrderBy = orderBy.Split(',');
            }
        }
    }

     
        internal class GetEnrolledUsersQueryHandler : IRequestHandler<GetEnrolledUsersQuery, PaginatedResult<GetEnrolledUsersResponse>>
        {
            private readonly IProductRepository _productRepository;
            public GetEnrolledUsersQueryHandler(IProductRepository productRepository )
            {
                _productRepository = productRepository;           
                    }
            public Task<PaginatedResult<GetEnrolledUsersResponse>> Handle(GetEnrolledUsersQuery request, CancellationToken cancellationToken)
            {

            int classId = request.ClassId;

                var enrolledUsersResponses = _productRepository.GeEnrolledUsersAsync(request,classId);

                return enrolledUsersResponses;
             
            }

            
        }
    }


