using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timbangan.Domain.Entities;

[Table("penugasan")]
public class Penugasan
{
#nullable disable

    [Key]
    [Required(ErrorMessage = "Kode Penugasan Wajib Diisi")]
    [MaxLength(10)]
    public string PenugasanID { get; set; }

    [Required(ErrorMessage = "Nama Penugasan Wajib Diisi")]
    [MaxLength(75)]
    public string NamaPenugasan { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public List<AreaKerja> AreaKerjas { get; set; }
}
