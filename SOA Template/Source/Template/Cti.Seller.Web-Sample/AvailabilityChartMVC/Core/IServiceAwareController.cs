using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common.Contracts;

namespace AvailabilityChartMVC.Controllers.Web.Core
{
    public interface IServiceAwareController
    {
        void RegisterDisposableServices(List<IServiceContract> disposableServices);

        List<IServiceContract> DisposableServices { get; }
    }
}
