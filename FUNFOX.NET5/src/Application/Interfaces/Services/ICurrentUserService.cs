using FUNFOX.NET5.Application.Interfaces.Common;

namespace FUNFOX.NET5.Application.Interfaces.Services
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}