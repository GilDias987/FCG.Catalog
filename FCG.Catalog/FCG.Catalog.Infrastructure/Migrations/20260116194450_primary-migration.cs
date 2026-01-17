using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCG.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class primarymigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_GENERO",
                columns: table => new
                {
                    ISN_GENERO = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DSC_TITULO = table.Column<string>(type: "VARCHAR(500)", nullable: false),
                    DTH_CRIACAO = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DTH_ATUALIZACAO = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_GENERO", x => x.ISN_GENERO);
                });

            migrationBuilder.CreateTable(
                name: "TB_PLATAFORMA",
                columns: table => new
                {
                    ISN_PLATAFORMA = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DSC_TITULO = table.Column<string>(type: "VARCHAR(500)", nullable: false),
                    DTH_CRIACAO = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DTH_ATUALIZACAO = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PLATAFORMA", x => x.ISN_PLATAFORMA);
                });

            migrationBuilder.CreateTable(
                name: "TB_JOGO",
                columns: table => new
                {
                    ISN_JOGO = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DSC_TITULO = table.Column<string>(type: "VARCHAR(500)", nullable: false),
                    DSC_DESCRICAO = table.Column<string>(type: "VARCHAR(2000)", nullable: false),
                    VLR_PRECO = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: true),
                    VLR_DESCONTO = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: true),
                    ISN_GENERO = table.Column<int>(type: "INT", nullable: false),
                    ISN_PLATAFORMA = table.Column<int>(type: "INT", nullable: false),
                    DTH_CRIACAO = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DTH_ATUALIZACAO = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_JOGO", x => x.ISN_JOGO);
                    table.ForeignKey(
                        name: "FK_TB_JOGO_TB_GENERO_ISN_GENERO",
                        column: x => x.ISN_GENERO,
                        principalTable: "TB_GENERO",
                        principalColumn: "ISN_GENERO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_JOGO_TB_PLATAFORMA_ISN_PLATAFORMA",
                        column: x => x.ISN_PLATAFORMA,
                        principalTable: "TB_PLATAFORMA",
                        principalColumn: "ISN_PLATAFORMA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_USUARIO_JOGO",
                columns: table => new
                {
                    ISN_USUARIO_JOGO = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ISN_USUARIO = table.Column<int>(type: "INT", nullable: false),
                    ISN_JOGO = table.Column<int>(type: "INT", nullable: false),
                    DTH_CRIACAO = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DTH_ATUALIZACAO = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USUARIO_JOGO", x => x.ISN_USUARIO_JOGO);
                    table.ForeignKey(
                        name: "FK_TB_USUARIO_JOGO_TB_JOGO_ISN_JOGO",
                        column: x => x.ISN_JOGO,
                        principalTable: "TB_JOGO",
                        principalColumn: "ISN_JOGO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_JOGO_ISN_GENERO",
                table: "TB_JOGO",
                column: "ISN_GENERO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_JOGO_ISN_PLATAFORMA",
                table: "TB_JOGO",
                column: "ISN_PLATAFORMA");

            migrationBuilder.CreateIndex(
                name: "IX_TB_USUARIO_JOGO_ISN_JOGO",
                table: "TB_USUARIO_JOGO",
                column: "ISN_JOGO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_USUARIO_JOGO");

            migrationBuilder.DropTable(
                name: "TB_JOGO");

            migrationBuilder.DropTable(
                name: "TB_GENERO");

            migrationBuilder.DropTable(
                name: "TB_PLATAFORMA");
        }
    }
}
