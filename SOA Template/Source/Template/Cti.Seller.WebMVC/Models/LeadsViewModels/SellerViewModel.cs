using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecrm.Models.LeadsViewModels
{
    public class SellerViewModel
    {
        public string Name { get; set; }
        public int SellerID { get; set; }
        public int RankLevel { get; set; }
        public string RankLevelString { get; set; }
    }
}