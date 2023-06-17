using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Entities.Timbangan;
using SharedLibrary.Repositories.Timbangan;
using Timbangan.Models;

namespace Timbangan.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ITransaction repo;

    public HomeController(ITransaction trans)
    {
        this.repo = trans;
    }

    public async Task<IActionResult> Index()
    {
        if (User.IsInRole("OpMasuk"))
        {
            Transaction? trx = await repo.Transactions                
                .OrderByDescending(x => x.TransactionID)
                .FirstOrDefaultAsync();

            IndexMasukVM model = new();

            if (trx != null)
            {
                model.TruckID = trx.NoPolisi;
                model.NoLambung = trx.NoPintu;
                model.BeratMasuk = trx.BeratMasuk;
            }

            return View("~/Views/Transaction/Masuk/Masuk.cshtml", model);

        } else if (User.IsInRole("OpKeluar"))
        {
            Transaction? trx = await repo.Transactions
                .Where(x => x.StatusID == 4)
                .OrderByDescending(x => x.TransactionID)
                .FirstOrDefaultAsync();

            IndexKeluarVM model = new();

            if (trx != null)
            {
                model.TruckID = trx.NoPolisi;
                model.NoLambung = trx.NoPintu;
                model.BeratKeluar = trx.BeratKeluar;
            }

            return View("~/Views/Transaction/Keluar/Index.cshtml", model);
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}