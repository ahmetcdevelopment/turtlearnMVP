using Microsoft.AspNetCore.Mvc;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.WEB.Models;

namespace turtlearnMVP.WEB.ViewComponents
{
    public class SidebarViewComponent :ViewComponent
    {
        private readonly ISessionService _sessionService;
        public SidebarViewComponent(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        public IViewComponentResult Invoke(dynamic arguments)
        {
            var model = new SidebarListViewmodel();
            var sessions = _sessionService.FetchAllDtos().Data.Where(x => x.CourseId  == arguments.CourseId).OrderBy(x => x.Queue).ToList();
            model.Sessions = sessions;
            model.CourseId = arguments.CourseId;
            return View(model);
        }
    }
}
