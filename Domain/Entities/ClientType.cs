using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timbangan.Domain.Entities;

[Table("ClientType")]
public class ClientType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ClientTypeID { get; set; }

    [MaxLength(30)]
    public string TypeName { get; set; } = default!;

#nullable disable

    public List<Client> Clients { get; set; }

}
