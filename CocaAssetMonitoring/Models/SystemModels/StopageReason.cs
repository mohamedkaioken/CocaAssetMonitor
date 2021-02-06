using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CocaAssetMonitoring.Models.SystemModels
{
    public class StopageReason
    {
        public int Id { get; set; }

        public string Reason { get; set; }

        public MachineInfo machine { get; set; }

        [ForeignKey("machine")]
        public int machineId { get; set; }


    }
}
