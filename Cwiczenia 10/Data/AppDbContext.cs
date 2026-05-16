using Cwiczenia_10.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenia_10.Data;

public class AppDbContext : DbContext {
    protected AppDbContext() {
    }

    public AppDbContext(DbContextOptions options) : base(options) {
    }
    
    public DbSet<PCs> PCs { get; set; }
    public DbSet<PCComponents> PCComponents { get; set; }
    public DbSet<Components> Components { get; set; }
    public DbSet<ComponentManufacturers> ComponentManufacturers { get; set; }
    public DbSet<ComponentTypes> ComponentTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<PCs>(e => {
            e.HasKey(p => p.Id);
            e.Property(p => p.Name).HasMaxLength(50);
            e.Property(p => p.Weight).HasColumnType("float(5)");
            e.Property(p => p.CreatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<ComponentTypes>(e => {
            e.HasKey(p => p.Id);
            e.Property(p => p.Abbreviation).HasMaxLength(30);
            e.Property(p => p.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<ComponentManufacturers>(e => {
            e.HasKey(p => p.Id);
            e.Property(p => p.Abbreviation).HasMaxLength(30);
            e.Property(p => p.FullName).HasMaxLength(300);
            e.Property(p => p.FoundationDate).HasColumnType("date");
        });

        modelBuilder.Entity<Components>(e => {
            e.HasKey(p => p.Code);
            e.HasOne(p => p.ComponentType).WithMany(m => m.Components).HasForeignKey(p => p.ComponentTypesId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(p => p.ComponentManufacturers).WithMany(m => m.Components).HasForeignKey(p => p.ComponentManufacturersId).OnDelete(DeleteBehavior.Cascade);
            e.Property(p => p.Code).HasMaxLength(10);
            e.Property(p => p.Name).HasMaxLength(300);
            e.Property(p =>  p.Description).HasMaxLength(int.MaxValue);
        });

        modelBuilder.Entity<PCComponents>(e => {
            e.HasKey(p => new { p.PCId, p.ComponentCode });
            e.HasOne(p => p.PCs).WithMany(m => m.PCComponents).HasForeignKey(p => p.PCId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(p => p.Components).WithMany(m => m.PCComponents).HasForeignKey(p => p.ComponentCode).OnDelete(DeleteBehavior.Cascade);
            e.Property(p => p.ComponentCode).HasMaxLength(10);
        });

        modelBuilder.Entity<PCs>().HasData(new List<PCs> {
            new PCs() { Id = 1, Name = "PC 1", Weight = 10, Warranty = 3, CreatedAt = new DateTime(2024, 1, 1, 12, 0, 0), Stock = 10},
            new PCs() { Id = 2, Name = "PC 2", Weight = 15, Warranty = 6, CreatedAt = new DateTime(2024, 2, 1, 12, 0, 0), Stock = 15},
            new PCs() { Id = 3, Name = "PC 3", Weight = 20, Warranty = 9, CreatedAt = new DateTime(2024, 3, 1, 12, 0, 0), Stock = 20},
        });
        
        modelBuilder.Entity<ComponentTypes>().HasData(new List<ComponentTypes> {
            new ComponentTypes { Id = 1, Abbreviation = "CPU", Name = "Central Processing Unit" },
            new ComponentTypes { Id = 2, Abbreviation = "GPU", Name = "Graphics Processing Unit" },
            new ComponentTypes { Id = 3, Abbreviation = "RAM", Name = "Random Access Memory" }
        });
        
        modelBuilder.Entity<ComponentManufacturers>().HasData(new List<ComponentManufacturers> {
            new ComponentManufacturers { Id = 1, Abbreviation = "INTL", FullName = "Intel Corporation", FoundationDate = new DateTime(1968, 7, 18) },
            new ComponentManufacturers { Id = 2, Abbreviation = "AMD", FullName = "Advanced Micro Devices", FoundationDate = new DateTime(1969, 5, 1) },
            new ComponentManufacturers { Id = 3, Abbreviation = "NVDA", FullName = "NVIDIA Corporation", FoundationDate = new DateTime(1993, 4, 5) }
        });
        
        modelBuilder.Entity<Components>().HasData(new List<Components> {
            new Components { Code = "COMP-001", Name = "Intel Core i7-13700K", Description = "Wydajny procesor 16-rdzeniowy", ComponentManufacturersId = 1, ComponentTypesId = 1 },
            new Components { Code = "COMP-002", Name = "Nvidia RTX 4070", Description = "Karta graficzna 12GB VRAM", ComponentManufacturersId = 3, ComponentTypesId = 2 },
            new Components { Code = "COMP-003", Name = "AMD Radeon RX 7900", Description = "Topowa karta graficzna AMD", ComponentManufacturersId = 2, ComponentTypesId = 2 }
        });
        
        modelBuilder.Entity<PCComponents>().HasData(new List<PCComponents> {
            new PCComponents { PCId = 1, ComponentCode = "COMP-001", Amount = 1 },
            new PCComponents { PCId = 2, ComponentCode = "COMP-002", Amount = 1 },
            new PCComponents { PCId = 3, ComponentCode = "COMP-003", Amount = 2 }
        });
        
        base.OnModelCreating(modelBuilder);
    }
}