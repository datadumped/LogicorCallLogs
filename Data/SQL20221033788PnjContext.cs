using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LogicorSupportCalls.Models.SQL2022_1033788_pnj;

namespace LogicorSupportCalls.Data
{
    public partial class SQL2022_1033788_pnjContext : DbContext
    {
        public SQL2022_1033788_pnjContext()
        {
        }

        public SQL2022_1033788_pnjContext(DbContextOptions<SQL2022_1033788_pnjContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog>()
              .Property(p => p.CallDate)
              .HasColumnType("datetime");
            this.OnModelBuilding(builder);
        }

        public DbSet<LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog> LogicorSupportCallLogs { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    
    }
}