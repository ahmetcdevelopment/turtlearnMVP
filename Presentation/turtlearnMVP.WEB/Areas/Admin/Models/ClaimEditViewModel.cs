using Microsoft.AspNetCore.Mvc.Rendering;

namespace turtlearnMVP.WEB.Areas.Admin.Models
{
    public class ClaimEditViewModel
    {
        public int? Id { get; set; }
        public int? ClaimTypeId { get; set; }
        public string? ClaimTypeStr { get; set; }
        public string ClaimValue { get; set; }
        public SelectList? SelTableList { get; set; }
    }
}
