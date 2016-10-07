using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ecrm.Models.DashboardViewModels
{
    public class DashboardItemViewModel
    {
        [Display(Name = "PositionCode", ResourceType = typeof(Resources))]
        public string PositionCode { get; set; }
  
        [Display(Name = "SellerName", ResourceType = typeof(Resources))]
        public string SellerName { get; set; }

        [Display(Name = "UnconvertedLeads", ResourceType = typeof(Resources))]
        public int UnconvertedLeads { get; set; }

        [Display(Name = "Prospects", ResourceType = typeof(Resources))]
        public int Prospects { get; set; }

        [Display(Name = "UnitsOffered", ResourceType = typeof(Resources))]
        public int UnitsOffered { get; set; }

        [Display(Name = "Reservations", ResourceType = typeof(Resources))]
        public int Reservations { get; set; }


    }
}