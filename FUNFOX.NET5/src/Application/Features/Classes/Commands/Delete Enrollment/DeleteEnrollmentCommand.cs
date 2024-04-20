using FUNFOX.NET5.Application.Features.Classes.Commands.Delete;
using FUNFOX.NET5.Application.Interfaces.Repositories;
using FUNFOX.NET5.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Application.Features.Products.Commands.Delete_Enrollment
{
    public class DeleteEnrollmentCommand:IRequest<IResult<int>>
    {
        public string UserId { get; set; }
        public int ClassId { get; set; }

        public DeleteEnrollmentCommand(string userId, int classId)
        {
            UserId = userId;
            ClassId = classId;
        }
    }
    internal class DeleteEnrollmentCommandHandler :IRequestHandler<DeleteEnrollmentCommand,IResult<int>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IStringLocalizer<DeleteEnrollmentCommandHandler> _localizer;

        public DeleteEnrollmentCommandHandler(IProductRepository productRepository,IStringLocalizer<DeleteEnrollmentCommandHandler> stringLocalizer )
    {
            _productRepository = productRepository;
            _localizer = stringLocalizer;
        }
        

        public  async Task<IResult<int>> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
        {
            try {
                var result = await _productRepository.DeletedEnrollment(request.ClassId, request.UserId, cancellationToken);
                if (result.Succeeded == false)
                {
                    // If the result is a failure, return the error message
                    return await Result<int>.FailAsync(_localizer["Some Error Occur While Deleting Enrollment!"]);
                }
                // Return the result obtained from the repository
                return await Result<int>.SuccessAsync(request.ClassId, _localizer["Deletion  Sucessfull"]);

            }
            catch (Exception ex)
            {

                return await Result<int>.FailAsync( _localizer["Some Error Occur!"]);

            }
            throw new NotImplementedException();
        }
    }   
}
