using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Models.BrokerInterface
{
    public class ReadsTypetwo
    {
        public int Id { get; set; }
        public string BrokerId { get; set; }
        public string MachineId { get; set; }
        public int ProductionCount { get; set; }
        public int AcceptedCount { get; set; }
        public int RejectedCount { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
