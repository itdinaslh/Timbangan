using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Timbangan.Models;
using Timbangan.Helpers;

namespace Timbangan.Controllers;

[Authorize]
public class RoleController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    [HttpGet("/registrasi/roles")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("/registrasi/roles/create")]
    public IActionResult Create()
    {
        return PartialView("~/Views/Role/AddEdit.cshtml", new RoleVM
        {
            IsEdit = false
        });
    }

    [HttpGet("/registrasi/roles/edit")]
    public async Task<IActionResult> Edit(string id)
    {
        IdentityRole? data = await _roleManager.FindByIdAsync(id);

        if (data is not null)
        {
            return PartialView("~/Views/Role/AddEdit.cshtml", new RoleVM
            {
                RoleID = data.Id,
                RoleName = data.Name,
                IsEdit = true
            });
        }

        return NotFound();
    }


    [HttpPost("/registrasi/roles/store")]
    public async Task<IActionResult> Store(RoleVM model)
    {
        if (ModelState.IsValid)
        {
            if (model.RoleID is null)
            {
                await _roleManager.CreateAsync(new IdentityRole(model.RoleName!.Trim()));
            } else
            {
                IdentityRole data = await _roleManager.FindByIdAsync(model.RoleID!);

                data.Name = model.RoleName.Trim();

                await _roleManager.UpdateAsync(data);
            }

            return Json(Result.Success());
        }

        return PartialView("~/Views/Role/AddEdit.cshtml", model);
    }
}
