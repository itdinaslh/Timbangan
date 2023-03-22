using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Timbangan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Timbangan.Controllers.api;

[Route("api/[controller]")]
[ApiController]
public class AreaKerjaApiController : ControllerBase
{
    private readonly IAreaKerja repo;

    public AreaKerjaApiController(IAreaKerja repo) => this.repo= repo;

    [HttpPost("/api/master/area-kerja")]
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

        var init = repo.AreaKerjas.Select(x => new {
            areaKerjaID = x.AreaKerjaID,
            namaArea = x.NamaArea,
            namaPenugasan = x.Penugasan.NamaPenugasan
        });

        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            init = init.OrderBy(sortColumn + " " + sortColumnDirection);
        }

        if (!string.IsNullOrEmpty(searchValue))
        {
            init = init.Where(a => a.namaArea.ToLower().Contains(searchValue.ToLower()));
        }

        recordsTotal = init.Count();

        var result = await init.Skip(skip).Take(pageSize).ToListAsync();

        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result };

        return Ok(jsonData);
    }

    [HttpGet("/api/master/area-kerja/search")]
    public async Task<IActionResult> Search(string? term, string tugas)
    {
        var data = await repo.AreaKerjas
            .Where(p => p.PenugasanID == tugas)
            .Where(k => !String.IsNullOrEmpty(term) ?
                k.NamaArea.ToLower().Contains(term.ToLower()) : true
            ).Select(s => new {
                id = s.AreaKerjaID,
                data = s.NamaArea
            }).ToListAsync();

        return Ok(data);
    }
}
