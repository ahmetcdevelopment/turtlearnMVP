using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turtlearnMVP.Domain.DTOs
{
    public class ClaimsByModulesDTO
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public List<ClaimDTO>? Claims { get; set; }
        
    }
}
