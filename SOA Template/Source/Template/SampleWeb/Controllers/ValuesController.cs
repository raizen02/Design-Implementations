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
using System.Web.Http;

namespace SampleWeb.Controllers
{

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [UsesDisposableService]

    public class ValuesController : ApiController, IServiceAwareController
    {

        [ImportingConstructor]
        public ValuesController(IUnitInventoryService inventoryService, ILocationService locationService)
        {
            _IUnitInventoryService = inventoryService;
            _ILocationService = locationService;

        }
        IUnitInventoryService _IUnitInventoryService;
        ILocationService _ILocationService;
        List<IServiceContract> _DisposableServices;
        public List<IServiceContract> DisposableServices
        {
            get
            {
                if (_DisposableServices == null)
                    _DisposableServices = new List<IServiceContract>();

                return _DisposableServices;
            }
        }

        protected  void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IUnitInventoryService);
            disposableServices.Add(_ILocationService);
        }


        // GET api/values
        public IEnumerable<Location> Get()
        {

            //List<Location> locations = new List<Location>();
            //Location loc = new Location() { Code = 111, Barangay = "sdfsdfsdf" };
            //locations.Add(loc);

             ProjectParams searchParams = new ProjectParams() { ProjectId = "0000000033", PhaseId = 1};
            Location[] location = _ILocationService.GetLocations();
  
            return location;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        void IServiceAwareController.RegisterDisposableServices(List<IServiceContract> disposableServices)
        {
            RegisterServices(disposableServices);
        }
    }
}
