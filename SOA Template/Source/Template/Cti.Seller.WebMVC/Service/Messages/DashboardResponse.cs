using ecrm.Models.DashboardViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ecrm.Service.Messages
{
    public class DashboardResponse : BaseResponse
    {
        public DashboardViewModel DashboardInfo { get; set; }
    }
}