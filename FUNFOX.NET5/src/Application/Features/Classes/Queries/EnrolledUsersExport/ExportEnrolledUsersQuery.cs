using FUNFOX.NET5.Application.Features.Classes.Queries.Export;
using FUNFOX.NET5.Application.Interfaces.Repositories;
using FUNFOX.NET5.Application.Interfaces.Services;
using FUNFOX.NET5.Application.Specifications.Catalog;
using FUNFOX.NET5.Domain.Entities.Catalog;
using FUNFOX.NET5.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Application.Features.Products.Queries.EnrolledUsersExport
{
    public class ExportEnrolledUsersQuery : IRequest<IResult<string>>
    {
        public string SearchString { get; set; }

        public ExportEnrolledUsersQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }
    internal class ExportEnrolledUsersQueryHandler : IRequestHandler<ExportEnrolledUsersQuery, IResult<string>>
    {
        private readonly IProductRepository productRepository;
        private readonly IStringLocalizer<ExportEnrolledUsersQueryHandler> _localizer;

        public ExportEnrolledUsersQueryHandler(IProductRepository productRepository, IStringLocalizer<ExportEnrolledUsersQueryHandler> localizer)
        {
            this.productRepository = productRepository;

            _localizer = localizer;
        }

        public async Task<IResult<string>> Handle(ExportEnrolledUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Call the ExportEnrolledUsers method and await the result
                string data = await productRepository.ExportEnrolledUsers( cancellationToken,request.SearchString);
                
                if (string.IsNullOrEmpty(data))
                {
                    // Return a failure result with the error message
                    return Result<string>.Fail(_localizer["Export Failed"]);
                }
                else
                {

                    return Result<string>.Success(data);
                }
                // Wrap the result string in a success result
            }
            catch (Exception ex)
            {
                // Return a failure result with the error message
                return Result<string>.Fail(ex.Message);
            }

        }

    }
}
