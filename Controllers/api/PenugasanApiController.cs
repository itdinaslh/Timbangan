﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Repositories.Common;

namespace Timbangan.Controllers.api;

[Route("api/[controller]")]
[ApiController]
public class PenugasanApiController : ControllerBase
{
    private readonly IClient repo;

    public PenugasanApiController(IClient repo) => this.repo = repo;


    [HttpPost("/api/master/penugasan")]
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

        var init = repo.Clients;

        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            init = init.OrderBy(sortColumn + " " + sortColumnDirection);
        }

        if (!string.IsNullOrEmpty(searchValue))
        {
            init = init.Where(a => a.ClientName.ToLower().Contains(searchValue.ToLower()));
        }

        recordsTotal = init.Count();

        var result = await init.Skip(skip).Take(pageSize).ToListAsync();

        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result };

        return Ok(jsonData);
    }

    [HttpGet("/api/master/penugasan/search")]
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
