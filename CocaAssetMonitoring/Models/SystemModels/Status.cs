using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Models.SystemModels
{
    public class Status
    {
        public int Id { get; set; }
        public int State { get; set; }
        public string MachineId { get; set; }
        public DateTime TimeStamp { get; set; } 
    }
}
