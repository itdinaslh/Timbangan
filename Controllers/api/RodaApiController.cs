using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Timbangan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Timbangan.Controllers.api;

[Route("api/[controller]")]
[ApiController]
public class RodaApiController : ControllerBase
{
    private readonly IRoda repo;

    public RodaApiController(IRoda repo) => this.repo = repo;

    [HttpGet("/api/master/roda/search")]
    public async Task<IActionResult> Search(string? term)
    {
        var data = await repo.Rodas
            .Where(k => !String.IsNullOrEmpty(term) ?
                k.JumlahRoda.ToLower().Contains(term.ToLower()) : true
            ).Select(s => new {
                id = s.RodaID,
                data = s.JumlahRoda
            }).ToListAsync();

        return Ok(data);
    }
}
