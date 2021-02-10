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
            var machines = _context.MachineInfo.ToList();
            foreach (var machine in machines)
            {
                Status status = new Status
                {
                    MachineId=machine.MachineId,
                    TimeStamp =DateTime.Now
                };
                var configOne = _interfaceDb.ConfigsOne.OrderByDescending(r=>r.Id).Where(r => r.MachineId == machine.MachineId && r.TimeStamp >= DateTime.Today
                                                              && r.TimeStamp <= DateTime.Now).FirstOrDefault();
                var configTwo = _interfaceDb.ConfigsTwo.OrderByDescending(r=>r.Id).Where(r => r.MachineId == machine.MachineId && r.TimeStamp >= DateTime.Today
                                                              && r.TimeStamp <= DateTime.Now).FirstOrDefault();
                var configThree = _interfaceDb.ConfigsThree.OrderByDescending(r=>r.Id).Where(r => r.MachineId == machine.MachineId && r.TimeStamp >= DateTime.Today
                                                              && r.TimeStamp <= DateTime.Now).FirstOrDefault();
                if(configOne !=null)
                {
                    status.State = configOne.State;
                }
                else if (configTwo != null)
                {
                    status.State = configTwo.State;
                }
                else if (configThree != null)
                {
                    status.State = configThree.State;
                }
                _context.Status.Add(status);
            }
            _context.SaveChanges();
        }
    }
}
