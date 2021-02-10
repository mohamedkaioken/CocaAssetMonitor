using CocaAssetMonitoring.Domain.IServices;
using CocaAssetMonitoring.Models.SystemModels;
using CocaAssetMonitoring.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Services
{
    public class MCProcessService : IMCProcessService
    {
        private readonly ApplicationDbContext _context;
        private readonly InterfaceDbContext _interfaceDb;
        private readonly BrokerDbContext _brokerDb;
        public MCProcessService(BrokerDbContext brokerDb, InterfaceDbContext interfaceDb, ApplicationDbContext context)
        {
            _context = context;
            _interfaceDb = interfaceDb;
            _brokerDb = brokerDb;
        }

        public async Task ProcessAsync()
        {
            var machines = _context.MachineInfo.ToList();
            foreach (var machine in machines)
            {
                MCProcess process = new MCProcess
                {
                    MachineId = machine.MachineId,
                    TimeStamp = DateTime.Now
                };
                if (machine.StageName == "Mixer")
                {
                    var machineProcess = _interfaceDb.ConfigsOne.OrderByDescending(r => r.Id).Where(r => r.MachineId == machine.MachineId && r.TimeStamp >= DateTime.Today
                                                                && r.TimeStamp <= DateTime.Now).ToList();
                    if (machineProcess.FirstOrDefault().Syrp == 0)
                    {
                        process.StartTime = machineProcess.FirstOrDefault().TimeStamp;

                    }
                }
                else if (machine.StageName == "Filler")
                {
                    var machineProcess = _interfaceDb.ConfigsTwo.OrderByDescending(r => r.Id).Where(r => r.MachineId == machine.MachineId && r.TimeStamp >= DateTime.Today
                                                                && r.TimeStamp <= DateTime.Now).ToList();
                    if (machineProcess.FirstOrDefault().ProductionCount == 0)
                    {
                        process.StartTime = machineProcess.FirstOrDefault().TimeStamp;

                    }
                }
                else if (machine.StageName == "Palletizer")
                {
                    var machineProcess = _interfaceDb.ConfigsThree.OrderByDescending(r => r.Id).Where(r => r.MachineId == machine.MachineId && r.TimeStamp >= DateTime.Today
                                                                && r.TimeStamp <= DateTime.Now).ToList();
                    if (machineProcess.FirstOrDefault().PallateCount == 0)
                    {
                        process.StartTime = machineProcess.FirstOrDefault().TimeStamp;

                    }
                }
                if(process.StartTime != null)
                {
                    TimeSpan time = (TimeSpan)(DateTime.Now - process.StartTime);

                        process.ProductionTime = (int)time.TotalHours;
                    
                }
                else
                {
                    process.ProductionTime = 0;
                }

                _context.MCProcesses.Add(process);
                

            }
            _context.SaveChanges();
        }
    }
}
