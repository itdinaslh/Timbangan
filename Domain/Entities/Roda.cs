using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timbangan.Domain.Entities;

[Table("Roda")]
public class Roda
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RodaID { get; set; }

    [Required(ErrorMessage = "Jumlah Roda Wajib Diisi")]
    [MaxLength(5)]
    public string JumlahRoda { get; set; } = default!;

    public List<Kendaraan> Kendaraans { get; set; } = default!;
}
