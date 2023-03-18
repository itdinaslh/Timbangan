using Timbangan.Data;
using Timbangan.Domain.Entities;
using Timbangan.Domain.Repositories;

namespace Timbangan.Domain.Services;

public class PenugasanService : IPenugasan
{
    private readonly AppDbContext context;

    public PenugasanService(AppDbContext context)
    {
        this.context = context;
    }

    public IQueryable<Penugasan> Penugasans => context.Penugasans;

    public async Task SaveDataAsync(Penugasan penugasan)
    {
        if (penugasan.PenugasanID == String.Empty)
        {
            await context.Penugasans.AddAsync(penugasan);
        } else
        {
            Penugasan? data = await context.Penugasans.FindAsync(penugasan.PenugasanID);

            if (data is not null)
            {
                data.NamaPenugasan = penugasan.NamaPenugasan;
                data.UpdatedAt = DateTime.Now;

                context.Penugasans.Update(data);
            }
        }

        await context.SaveChangesAsync();
    }
}
