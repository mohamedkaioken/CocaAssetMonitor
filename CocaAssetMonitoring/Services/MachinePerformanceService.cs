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
                        var speedReads = _interfaceDb.ConfigsTwo.Where(r => r.MachineId == machine.MachineId
                                                                        && r.TimeStamp >= DateTime.Now.AddMinutes(-15)
                                                                        && r.TimeStamp <= DateTime.Now).ToList();
                        if (designedSpeed != null)
                        {
                            var machineSpeed = speedReads.Select(r => r.ActualSpeed).DefaultIfEmpty(0).Average();
                            machinePerformance.Performance = machineSpeed / designedSpeed.DesignedSpeed;
                        }
                    }
                    else if(machine.StageName == "Palletizer")
                    {
                        var designedSpeed = _context.Settings.Where(r => r.MachineId == machine.MachineId).FirstOrDefault();
                        var speedReads = _interfaceDb.ConfigsThree.Where(r => r.MachineId == machine.MachineId
                                                                        && r.TimeStamp >= DateTime.Now.AddMinutes(-15)
                                                                        && r.TimeStamp <= DateTime.Now).ToList();
                        if (designedSpeed != null)
                        {
                            var palletCount = speedReads.Select(r => r.PallateCount).DefaultIfEmpty(0).Average();
                            var machineSpeed = (decimal)palletCount / 15;
                            machinePerformance.Performance = machineSpeed / designedSpeed.DesignedSpeed;
                        }
                    }
                    else if(machine.StageName == "Filler")
                    {
                        var designedSpeed = _context.Settings.Where(r => r.MachineId == machine.MachineId).FirstOrDefault();
                        var speedReads = _interfaceDb.ConfigsTwo.Where(r => r.TimeStamp >= DateTime.Now.AddMinutes(-15)
                                                                        && r.TimeStamp <= DateTime.Now).ToList();
                        if (designedSpeed != null)
                        {
                            var machineSpeed = speedReads.Select(r => r.ActualSpeed).DefaultIfEmpty(0).Average();
                            machinePerformance.Performance = machineSpeed / designedSpeed.DesignedSpeed;
                        }
                    }
                   

                    var statusReads = _interfaceDb.ConfigsTwo.Where(r => r.MachineId == machine.MachineId
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

                    }


                    if (machine.AccumulativeFlag == 0)
                    {
                        var productionCount = _brokerDb.SignalBrokerTypeTwo.OrderByDescending(r => r.Id).Where(r => r.MachineId == machine.MachineId && r.TimeStamp >= DateTime.Today && r.TimeStamp <= DateTime.Now).FirstOrDefault();
                        if (productionCount != null)
                        {
                            var TotalCount = productionCount.ProductionCount;
                            var RejectedCount = productionCount.RejectedCount;
                            machinePerformance.Quality = RejectedCount / TotalCount;
                        }
                        else
                        {
                            var productCount = _brokerDb.SignalBrokerTypeTwo.OrderByDescending(r => r.Id).Where(r => r.TimeStamp >= DateTime.Today 
                                                                                                                  && r.TimeStamp <= DateTime.Now).FirstOrDefault();
                            if(productCount!=null)
                            {
                                var TotalCount = productCount.ProductionCount;
                                var RejectedCount = productCount.RejectedCount;
                                machinePerformance.Quality = RejectedCount / TotalCount;
                            }
                        }
                    }
                    else if (machine.AccumulativeFlag == 1)
                    {
                        var productionCount = _brokerDb.SignalBrokerTypeTwo.Where(r => r.MachineId == machine.MachineId && r.TimeStamp >= DateTime.Today && r.TimeStamp <= DateTime.Now).ToList();
                        if (productionCount.Any())
                        {
                            var TotalCount = productionCount.Select(r => r.ProductionCount).DefaultIfEmpty(0).Sum();
                            var RejectedCount = productionCount.Select(r => r.RejectedCount).DefaultIfEmpty(0).Sum();
                            machinePerformance.Quality = RejectedCount / TotalCount;
                        }
                        else
                        {
                            var productCount = _brokerDb.SignalBrokerTypeTwo.Where(r => r.MachineId == machine.MachineId && r.TimeStamp >= DateTime.Today && r.TimeStamp <= DateTime.Now).ToList();
                            if (productCount != null)
                            {
                                var TotalCount = productCount.Select(r => r.ProductionCount).DefaultIfEmpty(0).Sum();
                                var RejectedCount = productCount.Select(r => r.RejectedCount).DefaultIfEmpty(0).Sum();
                                machinePerformance.Quality = RejectedCount / TotalCount;
                            }
                        }
                        machinePerformance.OEE = machinePerformance.Quality * machinePerformance.Performance * machinePerformance.Availability; 
                    }
                    _context.MachinePerformance.Add(machinePerformance);

                }
            _context.SaveChanges();

        }
    }
}
