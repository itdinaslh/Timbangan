using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timbangan.Models;
using Timbangan.Domain.Entities;
using Timbangan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Timbangan.Helpers;

namespace Timbangan.Controllers;

public class ClientController : Controller
{
    private readonly IClient repo;

    public ClientController(IClient repo) => this.repo = repo;

    [Authorize]
    [HttpGet("/registrasi/ekspenditur")]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    [HttpGet("/registrasi/ekspenditur/create")]
    public IActionResult Create()
    {
        return PartialView("~/Views/Client/AddEdit.cshtml", new ClientVM
        {
            Client = new Client()
        });
    }

    [Authorize]
    [HttpGet("/registrasi/ekspenditur/edit")]
    public async Task<IActionResult> Edit(int id)
    {
        Client? data = await repo.Clients
            .Include(s => s.Status)
            .FirstOrDefaultAsync(x => x.ClientID == id);

        if (data is not null)
        {
            return PartialView("~/Views/Client/AddEdit.cshtml", new ClientVM
            {
                Client = data,
                StatusName = data.Status.StatusName
            });
        }

        return NotFound();
    }

    [Authorize]
    [HttpPost("/registrasi/ekspenditur/store")]
    public async Task<IActionResult> Store(ClientVM model)
    {
        if (ModelState.IsValid)
        {
            await repo.SaveDataAsync(model.Client);

            return Json(Result.Success());
        }

        return PartialView("~/Views/Client/AddEdit.cshtml", model);
    }
}
