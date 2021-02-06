using CocaAssetMonitoring.Domain.IJobs;
using CocaAssetMonitoring.Domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Scheduler.Jobs
{
    public class MachinePerformanceJob : IMachinePerformanceJob
    {
        private readonly IMachinePerformanceService _service;
        public MachinePerformanceJob(IMachinePerformanceService service)
        {
            _service = service;
        }
        public async Task TriggerRecurring()
        {
            await _service.ProcessAsync();
        }
    }
}
