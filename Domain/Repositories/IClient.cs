using Timbangan.Domain.Entities;

namespace Timbangan.Domain.Repositories;

public interface IClient
{
    IQueryable<Client> Clients { get; }

    Task SaveDataAsync(Client client);
}
