using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timbangan.Domain.Entities;
using Timbangan.Domain.Repositories;
using Timbangan.Helpers;

namespace Timbangan.Controllers;

public class TipeKendaraanController : Controller
{
    private readonly ITipeKendaraan repo;

    public TipeKendaraanController(ITipeKendaraan repo) => this.repo = repo;

    [HttpGet("/master/tipe-kendaraan")]
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("/master/tipe-kendaraan/create")]
    public IActionResult Create()
    {
        return PartialView("~/Views/TipeKendaraan/AddEdit.cshtml", new TipeKendaraan());
    }

    [HttpPost("/master/tipe-kendaraan/store")]
    [Authorize]
    public async Task<IActionResult> Store(TipeKendaraan tipe)
    {
        if (ModelState.IsValid)
        {
            await repo.SaveDataAsync(tipe);

            return Json(Result.Success());
        }

        return PartialView("~/Views/TipeKendaraan/AddEdit.cshtml", tipe);


    }
}
