using Timbangan.Data;
using Timbangan.Domain.Entities;
using Timbangan.Domain.Repositories;

namespace Timbangan.Domain.Services;

public class AreaKerjaService : IAreaKerja
{
    private readonly AppDbContext context;

    public AreaKerjaService(AppDbContext context) => this.context = context;

    public IQueryable<AreaKerja> AreaKerjas => context.AreaKerjas;

    public async Task SaveDataAsync(AreaKerja area)
    {
        if (area.AreaKerjaID == 0)
        {
            await context.AreaKerjas.AddAsync(area);
        } else
        {
            AreaKerja? data = await context.AreaKerjas.FindAsync(area.AreaKerjaID);

            if (data is not null)
            {
                data.NamaArea = area.NamaArea;
                data.PenugasanID = area.PenugasanID;
                data.UpdatedAt = DateTime.Now;

                context.AreaKerjas.Update(data);
            }
        }

        await context.SaveChangesAsync();
    }
}
