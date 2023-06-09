﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Timbangan.Domain.Entities;

namespace Timbangan.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
#nullable disable
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ClientType> ClientTypes { get; set; }

    public DbSet<TipeKendaraan> TipeKendaraans { get; set; }

    public DbSet<Status> Statuses { get; set; }

    public DbSet<Roda> Rodas { get; set; }

    public DbSet<Penugasan> Penugasans { get; set; }

    public DbSet<AreaKerja> AreaKerjas { get;set; }

    public DbSet<Client> Clients { get; set; }

    public DbSet<Kendaraan> Kendaraans { get; set; }

    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Move Identity to "myschema" Schema:
        modelBuilder.Entity<ApplicationUser>().ToTable("Users");
        modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");

    }
}
