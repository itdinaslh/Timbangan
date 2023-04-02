using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timbangan.Domain.Entities;

[Table("AreaKerja")]
public class AreaKerja
{

#nullable disable
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AreaKerjaID { get; set; }

    [MaxLength(50)]
    [Required(ErrorMessage = "Nama Area Kerja Wajib Diisi")]
    public string NamaArea { get; set; }

    [MaxLength(10)]
    public string PenugasanID { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public Penugasan Penugasan { get; set; }

    public List<Kendaraan> Kendaraans { get; set; }


}
