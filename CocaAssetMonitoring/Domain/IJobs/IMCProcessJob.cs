using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Domain.IJobs
{
    public interface IMCProcessJob
    {
        public Task TriggerRecurring();
    }
}
