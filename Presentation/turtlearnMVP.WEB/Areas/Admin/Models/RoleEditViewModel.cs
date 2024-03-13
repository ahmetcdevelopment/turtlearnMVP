using turtlearnMVP.Domain.DTOs;

namespace turtlearnMVP.WEB.Areas.Admin.Models
{
    public class RoleEditViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name for this role.
        /// </summary>
        public string Name { get; set; }
        public IList<ClaimsByModulesDTO>? Modules { get; set; }
        public string? SelectedIds { get; set; }
    }
}
