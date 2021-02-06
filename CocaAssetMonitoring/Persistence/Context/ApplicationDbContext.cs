using CocaAssetMonitoring.Models.SystemModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Settings> Settings { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<MachineProcess> MachineProcesses { get; set; }
        public DbSet<MachineAttributes> MachineAttributes { get; set; }
        public DbSet<MachinePerformance> MachinePerformance { get; set; }
        public DbSet<MachineInfo> MachineInfo { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
