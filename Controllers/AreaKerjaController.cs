using Microsoft.AspNetCore.Mvc;
using Timbangan.Domain.Repositories;
using Timbangan.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Timbangan.Models;
using Timbangan.Helpers;

namespace Timbangan.Controllers;

[Authorize]
public class AreaKerjaController : Controller
{
    private readonly IAreaKerja repo;

    public AreaKerjaController(IAreaKerja repo)
    {
        this.repo = repo;
    }

    [HttpGet("/master/area-kerja")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("/master/area-kerja/create")]
    public IActionResult Create()
    {
        return PartialView("~/Views/AreaKerja/AddEdit.cshtml", new AreaKerjaVM
        {
            AreaKerja = new AreaKerja()
        });
    }

    [HttpPost("/master/area-kerja/edit")]
    public async Task<IActionResult> Edit(int id)
    {
        AreaKerja? data = await repo.AreaKerjas.FirstOrDefaultAsync(x => x.AreaKerjaID == id);

        if (data is not null)
        {
            return PartialView("~/Views/AreaKerja/AddEdit.cshtml", data);
        }

        return NotFound();
    }

    [HttpPost("~/master/area-kerja/store")]
    public async Task<IActionResult> Store(AreaKerjaVM model)
    {
        if (ModelState.IsValid)
        {
            await repo.SaveDataAsync(model.AreaKerja!);

            return Json(Result.Success());
        }

        return PartialView("~/Views/AreaKerja/AddEdit.cshtml", model);
    }
}
