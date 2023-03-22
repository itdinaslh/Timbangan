using Timbangan.Data;
using Timbangan.Domain.Entities;
using Timbangan.Domain.Repositories;

namespace Timbangan.Domain.Services;

public class KendaraanService : IKendaraan
{
    private readonly AppDbContext context;

    public KendaraanService(AppDbContext context) => this.context = context;

    public IQueryable<Kendaraan> Kendaraans => context.Kendaraans;

    public async Task SaveDataAsync(Kendaraan kendaraan)
    {
        if (kendaraan.KendaraanID == 0)
        {
            await context.Kendaraans.AddAsync(kendaraan);
        } else
        {
            Kendaraan? data = await context.Kendaraans.FindAsync(kendaraan.KendaraanID);

            if (data is not null)
            {
                data.NoPolisi = kendaraan.NoPolisi;
                data.NoPintu = kendaraan.NoPintu;
                data.UniqueID = kendaraan.UniqueID;
                data.RFID = kendaraan.RFID;
                data.ClientID = kendaraan.ClientID;
                data.AreaKerjaID = kendaraan.AreaKerjaID;
                data.TipeKendaraanID = kendaraan.TipeKendaraanID;
                data.RodaID = kendaraan.RodaID;
                data.StatusID = kendaraan.StatusID;
                data.BeratKIR = kendaraan.BeratKIR;
                data.UpdatedBy = kendaraan.UpdatedBy;
                data.UpdatedAt = DateTime.Now;

                context.Kendaraans.Update(data);
            }
        }

        await context.SaveChangesAsync();
    }
}
