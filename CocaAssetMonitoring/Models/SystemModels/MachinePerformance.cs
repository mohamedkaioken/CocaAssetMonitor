using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Models.SystemModels
{
    public class MachinePerformance
    {
        public int Id { get; set; }
        public string MachineId { get; set; }
        public decimal EnergyConsumed { get; set; }
        public decimal Performance { get; set; }
        public decimal Availability { get; set; }
        public decimal Quality { get; set; }
        public decimal OEE { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}
