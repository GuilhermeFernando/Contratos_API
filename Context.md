# Contexto do Projeto — Contratos

Este documento serve como base única (source of truth) para qualquer mudança no projeto.

Resumo geral
- Objetivo: API REST para gerenciamento de contratos entre empresas e contratantes, com suporte a multi-tenant.
- Tecnologia: .NET 8, C# 12, Entity Framework Core, AutoMapper.
- Estrutura visível: DbContext (`Data/ContratoContext.cs`), modelos em `Model/`, controller de Usuário (`Controllers/UsuarioController.cs`). DTOs e perfis AutoMapper para Usuário existem.
        
Arquitetura e responsabilidades
- API Web (endpoints CRUD) expõe operações sobre entidades do domínio.
- `ContratoContext`: configura relacionamentos via Fluent API e define `DbSet<>` para todas entidades principais.
- AutoMapper: usado para mapear DTOs ⇄ entidades (ex.: `Usuario`).

Entidades principais e relações (resumo)
- Tenant
  - TenantId, Nome, Email, DataCriacao, Ddd, Telefone, UrlLogo
  - Relacionamento: 1:N com Empresa e Contrato (TenantId em Empresa/Contrato)

- Empresa
  - EmpresaId, EnderecoId, UsuarioId, TenantId, RazaoSocial, CNPJ, NomeFantasia, IE, IM, NaturezaJuridica, DataAbertura
  - Relacionamentos:
    - 1:1 Endereco (EnderecoId é FK em Empresa)
    - N:1 Tenant
    - N:1 Usuario (via UsuarioId)

- Endereco
  - Entidade de endereço (relacionada 1:1 com Empresa e com Contratante)

- Usuario
  - CRUD implementado (`Controllers/UsuarioController.cs`)
  - Possui TenantId

- Contratante
  - ContratanteId, EmpresaId, EnderecoId, RazaoSocial, NomeFantasia, Documento
  - Relacionamentos:
    - 1:1 Endereco
    - N:1 Empresa

- Contrato
  - ContratoId, EmpresaId, TenantId, ContratanteId, Titulo, Objeto, DataInicio, DataFim, Valor
  - Relacionamentos:
    - N:1 Empresa (EmpresaId) — DeleteBehavior.Restrict
    - N:1 Tenant (TenantId) — DeleteBehavior.Restrict
    - 1:1 Contratante (ContratanteId) — DeleteBehavior.Restrict
    - 1:N FormasPagamento — Cascade (remoção de contrato remove pagamentos)

- FormaPagamento
  - FormaPagamentoId, Descricao, NumeroParcela, Ativo, DataCriacao, DataAlteracao, ContratoId
  - Relacionamento: N:1 Contrato

Observações do DbContext (`Data/ContratoContext.cs`)
- Mapeamentos principais:
  - Empresa ⇄ Endereco: `HasOne().WithOne().HasForeignKey<Empresa>(EnderecoId)`, `DeleteBehavior.Restrict`
  - Empresa ⇄ Tenant: `HasOne().WithMany().HasForeignKey(TenantId)`, `DeleteBehavior.Restrict`
  - Contrato ⇄ Empresa / Tenant: `HasOne().WithMany().HasForeignKey(...)`, `DeleteBehavior.Restrict`
  - Contrato ⇄ Contratante: `HasOne<Contratante>().WithOne().HasForeignKey<Contrato>(ContratanteId)`, `DeleteBehavior.Restrict`
  - Contrato ⇄ FormasPagamento: `HasMany(c => c.FormasPagamento).WithOne(fp => fp.Contrato).HasForeignKey(fp => fp.ContratoId).OnDelete(Cascade)`

Problemas e inconsistências identificadas
- Modelo `Contrato` tinha `FormasPagamento` como tipo único; DbContext mapeia `HasMany` → incompatibilidade. Recomenda-se usar `ICollection<FormaPagamento>`.
- Classe `FormaPagamento` apresentava duplicidade de propriedade `Ativo` e sintaxe inválida em versões anteriores; precisa ser corrigida.
- Anotações de nullability e `[Required]` inconsistentes (ex.: propriedades marcadas `[Required]` mas declaradas como nullable). Recomenda-se revisar com nullable reference types ativado.
- Poucas APIs implementadas: apenas `Usuario` tem controller/DTOs; demais entidades ainda sem endpoints públicos.
- Recomendações de modelagem e validação ausentes (tamanhos de campos, índices, regras de negócio e preenchimento automático de `DataCriacao`/`DataAlteracao`).

