using CocaAssetMonitoring.Domain.IServices;
using CocaAssetMonitoring.Models.SystemModels;
using CocaAssetMonitoring.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Services
{
    public class MachinePerformanceService : IMachinePerformanceService
    {
        private readonly ApplicationDbContext _context;
        private readonly InterfaceDbContext _interfaceDb;
        private readonly BrokerDbContext _brokerDb;
        public MachinePerformanceService(BrokerDbContext brokerDb, InterfaceDbContext interfaceDb, ApplicationDbContext context)
        {
            _context = context;
            _interfaceDb = interfaceDb;
            _brokerDb = brokerDb;
        }

        public async Task ProcessAsync()
        {
            var machines = _context.MachineInfo.ToList();
            if (machines.Any())
                foreach (var machine in machines)
                {
                    MachinePerformance machinePerformance = new MachinePerformance
                    {
                        MachineId = machine.MachineId,
                        TimeStamp = DateTime.Now
                    };

                        


                    if (machine.StageName == "Mixer")
                    {
                        var designedSpeed = _context.Settings.Where(r => r.MachineId == machine.MachineId).FirstOrDefault();
                        var speedReads = _context.MachineProcesses.Where(r => r.MachineId == machine.MachineId && r.ActualSpeed > 0 && r.TimeStamp >= DateTime.Today
                                                                        && r.TimeStamp <= DateTime.Now).ToList();

                        var machineSpeed = (decimal)speedReads.Where(r=>r.ActualSpeed > 0).Select(r => r.ActualSpeed).DefaultIfEmpty(0).Average();
                        machinePerformance.Performance = (decimal)machineSpeed / designedSpeed.DesignedSpeed;

                    }
                    else if (machine.StageName == "Filler")
                    {
                        var designedSpeed = _context.Settings.Where(r => r.MachineId == machine.MachineId).FirstOrDefault();
                        var speedReads = _context.MachineProcesses.Where(r => r.MachineId == machine.MachineId && r.ActualSpeed > 0 && r.TimeStamp >= DateTime.Today
                                                                        && r.TimeStamp <= DateTime.Now).ToList();

                        var machineSpeed = speedReads.Where(r => r.ActualSpeed > 0).Select(r => r.ActualSpeed).DefaultIfEmpty(0).Average();
                        machinePerformance.Performance = (decimal)machineSpeed / designedSpeed.DesignedSpeed;

                    }
                    else if (machine.StageName == "Palletizer")
                    {
                        var designedSpeed = _context.Settings.Where(r => r.MachineId == machine.MachineId).FirstOrDefault();
                        var speedReads = _context.MachineProcesses.Where(r => r.MachineId == machine.MachineId && r.ActualSpeed > 0 && r.TimeStamp >= DateTime.Today
                                                                        && r.TimeStamp <= DateTime.Now).ToList();

                        var machineSpeed = speedReads.Where(r => r.ActualSpeed > 0).Select(r => r.ActualSpeed).DefaultIfEmpty(0).Average();
                        machinePerformance.Performance = (decimal)machineSpeed / designedSpeed.DesignedSpeed;

                    }


                    if (machine.StageName == "Mixer")
                    {
                        var allState = (decimal)_interfaceDb.ConfigsOne.Where(r => 
                                                                   r.TimeStamp >= DateTime.Today
                                                                   && r.TimeStamp <= DateTime.Now).ToList().Count;
                        var onState = (decimal)_interfaceDb.ConfigsOne.Where(r=>r.State == 1 && 
                                                                   r.TimeStamp >= DateTime.Today
                                                                   && r.TimeStamp <= DateTime.Now).ToList().Count;
                        machinePerformance.Availability = onState / allState;

                    }
                    else if (machine.StageName == "Filler")
                    {
                        var allState = (decimal)_interfaceDb.ConfigsTwo.Where(r => 
                                                                   r.TimeStamp >= DateTime.Today
                                                                   && r.TimeStamp <= DateTime.Now).ToList().Count;
                        var onState =(decimal) _interfaceDb.ConfigsTwo.Where(r => r.State == 1 &&
                                                                   r.TimeStamp >= DateTime.Today
                                                                   && r.TimeStamp <= DateTime.Now).ToList().Count;
                        machinePerformance.Availability = (decimal)(onState / allState);

                    }
                    else if (machine.StageName == "Palletizer")
                    {
                        var allState = (decimal) _interfaceDb.ConfigsThree.Where(r => 
                                                                   r.TimeStamp >= DateTime.Today
                                                                   && r.TimeStamp <= DateTime.Now).ToList().Count;
                        var onState = (decimal) _interfaceDb.ConfigsThree.Where(r => r.State == 1 &&
                                                                   r.TimeStamp >= DateTime.Today
                                                                   && r.TimeStamp <= DateTime.Now).ToList().Count;
                        machinePerformance.Availability = onState / allState;

                    }

                    /*var statusReads = _interfaceDb.ConfigsTwo.Where(r => r.MachineId == machine.MachineId
                                                                    && r.TimeStamp >= DateTime.Today
                                                                    && r.TimeStamp <= DateTime.Now).ToList();
                    var totalReads = (decimal)statusReads.Count();
                    if(statusReads.Any())
                    {
                        var OnRead = (decimal)statusReads.Where(r => r.State == 1).ToList().Count();
                        var reads = (OnRead / totalReads) * 100;
                        machinePerformance.Availability = (OnRead / totalReads)*100;
                        Console.WriteLine(machinePerformance.Availability);
                        Console.WriteLine(machinePerformance.Availability);

                    }*/


                    if (machine.AccumulativeFlag == 0)
                    {
                        var productionCount = _brokerDb.SignalBrokerTypeTwo.OrderByDescending(r => r.Id).Where(r => r.ProductionCount > 0).FirstOrDefault();
                        if (productionCount != null)
                        {
                            var TotalCount = (decimal)productionCount.ProductionCount;
                            var RejectedCount = productionCount.RejectedCount;
                            var AcceptedCount = (decimal)productionCount.AcceptedCount;
                            if(TotalCount == 0)
                            {
                                machinePerformance.Quality = 0;
                            }
                            else
                            {
                                machinePerformance.Quality = AcceptedCount / TotalCount;

                            }
                        }
                    }
                    else if (machine.AccumulativeFlag == 1)
                    {
                        var productionCount = _brokerDb.SignalBrokerTypeTwo.Where(r => r.MachineId == machine.MachineId && r.TimeStamp >= DateTime.Today && r.TimeStamp <= DateTime.Now && r.ProductionCount > 0).ToList();
                        if (productionCount.Any())
                        {
                            var TotalCount = (decimal)productionCount.Select(r => r.ProductionCount).DefaultIfEmpty(0).Sum();
                            var RejectedCount = productionCount.Select(r => r.RejectedCount).DefaultIfEmpty(0).Sum();
                            var AcceptedCount = (decimal)productionCount.Select(r => r.AcceptedCount).DefaultIfEmpty(0).Sum();
                            machinePerformance.Quality = AcceptedCount / TotalCount;
                            
                        }
                        
                    }
                    machinePerformance.OEE = machinePerformance.Quality * machinePerformance.Performance * machinePerformance.Availability;
                    _context.MachinePerformance.Add(machinePerformance);

                }
            _context.SaveChanges();

        }
    }
}
