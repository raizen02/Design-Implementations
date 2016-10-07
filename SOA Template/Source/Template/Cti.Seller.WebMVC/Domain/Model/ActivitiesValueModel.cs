using System.Collections.Generic;

namespace ecrm.Domain.Model
{
    public class ActivitiesValueModel
    {
        public int TotalRecordCount { get; set; }
        public IList<Activity> Activities { get; set; }
    }
}