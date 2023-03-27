using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timbangan.Domain.Repositories;
using Timbangan.Helpers;
using Timbangan.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Timbangan.Controllers;

public class TransactionController : Controller
{
    private readonly ITransaction repo;
    private readonly IKendaraan kRepo;

    public TransactionController(ITransaction repo, IKendaraan kRepo)
    {
        this.repo = repo;
        this.kRepo = kRepo;
    }

    [HttpGet("/transaction/masuk")]
    [Authorize(Roles = "OpMasuk")]
    public IActionResult Index()
    {
        return View("~/Views/Transaksi/Masuk/Masuk.html");
    }

    [HttpPost("/transaction/masuk/store")]
    [Authorize(Roles = "SysAdmin, OpMasuk")]
    public async Task<IActionResult> StoreMasuk(string noRFID, string berat)
    {
        string rf = noRFID;
        if (rf is not null)
        {
            Kendaraan? truk = await kRepo.Kendaraans
                .Include(x => x.AreaKerja.Penugasan)
                .FirstOrDefaultAsync(x => x.RFID == rf);

            if (truk is not null)
            {
                Transaction trans = new();

                trans.NoPolisi = truk.NoPolisi;
                trans.NoPintu = truk.NoPintu;
                trans.BeratMasuk = Convert.ToInt32(berat);
                trans.TglMasuk = DateOnly.FromDateTime(DateTime.Now);
                trans.JamMasuk = TimeOnly.FromDateTime(DateTime.Now);
                trans.InDateTime = DateTime.Now;
                trans.CreatedBy = User.Identity!.Name;
                trans.KendaraanID = truk.KendaraanID;
                trans.StatusID = 1;
                trans.RFID = truk.RFID;
                trans.AreaKerjaName = truk.AreaKerja.NamaArea;
                trans.PenugasanName = truk.AreaKerja.Penugasan.NamaPenugasan;
                trans.UpdatedBy = User.Identity!.Name;
                trans.UpdatedAt = DateTime.Now;

                await repo.AddDataAsync(trans);

                return Json(Result.SuccessMasuk(berat, truk.NoPintu, truk.NoPolisi));
            }
        }

        return Json(Result.Failed());
    }

    [HttpPost("/transaction/keluar/store")]
    [Authorize(Roles = "SysAdmin, OpKeluar")]
    public async Task<IActionResult> StoreKeluar(string noRFID, string berat)
    {
        string rf = noRFID;

        Kendaraan? truk = await kRepo.Kendaraans.FirstOrDefaultAsync(x => x.RFID == rf);

        if (truk is not null)
        {
            Transaction? trans = await repo.Transactions
                .Where(x => x.KendaraanID == truk!.KendaraanID)
                .Where(x => x.StatusID == 1)
                .FirstOrDefaultAsync();

            if (trans is not null)
            {
                trans.BeratKeluar = Convert.ToInt32(berat);
                trans.UpdatedBy = User.Identity!.Name;            

                await repo.UpdateAsync(trans);

                return Json(Result.SuccessKeluar(berat, truk.NoPintu, truk.NoPolisi));
            }
        }

        return Json(Result.Failed());
    }
}
