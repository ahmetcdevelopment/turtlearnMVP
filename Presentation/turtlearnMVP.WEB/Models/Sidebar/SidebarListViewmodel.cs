using turtlearnMVP.Domain.DTOs;

namespace turtlearnMVP.WEB.Models
{
    public class SidebarListViewmodel
    {
        public List<SessionDTO>? Sessions { get; set; }

        public int CourseId { get; set; }
    }
}
