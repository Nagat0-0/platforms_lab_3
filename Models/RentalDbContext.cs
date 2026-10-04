using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace lab_3.Models;

public partial class RentalDbContext : DbContext
{
    public RentalDbContext()
    {
    }

    public RentalDbContext(DbContextOptions<RentalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Rental> Rentals { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clients__3214EC0770D6A284");

            entity.HasIndex(e => e.PassportNumber, "UQ__Clients__45809E71F774AAF5").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(150);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(50);
            entity.Property(e => e.PassportNumber).HasMaxLength(20);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC077A2BDC20");

            entity.Property(e => e.Cost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DailyRentalPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rentals__3214EC07413D8B7B");

            entity.HasOne(d => d.Client).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rentals__ClientI__4F7CD00D");

            entity.HasOne(d => d.Product).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rentals__Product__5070F446");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
