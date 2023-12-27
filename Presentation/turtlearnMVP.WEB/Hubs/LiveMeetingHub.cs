using Microsoft.AspNetCore.SignalR;

namespace turtlearnMVP.WEB.Hubs
{
    public class LiveMeetingHub : Hub
    {
        public async Task SendMessageAsync(string message)
        {
            //abone olan tüm clientlar'a mesajı gönder
            //client'ta receiveMessage isimli bir method olacak ona message gönder.
            await Clients.All.SendAsync("receiveMessage", message);
        }
    }
}
