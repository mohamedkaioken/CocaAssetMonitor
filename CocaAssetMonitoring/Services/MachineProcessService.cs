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
    public class MachineProcessService : IMachineProcessService
    {
        private readonly ApplicationDbContext _context;
        private readonly InterfaceDbContext _interfaceDb;
        private readonly BrokerDbContext _brokerDb;
        public MachineProcessService(BrokerDbContext brokerDb, InterfaceDbContext interfaceDb, ApplicationDbContext context)
        {
            _context = context;
            _interfaceDb = interfaceDb;
            _brokerDb = brokerDb;
        }

        public async Task ProcessAsync()
        {
            var machines = _context.MachineInfo.Where(r => r.StageName == "Broker").ToList();
            if(machines.Any())
            {
                foreach (var machine in machines)
                {
                    MachineProcess machineProcess = new MachineProcess
                    {
                        MachineId = machine.MachineId,
                        TimeStamp = DateTime.Now,

                    };
                if (machine.AccumulativeFlag == 0)
                    {
                        var productionCount = _brokerDb.SignalBrokerTypeTwo.OrderByDescending(r=>r.Id).Where(r => r.MachineId == machine.MachineId && r.TimeStamp >= DateTime.Today && r.TimeStamp <= DateTime.Now).FirstOrDefault();
                        if(productionCount !=null)
                        {
                            machineProcess.TotalCount = productionCount.ProductionCount;
                            machineProcess.AcceptedCount = productionCount.AcceptedCount;
                            machineProcess.RejectedCount = productionCount.RejectedCount;
                        }
                    }
                else if(machine.AccumulativeFlag ==1 )
                    {
                        var productionCount = _brokerDb.SignalBrokerTypeTwo.Where(r => r.MachineId == machine.MachineId && r.TimeStamp >= DateTime.Today && r.TimeStamp <= DateTime.Now).ToList();
                        if (productionCount.Any())
                        {
                            machineProcess.TotalCount = productionCount.Select(r=>r.ProductionCount).DefaultIfEmpty(0).Sum();
                            machineProcess.AcceptedCount = productionCount.Select(r => r.AcceptedCount).DefaultIfEmpty(0).Sum();
                            machineProcess.RejectedCount = productionCount.Select(r=>r.RejectedCount).DefaultIfEmpty(0).Sum();
                        }
                    }
                    _context.MachineProcesses.Add(machineProcess);
                }
                _context.SaveChanges();
            }
           
        }
    }
}
