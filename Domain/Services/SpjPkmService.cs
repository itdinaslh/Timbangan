using Timbangan.Data;
using Timbangan.Domain.Entities;
using Timbangan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Timbangan.Domain.Services;

public class SpjPkmService : ISpjPKM
{
    private readonly PkmDbContext context;

    public SpjPkmService(PkmDbContext context)
    {
        this.context = context;
    }

    public IQueryable<SpjAngkut> SpjAngkuts => context.SpjAngkuts;

    public async Task UpdateSPJ(SpjAngkut data)
    {
        var spj = await context.SpjAngkuts
            .Where(x => x.NoSPJ == data.NoSPJ)
            .FirstOrDefaultAsync();

        if (spj != null) {
            spj.TonaseTimbangan = data.TonaseTimbangan;
            spj.NoStruk = data.NoStruk;

            context.SpjAngkuts.Update(spj);

            await context.SaveChangesAsync();
        }
    }
}