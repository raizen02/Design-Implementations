using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecrm.Infrastructure.Repository
{
    public interface IRepositoryFactory
    {
        ILeadsRepository CreateLeadsRepository();
    }
}
