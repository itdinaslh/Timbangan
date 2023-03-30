using Microsoft.AspNetCore.SignalR;

namespace Timbangan.Hubs;

public class PrintHub : Hub
{
    public async Task PrintStruk(string noStruk, string noPolisi, string noPintu, 
        string penugasan, string masuk, string keluar, string beratMasuk, string beratKeluar, string nett)
    {
        await Clients.All.SendAsync("PrintStruk", noStruk, noPolisi, noPintu, penugasan, masuk, keluar, beratMasuk, beratKeluar, nett);
    }

    public async Task StatusChange(string status)
    {
        await Clients.All.SendAsync("StatusChange", status);
    }
}
