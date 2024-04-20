using FluentValidation;
using FUNFOX.NET5.Application.Features.Classes.Commands.AddEdit;
using Microsoft.Extensions.Localization;

namespace FUNFOX.NET5.Application.Validators.Features.Classes.Commands.AddEdit
{
    public class AddEditClassCommandValidator : AbstractValidator<AddEditClassCommand>
    {
        public AddEditClassCommandValidator(IStringLocalizer<AddEditClassCommandValidator> localizer)
        {
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required!"]);
          //  RuleFor(request => request.ImageDataURL)
            //    .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Barcode is required!"]);
            RuleFor(request => request.Description)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Description is required!"]);
             }
    }
}