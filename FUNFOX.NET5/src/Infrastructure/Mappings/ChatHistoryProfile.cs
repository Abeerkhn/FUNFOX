using AutoMapper;
using FUNFOX.NET5.Application.Interfaces.Chat;
using FUNFOX.NET5.Application.Models.Chat;
using FUNFOX.NET5.Infrastructure.Models.Identity;

namespace FUNFOX.NET5.Infrastructure.Mappings
{
    public class ChatHistoryProfile : Profile
    {
        public ChatHistoryProfile()
        {
            CreateMap<ChatHistory<IChatUser>, ChatHistory<BlazorHeroUser>>().ReverseMap();
        }
    }
}