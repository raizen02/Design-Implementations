using ecrm.Models.LeadsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ecrm.Service.Messages
{
    public class LeadsResponse : BaseResponse
    {
        public LeadsListViewModel LeadsList { get; set; }
        public LeadInfoViewModel LeadsInfo { get; set; }       
        public LeadOfferingViewModel LeadOfferingList { get; set; }
        public LeadOfferingItemViewModel LeadOfferingInfo { get; set; }
        public LeadActivityViewModel LeadActivityList { get; set; }
        public LeadActivityItemViewModel LeadActivityInfo { get; set; }
    }
}