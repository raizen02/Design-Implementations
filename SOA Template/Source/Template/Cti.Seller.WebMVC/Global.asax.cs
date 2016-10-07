using AutoMapper;
using ecrm.Domain.Model;
using ecrm.Infrastructure.AutoMapper;
using ecrm.Infrastructure.Logging;
using ecrm.Models.LeadsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ecrm
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperConfiguration.RegisterMappings();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            EcrmEventSource.Log.Error(this.GetType().FullName, exception.ToString());
            //Response.Redirect("/Home/Error");
        }
    }
}