Correções/ações já sugeridas
- Converter `Contrato.FormasPagamento` para `ICollection<FormaPagamento>`.
- Corrigir `FormaPagamento` removendo duplicatas / sintaxe inválida; adicionar navegação opcional `Contrato?`.
- Rever atributos `[Required]` e nullability do conjunto de modelos.
- Implementar preenchimento automático de `DataCriacao`/`DataAlteracao` (antes de `SaveChanges` ou via interceptores).
- Criar e aplicar migrations.
- Adicionar testes de integração para endpoints críticos.
- Planejar autenticação/autorização (JWT, policies) e versionamento de API.
- Revisar políticas de deleção e integridade referencial conforme regras de negócio.

---

Autenticação/Autorização (JWT) — alterações realizadas até o momento
- Objetivo: adicionar autenticação baseada em JWT, hashing de senha e endpoint de login.
- Arquivos/itens adicionados ou parcialmente implementados no workspace:
  - `appsettings.json` (seção `Jwt`) — contém `Key`, `Issuer`, `Audience`, `ExpiresInMinutes`/`ExpirationMinutes` (ver inconsistência abaixo).
  - `Program.cs` — registro de serviços:
    - AutoMapper, DbContext, Identity `PasswordHasher<Usuario>`.
    - Registro de `IJwtService` como `JwtService` (observação: nome de classe precisa existir).
    - Configuração de `AddAuthentication().AddJwtBearer(...)` com `TokenValidationParameters`.
    - Observação: `app.UseAuthentication()` deve ser chamado antes de `app.UseAuthorization()` (ver correção).
  - DTOs:
    - `Data/Dto/UsuarioDto/LoginUsuarioDto.cs` (email + senha).
    - `Data/Dto/UsuarioDto/TokenDto/TokenDto.cs` (Token, Expiration) — já criado.
  - Serviço JWT:
    - `Services/JwtServices.cs` — implementação de geração de token encontrada no workspace. Contém a lógica de claims, criação do `JwtSecurityToken` e retorno do token + expiration.
  - `Controllers/UsuarioController.cs` (já presente) — propostas para:
    - Hash de senha no cadastro (`PasswordHasher<Usuario>`).
    - Endpoint `POST /api/usuario/login` que verifica senha e retorna `TokenDto`.

Erros detectados no código atual (compilação / inconsistências) e como corrigir
1. Nome da classe/registro do serviço JWT inconsistente
   - Problema: `Program.cs` registra `IJwtService, JwtService` mas o arquivo em `Services` define `public class JwtServices : IJwtService` (nome plural `"JwtServices"` e possivelmente implementação/interface ausente).
   - Correção: padronizar o nome. Recomenda-se:
     - Renomear o arquivo `Services/JwtServices.cs` → `Services/JwtService.cs`
     - Alterar `public class JwtServices` → `public class JwtService : IJwtService`.
     - Garantir que exista a interface `IJwtService` (ex.: `public interface IJwtService { (string Token, DateTime Expiration) GenerateToken(Usuario usuario); }`).
   - Resultado: `builder.Services.AddScoped<IJwtService, JwtService>();` compilará.

2. Usings faltando em `Program.cs` (tipos não resolvidos)
   - Problema: `JwtBearerDefaults`, `TokenValidationParameters`, `SymmetricSecurityKey`, `Encoding` não estão reconhecidos sem os `using`.
   - Correção: adicionar no topo de `Program.cs`:
     - `using Microsoft.AspNetCore.Authentication.JwtBearer;`
     - `using Microsoft.IdentityModel.Tokens;`
     - `using System.Text;`
   - Exemplo de cabeçalho atualizado:
     - `using Contratos.Data;`
     - `using Microsoft.EntityFrameworkCore;`
     - `using System;`
     - `using AutoMapper;`
     - `using Microsoft.AspNetCore.Identity;`
     - `using Contratos.Model;`
     - `using Microsoft.AspNetCore.Authentication.JwtBearer;`
     - `using Microsoft.IdentityModel.Tokens;`
     - `using System.Text;`

3. Ordem de middlewares: falta `UseAuthentication()`
   - Problema: `Program.cs` atualmente chama `app.UseAuthorization();` mas não chama `app.UseAuthentication();` — sem isto, autenticação JWT não será aplicada.
   - Correção: inserir `app.UseAuthentication();` imediatamente antes de `app.UseAuthorization();`.
   - Ordem recomendada:
     - `app.UseHttpsRedirection();`
     - `app.UseAuthentication();`
     - `app.UseAuthorization();`

4. Usings faltando e namespaces em `Services/JwtServices.cs`
   - Problema: o arquivo usa tipos (`JwtRegisteredClaimNames`, `SymmetricSecurityKey`, `SigningCredentials`, `JwtSecurityToken`, `JwtSecurityTokenHandler`, `Encoding`, `IConfiguration`) sem importar os namespaces.
   - Correção: adicionar no topo de `Services/JwtService.cs` (após renomear):
     - `using System.IdentityModel.Tokens.Jwt;`
     - `using Microsoft.IdentityModel.Tokens;`
     - `using System.Text;`
     - `using System.Security.Claims;`
     - `using Microsoft.Extensions.Configuration;`
     - `using System.Collections.Generic;`
   - Garantir que `IJwtService` exista e o namespace esteja `Contratos.Services`.

