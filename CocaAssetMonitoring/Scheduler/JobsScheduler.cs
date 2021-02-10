using CocaAssetMonitoring.Domain.IScheduler;
using CocaAssetMonitoring.Models;
using CocaAssetMonitoring.Scheduler.Jobs;
using Hangfire;
using Hangfire.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Scheduler
{
    public class JobsScheduler : IJobsScheduler
    {
        public void ScheduleRecurringJobs()
        {
            RecurringJob.RemoveIfExists(nameof(MachineProcessJob));
            RecurringJob.AddOrUpdate<MachineProcessJob>(nameof(MachineProcessJob),
                job => job.TriggerRecurring(),
                "*/15 * * * *");

            RecurringJob.RemoveIfExists(nameof(MachinePerformanceJob));
            RecurringJob.AddOrUpdate<MachinePerformanceJob>(nameof(MachinePerformanceJob),
                job => job.TriggerRecurring(),
                "*/15 * * * *");
            RecurringJob.RemoveIfExists(nameof(StateJob));
            RecurringJob.AddOrUpdate<StateJob>(nameof(StateJob),
                job => job.TriggerRecurring(),
                "*/1 * * * *");
            RecurringJob.RemoveIfExists(nameof(MCProcessJob));
            RecurringJob.AddOrUpdate<MCProcessJob>(nameof(MCProcessJob),
                job => job.TriggerRecurring(),
                "*/10 * * * *");






        }
    }
}
