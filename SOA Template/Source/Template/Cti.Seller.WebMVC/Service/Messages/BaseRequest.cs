using ecrm.Domain.Model;
using ecrm.Models.LeadsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecrm.Service.Messages
{
    public class BaseRequest
    {
        public SellerViewModel Seller { get; set; }
    }
}