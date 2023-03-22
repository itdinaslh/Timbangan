using Timbangan.Data;
using Timbangan.Domain.Entities;
using Timbangan.Domain.Repositories;

namespace Timbangan.Domain.Services;

public class RodaService : IRoda
{
    private readonly AppDbContext context;

    public RodaService(AppDbContext context) => this.context = context;

    public IQueryable<Roda> Rodas => context.Rodas;
}
