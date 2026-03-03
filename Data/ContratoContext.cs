using Contratos.Interface;
using Contratos.Model;
using Microsoft.EntityFrameworkCore;

namespace Contratos.Data;

public class ContratoContext : DbContext
{
    private readonly ITenantServices? _tenantServices;

    public ContratoContext(DbContextOptions<ContratoContext> opt, ITenantServices? tenantServices = null)
        : base(opt)
    {
        _tenantServices = tenantServices;
    }

    public DbSet<Empresa> Empresa { get; set; }
    public DbSet<Endereco> Endereco { get; set; }
    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Tenant> Tenant { get; set; }
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
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Contratante>()
            .HasOne(c => c.Endereco)
            .WithOne()
            .HasForeignKey<Contratante>(c => c.EnderecoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamentos com Tenant
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

        modelBuilder.Entity<Contratante>()
            .HasOne(c => c.Tenant)
            .WithMany()
            .HasForeignKey(c => c.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        // Query Filters para Multi-Tenancy
        modelBuilder.Entity<Usuario>()
            .HasQueryFilter(u => _tenantServices == null ||
                                 !_tenantServices.HasTenant() ||
                                 u.TenantId == _tenantServices.GetCurrentTenantId());

        modelBuilder.Entity<Empresa>()
            .HasQueryFilter(e => _tenantServices == null ||
                                 !_tenantServices.HasTenant() ||
                                 e.TenantId == _tenantServices.GetCurrentTenantId());

        modelBuilder.Entity<Contrato>()
            .HasQueryFilter(c => _tenantServices == null ||
                                 !_tenantServices.HasTenant() ||
                                 c.TenantId == _tenantServices.GetCurrentTenantId());

        modelBuilder.Entity<Contratante>()
            .HasQueryFilter(ct => _tenantServices == null ||
                                  !_tenantServices.HasTenant() ||
                                  ct.TenantId == _tenantServices.GetCurrentTenantId());
        modelBuilder.Entity<RefreshToken>()
            .HasQueryFilter(rt => _tenantServices == null ||
                                  !_tenantServices.HasTenant() ||
                                  rt.TenantId == _tenantServices.GetCurrentTenantId());

        modelBuilder.Entity<FormaPagamento>()
            .HasQueryFilter(fp => _tenantServices == null ||
                                  !_tenantServices.HasTenant() ||
                                  fp.TenantId == _tenantServices.GetCurrentTenantId());

        base.OnModelCreating(modelBuilder);
    }
    
 }
