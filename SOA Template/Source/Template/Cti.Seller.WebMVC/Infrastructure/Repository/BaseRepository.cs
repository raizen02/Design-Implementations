using ecrm.Domain.Model;
using ecrm.Infrastructure.Enum;
using ecrm.Infrastructure.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ecrm.Infrastructure.Repository
{
    public class BaseRepository : IDisposable
    {
        protected EcrmContext _context;

        public BaseRepository()
        {
            _context = new EcrmContext();

        }

        protected async Task<Guid> StartBatch(IList<int> leadIDs)
        {
            var batchID = Guid.NewGuid();
            var batchProcessRecords = leadIDs.Select(l => new BatchProcess { BatchID = batchID, RowID = l });
            _context.BatchProcess.AddRange(batchProcessRecords);
            await this.SaveChanges();
            return batchID;
        }

        protected async Task EndBatch(Guid batchID)
        {
            await _context.Database.ExecuteSqlCommandAsync(@"DELETE FROM BatchProcesses where BatchID = @batchID", new SqlParameter(@"batchID", batchID));
        }

        public async Task<int> SaveChanges()
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);
            _context.Configuration.ValidateOnSaveEnabled = false;
            var result = await _context.SaveChangesAsync();
            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return result;
        }

        public async Task<AuditLog> CreateAuditLog(int userID, AuditLogRecordTypeEnum recordType, AuditLogTransactionEnum transactionType, int recordID, Object viewModelObject)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);

            var auditLog = new AuditLog();
            auditLog.UserID = userID;
            auditLog.RecordType = (int)recordType;
            auditLog.TransactionType = (int)transactionType;
            auditLog.RecordID = recordID;
            auditLog.Data = JsonConvert.SerializeObject(viewModelObject);
            auditLog.CreatedDate = DateTime.Now;
            auditLog.UpdatedDate = DateTime.Now;

            AuditLog record = null;
            try
            {
                record = _context.AuditLog.Add(auditLog);
                var result = await this.SaveChanges();
            }
            catch (Exception e)
            {
                EcrmEventSource.Log.Error(this.GetType().FullName, e.ToString());
            }

            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return record;
        }

        public async Task CreateAuditLogBatch(int userID, AuditLogRecordTypeEnum recordType, AuditLogTransactionEnum transactionType, IList<int> recordIDs, string message)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);

            var logs = recordIDs.Select(r => new AuditLog
            {
                UserID = userID,
                RecordType = (int)recordType,
                TransactionType = (int)transactionType,
                RecordID = r,
                Data = string.Format(message, r),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });

            try
            {
                _context.AuditLog.AddRange(logs);
                var result = await this.SaveChanges();
            }
            catch (Exception e)
            {
                EcrmEventSource.Log.Error(this.GetType().FullName, e.ToString());
            }

            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}