using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using turtlearnMVP.WEB.Hubs;

namespace turtlearnMVP.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChatController : Controller
    {
        private readonly IHubContext<LiveMeetingHub> _HubContext;

        public ChatController(IHubContext<LiveMeetingHub> hubContext)
        {
            _HubContext = hubContext;
        }

        public async Task<IActionResult> Index()
        {
            return Redirect($"/Admin/Chat/{Guid.NewGuid()}");
        }
        [Route("testmessenge-{id}")]
        public IActionResult TestMessenge(string id)
        {
            ViewBag.MessengerId = id;
            return View();
        }
        [HttpGet("/Admin/Chat/{roomId}")]
        public IActionResult Room(string roomId)
        {
            ViewBag.roomId = roomId;
            return View();
        }
    }
}
