namespace Timbangan.Models;

public class RoleVM
{
#nullable disable

    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Nama role wajib diisi")]
    public string RoleName { get; set; }

    public bool IsEdit { get; set; }

#nullable enable
    public string? RoleID { get; set; }
}
