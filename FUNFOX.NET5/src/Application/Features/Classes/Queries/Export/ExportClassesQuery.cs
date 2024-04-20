using FUNFOX.NET5.Application.Extensions;
using FUNFOX.NET5.Application.Interfaces.Repositories;
using FUNFOX.NET5.Application.Interfaces.Services;
using FUNFOX.NET5.Application.Specifications.Catalog;
using FUNFOX.NET5.Domain.Entities.Catalog;
using FUNFOX.NET5.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Application.Features.Classes.Queries.Export
{
    public class ExportClassesQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportClassesQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportClassesQueryHandler : IRequestHandler<ExportClassesQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportClassesQueryHandler> _localizer;

        public ExportClassesQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportClassesQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportClassesQuery request, CancellationToken cancellationToken)
        {
            var productFilterSpec = new ClassesFilterSpecification(request.SearchString);
            var products = await _unitOfWork.Repository<Class>().Entities
                .Specify(productFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(products, mappers: new Dictionary<string, Func<Class, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Name"], item => item.Name },
                { _localizer["Description"], item => item.Description },
              }, sheetName: _localizer["Classes"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}