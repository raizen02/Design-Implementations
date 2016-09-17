using System;
using System.Collections.Generic;
using System.Linq;

namespace Cti.Seller.Common
{
    public class UnitNotAvailableException : ApplicationException
    {
        public UnitNotAvailableException(string message)
            : base(message)
        {
        }

        public UnitNotAvailableException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}
