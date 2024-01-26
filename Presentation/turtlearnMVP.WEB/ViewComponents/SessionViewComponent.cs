using Microsoft.AspNetCore.Mvc;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.WEB.Models;

namespace turtlearnMVP.WEB.ViewComponents
{
    public class SessionViewComponent : ViewComponent
    {
        private readonly ISessionService _sessionService;
        public SessionViewComponent(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        public IViewComponentResult Invoke(dynamic arguments)
        {
            var session = _sessionService.GetById(arguments.Id).Result.Data;
            return View(session);
        }
    }
}
