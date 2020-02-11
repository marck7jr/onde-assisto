using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OndeAssisto.Web.Api.Migrations
{
    public partial class AddEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Accounts",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    AuthorGuid = table.Column<Guid>(nullable: false),
                    Cover = table.Column<string>(nullable: true),
                    ReleaseYear = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Works_Authors_AuthorGuid",
                        column: x => x.AuthorGuid,
                        principalTable: "Authors",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    IsOutDated = table.Column<bool>(nullable: false),
                    AccountGuid = table.Column<Guid>(nullable: false),
                    WorkGuid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Medias_Accounts_AccountGuid",
                        column: x => x.AccountGuid,
                        principalTable: "Accounts",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medias_Works_WorkGuid",
                        column: x => x.WorkGuid,
                        principalTable: "Works",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Logo = table.Column<string>(nullable: true),
                    Site = table.Column<string>(nullable: true),
                    MediaGuid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Platforms_Medias_MediaGuid",
                        column: x => x.MediaGuid,
                        principalTable: "Medias",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    AccountGuid = table.Column<Guid>(nullable: true),
                    MediaGuid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Reviews_Accounts_AccountGuid",
                        column: x => x.AccountGuid,
                        principalTable: "Accounts",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Medias_MediaGuid",
                        column: x => x.MediaGuid,
                        principalTable: "Medias",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medias_AccountGuid",
                table: "Medias",
                column: "AccountGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_WorkGuid",
                table: "Medias",
                column: "WorkGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_MediaGuid",
                table: "Platforms",
                column: "MediaGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AccountGuid",
                table: "Reviews",
                column: "AccountGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MediaGuid",
                table: "Reviews",
                column: "MediaGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Works_AuthorGuid",
                table: "Works",
                column: "AuthorGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Platforms");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 64);
        }
    }
}
