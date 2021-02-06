using CocaAssetMonitoring.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Persistence.Context
{
    public class InterfaceDbContext : DbContext
    {
        public DbSet<ConfigOne> ConfigsOne { get; set; }
        public DbSet<ConfigTwo> ConfigsTwo { get; set; }
        public DbSet<ConfigThree> ConfigsThree { get; set; }
        public InterfaceDbContext(DbContextOptions<InterfaceDbContext> options) : base(options) { }
    }
}
