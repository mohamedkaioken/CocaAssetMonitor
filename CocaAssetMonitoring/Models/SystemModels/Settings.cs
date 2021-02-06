using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Models.SystemModels
{
    public class Settings
    {
        public int Id { get; set; }
        public string MachineId { get; set; }
        public decimal DesignedSpeed { get; set; }
        public int AccumulativeFlag { get; set; }
    }
}
