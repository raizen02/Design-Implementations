using AvailabilityChartMVC.Controllers.Web.Core;
using Core.Common.Contracts;
using Cti.Seller.Client.Contracts;
using Cti.Seller.Client.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AvailabilityChartMVC.Controllers.API
{

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    [RoutePrefix("api/availability")]
    [UsesDisposableService]
        public class AvailabilityApiController : ApiControllerBase
        {

        [ImportingConstructor]
        public AvailabilityApiController(IUnitInventoryService inventoryService,ILocationService locationService )
        {
            _IUnitInventoryService = inventoryService;
            _ILocationService = locationService;

        }
        IUnitInventoryService _IUnitInventoryService;
        ILocationService _ILocationService;
        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IUnitInventoryService);
            disposableServices.Add(_ILocationService);
        }

        [HttpGet]
        [Route("availableunits")]
        public HttpResponseMessage GetAvailableUnits(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {

                ProjectParams searchParams = new ProjectParams() { ProjectId = "0000000033", LocationId = "ANGONO" };
                Unit[] units = _IUnitInventoryService.GetAvailableUnits(searchParams);

                return request.CreateResponse<Unit[]>(HttpStatusCode.OK, units);
            });
        }



        [HttpGet]
        [Route("locations")]
        public HttpResponseMessage GetLocation(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {

                //  ProjectParams searchParams = new ProjectParams() { ProjectId = "0000000033", LocationId = "ANGONO" };
                //  Location[] location = _ILocationService.GetLocations(searchParams);
                //Location[] location = _ILocationService.GetLocations(searchParams);
                List<Location> locations = new List<Location>();
                Location loc = new Location() { Code = 111, Barangay = "sdfsdfsdf" };
                locations.Add(loc);

                return request.CreateResponse<Location[]>(HttpStatusCode.OK, locations.ToArray());
            });
        }


    }
}
