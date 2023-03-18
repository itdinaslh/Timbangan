using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timbangan.Domain.Entities;

[Table("status")]
public class Status
{
#nullable disable
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StatusID { get; set; }

    [MaxLength(50)]
    [Required(ErrorMessage = "Nama Status Wajib Diisi")]
    public string StatusName { get; set; }

    public List<Client> Clients { get; set; }

    public List<Kendaraan> Kendaraans { get; set; }
}
