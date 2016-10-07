using System.Threading.Tasks;
using ecrm.Service.Messages;

namespace ecrm.Service
{
    public interface IDashboardService
    {
        Task<DashboardResponse> GetDashboardInfo(DashboardRequest dashboardInfoRequest);
    }
}