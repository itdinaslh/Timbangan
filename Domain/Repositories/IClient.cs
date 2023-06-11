using Timbangan.Domain.Entities;

namespace Timbangan.Domain.Repositories;

public interface IClient
{
    IQueryable<Client> Clients { get; }

    IQueryable<ClientType> ClientTypes { get; }

    Task SaveDataAsync(Client client);
}
