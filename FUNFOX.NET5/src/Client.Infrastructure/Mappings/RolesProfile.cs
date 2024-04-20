using AutoMapper;
using FUNFOX.NET5.Application.Requests.Identity;
using FUNFOX.NET5.Application.Responses.Identity;

namespace FUNFOX.NET5.Client.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<PermissionResponse, PermissionRequest>().ReverseMap();
            CreateMap<RoleClaimResponse, RoleClaimRequest>().ReverseMap();
        }
    }
}