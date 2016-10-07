using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ecrm.Models.LeadsViewModels;

namespace ecrm.Service.Messages
{
    public class LeadsRequest : BaseRequest
    {
        public LeadsListViewModel LeadsList { get; set; }
        public LeadInfoViewModel LeadInfo { get; set; }
        public LeadOfferingViewModel LeadOfferingList { get; set; }
        public LeadOfferingItemViewModel LeadOfferingInfo { get; set; }
        public LeadActivityViewModel LeadActivityList { get; set; }
        public LeadActivityItemViewModel LeadActivityInfo { get; set; }
    }
}