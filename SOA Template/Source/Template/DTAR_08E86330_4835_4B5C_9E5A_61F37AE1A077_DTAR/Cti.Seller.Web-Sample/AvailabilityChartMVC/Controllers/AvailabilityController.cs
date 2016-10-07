using AvailabilityChartMVC.Controllers.Web.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;

namespace AvailabilityChartMVC.Controllers
{

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    [RoutePrefix("customer")]
    public class AvailabilityController : ViewControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("account")]
        public ActionResult MyAccount()
        {
            return View();
        }

        [HttpGet]
        // the route is defined in RouteConfig
        public ActionResult ReserveCar()
        {
            return View();
        }

        [HttpGet]
        [Route("availableunits")]
        public ActionResult AvailableUnits()
        {
            return View();
        }

     
    }



}