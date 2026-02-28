using Contratos.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Contratos.Data;

public class ContratoContext : DbContext
{
    public ContratoContext( DbContextOptions<ContratoContext> opt): base(opt)
    {
    }
    public DbSet<Empresa> Empresa { get; set; }
    public DbSet<Endereco> Endereco { get; set; }
    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Tenant> Tenant { get; set; }
    public DbSet<Contratante> Contratante { get; set; }
    public DbSet<Contrato> Contrato { get; set; }
    public DbSet<FormaPagamento> FormaPagamento { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        modelBuilder.Entity<Empresa>()
            .HasOne(e => e.Tenant)
            .WithMany()
            .HasForeignKey(e => e.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Contrato>()
            .HasOne(c => c.Tenant)
            .WithMany()
            .HasForeignKey(c => c.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Contrato>()
            .HasOne<Contratante>()
            .WithOne()
            .HasForeignKey<Contrato>(c => c.ContratanteId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
   

}
