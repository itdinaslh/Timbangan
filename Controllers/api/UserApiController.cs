using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Timbangan.Data;
using System.Linq.Dynamic.Core;

namespace Timbangan.Controllers.api;

[Route("api/[controller]")]
[ApiController]
public class UserApiController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserApiController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("/api/registrasi/users")]
    public async Task<IActionResult> UserTable(CancellationToken cancellationToken)
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        int skip = start != null ? Convert.ToInt32(start) : 0;
        int recordsTotal = 0;

        //checks if cache entries exists        

        var data = _userManager.Users
            .Select(x => new {
                id = x.Id,
                userName = x.UserName,
                fullName = x.FullName,
                email = x.Email
            });


        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            data = data.OrderBy(sortColumn + " " + sortColumnDirection);
        }

        if (!string.IsNullOrEmpty(searchValue))
        {
            data = data
                .Where(a => a.userName.ToLower()
                .Contains(searchValue.ToLower()) ||
                    a.email.ToLower().Contains(searchValue.ToLower()) ||
                    a.fullName!.ToLower().Contains(searchValue.ToLower())
            );
        }

        recordsTotal = data.Count();

        var result = await data.Skip(skip).Take(pageSize).ToListAsync();

        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result };

        return Ok(jsonData);
    }
}
