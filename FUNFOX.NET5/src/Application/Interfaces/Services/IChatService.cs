using FUNFOX.NET5.Application.Interfaces.Chat;
using FUNFOX.NET5.Application.Models.Chat;
using FUNFOX.NET5.Application.Responses.Identity;
using FUNFOX.NET5.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Application.Interfaces.Services
{
    public interface IChatService
    {
        Task<Result<IEnumerable<ChatUserResponse>>> GetChatUsersAsync(string userId);

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> message);

        Task<Result<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string userId, string contactId);
    }
}