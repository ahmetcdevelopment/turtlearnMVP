using Microsoft.AspNetCore.Mvc;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.WEB.ViewComponents
{
    public class SidebarViewComponent :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            int commonCourseId = 101;  // Set a common course ID

            List<Session> sessions = new List<Session>
            {
                new Session { Id = 1, CourseId = commonCourseId, Queue = 1, Name = "Session 1", StartDate = DateTime.Now.AddDays(1), Link = "http://example.com/session1", Description = "Description for Session 1" },
                new Session { Id = 2, CourseId = commonCourseId, Queue = 2, Name = "Session 2", StartDate = DateTime.Now.AddDays(2), Link = "http://example.com/session2", Description = "Description for Session 2" },
                new Session { Id = 3, CourseId = commonCourseId, Queue = 3, Name = "Session 3", StartDate = DateTime.Now.AddDays(3), Link = "http://example.com/session3", Description = "Description for Session 3" },
                new Session { Id = 4, CourseId = commonCourseId, Queue = 4, Name = "Session 4", StartDate = DateTime.Now.AddDays(4), Link = "http://example.com/session4", Description = "Description for Session 4" },
                new Session { Id = 5, CourseId = commonCourseId, Queue = 5, Name = "Session 5", StartDate = DateTime.Now.AddDays(5), Link = "http://example.com/session5", Description = "Description for Session 5" },
                new Session { Id = 6, CourseId = commonCourseId, Queue = 6, Name = "Session 6", StartDate = DateTime.Now.AddDays(6), Link = "http://example.com/session6", Description = "Description for Session 6" },
                new Session { Id = 7, CourseId = commonCourseId, Queue = 7, Name = "Session 7", StartDate = DateTime.Now.AddDays(7), Link = "http://example.com/session7", Description = "Description for Session 7" },
                new Session { Id = 8, CourseId = commonCourseId, Queue = 8, Name = "Session 8", StartDate = DateTime.Now.AddDays(8), Link = "http://example.com/session8", Description = "Description for Session 8" },
                new Session { Id = 9, CourseId = commonCourseId, Queue = 9, Name = "Session 9", StartDate = DateTime.Now.AddDays(9), Link = "http://example.com/session9", Description = "Description for Session 9" },
                new Session { Id = 10, CourseId = commonCourseId, Queue = 10, Name = "Session 10", StartDate = DateTime.Now.AddDays(10), Link = "http://example.com/session10", Description = "Description for Session 10" }
            };

            return View(sessions);
        }
    }
}
