using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSQL.Data.Domain.Migrations
{
    public partial class AddGinTrgmIndexToText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Messages_Text",
                table: "Messages",
                column: "Text")
                .Annotation("Npgsql:IndexMethod", "GIN")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Messages_Text",
                table: "Messages");
        }
    }
}
