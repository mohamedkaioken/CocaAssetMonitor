using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Models.SystemModels
{
    public class MCAttributes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MachineId { get; set; }
        public string MachineStage { get; set; }
    }
}
