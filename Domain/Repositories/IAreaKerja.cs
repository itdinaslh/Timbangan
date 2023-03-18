using Timbangan.Domain.Entities;

namespace Timbangan.Domain.Repositories;

public interface IAreaKerja
{
    IQueryable<AreaKerja> AreaKerjas { get; }

    Task SaveDataAsync(AreaKerja areaKerja);
}
