using AutoMapper.Internal;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using turtlearnMVP.Persistance.Context;

namespace turtlearnMVP.WEB.Hubs;

public class LiveMeetingHub : Hub
{

    private static readonly ConcurrentDictionary<string, List<string>> roomUserIds = new ConcurrentDictionary<string, List<string>>();
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
        var senderName = "deneme";
        await Clients.Group(groupName)
  .SendAsync("ReceiveGroupMessage", message, DateTime.UtcNow, Context.ConnectionId, senderName);
    }
    //public async Task SendGroupMessage(string groupName, string message)
    //{
    //    await Clients.Group(groupName).SendAsync("ReceiveGroupMessage", message, DateTime.UtcNow, Context.ConnectionId);
    //}
    //public async Task JoinRoom(string roomId, string userId)
    //{
    //    await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
    //    await Clients.Group(roomId).SendAsync("user-connected", userId);
    //}


    public async Task JoinRoom(string roomId, string userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

        // Ensure room exists in dictionary
        roomUserIds.TryAdd(roomId, new List<string>());

        // Add user ID to the room's user list
        roomUserIds[roomId].TryAdd(userId);

        await Clients.Group(roomId).SendAsync("user-connected", userId);
    }

    public async Task<IEnumerable<string>> GetConnectedUsers(string roomId)
    {
        // Check if room exists
        if (roomUserIds.TryGetValue(roomId, out var connectedUsers))
        {
            return connectedUsers.ToList();
        }

        return new List<string>(); // Empty list if room doesn't exist
    }

    //public async Task<IEnumerable<string>> GetConnectedUsers()
    //{
    //    // Odadaki bağlı kullanıcıların userId bilgilerini alın
    //    var connectedUserIds = GetConnectedUserIdsFromRoom(Context.ConnectionId); // Oda bilgilerini Context.ConnectionId üzerinden alabilirsiniz.
    //    return connectedUserIds;
    //}

    //private IEnumerable<string> GetConnectedUserIdsFromRoom(string roomId)
    //{
    //    // Odada bulunan tüm kullanıcıların `userId` bilgilerini bir listede toplayacağız.
    //    var connectedUserIds = new List<string>();

    //    // **1. Yol: Clients.ClientList Kullanarak**

    //    // Clients.ClientList, tüm bağlı client'ların bir listesini içerir.
    //    // Her client'ın bilgilerine `x => x.Value` ile erişebilirsiniz.
    //    // Client'ın bağlı olduğu gruplara `x.Value.ConnectionGroups` üzerinden ulaşabilirsiniz.

    //    foreach (var client in Clients.ClientList)
    //    {
    //        // Client'ın bağlı olduğu gruplar arasında "roomId" varsa, o client'ın userId'sini listeye ekleyin.
    //        if (client.Value.ConnectionGroups.Any(group => group.Equals(roomId, StringComparison.OrdinalIgnoreCase)))
    //        {
    //            connectedUserIds.Add(client.Key);
    //        }
    //    }

    //    // **2. Yol: Groups.GetClientsInGroup Kullanarak**

    //    // Groups.GetClientsInGroup, belirli bir gruptaki tüm client'ların bir listesini döndürür.

    //    //var clientsInRoom = await Groups.GetClientsInGroupAsync(roomId);
    //    //connectedUserIds = clientsInRoom.ToList();

    //    // **3. Yol: Kendi Veritabanı veya Depolama Sisteminizi Kullanarak**

    //    // Odadaki kullanıcıların listesini kendi veritabanınız veya depolama sisteminizde tutuyorsanız,
    //    // buradan gerekli bilgileri alarak `connectedUserIds` listesine ekleyebilirsiniz.

    //    // Örnek kod:
    //    //var db = new MyDatabaseContext();
    //    //var connectedUsersInRoom = db.ConnectedUsers.Where(x => x.RoomId == roomId).ToList();
    //    //connectedUserIds = connectedUsersInRoom.Select(x => x.UserId).ToList();

    //    // **Hangi yöntemi kullanacağınız, kendi uygulama mimarinize ve veri saklama şeklinize bağlıdır.**

    //    return connectedUserIds;
    //}
}
