using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timbangan.Domain.Entities;

[Table("kendaraan")]
public class Kendaraan
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int KendaraanID { get; set; }

    public Guid? UniqueID { get; set; } = Guid.NewGuid();

    [MaxLength(20)]
    [Required(ErrorMessage = "No Polisi Kendaraan Wajib Diisi")]
    public string NoPolisi { get; set; } = default!;

    [MaxLength(30)]
    [Required(ErrorMessage = "No Pintu Wajib Diisi")]
    public string NoPintu { get; set; } = default!;

    public string? RFID { get; set; }

    [Required(ErrorMessage = "Nama Ekspenditur Wajib Diisi")]
    public int ClientID { get; set; }

    [Required(ErrorMessage = "Area Kerja Wajib Diisi")]
    public int AreaKerjaID { get; set; }

    [Required(ErrorMessage = "Tipe Kendaraan Wajib Diisi")]
    public int TipeKendaraanID { get; set; }

    [Required(ErrorMessage = "Jumlah Roda Wajib Diisi")]
    public int RodaID { get; set; }

    public int? StatusID { get; set; }

    public int? AvgMasuk { get; set; } = 0;

    public int? AvgKeluar { get; set; } = 0;

    public int? BeratKIR { get; set; } = 0;

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public Client Client { get; set; } = default!;

    public AreaKerja AreaKerja { get; set; } = default!;

    public Roda Roda { get; set; } = default!;

    public Status Status { get; set; } = default!;

    public TipeKendaraan TipeKendaraan { get; set; } = default!;


}
