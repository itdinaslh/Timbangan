using Timbangan.Domain.Entities;

namespace Timbangan.Domain.Repositories;

public interface IPenugasan
{
    IQueryable<Penugasan> Penugasans { get; }

    Task SaveDataAsync(Penugasan penugasan);
}
