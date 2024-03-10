using Microsoft.AspNetCore.SignalR;
using turtlearnMVP.Persistance.Context;

namespace turtlearnMVP.WEB.Hubs;

public class LiveMeetingHub : Hub
{
    private static List<string> Names { get; set; } = new List<string>();
    public static int ClientCount { get; set; } = 0;
    private static int TeamCount { get; set; } = 3;
    private readonly turtlearnMVPContext _Context;
    public LiveMeetingHub(turtlearnMVPContext context)
    {
        _Context = context;
    }

    public async Task SendMessageAsync(string message)
    {
        //abone olan tüm clientlar'a mesajı gönder
        //client'ta receiveMessage isimli bir method olacak ona message gönder.
        await Clients.All.SendAsync("receiveMessage", message);
    }
    public async Task SendName(string name)
    {
        if (Names.Count >= TeamCount)
        {
            await Clients.Caller.SendAsync("Error", $"Konuşmada en fazla {TeamCount} mesaj olabilir.");
        }
        else
        {
            Names.Add(name);
            await Clients.All.SendAsync("recieveName", name);
        }
    }
    public async Task GetNames()
    {
        await Clients.All.SendAsync("recieveNames", Names);
    }
    public async override Task OnConnectedAsync()
    {
        ClientCount++;
        await Clients.All.SendAsync("receiveClientCount", ClientCount);
        await base.OnConnectedAsync();
    }
    //public async Task AddToGroup(string teamName)
    //{
    //    await Groups.AddToGroupAsync(Context.ConnectionId, teamName);
    //}
    //public async Task SendNameByGroup(string name, string teamName)
    //{
    //    var team = _Context.Chats.Where(x=>x.Name == teamName).FirstOrDefault();
    //    if (team!=null)
    //    {
    //        team
    //    }
    //}
    public async Task RemoveGroup(string teamName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, teamName);
    }
    public async override Task OnDisconnectedAsync(Exception exception)
    {
        ClientCount--;
        await Clients.All.SendAsync("RecieveClientCount", ClientCount);
        await base.OnDisconnectedAsync(exception);
    }
    public async Task JoinGroupChat(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }
    public async Task SendGroupMessage(string groupName, string message)
    {
        await Clients.Group(groupName).SendAsync("ReceiveGroupMessage", message, DateTime.UtcNow, Context.ConnectionId);
    }
    public async Task JoinRoom(string roomId, string userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        await Clients.Group(roomId).SendAsync("user-connected", userId);
    }
}
