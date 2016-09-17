using System;
using System.Collections.Generic;
using System.Linq;
using Cti.Seller.Business.Entities;

namespace Cti.Seller.Data.Contracts
{
    public class CustomerReservationInfo
    {
        public Account Customer { get; set; }
        public Car Car { get; set; }
        public Reservation Reservation { get; set; }
    }
}
