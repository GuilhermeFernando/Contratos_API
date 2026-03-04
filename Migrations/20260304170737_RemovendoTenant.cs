using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contratos.Migrations
{
    /// <inheritdoc />
    public partial class RemovendoTenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratante_Tenant_TenantId",
                table: "Contratante");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Tenant_TenantId",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Empresa_Tenant_TenantId",
                table: "Empresa");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Tenant_TenantId",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "Tenant");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_TenantId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Empresa_TenantId",
                table: "Empresa");

            migrationBuilder.DropIndex(
                name: "IX_Contrato_TenantId",
                table: "Contrato");

            migrationBuilder.DropIndex(
                name: "IX_Contratante_TenantId",
                table: "Contratante");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "FormaPagamento");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Contratante");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Usuario",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "RefreshToken",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "FormaPagamento",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Contrato",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Contratante",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ddd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlLogo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.TenantId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_TenantId",
                table: "Usuario",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_TenantId",
                table: "Empresa",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_TenantId",
                table: "Contrato",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratante_TenantId",
                table: "Contratante",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratante_Tenant_TenantId",
                table: "Contratante",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Tenant_TenantId",
                table: "Contrato",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Empresa_Tenant_TenantId",
                table: "Empresa",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Tenant_TenantId",
                table: "Usuario",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
