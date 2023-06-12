using Microsoft.AspNetCore.Mvc;
using Timbangan.Helpers;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Repositories.Common;
using SharedLibrary.Entities.Common;
using Microsoft.AspNetCore.Authorization;

namespace Timbangan.Controllers;

[Authorize(Roles = "SysAdmin")]
public class StatusController : Controller
{
    private readonly IStatus repo;

    public StatusController(IStatus repo) => this.repo = repo;

    [HttpGet("/master/status")]
    public IActionResult Index()
    {
        return View();
    }


    [HttpGet("/master/status/create")]
    public IActionResult Create()
    {
        return PartialView("~/Views/Status/AddEdit.cshtml", new Status());
    }

    [HttpGet("/master/status/edit")]
    public async Task<IActionResult> Edit(int id)
    {
        Status? data = await repo.Statuses.FirstOrDefaultAsync(x => x.StatusID == id);

        if (data is not null)
        {
            return PartialView("~/Views/Status/AddEdit.cshtml", data);
        }

        return NotFound();
    }

    [HttpPost("/master/status/store")]
    public async Task<IActionResult> Store(Status model)
    {
        if (ModelState.IsValid)
        {
            await repo.SaveDataAsync(model);

            return Json(Result.Success());
        }

        return PartialView("~/Views/Status/AddEdit.cshtml", model);
    }
}
