namespace Timbangan.Models;

#nullable disable

public class ManageUserRolesVM
{
    public string UserID { get; set; }
    public IList<UserRolesVM> UserRoles { get; set; }
}

public class UserRolesVM
{
    public string RoleName { get; set; }

    public bool Selected { get; set; }
}
