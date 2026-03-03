namespace Contratos.Interface
{
    public interface ITenantServices
    {
        Guid GetCurrentTenantId();
        bool HasTenant();
    }
}
