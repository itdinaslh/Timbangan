using Microsoft.EntityFrameworkCore;
using Timbangan.Domain.Entities;

namespace Timbangan.Data;

public class PkmDbContext : DbContext {
#nullable disable

    public PkmDbContext(DbContextOptions<PkmDbContext> options) : base(options) { }

    public DbSet<SpjAngkut> SpjAngkuts { get; set; }
}