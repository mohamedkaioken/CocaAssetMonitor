using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Domain.IServices
{
    public interface IMachinePerformanceService
    {
        public Task ProcessAsync();
    }
}
