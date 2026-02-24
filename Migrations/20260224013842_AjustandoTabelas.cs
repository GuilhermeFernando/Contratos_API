using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contratos.Migrations
{
    /// <inheritdoc />
    public partial class AjustandoTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratantes_Empresas_EmpresaId1",
                table: "Contratantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratantes_Enderecos_EnderecoId1",
                table: "Contratantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_Contratantes_ContratanteId",
                table: "Contratos");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_Contratantes_ContratanteId1",
                table: "Contratos");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_Empresas_EmpresaId",
                table: "Contratos");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_FormasPagamento_ContratanteId",
                table: "Contratos");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_Tenants_TenantId",
                table: "Contratos");

            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_Enderecos_EnderecoId",
                table: "Empresas");

            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_Tenants_TenantId",
                table: "Empresas");

            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_Usuarios_UsuarioId",
                table: "Empresas");

            migrationBuilder.DropForeignKey(
                name: "FK_FormasPagamento_Contratos_ContratoId",
                table: "FormasPagamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Tenants_TenantId",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormasPagamento",
                table: "FormasPagamento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enderecos",
                table: "Enderecos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empresas",
                table: "Empresas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contratos",
                table: "Contratos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contratantes",
                table: "Contratantes");

            migrationBuilder.DropColumn(
                name: "UrlLogo",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "Tenants",
                newName: "Tenant");

            migrationBuilder.RenameTable(
                name: "FormasPagamento",
                newName: "FormaPagamento");

            migrationBuilder.RenameTable(
                name: "Enderecos",
                newName: "Endereco");

            migrationBuilder.RenameTable(
                name: "Empresas",
                newName: "Empresa");

            migrationBuilder.RenameTable(
                name: "Contratos",
                newName: "Contrato");

            migrationBuilder.RenameTable(
                name: "Contratantes",
                newName: "Contratante");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_TenantId",
                table: "Usuario",
                newName: "IX_Usuario_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_FormasPagamento_ContratoId",
                table: "FormaPagamento",
                newName: "IX_FormaPagamento_ContratoId");

            migrationBuilder.RenameIndex(
                name: "IX_Empresas_UsuarioId",
                table: "Empresa",
                newName: "IX_Empresa_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Empresas_TenantId",
                table: "Empresa",
                newName: "IX_Empresa_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Empresas_EnderecoId",
                table: "Empresa",
                newName: "IX_Empresa_EnderecoId");

            migrationBuilder.RenameIndex(
                name: "IX_Contratos_TenantId",
                table: "Contrato",
                newName: "IX_Contrato_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Contratos_EmpresaId",
                table: "Contrato",
                newName: "IX_Contrato_EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_Contratos_ContratanteId1",
                table: "Contrato",
                newName: "IX_Contrato_ContratanteId1");

            migrationBuilder.RenameIndex(
                name: "IX_Contratos_ContratanteId",
                table: "Contrato",
                newName: "IX_Contrato_ContratanteId");

            migrationBuilder.RenameIndex(
                name: "IX_Contratantes_EnderecoId1",
                table: "Contratante",
                newName: "IX_Contratante_EnderecoId1");

            migrationBuilder.RenameIndex(
                name: "IX_Contratantes_EmpresaId1",
                table: "Contratante",
                newName: "IX_Contratante_EmpresaId1");

            migrationBuilder.AlterColumn<string>(
                name: "NomeUsuario",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<Guid>(
                name: "ContratanteId1",
                table: "Contrato",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tenant",
                table: "Tenant",
                column: "TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormaPagamento",
                table: "FormaPagamento",
                column: "FormaPagamentoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Endereco",
                table: "Endereco",
                column: "EnderecoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empresa",
                table: "Empresa",
                column: "EmpresaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contrato",
                table: "Contrato",
                column: "ContratoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contratante",
                table: "Contratante",
                column: "ContratanteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratante_Empresa_EmpresaId1",
                table: "Contratante",
                column: "EmpresaId1",
                principalTable: "Empresa",
                principalColumn: "EmpresaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contratante_Endereco_EnderecoId1",
                table: "Contratante",
                column: "EnderecoId1",
                principalTable: "Endereco",
                principalColumn: "EnderecoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Contratante_ContratanteId",
                table: "Contrato",
                column: "ContratanteId",
                principalTable: "Contratante",
                principalColumn: "ContratanteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Contratante_ContratanteId1",
                table: "Contrato",
                column: "ContratanteId1",
                principalTable: "Contratante",
                principalColumn: "ContratanteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Empresa_EmpresaId",
                table: "Contrato",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "EmpresaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_FormaPagamento_ContratanteId",
                table: "Contrato",
                column: "ContratanteId",
                principalTable: "FormaPagamento",
                principalColumn: "FormaPagamentoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Tenant_TenantId",
                table: "Contrato",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Empresa_Endereco_EnderecoId",
                table: "Empresa",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "EnderecoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Empresa_Tenant_TenantId",
                table: "Empresa",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Empresa_Usuario_UsuarioId",
                table: "Empresa",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormaPagamento_Contrato_ContratoId",
                table: "FormaPagamento",
                column: "ContratoId",
                principalTable: "Contrato",
                principalColumn: "ContratoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Tenant_TenantId",
                table: "Usuario",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratante_Empresa_EmpresaId1",
                table: "Contratante");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratante_Endereco_EnderecoId1",
                table: "Contratante");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Contratante_ContratanteId",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Contratante_ContratanteId1",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Empresa_EmpresaId",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_FormaPagamento_ContratanteId",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Tenant_TenantId",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Empresa_Endereco_EnderecoId",
                table: "Empresa");

            migrationBuilder.DropForeignKey(
                name: "FK_Empresa_Tenant_TenantId",
                table: "Empresa");

            migrationBuilder.DropForeignKey(
                name: "FK_Empresa_Usuario_UsuarioId",
                table: "Empresa");

            migrationBuilder.DropForeignKey(
                name: "FK_FormaPagamento_Contrato_ContratoId",
                table: "FormaPagamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Tenant_TenantId",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tenant",
                table: "Tenant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormaPagamento",
                table: "FormaPagamento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Endereco",
                table: "Endereco");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empresa",
                table: "Empresa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contrato",
                table: "Contrato");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contratante",
                table: "Contratante");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Tenant",
                newName: "Tenants");

            migrationBuilder.RenameTable(
                name: "FormaPagamento",
                newName: "FormasPagamento");

            migrationBuilder.RenameTable(
                name: "Endereco",
                newName: "Enderecos");

            migrationBuilder.RenameTable(
                name: "Empresa",
                newName: "Empresas");

            migrationBuilder.RenameTable(
                name: "Contrato",
                newName: "Contratos");

            migrationBuilder.RenameTable(
                name: "Contratante",
                newName: "Contratantes");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_TenantId",
                table: "Usuarios",
                newName: "IX_Usuarios_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_FormaPagamento_ContratoId",
                table: "FormasPagamento",
                newName: "IX_FormasPagamento_ContratoId");

            migrationBuilder.RenameIndex(
                name: "IX_Empresa_UsuarioId",
                table: "Empresas",
                newName: "IX_Empresas_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Empresa_TenantId",
                table: "Empresas",
                newName: "IX_Empresas_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Empresa_EnderecoId",
                table: "Empresas",
                newName: "IX_Empresas_EnderecoId");

            migrationBuilder.RenameIndex(
                name: "IX_Contrato_TenantId",
                table: "Contratos",
                newName: "IX_Contratos_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Contrato_EmpresaId",
                table: "Contratos",
                newName: "IX_Contratos_EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_Contrato_ContratanteId1",
                table: "Contratos",
                newName: "IX_Contratos_ContratanteId1");

            migrationBuilder.RenameIndex(
                name: "IX_Contrato_ContratanteId",
                table: "Contratos",
                newName: "IX_Contratos_ContratanteId");

            migrationBuilder.RenameIndex(
                name: "IX_Contratante_EnderecoId1",
                table: "Contratantes",
                newName: "IX_Contratantes_EnderecoId1");

            migrationBuilder.RenameIndex(
                name: "IX_Contratante_EmpresaId1",
                table: "Contratantes",
                newName: "IX_Contratantes_EmpresaId1");

            migrationBuilder.AlterColumn<string>(
                name: "NomeUsuario",
                table: "Usuarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UrlLogo",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContratanteId1",
                table: "Contratos",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants",
                column: "TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormasPagamento",
                table: "FormasPagamento",
                column: "FormaPagamentoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enderecos",
                table: "Enderecos",
                column: "EnderecoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empresas",
                table: "Empresas",
                column: "EmpresaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contratos",
                table: "Contratos",
                column: "ContratoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contratantes",
                table: "Contratantes",
                column: "ContratanteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratantes_Empresas_EmpresaId1",
                table: "Contratantes",
                column: "EmpresaId1",
                principalTable: "Empresas",
                principalColumn: "EmpresaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contratantes_Enderecos_EnderecoId1",
                table: "Contratantes",
                column: "EnderecoId1",
                principalTable: "Enderecos",
                principalColumn: "EnderecoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_Contratantes_ContratanteId",
                table: "Contratos",
                column: "ContratanteId",
                principalTable: "Contratantes",
                principalColumn: "ContratanteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_Contratantes_ContratanteId1",
                table: "Contratos",
                column: "ContratanteId1",
                principalTable: "Contratantes",
                principalColumn: "ContratanteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_Empresas_EmpresaId",
                table: "Contratos",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "EmpresaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_FormasPagamento_ContratanteId",
                table: "Contratos",
                column: "ContratanteId",
                principalTable: "FormasPagamento",
                principalColumn: "FormaPagamentoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_Tenants_TenantId",
                table: "Contratos",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_Enderecos_EnderecoId",
                table: "Empresas",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "EnderecoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_Tenants_TenantId",
                table: "Empresas",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_Usuarios_UsuarioId",
                table: "Empresas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormasPagamento_Contratos_ContratoId",
                table: "FormasPagamento",
                column: "ContratoId",
                principalTable: "Contratos",
                principalColumn: "ContratoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Tenants_TenantId",
                table: "Usuarios",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
