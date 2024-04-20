using FUNFOX.NET5.Application.Features.Dashboards.Queries.GetData;
using FUNFOX.NET5.Client.Infrastructure.Extensions;
using FUNFOX.NET5.Shared.Wrapper;
using System.Net.Http;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Client.Infrastructure.Managers.Dashboard
{
    public class DashboardManager : IDashboardManager
    {
        private readonly HttpClient _httpClient;

        public DashboardManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<DashboardDataResponse>> GetDataAsync()
        {
            var response = await _httpClient.GetAsync(Routes.DashboardEndpoints.GetData);
            var data = await response.ToResult<DashboardDataResponse>();
            return data;
        }
    }
}