5. Inconsistência no nome da chave de expiração no `appsettings.json` vs código
   - Problema: o serviço usa `expirationMinutes = jwtSection.GetValue<int>("ExpirationMinutes")` enquanto outras referências usam `ExpiresInMinutes` ou `ExpiresInMinutes`.
   - Correção: padronizar um nome, por exemplo:
     - No `appsettings.json`:
       ```json
       "Jwt": {
         "Key": "sua-secret-32+bytes-aqui",
         "Issuer": "ContratosAPI",
         "Audience": "ContratosAPIClient",
         "ExpirationMinutes": 60
       }
       ```
     - No serviço: `jwtSection.GetValue<int>("ExpirationMinutes");`

6. Falta da interface `IJwtService`
   - Problema: se a interface não existir, a injeção falha.
   - Correção: criar `Services/IJwtService.cs` com a assinatura do método `GenerateToken`.

7. Pacotes NuGet necessários (se ainda não instalados)
   - `Microsoft.AspNetCore.Authentication.JwtBearer` (contém middleware JWT).
   - `Microsoft.AspNetCore.Identity` (para `PasswordHasher<T>` se não estiver disponível).
   - `Microsoft.EntityFrameworkCore.*` conforme provider (ex.: `Pomelo.EntityFrameworkCore.MySql` ou `MySql.EntityFrameworkCore`) — já usado no projeto.
   - Comando exemplo:
     - `dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer`
     - `dotnet add package Microsoft.AspNetCore.Identity`

8. Boas práticas observadas (não erros, mas recomendações)
   - Secret (`Key`) não deve ficar em texto no repositório — usar variáveis de ambiente ou Secret Manager.
   - Validar `issuer` e `audience` conforme ambiente.
   - Usar `ClockSkew` em `TokenValidationParameters` se necessário:
     - `TokenValidationParameters.ClockSkew = TimeSpan.FromSeconds(30);`

Passos recomendados para correção rápida (ordem)
1. Criar `Services/IJwtService.cs` com a interface.
2. Renomear `Services/JwtServices.cs` → `Services/JwtService.cs` e ajustar `public class JwtService : IJwtService` + adicionar `using` faltantes.
3. Atualizar `appsettings.json` para usar `ExpirationMinutes` (ou o nome escolhido).
4. Atualizar `Program.cs`:
   - adicionar `using Microsoft.AspNetCore.Authentication.JwtBearer;`, `using Microsoft.IdentityModel.Tokens;`, `using System.Text;`
   - garantir `builder.Services.AddScoped<IJwtService, JwtService>();`
   - adicionar `app.UseAuthentication();` antes de `app.UseAuthorization();`
5. Garantir que `Controllers/UsuarioController.cs` use `IPasswordHasher<Usuario>` e `IJwtService` corretamente (injetados via construtor) e que o login retorne `TokenDto`.
6. Instalar/confirmar pacotes NuGet.

Uso deste arquivo
- Este `Context.md` é a fonte autorizada para entendimento do domínio e para decisões de mudança. Antes de qualquer PR que altere modelos, relacionamentos ou contratos da API, valide as alterações contra este documento.

Arquivos atualmente abertos (referência)
- `Program.cs`
- `appsettings.json`
- `Services/JwtServices.cs` (recomenda-se renomear para `JwtService.cs` conforme observado)
- `Data/Dto/UsuarioDto/LoginUsuarioDto.cs`
- `Data/Dto/UsuarioDto/TokenDto/TokenDto.cs`
- `Context.md`

Se desejar, posso aplicar as correções sugeridas automaticamente nos arquivos do workspace (criar `IJwtService`, renomear/ajustar `JwtService`, atualizar `Program.cs` com os `using` e `app.UseAuthentication()` e alinhar `appsettings.json`). Quer que eu faça essas alterações agora e gere um PR?

---

flowchart LR
    A["Pendências"] --> B["❌ Models com int vs Guid\nEmpresa / Contratante"]
    A --> C["❌ EF Core 6 → 8\nAtualizar .csproj"]
    A --> D["❌ Swagger AddSecurityDefinition\nBotão Authorize JWT"]
    A --> E["❌ Pomelo.MySql no .csproj\nRemover pacote desnecessário"]
    A --> F["❌ Controllers faltando\nTenant, Endereco, Contratante, FormaPagamento"]
    A --> G["❌ Migrations\nRecriar após correções de tipos"]