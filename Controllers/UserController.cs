using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Timbangan.Data;
using Timbangan.Helpers;
using Timbangan.Models;

namespace Timbangan.Controllers;

public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [Authorize]
    [HttpGet("/registrasi/users")]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    [HttpGet("/registrasi/users/create")]
    public IActionResult Create()
    {
        return PartialView("~/Views/User/Add.cshtml", new UserVM());
    }

    [Authorize]
    [HttpGet("/registrasi/users/edit")]
    public async Task<IActionResult> Edit(string id)
    {
        ApplicationUser? data = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (data is not null)
        {
            return PartialView("~/Views/User/Edit.cshtml", new UserUpdateVM
            {
                UserID = data.Id,
                UserName = data.UserName,
                Email = data.Email,
                FullName = data.FullName
            });
        }

        return NotFound();
    }

    [Authorize]
    [HttpPost("/registrasi/users/store")]
    public async Task<IActionResult> Store(UserVM model)
    {
        if (ModelState.IsValid)
        {
            var user = CreateUser();

            user.FullName = model.FullName;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Json(Result.Success());
            }
        }

        return PartialView("~/Views/User/Add.cshtml", model);
    }

    [Authorize]
    [HttpPost("~/registrasi/users/update")]
    public async Task<IActionResult> Update(UserUpdateVM model)
    {
        if (ModelState.IsValid)
        {
            ApplicationUser? data = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == model.UserID);

            if (data is not null)
            {
                data.UserName = model.UserName;
                data.Email = model.Email;
                data.FullName = model.FullName;

                await _userManager.UpdateAsync(data);

                return Json(Result.Success());
            }
        }

        return PartialView("~/Views/User/Edit.cshtml", model);
    }

    [Authorize]
    [HttpGet("~/registrasi/manage/roles")]
    public async Task<IActionResult> ManageRoles(string id)
    {
        var viewModel = new List<UserRolesVM>();
        var user = await _userManager.FindByIdAsync(id);

        if (user is not null)
        {
            foreach (var role in await _roleManager.Roles.ToListAsync())
            {
                var userRolesViewModel = new UserRolesVM
                {
                    RoleName = role.Name
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }

                viewModel.Add(userRolesViewModel);
            }

            var model = new ManageUserRolesVM()
            {
                UserID = id,
                UserRoles = viewModel
            };

            return View(model);
        }

        return NotFound();
    }

    public async Task<IActionResult> UpdateRoles(string id, ManageUserRolesVM model)
    {
        var user = await _userManager.FindByIdAsync(id);
        var roles = await _userManager.GetRolesAsync(user);
        var result = await _userManager.RemoveFromRolesAsync(user, roles);

        result = await _userManager.AddToRolesAsync(user, model.UserRoles.Where(x => x.Selected).Select(y => y.RoleName));

        return RedirectToAction("Index");
    }

    private ApplicationUser CreateUser()
    {
        try
        {
            return Activator.CreateInstance<ApplicationUser>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
        }
    }

    [HttpGet("/user/password/change")]
    public IActionResult ChangePassword(string user) {
        return PartialView(new ChangeModel {
            UserID = user
        });
    }

    [HttpPost("/user/password/change")]
    public async Task<IActionResult> ChangePassword(ChangeModel model) {
        if (ModelState.IsValid) {
            var user = await _userManager.FindByIdAsync(model.UserID);

            if (user is not null) {               

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, token, model.Password);

                if (result.Succeeded) {
                    return Json(Result.Success());
                } else {
                    return Json(Result.Failed());
                }                
            }
        }        

        return PartialView(model);
           
    }
}
