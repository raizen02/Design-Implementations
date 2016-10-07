using ecrm.Models.LeadsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecrm.Service.Messages
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }


}