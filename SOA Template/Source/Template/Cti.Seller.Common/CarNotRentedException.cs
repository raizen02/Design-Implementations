using System;
using System.Collections.Generic;
using System.Linq;

namespace Cti.Seller.Common
{
    public class CarNotRentedException : ApplicationException
    {
        public CarNotRentedException(string message)
            : base(message)
        {
        }

        public CarNotRentedException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}
