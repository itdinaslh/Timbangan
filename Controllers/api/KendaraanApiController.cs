using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Timbangan.Domain.Repositories;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace Timbangan.Controllers.api;

[Route("api/[controller]")]
[ApiController]
public class KendaraanApiController : ControllerBase
{
    private readonly IKendaraan repo;

    public KendaraanApiController(IKendaraan repo)
    {
        this.repo = repo;
    }

    [Authorize]
    [HttpPost("/api/registrasi/kendaraan")]
    public async Task<IActionResult> DataTable()
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

        var init = repo.Kendaraans;

        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            init = init.OrderBy(sortColumn + " " + sortColumnDirection);
        }

        if (!string.IsNullOrEmpty(searchValue))
        {
            init = init.Where(a => a.NoPolisi.ToLower().Contains(searchValue.ToLower()) || a.NoPintu.ToLower().Contains(searchValue.ToLower()));
        }

        recordsTotal = init.Count();

        var result = await init.Select(x => new {
            kendaraanID = x.KendaraanID,
            noPolisi = x.NoPolisi,
            noPintu = x.NoPintu,
            clientName = x.Client.ClientName,
            createdAt = x.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
            beratKIR = x.BeratKIR != null ? Convert.ToInt32(x.BeratKIR).ToString("#,###") : "",
            statusName = x.Status.StatusName
        }).Skip(skip).Take(pageSize).ToListAsync();

        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result };

        return Ok(jsonData);
    }
}
