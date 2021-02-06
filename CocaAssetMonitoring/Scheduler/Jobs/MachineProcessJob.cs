using CocaAssetMonitoring.Domain.IJobs;
using CocaAssetMonitoring.Domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Scheduler.Jobs
{
    public class MachineProcessJob : IMachineProcessJob
    {
        private readonly IMachineProcessService _service;
        public MachineProcessJob(IMachineProcessService service)
        {
            _service = service;
        }
        public async Task TriggerRecurring()
        {
            await _service.ProcessAsync();
        }
    }
}
