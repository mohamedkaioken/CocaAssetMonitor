using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Models
{
    public class ConfigOne
    {
        public int Id { get; set; }
        public string BrokerId { get; set; }
        public string MachineId { get; set; }
        public int State { get; set; }
        public decimal Fault { get; set; }
        public decimal Co2 { get; set; }
        public decimal H2O { get; set; }
        public decimal Syrp { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
