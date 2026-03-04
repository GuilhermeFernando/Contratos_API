using Contratos.Interface;
using Contratos.Model;
using Microsoft.EntityFrameworkCore;

namespace Contratos.Data;

public class ContratoContext : DbContext
{
    // Tenant logic removed
    public ContratoContext(DbContextOptions<ContratoContext> opt)
        : base(opt)
    {
    }

    public DbSet<Empresa> Empresa { get; set; }
    public DbSet<Endereco> Endereco { get; set; }
    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Contratante> Contratante { get; set; }
    public DbSet<Contrato> Contrato { get; set; }
    public DbSet<FormaPagamento> FormaPagamento { get; set; }
    public DbSet<RefreshToken> RefreshToken { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relacionamentos
        modelBuilder.Entity<Empresa>()
            .HasOne(e => e.Endereco)
            .WithOne()
            .HasForeignKey<Empresa>(e => e.EnderecoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Contrato>()
            .HasMany(c => c.FormasPagamento)
            .WithOne(fp => fp.Contrato)
            .HasForeignKey(fp => fp.ContratoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Contrato>()
            .HasOne(c => c.Empresa)
            .WithMany()
            .HasForeignKey(c => c.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Contrato>()
            .HasOne(c => c.Contratante)
            .WithMany()
            .HasForeignKey(c => c.ContratanteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.RefreshTokens)
            .WithOne(rt => rt.Usuario)
            .HasForeignKey(rt => rt.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Contratante>()
            .HasOne(c => c.Endereco)
            .WithOne()
            .HasForeignKey<Contratante>(c => c.EnderecoId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
    
 }
