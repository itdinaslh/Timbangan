using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Timbangan.Models;

public class UserVM
{
    public string? UserID { get; set; }

#nullable disable

    [Required(ErrorMessage = "Username wajib diisi")]
    public string UserName { get; set; }

    [Required(ErrorMessage ="Nama lengkap wajib diisi")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Email wajib diisi")]
    [EmailAddress(ErrorMessage = "Format email salah")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password wajib diisi")]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
    
}
