using System;

namespace ecrm.Domain.Model
{
    public class BatchProcess
    {
        public int BatchProcessID { get; set; }
        public Guid BatchID { get; set; }
        public int RowID { get; set; }
    }
}