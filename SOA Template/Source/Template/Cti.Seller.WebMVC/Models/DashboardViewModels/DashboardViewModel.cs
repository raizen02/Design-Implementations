using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecrm.Models.DashboardViewModels
{
    public class DashboardViewModel
    {

        public string SellerName { get; set; }
        public string SellerPosition { get; set; }  
        public IList<DashboardItemViewModel> DashboardItems { get; set; }

    }
}