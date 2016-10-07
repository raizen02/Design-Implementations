using Cti.Seller.Client.Contracts;
using Cti.Seller.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AvailabilityChartMVC.Controllers.API
{
    public class LocationApiController : ApiController
    {
        // GET: api/LocationApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //[HttpGet]
        //[Route("locations")]
        //public HttpResponseMessage GetLocation(HttpRequestMessage request)
        //{
          

        //        //ProjectParams searchParams = new ProjectParams() { ProjectId = "0000000033", LocationId = "ANGONO" };
        //        //Location[] location = new ILocationService().GetLocations(searchParams);

        //        //return request.CreateResponse<Location[]>(HttpStatusCode.OK, location);
         
        //}


        // GET: api/LocationApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/LocationApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/LocationApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/LocationApi/5
        public void Delete(int id)
        {
        }
    }
}
