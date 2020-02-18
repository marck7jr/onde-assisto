using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OndeAssisto.Web.Api.Migrations
{
    public partial class UpdateWorkWithGenre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GenreGuid",
                table: "Works",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Guid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Works_GenreGuid",
                table: "Works",
                column: "GenreGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Genres_GenreGuid",
                table: "Works",
                column: "GenreGuid",
                principalTable: "Genres",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_Genres_GenreGuid",
                table: "Works");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Works_GenreGuid",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "GenreGuid",
                table: "Works");
        }
    }
}
