using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timbangan.Models;
using Timbangan.Helpers;
using SharedLibrary.Repositories.Transportation;
using SharedLibrary.Entities.Transportation;

namespace Timbangan.Controllers;

public class KendaraanController : Controller
{
    private readonly IKendaraan repo;

    public KendaraanController(IKendaraan repo) { this.repo = repo; }

    [Authorize(Roles = "SysAdmin")]
    [HttpGet("/registrasi/kendaraan")]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize(Roles = "SysAdmin")]
    [HttpGet("/registrasi/kendaraan/create")]
    public IActionResult Create()
    {
        return PartialView("~/Views/Kendaraan/AddEdit.cshtml", new KendaraanVM
        {
            Kendaraan = new Kendaraan
            {
                //CreatedBy = User.Identity!.Name
            }
        });
    }

    [Authorize(Roles = "SysAdmin")]
    [HttpPost("/registrasi/kendaraan/store")]
    public async Task<IActionResult> Store(KendaraanVM model)
    { 
        if (ModelState.IsValid)
        {
            //model.Kendaraan.UpdatedBy = User.Identity!.Name;
            model.Kendaraan.UpdatedAt = DateTime.Now;
            //await repo.SaveDataAsync(model.Kendaraan);

            return Json(Result.Success());
        }

        return PartialView("~/View/Kendaraan/AddEdit.cshtml", model);
    }


}
