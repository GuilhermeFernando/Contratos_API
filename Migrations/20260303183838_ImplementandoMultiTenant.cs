using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contratos.Migrations
{
    /// <inheritdoc />
    public partial class ImplementandoMultiTenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratante_Endereco_EnderecoId",
                table: "Contratante");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Usuario_UsuarioId",
                table: "RefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_Contratante_EnderecoId",
                table: "Contratante");

            migrationBuilder.RenameColumn(
                name: "Expirantion",
                table: "RefreshToken",
                newName: "Expiration");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "RefreshToken",
                newName: "CreatedAt");

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
                table: "Endereco",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Contratante",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Contratante_EnderecoId",
                table: "Contratante",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contratante_TenantId",
                table: "Contratante",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratante_Endereco_EnderecoId",
                table: "Contratante",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "EnderecoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contratante_Tenant_TenantId",
                table: "Contratante",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Usuario_UsuarioId",
                table: "RefreshToken",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratante_Endereco_EnderecoId",
                table: "Contratante");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratante_Tenant_TenantId",
                table: "Contratante");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Usuario_UsuarioId",
                table: "RefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_Contratante_EnderecoId",
                table: "Contratante");

            migrationBuilder.DropIndex(
                name: "IX_Contratante_TenantId",
                table: "Contratante");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "FormaPagamento");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Contratante");

            migrationBuilder.RenameColumn(
                name: "Expiration",
                table: "RefreshToken",
                newName: "Expirantion");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "RefreshToken",
                newName: "CreateAt");

            migrationBuilder.CreateIndex(
                name: "IX_Contratante_EnderecoId",
                table: "Contratante",
                column: "EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratante_Endereco_EnderecoId",
                table: "Contratante",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "EnderecoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Usuario_UsuarioId",
                table: "RefreshToken",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
