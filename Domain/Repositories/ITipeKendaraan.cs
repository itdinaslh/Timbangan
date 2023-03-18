using Timbangan.Domain.Entities;

namespace Timbangan.Domain.Repositories;

public interface ITipeKendaraan
{
    IQueryable<TipeKendaraan> TipeKendaraans { get; }

    Task SaveDataAsync(TipeKendaraan tipe);
}
