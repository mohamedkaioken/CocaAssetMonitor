using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Models.SystemModels
{
    public class MachineInfo
    {
        public int Id { get; set; }
        public string MachineId { get; set; }
        public string? FactoryName { get; set; }
        public string? StageName { get; set; }
        public int? LineNumber { get; set; }
        public string? MachineName { get; set; }
        public ICollection<StopageReason> StopageReasons { get; set; }
        public int AccumulativeFlag { get; set; }

    }
}
