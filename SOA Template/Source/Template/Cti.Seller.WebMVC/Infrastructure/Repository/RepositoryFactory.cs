using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecrm.Infrastructure.Repository
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public ILeadsRepository CreateLeadsRepository()
        {
            return new LeadsRepository();
        }
    }
}