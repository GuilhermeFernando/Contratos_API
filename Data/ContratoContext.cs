using Contratos.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Contratos.Data;

public class ContratoContext : DbContext
{
    public ContratoContext( DbContextOptions<ContratoContext> opt): base()
    {
    }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empresa>()
            .HasMany(c => c.Usuarios)
            .WithMany(c => c.Empresas)
            .UsingEntity(j => j.ToTable("UsuariosEmpresas"));
    }
   

}
