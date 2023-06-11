using Timbangan.Domain.Entities;

namespace Timbangan.Domain.Repositories;

public interface ISpjPKM
{
    IQueryable<SpjAngkut> SpjAngkuts {get;}

    Task UpdateSPJ(SpjAngkut data);
}