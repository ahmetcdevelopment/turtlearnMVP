using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using turtlearnMVP.WEB.Hubs;

namespace turtlearnMVP.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NotificationController : Controller
    {
        //private readonly IHubContext<LiveMeetingHub> _hubContext;
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
