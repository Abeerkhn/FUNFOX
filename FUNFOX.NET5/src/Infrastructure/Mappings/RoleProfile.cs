using AutoMapper;
using FUNFOX.NET5.Application.Responses.Identity;
using FUNFOX.NET5.Infrastructure.Models.Identity;

namespace FUNFOX.NET5.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, BlazorHeroRole>().ReverseMap();
        }
    }
}