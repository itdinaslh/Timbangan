using Timbangan.Domain.Entities;

namespace Timbangan.Domain.Repositories;

public interface IRoda
{
    IQueryable<Roda> Rodas { get; }
}
