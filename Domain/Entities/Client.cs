using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timbangan.Domain.Entities;

[Table("clients")]
public class Client
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ClientID { get; set; }

    [MaxLength(100)]
    [Required(ErrorMessage = "Nama Client Wajib Diisi")]
    public string ClientName { get; set; } = default!;

    public int? StatusID { get; set; }

    public Status Status { get; set; } = default!;

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public List<Kendaraan> Kendaraans { get; set; } = default!;
}
