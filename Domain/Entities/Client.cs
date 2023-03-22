using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timbangan.Domain.Entities;

[Table("clients")]
public class Client
{
#nullable disable
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ClientID { get; set; }

    [MaxLength(100)]
    [Required(ErrorMessage = "Nama Client Wajib Diisi")]
    public string ClientName { get; set; }

    public int? StatusID { get; set; }

    public Status Status { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public List<Kendaraan> Kendaraans { get; set; }

#nullable enable

    public Guid? PkmID { get; set; }
}
