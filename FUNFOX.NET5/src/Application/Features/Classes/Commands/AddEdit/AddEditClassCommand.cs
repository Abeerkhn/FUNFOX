using AutoMapper;
using FUNFOX.NET5.Application.Interfaces.Repositories;
using FUNFOX.NET5.Application.Interfaces.Services;
using FUNFOX.NET5.Application.Requests;
using FUNFOX.NET5.Domain.Entities.Catalog;
using FUNFOX.NET5.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Application.Features.Classes.Commands.AddEdit
{
    public partial class AddEditClassCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }

        public string ImageDataURL { get; set; }

        [Required(ErrorMessage = "The Level field is required.")]
        public string Level { get; set; }

        [Required(ErrorMessage = "The MaxClassSize field is required.")]
        public int MaxClassSize { get; set; }

        [Required(ErrorMessage = "The Start Time field is required.")]
        public string  StartTime { get; set; }

        [Required(ErrorMessage = "The End Time field is required.")]
        public string EndTime { get; set; }


        [Required(ErrorMessage = "The Frequency field is required.")]
        public string Frequency { get; set; }
        public UploadRequest UploadRequest { get; set; }
    }

    internal class AddEditClassCommandHandler : IRequestHandler<AddEditClassCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<AddEditClassCommandHandler> _localizer;

        public AddEditClassCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IUploadService uploadService, IStringLocalizer<AddEditClassCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditClassCommand command, CancellationToken cancellationToken)
        {
            var uploadRequest = command.UploadRequest;
            if (uploadRequest != null)
            {
                uploadRequest.FileName = $"P-{command.Name}{uploadRequest.Extension}";
            }
            if (command.Id == 0)
            {
                var product = _mapper.Map<Class>(command);
                if (uploadRequest != null)
                {
                    product.ImageDataURL = _uploadService.UploadAsync(uploadRequest);
                }
                await _unitOfWork.Repository<Class>().AddAsync(product);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(product.Id, _localizer["Product Saved"]);
            }
            else
            {
                var product = await _unitOfWork.Repository<Class>().GetByIdAsync(command.Id);
                if (product != null)
                {
                   var updatedProduct = _mapper.Map<Class>(command);
                    if (uploadRequest != null)
                    {
                        updatedProduct.ImageDataURL = _uploadService.UploadAsync(uploadRequest);
                    }          
                    await _unitOfWork.Repository<Class>().UpdateAsync(updatedProduct);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(updatedProduct.Id, _localizer["Product Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Product Not Found!"]);
                }
            }
        }
    }
}