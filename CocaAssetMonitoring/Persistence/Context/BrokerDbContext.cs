using CocaAssetMonitoring.Models.BrokerInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Persistence.Context
{
    public class BrokerDbContext : DbContext
    {
        public DbSet<Reads> SignalBroker { get; set; }
        public DbSet<ReadsTypetwo> SignalBrokerTypeTwo { get; set; }
        public BrokerDbContext(DbContextOptions<BrokerDbContext> options) : base(options) { }
    }
}
