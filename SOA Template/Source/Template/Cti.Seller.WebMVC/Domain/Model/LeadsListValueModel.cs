using System.Collections.Generic;

namespace ecrm.Domain.Model
{
    public class LeadsListValueModel
    {
        public int TotalRecordCount { get; set; }
        public IList<Lead> Leads { get; set; }
    }
}