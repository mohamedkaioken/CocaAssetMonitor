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
            var machines = _context.MachineInfo.ToList();
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
                        if(machine.StageName == "Inspection")
                        {
                            var productionCount = _brokerDb.SignalBrokerTypeTwo.OrderByDescending(r => r.Id).Where(r => r.MachineId == machine.MachineId && r.TimeStamp >= DateTime.Today && r.TimeStamp <= DateTime.Now).FirstOrDefault();
                            if (productionCount != null)
                            {
                                machineProcess.TotalCount = productionCount.ProductionCount;
                                machineProcess.AcceptedCount = productionCount.AcceptedCount;
                                machineProcess.RejectedCount = productionCount.RejectedCount;
                            }
                        }
                        else if(machine.StageName== "Filler")
                        {
                            var productionCount = _interfaceDb.ConfigsTwo.OrderByDescending(r => r.Id).Where(r => r.ProductionCount > 0 && r.TimeStamp >= DateTime.Today && r.TimeStamp <= DateTime.Now).FirstOrDefault().ProductionCount;
                            machineProcess.TotalCount = productionCount;
                            machineProcess.AcceptedCount = 0;
                            machineProcess.RejectedCount = 0;
                        }
                        else if (machine.StageName == "Palletizer")
                        {
                            var productionCount = _interfaceDb.ConfigsThree.OrderByDescending(r => r.Id).Where(r => r.PallateCount > 0 && r.TimeStamp >= DateTime.Today && r.TimeStamp<=DateTime.Now).FirstOrDefault().PallateCount;
                            machineProcess.TotalCount = productionCount;
                            machineProcess.AcceptedCount = 0;
                            machineProcess.RejectedCount = 0;
                        }
                        else if(machine.StageName == "Mixer")
                        {
                            var productionCount = _interfaceDb.ConfigsOne.OrderByDescending(r => r.Id).Where(r => r.Syrp > 0 && r.TimeStamp >= DateTime.Today && r.TimeStamp <= DateTime.Now).FirstOrDefault().Syrp;
                            machineProcess.TotalCount = (int)productionCount;
                            machineProcess.AcceptedCount = 0;
                            machineProcess.RejectedCount = 0;
                        }

                    }
                else if(machine.AccumulativeFlag ==1 )
                    {
                        if (machine.StageName == "Inspection")
                        {
                            var productionCount = _brokerDb.SignalBrokerTypeTwo.Where(r => r.MachineId == machine.MachineId && r.TimeStamp >= DateTime.Today && r.TimeStamp <= DateTime.Now).ToList();
                            if (productionCount.Any())
                            {
                                machineProcess.TotalCount = productionCount.Select(r => r.ProductionCount).DefaultIfEmpty(0).Sum();
                                machineProcess.AcceptedCount = productionCount.Select(r => r.AcceptedCount).DefaultIfEmpty(0).Sum();
                                machineProcess.RejectedCount = productionCount.Select(r => r.RejectedCount).DefaultIfEmpty(0).Sum();
                            }
                        }
                        else if (machine.StageName == "Filler")
                        {
                            var productionCount = _interfaceDb.ConfigsTwo.Where(r => r.ProductionCount > 0).FirstOrDefault().ProductionCount;
                            machineProcess.TotalCount = productionCount;
                            machineProcess.AcceptedCount = 0;
                            machineProcess.RejectedCount = 0;
                        }
                        else if (machine.StageName == "Palletizer")
                        {
                            var productionCount = _interfaceDb.ConfigsThree.Where(r => r.PallateCount > 0).FirstOrDefault().PallateCount;
                            machineProcess.TotalCount = productionCount;
                            machineProcess.AcceptedCount = 0;
                            machineProcess.RejectedCount = 0;
                        }
                        
                    }
                     if (machine.StageName == "Filler")
                    {
                        var actualSpeed = _interfaceDb.ConfigsTwo.OrderByDescending(r => r.Id).Where(r => r.ActualSpeed > 0 && r.TimeStamp >= DateTime.Today && r.TimeStamp <= DateTime.Now).ToList();
                        machineProcess.ActualSpeed = (int)actualSpeed.Select(r => r.ActualSpeed).DefaultIfEmpty(0).Average();
                    }
                    else if (machine.StageName == "Palletizer")
                    {
                        var actualSpeed = _interfaceDb.ConfigsThree.OrderByDescending(r=>r.Id).Where(r=>r.PallateCount > 0 && r.TimeStamp >= DateTime.Today && r.TimeStamp <= DateTime.Now).ToList();
                        var firstPallete = actualSpeed.FirstOrDefault().PallateCount;
                        var lastPallete = actualSpeed.LastOrDefault().PallateCount;
                        var diffDleta = (decimal) lastPallete- firstPallete ;
                        var speed = diffDleta / 96;
                        machineProcess.ActualSpeed = (int)(speed);
                    }
                    else if (machine.StageName == "Mixer")
                    {
                        var actualSpeed = _interfaceDb.ConfigsOne.OrderByDescending(r => r.Id).Where(r => r.Syrp > 0 && r.H2O>0 && r.TimeStamp >= DateTime.Today && r.TimeStamp <= DateTime.Now).ToList();
                        var firstH2O = actualSpeed.FirstOrDefault().H2O;
                        var lastH2O = actualSpeed.LastOrDefault().H2O;
                        var firstSyrp = actualSpeed.FirstOrDefault().Syrp;
                        var lastSyrp = actualSpeed.LastOrDefault().Syrp;
                        var firstDelta = (int)firstH2O + firstSyrp;
                        var lastDelta = (int)lastH2O + lastSyrp;
                        var diffDelta = (int)firstDelta - lastDelta;
                        machineProcess.ActualSpeed = (int)diffDelta / 15;

                    }

                    _context.MachineProcesses.Add(machineProcess);
                }
                _context.SaveChanges();
            }
           
        }
    }
}
