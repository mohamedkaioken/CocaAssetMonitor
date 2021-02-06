using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Domain.IScheduler
{
    public interface IJobsScheduler
    {
        public void ScheduleRecurringJobs();
    }
}
