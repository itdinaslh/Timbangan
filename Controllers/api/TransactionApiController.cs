using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Timbangan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
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
            .Where(s => s.StatusID == 6)            
            .Select(x => new {
                transactionGUID = x.TransactionGUID,
                tglMasuk = x.InDateTime,                
                noPolisi = x.NoPolisi,
                noPintu = x.NoPintu,
                beratMasuk = x.BeratMasuk
        });

        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            init = init.OrderBy(sortColumn + " " + sortColumnDirection);
        }

        if (!string.IsNullOrEmpty(searchValue))
        {
            init = init.Where(a => a.noPolisi.ToLower().Contains(searchValue.ToLower()) || a.noPintu.ToLower().Contains(searchValue.ToLower()));
        }

        recordsTotal = init.Count();

        var result = await init.Skip(skip).Take(pageSize).ToListAsync();

        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result };

        return Ok(jsonData);
    }

    [HttpPost("/api/transaksi/keluar")]
    public async Task<IActionResult> TableKeluar()
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
            .Where(s => s.StatusID == 5)
            .Where(t => t.TglMasuk == DateOnly.FromDateTime(DateTime.Now))
            .Select(x => new {
                transactionGUID = x.TransactionGUID,
                strukID = x.TransactionID,
                tglMasuk = x.InDateTime,
                noPolisi = x.NoPolisi,
                noPintu = x.NoPintu,
                beratMasuk = x.BeratMasuk
            });

        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            init = init.OrderBy(sortColumn + " " + sortColumnDirection);
        }

        if (!string.IsNullOrEmpty(searchValue))
        {
            init = init.Where(a => a.noPolisi.ToLower().Contains(searchValue.ToLower()) || a.noPintu.ToLower().Contains(searchValue.ToLower()));
        }

        recordsTotal = init.Count();

        var result = await init.Skip(skip).Take(pageSize).ToListAsync();

        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result };

        return Ok(jsonData);
    }
}
