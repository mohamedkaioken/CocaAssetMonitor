using CocaAssetMonitoring.Domain.IJobs;
using CocaAssetMonitoring.Domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CocaAssetMonitoring.Scheduler.Jobs
{
    public class StateJob : IStateJob
    {
        private readonly IStateService _service;
        public StateJob(IStateService service)
        {
            _service = service;
        }
        public async Task TriggerRecurring()
        {
            await _service.ProcessAsync();
        }
    }
}
