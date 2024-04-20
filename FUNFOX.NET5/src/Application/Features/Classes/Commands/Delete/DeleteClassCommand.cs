using FUNFOX.NET5.Application.Interfaces.Repositories;
using FUNFOX.NET5.Domain.Entities.Catalog;
using FUNFOX.NET5.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Application.Features.Classes.Commands.Delete
{
    public class DeleteClassCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteClassCommandHandler : IRequestHandler<DeleteClassCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<DeleteClassCommandHandler> _localizer;

        public DeleteClassCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteClassCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteClassCommand command, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Repository<Class>().GetByIdAsync(command.Id);
            if (product != null)
            {
                await _unitOfWork.Repository<Class>().DeleteAsync(product);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(product.Id, _localizer["Product Deleted"]);
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Product Not Found!"]);
            }
        }
    }
}