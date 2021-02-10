using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Models.SystemModels
{
    public class MCProcess
    {
        public int Id { get; set; }
        public string MachineId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int ProductionTime { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
