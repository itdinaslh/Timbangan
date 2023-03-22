using System.ComponentModel.DataAnnotations;

namespace Timbangan.Models;

public class UserUpdateVM
{
    public string? UserID { get; set; }

#nullable disable

    [Required(ErrorMessage = "Username wajib diisi")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Nama lengkap wajib diisi")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Email wajib diisi")]
    [EmailAddress(ErrorMessage = "Format email salah")]
    public string Email { get; set; }

}
