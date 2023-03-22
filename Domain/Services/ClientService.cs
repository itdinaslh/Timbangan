using Timbangan.Data;
using Timbangan.Domain.Entities;
using Timbangan.Domain.Repositories;

namespace Timbangan.Domain.Services;

public class ClientService : IClient
{
    private readonly AppDbContext context;

    public ClientService(AppDbContext context) => this.context = context;

    public IQueryable<Client> Clients => context.Clients;

    public async Task SaveDataAsync(Client client)
    {
        if (client.ClientID == 0)
        {
            await context.Clients.AddAsync(client);
        } else
        {
            Client? data = await context.Clients.FindAsync(client.ClientID);

            if (data is not null)
            {
                data.ClientName = client.ClientName;
                data.StatusID = client.StatusID;
                data.PkmID = client.PkmID;
                data.UpdatedAt = DateTime.Now;

                context.Clients.Update(data);
            }
        }

        await context.SaveChangesAsync();
    }
}
