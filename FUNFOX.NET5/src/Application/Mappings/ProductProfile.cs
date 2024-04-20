using AutoMapper;
using FUNFOX.NET5.Application.Features.Classes.Commands.AddEdit;
using FUNFOX.NET5.Domain.Entities.Catalog;

namespace FUNFOX.NET5.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddEditClassCommand, Class>().ReverseMap();
        }
    }
}