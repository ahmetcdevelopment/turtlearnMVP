using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turtlearnMVP.Domain.DTOs
{
    public class ClaimDTO
    {
        public int Id { get; set; }
        public int ClaimTypeId { get; set; }
        public string ClaimTypeStr { get; set; }
        public string ClaimValue { get; set; }
        public string? Checked { get; set; } = "";
    }
}
