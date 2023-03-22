using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Timbangan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;

namespace Timbangan.Controllers.api;

[Route("api/[controller]")]
[ApiController]
public class ClientApiController : ControllerBase
{
    private readonly IClient repo;

    public ClientApiController(IClient repo)
    {
        this.repo = repo;
    }

    [Authorize]
    [HttpPost("/api/registrasi/clients")]
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

        var init = repo.Clients.Select(x => new {
            clientID = x.ClientID,
            clientName = x.ClientName,
            pkmID = x.PkmID,
            statusName = x.Status.StatusName
        });

        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            init = init.OrderBy(sortColumn + " " + sortColumnDirection);
        }

        if (!string.IsNullOrEmpty(searchValue))
        {
            init = init.Where(a => a.clientName.ToLower().Contains(searchValue.ToLower()));
        }

        recordsTotal = init.Count();

        var result = await init.Skip(skip).Take(pageSize).ToListAsync();

        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result };

        return Ok(jsonData);
    }

    [HttpGet("/api/registrasi/clients/search")]
    public async Task<IActionResult> Search(string? term)
    {
        var data = await repo.Clients
            .Where(k => !String.IsNullOrEmpty(term) ?
                k.ClientName.ToLower().Contains(term.ToLower()) : true
            ).Select(s => new {
                id = s.ClientID,
                data = s.ClientName
            }).ToListAsync();

        return Ok(data);
    }
}
