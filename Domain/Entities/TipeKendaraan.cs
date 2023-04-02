using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timbangan.Domain.Entities;

[Table("TipeKendaraan")]
public class TipeKendaraan
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TipeKendaraanID { get; set; }

#nullable disable

    [Required(ErrorMessage = "Kode Kendaraan Wajib Diisi")]
    [MaxLength(5)]
    public string Kode { get; set; }

    [Required(ErrorMessage = "Nama Tipe Wajib Diisi!")]
    [MaxLength(50)]
    public string NamaTipe { get; set; }

    public List<Kendaraan> Kendaraans { get; set; }

}
