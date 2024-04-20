using FUNFOX.NET5.Application.Features.Dashboards.Queries.GetData;
using FUNFOX.NET5.Shared.Wrapper;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Client.Infrastructure.Managers.Dashboard
{
    public interface IDashboardManager : IManager
    {
        Task<IResult<DashboardDataResponse>> GetDataAsync();
    }
}