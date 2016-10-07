using ecrm.Domain.Model;
using ecrm.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecrm.Infrastructure.Repository
{
    public interface IBaseRepository : IDisposable
    {
        Task<int> SaveChanges();
        Task<AuditLog> CreateAuditLog(int userID, AuditLogRecordTypeEnum recordType, AuditLogTransactionEnum transactionType, int recordID, Object viewModelObject);
        Task CreateAuditLogBatch(int userID, AuditLogRecordTypeEnum recordType, AuditLogTransactionEnum transactionType, IList<int> recordIDs, string message);
    }
}
