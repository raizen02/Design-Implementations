using Cti.Seller.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cti.Seller.Client.BusinessDomain.Contracts
{
    public interface IUserProjects
    {
        List<Unit> GetUnits(Project proj, String CurrentPhase, String CurrentFloor);

    }
}
