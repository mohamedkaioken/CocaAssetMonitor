using CocaAssetMonitoring.Domain.IServices;
using CocaAssetMonitoring.Models.BrokerInterface;
using CocaAssetMonitoring.Models.SystemModels;
using CocaAssetMonitoring.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Services 
{
    public class StateService : IStateService
    {
        private readonly ApplicationDbContext _context;
        private readonly InterfaceDbContext _interfaceDb;
        private readonly BrokerDbContext _brokerDb;
        public StateService(BrokerDbContext brokerDb, InterfaceDbContext interfaceDb, ApplicationDbContext context)
        {
            _context = context;
            _interfaceDb = interfaceDb;
            _brokerDb = brokerDb;
        }

        public async Task ProcessAsync()
        {
        }
    }
}
