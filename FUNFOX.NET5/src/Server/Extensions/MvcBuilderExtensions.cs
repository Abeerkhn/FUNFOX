﻿using FluentValidation;
using FluentValidation.AspNetCore;
using FUNFOX.NET5.Application.Configurations;
using FUNFOX.NET5.Application.Features.ExtendedAttributes.Commands.AddEdit;
using FUNFOX.NET5.Application.Validators.Features.ExtendedAttributes.Commands.AddEdit;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace FUNFOX.NET5.Server.Extensions
{
    internal static class MvcBuilderExtensions
    {
        internal static IMvcBuilder AddValidators(this IMvcBuilder builder)
        {
            builder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AppConfiguration>());
            return builder;
        }

        internal static void AddExtendedAttributesValidators(this IServiceCollection services)
        {
            #region AddEditExtendedAttributeCommandValidator

            var addEditExtendedAttributeCommandValidatorType = typeof(AddEditExtendedAttributeCommandValidator<,,,>);
            var validatorTypes = addEditExtendedAttributeCommandValidatorType
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.BaseType?.IsGenericType == true)
                .Select(t => new
                {
                    BaseGenericType = t.BaseType,
                    CurrentType = t
                })
                .Where(t => t.BaseGenericType?.GetGenericTypeDefinition() == typeof(AddEditExtendedAttributeCommandValidator<,,,>))
                .ToList();

            foreach (var validatorType in validatorTypes)
            {
                var addEditExtendedAttributeCommandType = typeof(AddEditExtendedAttributeCommand<,,,>).MakeGenericType(validatorType.BaseGenericType.GetGenericArguments());
                var iValidator = typeof(IValidator<>).MakeGenericType(addEditExtendedAttributeCommandType);
                services.AddScoped(iValidator, validatorType.CurrentType);
            }

            #endregion AddEditExtendedAttributeCommandValidator
        }
    }
}