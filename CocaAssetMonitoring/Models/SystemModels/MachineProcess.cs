using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Models.SystemModels
{
    public class MachineProcess
    {
        public int Id { get; set; }
        public string MachineId { get; set; }
        public int ProductType { get; set; }
        public int TotalCount { get; set; }
        public int AcceptedCount { get; set; }
        public int RejectedCount { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
