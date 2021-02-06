using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Models.SystemModels
{
    public class MachineAttributes
    {
        public int Id { get; set; }
        public string MachineId { get; set; }
        public int State { get; set; }
        public decimal Speed { get; set; }
        public decimal Temperature { get; set; }
        public decimal Power { get; set; }
        public decimal PowerFactor { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
