using ecrm.Domain.Model;
using System.Data.Entity;

namespace ecrm.Infrastructure.Repository
{
    public class EcrmContext : DbContext
    {
        public virtual DbSet<Seller> Sellers { get; set; }
        public virtual DbSet<Lead> Leads { get; set; }
        public virtual DbSet<Offering> Offerings { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<AuditLog> AuditLog { get; set; }
        public virtual DbSet<BatchProcess> BatchProcess { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<EcrmContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}