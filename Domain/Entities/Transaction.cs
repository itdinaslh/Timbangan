using System.ComponentModel.DataAnnotations;

namespace Timbangan.Domain.Entities;

public class Transaction
{
    [Key]
    public Guid TransactionID { get; set; } = Guid.NewGuid();

#nullable disable
    [MaxLength(20)]
    public string NoPolisi { get; set; }

    [MaxLength(30)]
    public string NoPintu { get; set; }

    public int BeratMasuk { get; set; }
}
