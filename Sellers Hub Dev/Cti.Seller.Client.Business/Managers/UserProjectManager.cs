using Cti.Seller.Client.BusinessDomain.Contracts;
using Cti.Seller.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cti.Seller.Client.BusinessDomain.Managers
{
    public class UserProjectManager : ManagerBase , IUserProjects
    {

        public List<Unit> GetUnits(Project proj,String CurrentPhase, String CurrentFloor)
        {
            List<Unit> results = proj.PhaseBuildings.Where(P => P.Name == CurrentPhase)
                                               .SelectMany(P => P.FloorBlocks)
                                               .Where(F => F.Name == CurrentFloor)
                                               .SelectMany(F => F.Units).ToList();


            return results;
        }

    }
}
