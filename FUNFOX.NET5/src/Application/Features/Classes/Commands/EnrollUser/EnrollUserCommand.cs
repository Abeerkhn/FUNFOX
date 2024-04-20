using FUNFOX.NET5.Application.Features.Classes.Commands.AddEdit;
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

namespace FUNFOX.NET5.Application.Features.Products.Commands.EnrollUser
{
    public partial class EnrollUserCommand:IRequest<Result<int>>
    {
        public int classId { get; set; }
        public string userId { get; set; }
    }

    internal class EnrollUserCommandHandler : IRequestHandler<EnrollUserCommand, Result<int>>
    {
        private readonly IProductRepository productRepository;
        private readonly IStringLocalizer<EnrollUserCommandHandler> _localizer;

        public EnrollUserCommandHandler(IProductRepository productRepository,IStringLocalizer<EnrollUserCommandHandler> stringLocalizer)
        {
            this.productRepository = productRepository;
            _localizer = stringLocalizer;
        }
        public async Task<Result<int>> Handle(EnrollUserCommand request, CancellationToken cancellationToken)
        {

            try
            {
                // Call the EnrollUser method of the product repository
                var result = await productRepository.EnrollUser(request.classId, request.userId, cancellationToken);
                if (result.Succeeded == false)
                {
                    // If the result is a failure, return the error message
                    return await Result<int>.FailAsync(_localizer["Enrollment Failed!"]);
                }
                // Return the result obtained from the repository
                return await Result<int>.SuccessAsync(request.classId, _localizer["Enrolled Sucessfully"]);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return a failure result
                return await Result<int>.FailAsync(_localizer["Enrollment Failed!"]);
            }
        
        }

        //   throw new NotImplementedException();
    }


}

