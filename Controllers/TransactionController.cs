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
        return View("~/Views/Transaksi/Masuk/Index.html");
    }

    [HttpPost("/transaction/masuk/store")]
    public async Task<IActionResult> StoreMasuk(string noRFID, string berat)
    {
        string rf = noRFID;
        if (rf is not null)
        {
            Kendaraan? truk = await kRepo.Kendaraans.FirstOrDefaultAsync(x => x.RFID == rf);

            if (truk is not null)
            {
                Transaction trans = new();

                trans.NoPolisi = truk.NoPolisi;
                trans.NoPintu = truk.NoPintu;
                trans.BeratMasuk = Convert.ToInt32(berat);

                await repo.AddDataAsync(trans);

                return Json(Result.Success());
            }
        }

        return Json(Result.Failed());
    }
}
