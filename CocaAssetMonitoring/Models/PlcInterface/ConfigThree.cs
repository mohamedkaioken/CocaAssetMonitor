using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Models
{
    public class ConfigThree
    {
        public int Id { get; set; }
        public string BrokerId { get; set; }
        public string MachineId { get; set; }
        public int State { get; set; }
        public decimal Fault { get; set; }
        public int PallateCount { get; set; }
        public int PackCount { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
