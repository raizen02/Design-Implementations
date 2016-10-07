using System.Collections.Generic;

namespace ecrm.Domain.Model
{
    public class OfferingsValueModel
    {
        public int TotalRecordCount { get; set; }
        public IList<Offering> Offerings { get; set; }
    }
}