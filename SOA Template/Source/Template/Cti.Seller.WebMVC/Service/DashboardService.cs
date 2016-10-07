using ecrm.Infrastructure.Logging;
using ecrm.Models.DashboardViewModels;
using ecrm.Service.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ecrm.Service
{
    public class DashboardService : IDashboardService
    {
        public async Task<DashboardResponse> GetDashboardInfo(DashboardRequest dashboardInfoRequest)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);

            /// STATIC VALUES
            DashboardResponse dashboardInfoResponse = new DashboardResponse();
            dashboardInfoResponse.Success = true;
            dashboardInfoResponse.Message = Resources.SaveSuccess;

            DashboardViewModel dashboardInfo = new DashboardViewModel();
            IList<DashboardItemViewModel> dashboardItems = new List<DashboardItemViewModel>();

            dashboardItems.Add(new DashboardItemViewModel { PositionCode = "SC", SellerName = "AREVALLO, MARIETTE LIBUNAO", UnconvertedLeads = 3, Prospects = 5, UnitsOffered = 3, Reservations = 2 });
            dashboardItems.Add(new DashboardItemViewModel { PositionCode = "SC", SellerName = "ARNOSA, MANETH TORRES", UnconvertedLeads = 3, Prospects = 0, UnitsOffered = 0, Reservations = 13 });
            dashboardItems.Add(new DashboardItemViewModel { PositionCode = "SC", SellerName = "VERGARA, JOZA ZACAL", UnconvertedLeads = 2, Prospects = 5, UnitsOffered = 0, Reservations = 13 });
            dashboardItems.Add(new DashboardItemViewModel { PositionCode = "SC", SellerName = "VIRAY, ROMEO SE", UnconvertedLeads = 2, Prospects = 5, UnitsOffered = 10, Reservations = 15 });

            dashboardInfo.DashboardItems = dashboardItems;
            dashboardInfo.SellerName = "Sally Ruth Acosta";
            dashboardInfo.SellerPosition = "Sales Manager";
            dashboardInfoResponse.DashboardInfo = dashboardInfo;
            /// END STATIC VALUES

            EcrmEventSource.Log.MethodStop(this.GetType().FullName);

            return dashboardInfoResponse;
        }

    }
}