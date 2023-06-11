using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timbangan.Domain.Entities;

[Table("Kendaraan")]
public class Kendaraan
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int KendaraanID { get; set; }

    public Guid? UniqueID { get; set; } = Guid.NewGuid();

#nullable disable
    [MaxLength(20)]
    [Required(ErrorMessage = "No Polisi Kendaraan Wajib Diisi")]
    public string NoPolisi { get; set; }

    [MaxLength(30)]
    [Required(ErrorMessage = "No Pintu Wajib Diisi")]
    public string NoPintu { get; set; }

#nullable enable

    public string? RFID { get; set; }

#nullable disable

    [Required(ErrorMessage = "Nama Ekspenditur Wajib Diisi")]
    public int ClientID { get; set; }

    [Required(ErrorMessage = "Area Kerja Wajib Diisi")]
    public int AreaKerjaID { get; set; }

    [Required(ErrorMessage = "Tipe Kendaraan Wajib Diisi")]
    public int TipeKendaraanID { get; set; }

    [Required(ErrorMessage = "Jumlah Roda Wajib Diisi")]
    public int RodaID { get; set; }


#nullable enable

    public int? StatusID { get; set; }

    public int? AvgMasuk { get; set; } = 0;

    public int? AvgKeluar { get; set; } = 0;

    public int? BeratKIR { get; set; } = 0;

    public bool? IsPasar { get; set; } = false;

    public Guid? ExternalID { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    [MaxLength(100)]
    public string? CreatedBy { get; set; }

    [MaxLength(100)]
    public string? UpdatedBy { get; set; }

#nullable disable

    public Client Client { get; set; }

    public AreaKerja AreaKerja { get; set; }

    public Roda Roda { get; set; }

    public Status Status { get; set; }

    public TipeKendaraan TipeKendaraan { get; set; }

    public List<Transaction> Transactions { get; set; }
}
