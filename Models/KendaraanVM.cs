

using SharedLibrary.Entities.Transportation;

namespace Timbangan.Models;

public class KendaraanVM
{
#nullable disable

    public Kendaraan Kendaraan { get; set; }

    public string PenugasanID { get; set; }

#nullable enable
    public string? NamaExpenditur { get; set; }    

    public string? NamaPenugasan { get; set; }

    public string? AreaKerja { get; set; }

    public string? Tipe { get; set; }

    public string? JumlahRoda { get; set; }    
}
