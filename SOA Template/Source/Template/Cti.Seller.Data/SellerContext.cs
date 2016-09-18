using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Runtime.Serialization;
using Cti.Seller.Business.Entities;
using Core.Common.Contracts;

namespace Cti.Seller.Data
{
    public class SellerContext : DbContext
    {
        public SellerContext()
            : base("name=Seller")
        {
            Database.SetInitializer<SellerContext>(null);
        }

        public DbSet<Account> AccountSet { get; set; }

        public DbSet<Unit> UnitSet { get; set; }
       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

            modelBuilder.Entity<Account>().HasKey<int>(e => e.AccountId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Unit>().HasKey<int>(e => e.UnitId ).Ignore(e => e.EntityId);

        }
    }
}
