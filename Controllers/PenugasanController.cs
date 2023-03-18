using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Timbangan.Domain.Entities;
using Timbangan.Domain.Repositories;
using Timbangan.Helpers;

namespace Timbangan.Controllers;

public class PenugasanController : Controller
{
    private readonly IPenugasan repo;

    public PenugasanController(IPenugasan repo) => this.repo = repo;

    [HttpGet("/master/penugasan")]
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    [HttpGet("/master/penugasan/create")]
    public IActionResult Create()
    {
        return PartialView("~/Views/Penugasan/AddEdit.cshtml", new Penugasan());
    }

    [Authorize]
    [HttpGet("/master/penugasan/edit")]
    public async Task<IActionResult> Edit(string id)
    {
        Penugasan? data = await repo.Penugasans.FirstOrDefaultAsync(x => x.PenugasanID == id);

        if (data is not null)
        {
            return PartialView("~/Views/Penugasan/AddEdit.cshtml", data);
        }

        return NotFound();
    }

    [Authorize]
    [HttpPost("/master/penugasan/store")]
    public async Task<IActionResult> Store(Penugasan penugasan)
    {
        if (ModelState.IsValid)
        {
            await repo.SaveDataAsync(penugasan);

            return Json(Result.Success());
        }

        return PartialView("~/Views/Penugasan/AddEdit.cshtml", penugasan);
    }

}
