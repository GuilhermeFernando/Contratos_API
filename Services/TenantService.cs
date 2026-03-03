using Contratos.Interface;

namespace Contratos.Services
{
    public class TenantService : ITenantServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TenantService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetCurrentTenantId()
        {
            var tenantIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst("TenantId")?.Value;
            
            if(string.IsNullOrEmpty(tenantIdClaim) || !Guid.TryParse(tenantIdClaim, out var tenantId)) 
            {
                throw new InvalidOperationException("TenantId nao encontrado  no token.");
            }
            return tenantId;

        }

        public bool HasTenant()
        {
            var tenantIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst("TenantId")?.Value;
            return !string.IsNullOrEmpty(tenantIdClaim);
        }
    }
}
