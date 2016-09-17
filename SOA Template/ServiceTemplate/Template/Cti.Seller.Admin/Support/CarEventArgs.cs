using System;
using System.Collections.Generic;
using System.Linq;
using Cti.Seller.Client.Entities;

namespace Cti.Seller.Admin.Support
{
    public class CarEventArgs : EventArgs
    {
        public CarEventArgs(Car car, bool isNew)
        {
            Car = car;
            IsNew = isNew;
        }

        public Car Car { get; set; }
        public bool IsNew { get; set; }
    }
}
