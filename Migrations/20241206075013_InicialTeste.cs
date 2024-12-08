using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechNation.Migrations
{
    public partial class InicialTeste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Provider = table.Column<string>(nullable: true),
                    HttpMethod = table.Column<string>(nullable: true),
                    StatusCode = table.Column<int>(nullable: false),
                    UriPath = table.Column<string>(nullable: true),
                    TimeTaken = table.Column<double>(nullable: false),
                    ResponseSize = table.Column<int>(nullable: false),
                    CacheStatus = table.Column<string>(nullable: true),
                    DataCadastro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_ID",
                table: "Logs",
                column: "ID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
