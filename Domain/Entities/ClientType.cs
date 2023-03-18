using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timbangan.Domain.Entities;

[Table("clienttype")]
public class ClientType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ClientTypeID { get; set; }

    [MaxLength(30)]
    public string TypeName { get; set; } = default!;

}
