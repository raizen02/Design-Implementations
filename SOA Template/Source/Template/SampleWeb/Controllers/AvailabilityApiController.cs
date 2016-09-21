
using Core.Common.Contracts;
using Cti.Seller.Client.Contracts;
using Cti.Seller.Client.Entities;
using SampleWeb.Controllers.Web.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace SampleWeb.Controllers.API
{

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
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

                Location[] location = _ILocationService.GetLocations();
                return request.CreateResponse<Location[]>(HttpStatusCode.OK, location.ToArray());
            });
        }


    }
}
