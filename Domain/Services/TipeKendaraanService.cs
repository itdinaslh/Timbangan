using Timbangan.Data;
using Timbangan.Domain.Entities;
using Timbangan.Domain.Repositories;

namespace Timbangan.Domain.Services;

public class TipeKendaraanService : ITipeKendaraan
{
    private readonly AppDbContext context;

    public TipeKendaraanService(AppDbContext context) => this.context = context;

    public IQueryable<TipeKendaraan> TipeKendaraans => context.TipeKendaraans;

    public async Task SaveDataAsync(TipeKendaraan tipe)
    {
        if (tipe.TipeKendaraanID == 0)
        {
            await context.AddAsync(tipe);
        } else
        {
            TipeKendaraan? data = await context.TipeKendaraans.FindAsync(tipe.TipeKendaraanID);

            if (data is not null)
            {
                data.NamaTipe = tipe.NamaTipe;
                data.Kode = tipe.Kode;

                context.TipeKendaraans.Update(data);
            }
        }

        await context.SaveChangesAsync();
    }
}
