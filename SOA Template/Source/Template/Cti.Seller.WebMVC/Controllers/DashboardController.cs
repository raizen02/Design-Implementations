using ecrm.Infrastructure.Logging;
using ecrm.Models.DashboardViewModels;
using ecrm.Service;
using ecrm.Service.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ecrm.Controllers
{
    public class DashboardController : Controller
    {
        private IDashboardService _dashboardService;
        public DashboardController() : this(new DashboardService())
        { }

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }


        // GET: Dashboard
        public async Task<ActionResult> Index()
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);
            DashboardRequest dashboardInfoRequest = new DashboardRequest();
            DashboardResponse dashboardInfoResponse = await _dashboardService.GetDashboardInfo(dashboardInfoRequest);

            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            dashboardViewModel = dashboardInfoResponse.DashboardInfo;

            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return View("Index", dashboardViewModel);
        }
    }
}