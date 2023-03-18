using Microsoft.AspNetCore.SignalR;

namespace Timbangan.Hubs;   

public class ScaleHub : Hub
{
    public async Task Timbangan1(string value)
    {
        await Clients.All.SendAsync("Timbangan1", value);
    }

    public async Task Timbangan2(string value)
    {
        await Clients.All.SendAsync("Timbangan2", value);
    }


    public async Task Timbangan3(string value)
    {
        await Clients.All.SendAsync("Timbangan3", value);
    }
}
