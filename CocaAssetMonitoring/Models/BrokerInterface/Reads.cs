using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Models.BrokerInterface
{
    public class Reads
    {
        public int Id { get; set; }
        public string BrokerId { get; set; }
        public string MachineId1 { get; set; }
        public int state1 { get; set; }
        public string MachineId2 { get; set; }
        public int state2 { get; set; }
        public string MachineId3 { get; set; }
        public int state3 { get; set; }
        public string MachineId4 { get; set; }
        public int state4 { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
