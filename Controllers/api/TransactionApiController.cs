using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Repositories.Timbangan;
using System.Linq.Dynamic.Core;

namespace Timbangan.Controllers.api;

[Route("api/[controller]")]
[ApiController]
public class TransactionApiController : ControllerBase
{
    private readonly ITransaction repo;

    public TransactionApiController(ITransaction repo) => this.repo = repo;

    [HttpPost("/api/transaksi/masuk")]
    public async Task<IActionResult> TableMasuk()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        int skip = start != null ? Convert.ToInt32(start) : 0;
        int recordsTotal = 0;

        var init = repo.Transactions
            .Where(x => x.StatusID == 3);

        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            init = init.OrderBy(sortColumn + " " + sortColumnDirection);
        }

        if (!string.IsNullOrEmpty(searchValue))
        {
            init = init.Where(a => a.NoPolisi.ToLower().Contains(searchValue.ToLower()) || a.NoPintu.ToLower().Contains(searchValue.ToLower()));
        }

        recordsTotal = init.Count();

        var result = await init
            .Select(x => new {
                transactionID = x.TransactionID,
                tglMasuk = x.InDateTime.ToString("dd-MM-yyyy HH:mm:ss"),
                noPolisi = x.NoPolisi,
                noPintu = x.NoPintu,
                beratMasuk = x.BeratMasuk
            })
            .Skip(skip).Take(pageSize).ToListAsync();

        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result };

        return Ok(jsonData);
    }

    [HttpPost("/api/transaksi/keluar")]
    public async Task<IActionResult> TableKeluar()
    {
        string today = DateTime.Now.ToString("dd/MM/yyyy");

        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        int skip = start != null ? Convert.ToInt32(start) : 0;
        int recordsTotal = 0;

        var init = repo.Transactions;

        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            init = init.OrderBy(sortColumn + " " + sortColumnDirection);
        }

        if (!string.IsNullOrEmpty(searchValue))
        {
            init = init.Where(a => a.NoPolisi.ToLower().Contains(searchValue.ToLower()) || a.NoPintu.ToLower().Contains(searchValue.ToLower()));
        }

        recordsTotal = init.Count();

        var result = await init
            .Where(s => s.StatusID == 4)
            .Where(t => t.TglKeluar == DateOnly.ParseExact(today, "dd/MM/yyyy"))
            .OrderByDescending(j => j.UpdatedAt)
            .Select(x => new {
                transactionGUID = x.TransactionGUID,
                transactionID = x.TransactionID,
                beratMasuk = x.BeratMasuk,
                beratKeluar = x.BeratKeluar,
                tglMasuk = x.InDateTime.ToString("dd-MM-yyyy HH:mm:ss"),
                tglKeluar = x.OutDateTime!.Value.ToString("dd-MM-yyyy HH:mm:ss"),
                noPolisi = x.NoPolisi,
                noPintu = x.NoPintu,
                nett = x.BeratMasuk - x.BeratKeluar
            })
            .Skip(skip).Take(pageSize).ToListAsync();

        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result };

        return Ok(jsonData);
    }

    //public async Task<IActionResult> GetCurrentDumping()
    //{

    //}
}
