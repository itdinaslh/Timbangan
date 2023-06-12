using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Entities.Common;
using SharedLibrary.Repositories.Common;
using Timbangan.Helpers;

namespace Timbangan.Controllers;

public class PenugasanController : Controller
{
    private readonly IClient repo;

    public PenugasanController(IClient repo) => this.repo = repo;

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
        return PartialView("~/Views/Penugasan/AddEdit.cshtml", new Client());
    }

    [Authorize]
    [HttpGet("/master/penugasan/edit")]
    public async Task<IActionResult> Edit(Guid id)
    {
        Client? data = await repo.Clients.FirstOrDefaultAsync(x => x.ClientID == id);

        if (data is not null)
        {
            return PartialView("~/Views/Penugasan/AddEdit.cshtml", data);
        }

        return NotFound();
    }

    [Authorize]
    [HttpPost("/master/penugasan/store")]
    public async Task<IActionResult> Store(Client penugasan)
    {
        if (ModelState.IsValid)
        {
            await repo.SaveClientAsync(penugasan);

            return Json(Result.Success());
        }

        return PartialView("~/Views/Penugasan/AddEdit.cshtml", penugasan);
    }

}
