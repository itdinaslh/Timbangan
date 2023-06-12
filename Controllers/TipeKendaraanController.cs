using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Entities.Transportation;
using SharedLibrary.Repositories.Transportation;
using Timbangan.Helpers;

namespace Timbangan.Controllers;

public class TipeKendaraanController : Controller
{
    private readonly IKendaraan repo;

    public TipeKendaraanController(IKendaraan repo) => this.repo = repo;

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

    [HttpGet("/master/tipe-kendaraan/edit")]
    [Authorize(Roles = "SysAdmin")]
    public async Task<IActionResult> Edit(int tipe)
    {
        var data = await repo.TipeKendaraans.FirstOrDefaultAsync(x => x.TipeKendaraanID == tipe);

        if (data is not null)
        {
            return PartialView("~/Views/TipeKendaraan/AddEdit.cshtml", data);
        }

        return NotFound();
    }

    [HttpPost("/master/tipe-kendaraan/store")]
    [Authorize]
    public async Task<IActionResult> Store(TipeKendaraan tipe)
    {
        if (ModelState.IsValid)
        {
            await repo.SaveTipeAsync(tipe);

            return Json(Result.Success());
        }

        return PartialView("~/Views/TipeKendaraan/AddEdit.cshtml", tipe);


    }
}
