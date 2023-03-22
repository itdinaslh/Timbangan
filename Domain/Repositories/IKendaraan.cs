using Timbangan.Domain.Entities;

namespace Timbangan.Domain.Repositories;

public interface IKendaraan
{
    IQueryable<Kendaraan> Kendaraans { get; }

    Task SaveDataAsync(Kendaraan kendaraan);
}
